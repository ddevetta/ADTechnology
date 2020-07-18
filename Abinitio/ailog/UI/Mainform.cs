// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.Mainform
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.AbInitio.Classes;
using ADTechnology.AbInitio.IO;
using ADTechnology.Windows.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
  public class Mainform : Form
  {
    private ProjectSource projectSource = ProjectSource.Unknown;
    private string optsFile = Application.UserAppDataPath + "\\ailog.options.dat";
    private long logSize = 0;
    private IContainer components = (IContainer) null;
    private LogFile log;
    private PhaseControl pc;
    private System.Windows.Forms.Timer timer;
    private System.Windows.Forms.Timer refreshTimer;
    private OpenFileDialog ofd;
    private OpenHostFileDialog ohfd;
    private Options opts;
    private ColorPallette pallette;
    private BackgroundWorker thread;
    private ProjectStatus previousStatus;
    private Label label1;
    private MenuStrip mainMenu;
    private ToolStripMenuItem fileToolStripMenuItem1;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem exitToolStripMenuItem1;
    private ToolStripMenuItem toolsToolStripMenuItem;
    private ToolStripMenuItem customizeToolStripMenuItem;
    private ToolStripMenuItem optionsToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem contentsToolStripMenuItem;
    private ToolStripMenuItem indexToolStripMenuItem;
    private ToolStripMenuItem searchToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ProjectControl projectControl;
    private ToolStripMenuItem vToolStripMenuItem;
    private ToolStripMenuItem startupToolStripMenuItem;
    private ToolStripMenuItem parametersToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem refreshToolStripMenuItem;
    private Panel mainPanel;
    private StatusStrip statusStrip;
    private ToolStripProgressBar toolStripProgressBar;
    private ToolStripStatusLabel lblStatus;
    private ToolStripMenuItem openHostToolStripMenuItem;
    private ToolStripMenuItem shutdownToolStripMenuItem;
    private ToolStripStatusLabel lblStatusRight;
    private ToolStripMenuItem errorFileToolStripMenuItem;
    private ToolStripMenuItem summaryFileToolStripMenuItem;
    private ToolStripMenuItem plotterToolStripMenuItem;
    private ToolStripMenuItem hideAllRevisionsToolStripMenuItem;
    private ToolStripMenuItem autoRefreshToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem logFileToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem aBREPORTToolStripMenuItem;

    public Mainform()
    {
      this.mainForm_initialise();
    }

    public Mainform(string logFile)
    {
      this.mainForm_initialise();
      this.loadFile(logFile);
    }

    private void mainForm_initialise()
    {
      this.InitializeComponent();
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
      this.mainPanel.Controls.Add((Control) this.pc);
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
      this.log.Parse(this.opts, (StreamReader) e.Argument, 0L);
    }

    private void thread_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.toolStripProgressBar.Value = e.ProgressPercentage;
    }

    private void thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Error != null)
      {
        if (e.Error.GetType() == typeof (ExpectedNotFoundException))
        {
          ExpectedNotFoundException error = (ExpectedNotFoundException) e.Error;
          this.renderDisplay();
          if (MessageBox.Show((IWin32Window) this, "An unexpected error was encountered on line " + error.LineNumber.ToString() + " of the log file.\r\n\r\n" + error.Message + (error.InnerException == null ? "" : "\r\n\r\nInner message stack is:\r\n" + error.InnerException.StackTrace) + "\r\n\r\nDo you wish to try a refresh?\r\n", "Error in Log File", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;
          this.refreshToolStripMenuItem_Click((object) this, new EventArgs());
        }
        else if (e.Error.GetType() == typeof (MissingVertexException))
        {
          MissingVertexException error = (MissingVertexException) e.Error;
          int num = (int) MessageBox.Show(error.Message + "\r\nVertex: " + error.VertexName, "Error during Vertex-Flow linkup");
        }
        else
        {
          int num1 = (int) MessageBox.Show(e.Error.Message + "\r\n" + e.Error.GetType().ToString() + "\r\n" + e.Error.StackTrace, "Unexpected Exception");
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
      this.thread.ReportProgress((int) (e.BytesSoFar * 100L / this.logSize));
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
        int num = (int) MessageBox.Show(ex.Message + "\r\n" + ex.GetType().ToString() + "\r\n" + ex.StackTrace, "Unexpected Exception");
        return;
      }
      StreamReader sr = new StreamReader((Stream) this.ohfd.FTPConnection.DataStream);
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
        if (MessageBox.Show((IWin32Window) this, "An unexpected error was encountered on line " + ex.LineNumber.ToString() + " of the log file.\r\n\r\n" + ex.Message + (ex.InnerException == null ? "" : "\r\n\r\nInner message stack is:\r\n" + ex.InnerException.StackTrace) + "\r\n\r\nDo you wish to try a refresh?\r\n", "Error in Log File", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
          return;
        this.refreshToolStripMenuItem_Click((object) this, new EventArgs());
        return;
      }
      catch (MissingVertexException ex)
      {
        int num = (int) MessageBox.Show(ex.Message + "\r\nVertex: " + ex.VertexName, "Error during Vertex-Flow linkup");
        return;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message + "\r\n" + ex.GetType().ToString() + "\r\n" + ex.StackTrace, "Unexpected Exception");
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
      string str = (string) null;
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
          str = new StreamReader((Stream) this.ohfd.FTPConnection.DataStream).ReadToEnd();
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
        int num = (int) MessageBox.Show(ex.Message, "Plotter Error");
        return;
      }
      foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
      {
        if (openForm.GetType() == typeof (GraphForm) && openForm.Text == "Plotter")
        {
          ((GraphForm) openForm).Add(plotLine.GraphLine);
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
      if (this.ofd.ShowDialog((IWin32Window) this) != DialogResult.OK)
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
      if (this.ohfd.ShowDialog((IWin32Window) this) != DialogResult.OK)
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
      if (this.showOpenForm(typeof (StaticDataForm), "Start Script Output"))
        return;
      new StaticDataForm(this.log.Startup, "Start Script Output").Show((IWin32Window) this);
    }

    private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.showOpenForm(typeof (StaticDataForm), "End Script Output"))
        return;
      new StaticDataForm(this.log.Shutdown, "End Script Output").Show((IWin32Window) this);
    }

    private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.showOpenForm(typeof (ExecutionParametersForm), "Execution Parameters"))
        return;
      new ExecutionParametersForm(this.log.ExecutionParameters).Show((IWin32Window) this);
    }

    private void logfileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.showOpenForm(typeof (StaticDataForm), this.log.LogFileName))
        return;
      new StaticDataForm(this.readTextFile(this.log.LogFileName), this.log.LogFileName).Show((IWin32Window) this);
    }

    private void errorFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.showOpenForm(typeof (StaticDataForm), this.log.ErrorFile))
        return;
      new StaticDataForm(this.readTextFile(this.log.ErrorFile), this.log.ErrorFile).Show((IWin32Window) this);
    }

    private void summaryFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.showOpenForm(typeof (StaticDataForm), this.log.SummaryFile))
        return;
      new StaticDataForm(this.readTextFile(this.log.SummaryFile), this.log.SummaryFile).Show((IWin32Window) this);
    }

    private void aBREPORTToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.showOpenForm(typeof (StaticDataForm), "AB_REPORT"))
        return;
      new StaticDataForm(this.log.ReportOptions.ToString(), "AB_REPORT").Show((IWin32Window) this);
    }

    private void plotterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.showOpenForm(typeof (GraphForm), "Plotter"))
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
      int num = (int) new AboutForm().ShowDialog();
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
      foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Mainform));
      this.label1 = new Label();
      this.mainMenu = new MenuStrip();
      this.fileToolStripMenuItem1 = new ToolStripMenuItem();
      this.openToolStripMenuItem = new ToolStripMenuItem();
      this.openHostToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator = new ToolStripSeparator();
      this.exitToolStripMenuItem1 = new ToolStripMenuItem();
      this.vToolStripMenuItem = new ToolStripMenuItem();
      this.parametersToolStripMenuItem = new ToolStripMenuItem();
      this.startupToolStripMenuItem = new ToolStripMenuItem();
      this.shutdownToolStripMenuItem = new ToolStripMenuItem();
      this.aBREPORTToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.logFileToolStripMenuItem = new ToolStripMenuItem();
      this.errorFileToolStripMenuItem = new ToolStripMenuItem();
      this.summaryFileToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.plotterToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem1 = new ToolStripSeparator();
      this.hideAllRevisionsToolStripMenuItem = new ToolStripMenuItem();
      this.autoRefreshToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.refreshToolStripMenuItem = new ToolStripMenuItem();
      this.toolsToolStripMenuItem = new ToolStripMenuItem();
      this.customizeToolStripMenuItem = new ToolStripMenuItem();
      this.optionsToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.contentsToolStripMenuItem = new ToolStripMenuItem();
      this.indexToolStripMenuItem = new ToolStripMenuItem();
      this.searchToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.aboutToolStripMenuItem = new ToolStripMenuItem();
      this.mainPanel = new Panel();
      this.statusStrip = new StatusStrip();
      this.toolStripProgressBar = new ToolStripProgressBar();
      this.lblStatus = new ToolStripStatusLabel();
      this.lblStatusRight = new ToolStripStatusLabel();
      this.projectControl = new ProjectControl();
      this.mainMenu.SuspendLayout();
      this.statusStrip.SuspendLayout();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Dock = DockStyle.Fill;
      this.label1.Location = new Point(0, 24);
      this.label1.Name = "label1";
      this.label1.Size = new Size(0, 13);
      this.label1.TabIndex = 1;
      this.mainMenu.AllowItemReorder = true;
      this.mainMenu.GripStyle = ToolStripGripStyle.Visible;
      this.mainMenu.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.fileToolStripMenuItem1,
        (ToolStripItem) this.vToolStripMenuItem,
        (ToolStripItem) this.toolsToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem
      });
      this.mainMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.mainMenu.Location = new Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.RenderMode = ToolStripRenderMode.Professional;
      this.mainMenu.Size = new Size(1016, 24);
      this.mainMenu.TabIndex = 2;
      this.mainMenu.Text = "mainMenu";
      this.fileToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.openToolStripMenuItem,
        (ToolStripItem) this.openHostToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator,
        (ToolStripItem) this.exitToolStripMenuItem1
      });
      this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
      this.fileToolStripMenuItem1.Size = new Size(37, 20);
      this.fileToolStripMenuItem1.Text = "&File";
      this.openToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("openToolStripMenuItem.Image");
      this.openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.ShortcutKeys = Keys.O | Keys.Control;
      this.openToolStripMenuItem.Size = new Size(183, 22);
      this.openToolStripMenuItem.Text = "&Open File...";
      this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
      this.openHostToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("openHostToolStripMenuItem.Image");
      this.openHostToolStripMenuItem.ImageTransparentColor = Color.White;
      this.openHostToolStripMenuItem.Name = "openHostToolStripMenuItem";
      this.openHostToolStripMenuItem.ShortcutKeys = Keys.N | Keys.Control;
      this.openHostToolStripMenuItem.Size = new Size(183, 22);
      this.openHostToolStripMenuItem.Text = "Open &Host...";
      this.openHostToolStripMenuItem.Click += new EventHandler(this.openHostToolStripMenuItem_Click);
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new Size(180, 6);
      this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
      this.exitToolStripMenuItem1.Size = new Size(183, 22);
      this.exitToolStripMenuItem1.Text = "E&xit";
      this.exitToolStripMenuItem1.Click += new EventHandler(this.exitToolStripMenuItem1_Click);
      this.vToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[15]
      {
        (ToolStripItem) this.parametersToolStripMenuItem,
        (ToolStripItem) this.startupToolStripMenuItem,
        (ToolStripItem) this.shutdownToolStripMenuItem,
        (ToolStripItem) this.aBREPORTToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.logFileToolStripMenuItem,
        (ToolStripItem) this.errorFileToolStripMenuItem,
        (ToolStripItem) this.summaryFileToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.plotterToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.hideAllRevisionsToolStripMenuItem,
        (ToolStripItem) this.autoRefreshToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.refreshToolStripMenuItem
      });
      this.vToolStripMenuItem.Name = "vToolStripMenuItem";
      this.vToolStripMenuItem.Size = new Size(44, 20);
      this.vToolStripMenuItem.Text = "&View";
      this.parametersToolStripMenuItem.Enabled = false;
      this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
      this.parametersToolStripMenuItem.Size = new Size(160, 22);
      this.parametersToolStripMenuItem.Text = "Parameters";
      this.parametersToolStripMenuItem.Click += new EventHandler(this.parametersToolStripMenuItem_Click);
      this.startupToolStripMenuItem.Enabled = false;
      this.startupToolStripMenuItem.Name = "startupToolStripMenuItem";
      this.startupToolStripMenuItem.Size = new Size(160, 22);
      this.startupToolStripMenuItem.Text = "Startup";
      this.startupToolStripMenuItem.Click += new EventHandler(this.startupToolStripMenuItem_Click);
      this.shutdownToolStripMenuItem.Enabled = false;
      this.shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
      this.shutdownToolStripMenuItem.Size = new Size(160, 22);
      this.shutdownToolStripMenuItem.Text = "Shutdown";
      this.shutdownToolStripMenuItem.Click += new EventHandler(this.shutdownToolStripMenuItem_Click);
      this.aBREPORTToolStripMenuItem.Enabled = false;
      this.aBREPORTToolStripMenuItem.Name = "aBREPORTToolStripMenuItem";
      this.aBREPORTToolStripMenuItem.Size = new Size(160, 22);
      this.aBREPORTToolStripMenuItem.Text = "AB_REPORT";
      this.aBREPORTToolStripMenuItem.Click += new EventHandler(this.aBREPORTToolStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(157, 6);
      this.logFileToolStripMenuItem.Enabled = false;
      this.logFileToolStripMenuItem.Name = "logFileToolStripMenuItem";
      this.logFileToolStripMenuItem.Size = new Size(160, 22);
      this.logFileToolStripMenuItem.Text = "Log File (raw)";
      this.logFileToolStripMenuItem.Click += new EventHandler(this.logfileToolStripMenuItem_Click);
      this.errorFileToolStripMenuItem.Enabled = false;
      this.errorFileToolStripMenuItem.Name = "errorFileToolStripMenuItem";
      this.errorFileToolStripMenuItem.Size = new Size(160, 22);
      this.errorFileToolStripMenuItem.Text = "Error File";
      this.errorFileToolStripMenuItem.Click += new EventHandler(this.errorFileToolStripMenuItem_Click);
      this.summaryFileToolStripMenuItem.Enabled = false;
      this.summaryFileToolStripMenuItem.Name = "summaryFileToolStripMenuItem";
      this.summaryFileToolStripMenuItem.Size = new Size(160, 22);
      this.summaryFileToolStripMenuItem.Text = "Summary File";
      this.summaryFileToolStripMenuItem.Click += new EventHandler(this.summaryFileToolStripMenuItem_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(157, 6);
      this.plotterToolStripMenuItem.Name = "plotterToolStripMenuItem";
      this.plotterToolStripMenuItem.Size = new Size(160, 22);
      this.plotterToolStripMenuItem.Text = "Plotter";
      this.plotterToolStripMenuItem.Click += new EventHandler(this.plotterToolStripMenuItem_Click);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(157, 6);
      this.hideAllRevisionsToolStripMenuItem.Name = "hideAllRevisionsToolStripMenuItem";
      this.hideAllRevisionsToolStripMenuItem.Size = new Size(160, 22);
      this.hideAllRevisionsToolStripMenuItem.Text = "Merge Revisions";
      this.hideAllRevisionsToolStripMenuItem.Click += new EventHandler(this.hideAllRevisionsToolStripMenuItem_Click);
      this.autoRefreshToolStripMenuItem.Checked = true;
      this.autoRefreshToolStripMenuItem.CheckState = CheckState.Checked;
      this.autoRefreshToolStripMenuItem.Name = "autoRefreshToolStripMenuItem";
      this.autoRefreshToolStripMenuItem.Size = new Size(160, 22);
      this.autoRefreshToolStripMenuItem.Text = "Auto Refresh";
      this.autoRefreshToolStripMenuItem.Click += new EventHandler(this.autoRefreshToolStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(157, 6);
      this.refreshToolStripMenuItem.Enabled = false;
      this.refreshToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("refreshToolStripMenuItem.Image");
      this.refreshToolStripMenuItem.ImageTransparentColor = Color.White;
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.ShortcutKeys = Keys.R | Keys.Control;
      this.refreshToolStripMenuItem.Size = new Size(160, 22);
      this.refreshToolStripMenuItem.Text = "&Refresh";
      this.refreshToolStripMenuItem.Click += new EventHandler(this.refreshToolStripMenuItem_Click);
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.customizeToolStripMenuItem,
        (ToolStripItem) this.optionsToolStripMenuItem
      });
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new Size(48, 20);
      this.toolsToolStripMenuItem.Text = "&Tools";
      this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
      this.customizeToolStripMenuItem.Size = new Size(130, 22);
      this.customizeToolStripMenuItem.Text = "&Customize";
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new Size(130, 22);
      this.optionsToolStripMenuItem.Text = "&Options";
      this.optionsToolStripMenuItem.Click += new EventHandler(this.optionsToolStripMenuItem_Click);
      this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.contentsToolStripMenuItem,
        (ToolStripItem) this.indexToolStripMenuItem,
        (ToolStripItem) this.searchToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.aboutToolStripMenuItem
      });
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
      this.contentsToolStripMenuItem.Size = new Size(122, 22);
      this.contentsToolStripMenuItem.Text = "&Contents";
      this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
      this.indexToolStripMenuItem.Size = new Size(122, 22);
      this.indexToolStripMenuItem.Text = "&Index";
      this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
      this.searchToolStripMenuItem.Size = new Size(122, 22);
      this.searchToolStripMenuItem.Text = "&Search";
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(119, 6);
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new Size(122, 22);
      this.aboutToolStripMenuItem.Text = "&About...";
      this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
      this.mainPanel.Dock = DockStyle.Fill;
      this.mainPanel.Location = new Point(0, 131);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.Size = new Size(1016, 513);
      this.mainPanel.TabIndex = 4;
      this.statusStrip.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.toolStripProgressBar,
        (ToolStripItem) this.lblStatus,
        (ToolStripItem) this.lblStatusRight
      });
      this.statusStrip.Location = new Point(0, 644);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new Size(1016, 22);
      this.statusStrip.TabIndex = 5;
      this.toolStripProgressBar.Name = "toolStripProgressBar";
      this.toolStripProgressBar.Size = new Size(100, 16);
      this.toolStripProgressBar.Style = ProgressBarStyle.Continuous;
      this.lblStatus.BorderStyle = Border3DStyle.Etched;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(679, 17);
      this.lblStatus.Spring = true;
      this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
      this.lblStatusRight.AutoSize = false;
      this.lblStatusRight.Name = "lblStatusRight";
      this.lblStatusRight.Size = new Size(220, 17);
      this.projectControl.Dock = DockStyle.Top;
      this.projectControl.FileName = "";
      this.projectControl.Location = new Point(0, 24);
      this.projectControl.Name = "projectControl";
      this.projectControl.Size = new Size(1016, 107);
      this.projectControl.TabIndex = 3;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1016, 666);
      this.Controls.Add((Control) this.mainPanel);
      this.Controls.Add((Control) this.statusStrip);
      this.Controls.Add((Control) this.projectControl);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.mainMenu);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.mainMenu;
      this.Name = nameof (Mainform);
      this.Text = "Ab Initio Log Viewer";
      this.FormClosing += new FormClosingEventHandler(this.Mainform_FormClosing);
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
