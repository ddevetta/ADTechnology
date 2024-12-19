// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.Mainform
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using ADTechnology.AbInitio.Classes;
using ADTechnology.AbInitio.IO;
using ADTechnology.Apps;

namespace ADTechnology.AbInitio.UI
{
    public partial class MainForm : Form
    {
        private ProjectSource projectSource = ProjectSource.Unknown;
        private string optsFile = Application.UserAppDataPath + "\\ailog.options.dat";
        private long logSize = 0;
//        private ProjectControl projectControl;

        public MainForm()
        {
            this.InitializeComponent();
            this.mainForm_initialise();
        }

        public MainForm(string logFile)
        {
            this.InitializeComponent();
            this.mainForm_initialise();
            this.loadFile(logFile);
        }
        private void mainForm_initialise()
        {
//            this.projectControl = new ProjectControl();
//            //
//            // projectControl
//            //
//            this.projectControl.Dock = DockStyle.Top;
//            this.projectControl.FileName = "";
//            this.projectControl.Location = new Point(0, 24);
//            this.projectControl.Name = "projectControl";
////            this.projectControl.Size = new Size(1016, 107);
//            this.projectControl.TabIndex = 3;
//            mainPanel.Controls.Add(this.projectControl);

            this.readOptions();
            this.log = new LogFile();
            this.log.ProgressTick += new ProgressTickEventHandler(this.log_ProgressTick);
            this.thread = new BackgroundWorker();
            this.thread.WorkerReportsProgress = true;
            this.thread.DoWork += new DoWorkEventHandler(this.thread_DoWork);
            this.thread.ProgressChanged += new ProgressChangedEventHandler(this.thread_ProgressChanged);
            this.thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.thread_RunWorkerCompleted);
            this.pc = new PhaseControl();
            this.pc.Visible = false;
            this.mainPanel.Controls.Add((Control)this.pc);
            this.toolStripProgressBar.Visible = false;
            this.toolStripProgressBar.Maximum = 100;
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.refreshTimer = new System.Windows.Forms.Timer();
            this.refreshTimer.Tick += new EventHandler(this.refreshTimer_Tick);
            this.projectControl.SelectedPhaseChanged += new EventHandler(this.projectControl_SelectedPhaseChanged);
            this.projectControl.SelectedRevisionChanged += new EventHandler(this.projectControl_SelectedRevisionChanged);
            this.pc.AddToPlotterSelected += new AddToPlotterEventHandler(this.pc_AddToPlotterSelected);
            this.ofd = new OpenFileDialog();
            this.ohfd = new OpenHostFileDialog();
            this.pallette = new ColorPallette();
        }

        private void readOptions()
        {
            this.opts = Options.Deserialize(this.optsFile);
            this.autoRefreshToolStripMenuItem.Checked = this.opts.AutoRefresh;
            this.hideAllRevisionsToolStripMenuItem.Checked = this.opts.MergeRevisions;
        }

        private void thread_DoWork(object sender, DoWorkEventArgs e)
        {
            this.log.Parse(this.opts, (StreamReader)e.Argument, 0L);
        }

