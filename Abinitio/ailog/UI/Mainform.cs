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
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ADTechnology.AbInitio.Classes;
using ADTechnology.AbInitio.IO;
using ADTechnology.Apps.OpenHostFileDialog;

namespace ADTechnology.AbInitio.UI
{
    public partial class MainForm : Form
    {
        private ProjectSource projectSource = ProjectSource.Unknown;
        private string optsFile = Application.UserAppDataPath + "\\ailog.options.dat";
        private long logSize = 0;

        private FileBrowserControl StartFileControl = null;
        private FileBrowserControl EndFileControl = null;
        private FileBrowserControl RawFileControl = null;
        private FileBrowserControl ErrorFileControl = null;
        private FileBrowserControl SummaryFileControl = null;
        private FileBrowserControl ABREPORTFileControl = null;
        private ParametersBrowserControl ParmsControl = null;
        private GraphContainerControl GraphContainer = null;

        private ProjectControl projectControl;

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

        private void projectControl_initialise()
        {
        }

        private void mainForm_initialise()
        {
            this.projectControl_initialise();

            this.readOptions();
            this.log = new LogFile();
            this.log.ProgressTick += new ProgressTickEventHandler(this.log_ProgressTick);
            this.thread = new BackgroundWorker();
            this.thread.WorkerReportsProgress = true;
            this.thread.DoWork += new DoWorkEventHandler(this.thread_DoWork);
            this.thread.ProgressChanged += new ProgressChangedEventHandler(this.thread_ProgressChanged);
            this.thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.thread_RunWorkerCompleted);
            //
            // projectControl
            //
            this.projectControl = new ProjectControl();
            this.projectControl.Dock = DockStyle.Top;
            this.projectControl.FileName = "";
            this.projectControl.Location = new Point(0, 24);
            this.projectControl.Name = "projectControl";
            this.projectControl.TabIndex = 3;
            this.mainPanel.Controls.Add(this.projectControl);
            //
            // PhaseControl
            //
            this.pc = new PhaseControl();
            this.pc.Visible = false;
            this.pc.Dock = DockStyle.Fill;
            this.pc.Padding = new Padding(0,107,0,0);
            this.mainPanel.Controls.Add(this.pc);

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
                    switch (MessageBox.Show(this, "An unexpected error was encountered on line " + error.LineNumber.ToString() + " of the log file.\r\n\r\n" 
                        + error.Message + (error.InnerException == null ? "" : "\r\n\r\nInner message stack is:\r\n" + error.InnerException.StackTrace) 
                        + "\r\n\r\nDo you wish to try a refresh?\r\n(Press Ignore to open the file in raw text mode)\r\n", "Error in Log File",
                        MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation))
                    { 
                        case DialogResult.Abort: 
                            return;
                        case DialogResult.Ignore:
                            this.logfileToolStripMenuItem_Click(sender, e);
                            return;
                    }
                    this.renderDisplay();
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
            StreamReader sr = null;
            this.lblStatus.Text = "Please wait while the project loads...";
            this.toolStripProgressBar.Value = 0;
            this.toolStripProgressBar.Visible = true;
            Application.DoEvents();
            try
            {
                this.logSize = this.ohfd.OpenStream();
                sr = getStreamReader((Stream)this.ohfd.FTPConnection.DataStream, this.ohfd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.GetType().ToString() + "\r\n" + ex.StackTrace, "Unexpected Host Exception");
                return;
            }
            this.projectSource = ProjectSource.Host;
            this.parse(sr, this.ohfd.FileName);
            this.ohfd.CloseStream();
        }

        private void loadFile(string name)
        {
            StreamReader sr = null;
            this.lblStatus.Text = "Please wait while the project loads...";
            this.toolStripProgressBar.Value = 0;
            this.toolStripProgressBar.Visible = true;
            Application.DoEvents();
            try
            {
                FileStream fs = new FileStream(name, FileMode.Open, FileAccess.Read);
                this.logSize = fs.Length;
                sr = getStreamReader((Stream)fs, name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.GetType().ToString() + "\r\n" + ex.StackTrace, "Unexpected Local Exception");
                return;
            }
            this.projectSource = ProjectSource.Network;
            this.parse(sr, name);
            sr.Close();
        }

        private StreamReader getStreamReader(Stream strm, string fileName)
        {
            StreamReader sr = null;
            switch (Path.GetExtension(fileName).ToLower())
            {
                case ".gz":
                    {
                        GZipStream z = new GZipStream(strm, CompressionMode.Decompress);
                        MemoryStream ms = new MemoryStream();
                        z.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        sr = new StreamReader(ms);
                        break;
                    }
                case ".zip":
                    {
                        strm.ReadByte();
                        strm.ReadByte();
                        DeflateStream z = new DeflateStream(strm, CompressionMode.Decompress);
                        MemoryStream ms = new MemoryStream();
                        z.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        sr = new StreamReader(ms);
                        break;
                    }
                default:
                    sr = new StreamReader(strm);
                    break;
            }

            return sr;
        }

        private void parse(StreamReader sr, string fileName)
        {
            LogFile newLog = new LogFile(fileName);
            Project newPrj = null;
            this.refreshTimer.Stop();
            DialogResult openRaw = DialogResult.No;
            string flashMsg = string.Empty;
            try
            {
                newPrj = newLog.Parse(opts, sr, logSize);
                //this.projectControl.Project = this.log.Parse(this.opts, sr, this.logSize);
            }
            catch (NotAnAILogException ex)
            {
                openAsRawFile(sr, newLog);
                flashMessage("Opening as raw file - " + ex.Message);
                return;
            }
            catch (EmptyLogException ex)
            {
                flashMsg = ex.Message;
            }
            catch (ExpectedNotFoundException ex)
            {
                openRaw = MessageBox.Show(ex.Message + "\n\nDo you wish to open the log anyway?\n\n(it will be opened as a raw text file)", "Error parsing logfile", MessageBoxButtons.OKCancel);
                if (openRaw == DialogResult.Cancel)
                    return;
                flashMsg = ex.Message;
            }
            catch (MissingVertexException ex)
            {
                MessageBox.Show(ex.Message + "\r\nVertex: " + ex.VertexName, "Error during Vertex-Flow linkup");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.GetType().ToString() + "\r\n" + ex.StackTrace, "Unexpected Exception");
                return;
            }

            this.toolStripProgressBar.Value = this.toolStripProgressBar.Maximum;
            this.log = newLog;
            this.projectControl.Project = newPrj;
            this.projectControl.Initialise(newLog.LogFileName);
            this.pc.Clear();
            this.renderDisplay();

            if (openRaw == DialogResult.OK)
                openAsRawFile(sr, newLog);
            if (flashMsg != string.Empty)
                flashMessage(flashMsg);
        }

        private void openAsRawFile(StreamReader sr, LogFile newLog)
        {
            FileBrowserControl fileControl = new FileBrowserControl();
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            sr.DiscardBufferedData();  // to synchronise the reader to the stream position
            fileControl.Fill(newLog.LogFileName, sr);
            this.pc.Visible = true;
            this.pc.AddOrReplaceTab("File Viewer", newLog.LogFileName, fileControl);
        }

        private void flashMessage(string message)
        {
            this.lblStatus.Text = message;
            this.toolStripProgressBar.Visible = false;
            this.timer.Interval = 8000;
            this.timer.Start();
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

        private StreamReader readTextFile(string file)
        {
            StreamReader sr = null;
            try
            {
                if (this.projectSource == ProjectSource.Network)
                {
                    FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                    this.logSize = fs.Length;
                    sr = getStreamReader((Stream)fs, file);
                }
                else if (this.projectSource == ProjectSource.Host)
                {
                    this.logSize = this.ohfd.FTPConnection.OpenStream(file);
                    sr = getStreamReader((Stream)this.ohfd.FTPConnection.DataStream, this.ohfd.FileName);
                }
            }
            catch (Exception ex)
            {
                sr = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes("AILOG: error reading the file : " + ex.Message)));
            }

            return sr;
        }

        private void projectControl_SelectedPhaseChanged(object sender, EventArgs e)
        {
            if (this.projectControl.SelectedPhase == null)
                return;
            this.pc.Phase = this.projectControl.SelectedPhase;
            this.pc.Revision = 0;
            this.pc.SelectVertexTab();
        }

        private void projectControl_SelectedRevisionChanged(object sender, EventArgs e)
        {
            if (this.projectControl.SelectedPhase == null)
                return;
            this.pc.Revision = this.projectControl.SelectedRevision;
            this.pc.SelectVertexTab();
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

            plotterToolStripMenuItem_Click(sender, e);
            GraphContainer.AddLine(plotLine.GraphLine);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ofd == null)
                this.ofd = new OpenFileDialog();
            if (this.ofd.ShowDialog((IWin32Window)this) == DialogResult.OK)
                this.loadFile(this.ofd.FileName);
        }

        private void openHostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ohfd == null)
                this.ohfd = new OpenHostFileDialog();
            if (this.ohfd.ShowDialog((IWin32Window)this) == DialogResult.OK)
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
            if (StartFileControl == null)
                StartFileControl = new FileBrowserControl();
            StartFileControl.Fill("Start Script Output", this.log.Startup);
            pc.AddOrReplaceTab("Startup", "sysout from Start Script execution", StartFileControl);
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EndFileControl == null)
                EndFileControl = new FileBrowserControl();
            EndFileControl.Fill("End Script Output", this.log.Shutdown);
            pc.AddOrReplaceTab("Shutdown", "sysout from End Script execution", EndFileControl);
        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ParmsControl == null)
                ParmsControl = new ParametersBrowserControl();
            ParmsControl.Fill(this.log.ExecutionParameters);
            pc.AddOrReplaceTab("Parameters", "Execution Parameters", ParmsControl);
        }

        private void logfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RawFileControl == null)
                RawFileControl = new FileBrowserControl();
            RawFileControl.Fill(this.log.LogFileName, this.readTextFile(this.log.LogFileName));
            pc.AddOrReplaceTab("Log File (raw)", this.log.LogFileName, RawFileControl);
        }

        private void errorFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ErrorFileControl == null)
                ErrorFileControl = new FileBrowserControl();
            ErrorFileControl.Fill(this.log.ErrorFile, this.readTextFile(this.log.ErrorFile));
            pc.AddOrReplaceTab("Error File", this.log.ErrorFile, ErrorFileControl);
        }

        private void summaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SummaryFileControl == null)
                SummaryFileControl = new FileBrowserControl();
            SummaryFileControl.Fill(this.log.SummaryFile, this.readTextFile(this.log.SummaryFile));
            pc.AddOrReplaceTab("Summary File", this.log.SummaryFile, SummaryFileControl);
        }

        private void aBREPORTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ABREPORTFileControl == null)
                ABREPORTFileControl = new FileBrowserControl();
            ABREPORTFileControl.Fill("AB_REPORT settings", this.log.ReportOptions.ToString());
            pc.AddOrReplaceTab("AB_REPORT", "AB_REPORT settings", ABREPORTFileControl);
        }

        private void plotterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GraphContainer == null)
                GraphContainer = new GraphContainerControl();
            pc.AddOrReplaceTab("Plotter", "Data-Time Plotter", GraphContainer);
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