        private void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar.Value = e.ProgressPercentage;
        }

        private void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (e.Error.GetType() == typeof(ExpectedNotFoundException))
                {
                    ExpectedNotFoundException error = (ExpectedNotFoundException)e.Error;
                    this.renderDisplay();
                    if (MessageBox.Show((IWin32Window)this, "An unexpected error was encountered on line " + error.LineNumber.ToString() + " of the log file.\r\n\r\n" + error.Message + (error.InnerException == null ? "" : "\r\n\r\nInner message stack is:\r\n" + error.InnerException.StackTrace) + "\r\n\r\nDo you wish to try a refresh?\r\n", "Error in Log File", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        return;
                    this.refreshToolStripMenuItem_Click((object)this, new EventArgs());
                }
                else if (e.Error.GetType() == typeof(MissingVertexException))
                {
                    MissingVertexException error = (MissingVertexException)e.Error;
                    int num = (int)MessageBox.Show(error.Message + "\r\nVertex: " + error.VertexName, "Error during Vertex-Flow linkup");
                }
                else
                {
                    int num1 = (int)MessageBox.Show(e.Error.Message + "\r\n" + e.Error.GetType().ToString() + "\r\n" + e.Error.StackTrace, "Unexpected Exception");
                }
            }
            else
            {
                this.toolStripProgressBar.Value = this.toolStripProgressBar.Maximum;
                this.renderDisplay();
                this.ohfd.CloseStream();
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (!this.opts.AutoRefresh)
                return;
            this.lblStatusRight.Text = "(Autorefreshing every " + this.opts.Interval.ToString() + " seconds)";
            this.refreshToolStripMenuItem_Click(sender, e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.lblStatus.Text = this.lblStatusRight.Text = "";
            this.toolStripProgressBar.Visible = false;
        }

        private void log_ProgressTick(object sender, ProgressTickEventArgs e)
        {
            if (e.BytesSoFar >= this.logSize)
                return;
            this.thread.ReportProgress((int)(e.BytesSoFar * 100L / this.logSize));
        }

        private void loadFile(HostFile host)
        {
            this.lblStatus.Text = "Please wait while the project loads...";
            this.toolStripProgressBar.Value = 0;
            this.toolStripProgressBar.Visible = true;
            Application.DoEvents();
            try
            {
                this.logSize = this.ohfd.OpenStream();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message + "\r\n" + ex.GetType().ToString() + "\r\n" + ex.StackTrace, "Unexpected Exception");
                return;
            }
            StreamReader sr = new StreamReader((Stream)this.ohfd.FTPConnection.DataStream);
            this.projectControl.FileName = host.ToString();
            this.projectSource = ProjectSource.Host;
            this.log.LogFileName = this.ohfd.FileName;
            this.parse(sr);
            this.ohfd.CloseStream();
        }

        private void loadFile(string name)
        {
            this.lblStatus.Text = "Please wait while the project loads...";
            this.toolStripProgressBar.Value = 0;
            this.toolStripProgressBar.Visible = true;
            Application.DoEvents();
            StreamReader sr = new StreamReader(name);
            this.projectControl.FileName = name;
            this.projectSource = ProjectSource.Network;
            this.log.LogFileName = name;
            this.parse(sr);
            sr.Close();
        }

        private void parse(StreamReader sr)
        {
            this.refreshTimer.Stop();
            try
            {
                this.projectControl.Project = this.log.Parse(this.opts, sr, this.logSize);
            }
            catch (ExpectedNotFoundException ex)
            {
                this.renderDisplay();
                if (MessageBox.Show((IWin32Window)this, "An unexpected error was encountered on line " + ex.LineNumber.ToString() + " of the log file.\r\n\r\n" + ex.Message + (ex.InnerException == null ? "" : "\r\n\r\nInner message stack is:\r\n" + ex.InnerException.StackTrace) + "\r\n\r\nDo you wish to try a refresh?\r\n", "Error in Log File", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                    return;
                this.refreshToolStripMenuItem_Click((object)this, new EventArgs());
                return;
            }
            catch (MissingVertexException ex)
            {
                int num = (int)MessageBox.Show(ex.Message + "\r\nVertex: " + ex.VertexName, "Error during Vertex-Flow linkup");
                return;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message + "\r\n" + ex.GetType().ToString() + "\r\n" + ex.StackTrace, "Unexpected Exception");
                return;
            }
            this.toolStripProgressBar.Value = this.toolStripProgressBar.Maximum;
            this.renderDisplay();
        }

        private void renderDisplay()
        {
            this.projectControl.DataBind();
            this.pc.Refresh();
            this.refreshToolStripMenuItem.Enabled = this.parametersToolStripMenuItem.Enabled = true;
            if (this.projectControl.Project != null && this.projectControl.Project.Status == ProjectStatus.NotStarted)
            {
                this.startupToolStripMenuItem.Enabled = this.shutdownToolStripMenuItem.Enabled = false;
                this.pc.Visible = false;
            }
            else
            {
                this.startupToolStripMenuItem.Enabled = this.shutdownToolStripMenuItem.Enabled = true;
                this.pc.Visible = true;
            }
            if (this.log.LogFileName == null)
                this.logFileToolStripMenuItem.Enabled = false;
            else
                this.logFileToolStripMenuItem.Enabled = true;
            if (this.log.ErrorFile == null)
                this.errorFileToolStripMenuItem.Enabled = false;
            else
                this.errorFileToolStripMenuItem.Enabled = true;
            if (this.log.SummaryFile == null)
                this.summaryFileToolStripMenuItem.Enabled = false;
            else
                this.summaryFileToolStripMenuItem.Enabled = true;
            if (this.log.ReportOptions == null)
                this.aBREPORTToolStripMenuItem.Enabled = false;
            else
                this.aBREPORTToolStripMenuItem.Enabled = true;
            this.timer.Interval = 300;
            this.timer.Start();
            this.refreshTimer.Enabled = false;
            if (this.projectControl.Project == null)
                return;
            if (this.opts.AutoRefresh && (this.projectControl.Project.Status == ProjectStatus.Running || this.projectControl.Project.Status == ProjectStatus.Started || this.opts.ExtraRefreshAfterCompletion && this.projectControl.Project.Status == ProjectStatus.Ended && this.previousStatus == ProjectStatus.Running))
            {
                this.refreshTimer.Interval = this.opts.Interval * 1000;
                this.refreshTimer.Start();
            }
            this.previousStatus = this.projectControl.Project.Status;
        }

        private string readTextFile(string file)
        {
            string str = (string)null;
            if (file == null)
                return "(ailog: file name was not specified)";
            if (this.projectSource == ProjectSource.Network)
            {
                try
                {
                    StreamReader streamReader = new StreamReader(file);
                    str = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                catch (Exception ex)
                {
                    return "AILOG: could not read the file :\n" + ex.Message;
                }
            }
            else if (this.projectSource == ProjectSource.Host)
            {
                try
                {
                    this.ohfd.FTPConnection.OpenStream(file);
                    str = new StreamReader((Stream)this.ohfd.FTPConnection.DataStream).ReadToEnd();
                    this.ohfd.FTPConnection.CloseStream();
                }
                catch (Exception ex)
                {
                    return "AILOG: could not read the file :\n" + ex.Message;
                }
            }
            return str;
        }

        private void projectControl_SelectedPhaseChanged(object sender, EventArgs e)
        {
            if (this.projectControl.SelectedPhase == null)
                return;
            this.pc.Phase = this.projectControl.SelectedPhase;
            this.pc.Revision = 0;
        }

        private void projectControl_SelectedRevisionChanged(object sender, EventArgs e)
        {
            if (this.projectControl.SelectedPhase == null)
                return;
            this.pc.Revision = this.projectControl.SelectedRevision;
            this.projectControl.Select();
        }

        private void pc_AddToPlotterSelected(object sender, AddToPlotterEventArgs e)
        {
            PlotLine plotLine;
            try
            {
                plotLine = new PlotLine(this.log.Project, this.pc.Phase.PhaseNumber, e.ObjectType, e.Object, e.Column, this.pallette.GetNextColor());
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "Plotter Error");
                return;
            }
            foreach (Form openForm in (ReadOnlyCollectionBase)Application.OpenForms)
            {
                if (openForm.GetType() == typeof(GraphForm) && openForm.Text == "Plotter")
                {
                    ((GraphForm)openForm).Add(plotLine.GraphLine);
                    openForm.WindowState = FormWindowState.Normal;
                    openForm.Refresh();
                    openForm.Activate();
                    return;
                }
            }
            GraphForm graphForm = new GraphForm();
            graphForm.Add(plotLine.GraphLine);
            graphForm.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ofd == null)
                this.ofd = new OpenFileDialog();
            if (this.ofd.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            if (this.ofd.FileName != this.projectControl.FileName)
            {
                this.log = new LogFile();
                this.previousStatus = ProjectStatus.NotStarted;
            }
            this.loadFile(this.ofd.FileName);
        }

        private void openHostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ohfd == null)
                this.ohfd = new OpenHostFileDialog();
            if (this.ohfd.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            if (this.ohfd.FileName != this.projectControl.FileName)
            {
                this.log = new LogFile();
                this.previousStatus = ProjectStatus.NotStarted;
            }
            this.loadFile(this.ohfd.HostFile);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.projectSource == ProjectSource.Unknown || this.log.Project == null || this.log.LogFileStreamReader == null)
                return;
            switch (this.projectSource)
            {
                case ProjectSource.Network:
                    if (this.projectControl.FileName == "")
                        break;
                    this.loadFile(this.projectControl.FileName);
                    break;
                case ProjectSource.Host:
                    if (this.ohfd == null || this.ohfd.HostFile == null)
                        break;
                    if (this.ohfd.FTPConnection.DataStream != null)
                        this.ohfd.CloseStream();
                    this.loadFile(this.ohfd.HostFile);
                    break;
            }
        }

        private void startupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(StaticDataForm), "Start Script Output"))
                return;
            new StaticDataForm(this.log.Startup, "Start Script Output").Show((IWin32Window)this);
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(StaticDataForm), "End Script Output"))
                return;
            new StaticDataForm(this.log.Shutdown, "End Script Output").Show((IWin32Window)this);
        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(ExecutionParametersForm), "Execution Parameters"))
                return;
            new ExecutionParametersForm(this.log.ExecutionParameters).Show((IWin32Window)this);
        }

        private void logfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(StaticDataForm), this.log.LogFileName))
                return;
            new StaticDataForm(this.readTextFile(this.log.LogFileName), this.log.LogFileName).Show((IWin32Window)this);
        }

        private void errorFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(StaticDataForm), this.log.ErrorFile))
                return;
            new StaticDataForm(this.readTextFile(this.log.ErrorFile), this.log.ErrorFile).Show((IWin32Window)this);
        }

        private void summaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(StaticDataForm), this.log.SummaryFile))
                return;
            new StaticDataForm(this.readTextFile(this.log.SummaryFile), this.log.SummaryFile).Show((IWin32Window)this);
        }

        private void aBREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(StaticDataForm), "AB_REPORT"))
                return;
            new StaticDataForm(this.log.ReportOptions.ToString(), "AB_REPORT").Show((IWin32Window)this);
        }

        private void plotterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.showOpenForm(typeof(GraphForm), "Plotter"))
                return;
            new GraphForm().Show();
        }

        private void hideAllRevisionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.opts.MergeRevisions = !this.opts.MergeRevisions;
            this.hideAllRevisionsToolStripMenuItem.Checked = this.opts.MergeRevisions;
            this.refreshToolStripMenuItem_Click(sender, e);
        }

        private void autoRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.opts.AutoRefresh = !this.opts.AutoRefresh;
            this.autoRefreshToolStripMenuItem.Checked = this.opts.AutoRefresh;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (int)new AboutForm().ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new OptionsForm()
            {
                OptionsFilePath = this.optsFile
            }.ShowDialog() != DialogResult.OK)
                return;
            this.readOptions();
        }

        private bool showOpenForm(System.Type formType, string formText)
        {
            foreach (Form openForm in (ReadOnlyCollectionBase)Application.OpenForms)
            {
                if (openForm.GetType() == formType && openForm.Text == formText)
                {
                    openForm.WindowState = FormWindowState.Normal;
                    openForm.Activate();
                    return true;
                }
            }
            return false;
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ohfd == null || this.ohfd.FTPConnection == null || this.ohfd.FTPConnection.DataStream == null)
                return;
            this.lblStatus.Text = "Closing connection...";
            Application.DoEvents();
            try
            {
                this.ohfd.CloseStream();
                this.ohfd.FTPConnection.Disconnect();
                this.ohfd.Close();
            }
            catch
            {
            }
            Thread.Sleep(250);
        }
    }
}
