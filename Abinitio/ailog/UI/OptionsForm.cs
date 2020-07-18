// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.OptionsForm
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.AbInitio.Classes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
  public class OptionsForm : Form
  {
    private string optsFile = Application.UserAppDataPath + "\\ailog.options.dat";
    private IContainer components = (IContainer) null;
    private Options opts;
    private CheckBox cbAutoRefresh;
    private CheckBox cbGetIntervalFromReportParms;
    private TextBox tbAutoRefreshInterval;
    private Label lblAutoRefreshInterval;
    private CheckBox cbExtraRefreshAfterCompletion;
    private Label label1;
    private TextBox tbErrorFile;
    private TextBox tbSummaryFile;
    private Label label2;
    private CheckBox cbMergeRevisions;
    private Button btnSave;
    private Button btnCancel;

    public string OptionsFilePath
    {
      get
      {
        return this.optsFile;
      }
      set
      {
        this.optsFile = value;
      }
    }

    public OptionsForm()
    {
      this.InitializeComponent();
      this.opts = Options.Deserialize(this.optsFile);
      this.cbGetIntervalFromReportParms.DataBindings.Add("Enabled", (object) this.cbAutoRefresh, "Checked");
      this.cbExtraRefreshAfterCompletion.DataBindings.Add("Enabled", (object) this.cbAutoRefresh, "Checked");
      this.cbAutoRefresh.DataBindings.Add("Checked", (object) this.opts, "AutoRefresh", false, DataSourceUpdateMode.OnPropertyChanged);
      this.cbGetIntervalFromReportParms.DataBindings.Add("Checked", (object) this.opts, "GetIntervalFromReportParms", false, DataSourceUpdateMode.OnPropertyChanged);
      this.tbAutoRefreshInterval.DataBindings.Add("Text", (object) this.opts, "Interval", false, DataSourceUpdateMode.OnPropertyChanged);
      this.cbExtraRefreshAfterCompletion.DataBindings.Add("Checked", (object) this.opts, "ExtraRefreshAfterCompletion", false, DataSourceUpdateMode.OnPropertyChanged);
      this.cbMergeRevisions.DataBindings.Add("Checked", (object) this.opts, "MergeRevisions", false, DataSourceUpdateMode.OnPropertyChanged);
      this.tbErrorFile.DataBindings.Add("Text", (object) this.opts, "ErrorVariable", false, DataSourceUpdateMode.OnPropertyChanged);
      this.tbSummaryFile.DataBindings.Add("Text", (object) this.opts, "SummaryVariable", false, DataSourceUpdateMode.OnPropertyChanged);
      this.cbAutoRefresh_CheckedChanged((object) this, new EventArgs());
      this.opts.OptionsChanged += new EventHandler(this.opts_OptionsChanged);
    }

    private void cbGetIntervalFromReportParms_CheckedChanged(object sender, EventArgs e)
    {
      this.lblAutoRefreshInterval.Enabled = !this.cbGetIntervalFromReportParms.Checked && this.cbAutoRefresh.Checked;
      this.tbAutoRefreshInterval.Enabled = !this.cbGetIntervalFromReportParms.Checked && this.cbAutoRefresh.Checked;
    }

    private void cbAutoRefresh_CheckedChanged(object sender, EventArgs e)
    {
      this.cbGetIntervalFromReportParms_CheckedChanged(sender, e);
    }

    private void tbAutoRefreshInterval_TextChanged(object sender, EventArgs e)
    {
      this.tbAutoRefreshInterval.Text = Regex.Replace(this.tbAutoRefreshInterval.Text, "[^0-9]+", "");
    }

    private void tbAutoRefreshInterval_Validating(object sender, CancelEventArgs e)
    {
      if (!(this.tbAutoRefreshInterval.Text == "") && !(this.tbAutoRefreshInterval.Text == "0"))
        return;
      this.tbAutoRefreshInterval.Text = "60";
    }

    private void tbErrorFile_TextChanged(object sender, EventArgs e)
    {
      this.tbErrorFile.Text = Regex.Replace(this.tbErrorFile.Text, "[^\\w]", "");
    }

    private void tbSummaryFile_TextChanged(object sender, EventArgs e)
    {
      this.tbSummaryFile.Text = Regex.Replace(this.tbSummaryFile.Text, "[^\\w]", "");
    }

    private void opts_OptionsChanged(object sender, EventArgs e)
    {
      this.btnSave.Enabled = true;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.opts.Serialize(this.optsFile);
      this.btnSave.Enabled = false;
      this.Close();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.btnSave.Enabled)
        return;
      switch (MessageBox.Show("Modifications have not been saved. Save before exiting?", "Unsaved Modifications", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
      {
        case DialogResult.Cancel:
          e.Cancel = true;
          break;
        case DialogResult.Yes:
          this.btnSave_Click(sender, (EventArgs) e);
          break;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.cbAutoRefresh = new CheckBox();
      this.cbGetIntervalFromReportParms = new CheckBox();
      this.tbAutoRefreshInterval = new TextBox();
      this.lblAutoRefreshInterval = new Label();
      this.cbExtraRefreshAfterCompletion = new CheckBox();
      this.label1 = new Label();
      this.tbErrorFile = new TextBox();
      this.tbSummaryFile = new TextBox();
      this.label2 = new Label();
      this.cbMergeRevisions = new CheckBox();
      this.btnSave = new Button();
      this.btnCancel = new Button();
      this.SuspendLayout();
      this.cbAutoRefresh.AutoSize = true;
      this.cbAutoRefresh.CheckAlign = ContentAlignment.MiddleRight;
      this.cbAutoRefresh.Location = new Point((int) sbyte.MaxValue, 38);
      this.cbAutoRefresh.Name = "cbAutoRefresh";
      this.cbAutoRefresh.Size = new Size(215, 17);
      this.cbAutoRefresh.TabIndex = 0;
      this.cbAutoRefresh.Text = "Auto-refresh Started or Running projects";
      this.cbAutoRefresh.UseVisualStyleBackColor = true;
      this.cbAutoRefresh.CheckedChanged += new EventHandler(this.cbAutoRefresh_CheckedChanged);
      this.cbGetIntervalFromReportParms.AutoSize = true;
      this.cbGetIntervalFromReportParms.CheckAlign = ContentAlignment.MiddleRight;
      this.cbGetIntervalFromReportParms.Location = new Point(71, 61);
      this.cbGetIntervalFromReportParms.Name = "cbGetIntervalFromReportParms";
      this.cbGetIntervalFromReportParms.Size = new Size(271, 17);
      this.cbGetIntervalFromReportParms.TabIndex = 1;
      this.cbGetIntervalFromReportParms.Text = "Get Auto-refresh interval from the project parameters";
      this.cbGetIntervalFromReportParms.UseVisualStyleBackColor = true;
      this.cbGetIntervalFromReportParms.CheckedChanged += new EventHandler(this.cbGetIntervalFromReportParms_CheckedChanged);
      this.tbAutoRefreshInterval.Location = new Point(327, 84);
      this.tbAutoRefreshInterval.MaxLength = 3;
      this.tbAutoRefreshInterval.Name = "tbAutoRefreshInterval";
      this.tbAutoRefreshInterval.Size = new Size(24, 20);
      this.tbAutoRefreshInterval.TabIndex = 2;
      this.tbAutoRefreshInterval.TextAlign = HorizontalAlignment.Right;
      this.tbAutoRefreshInterval.TextChanged += new EventHandler(this.tbAutoRefreshInterval_TextChanged);
      this.tbAutoRefreshInterval.Validating += new CancelEventHandler(this.tbAutoRefreshInterval_Validating);
      this.lblAutoRefreshInterval.AutoSize = true;
      this.lblAutoRefreshInterval.Location = new Point(171, 87);
      this.lblAutoRefreshInterval.Name = "lblAutoRefreshInterval";
      this.lblAutoRefreshInterval.Size = new Size(150, 13);
      this.lblAutoRefreshInterval.TabIndex = 3;
      this.lblAutoRefreshInterval.Text = "Auto-refresh interval (seconds)";
      this.cbExtraRefreshAfterCompletion.AutoSize = true;
      this.cbExtraRefreshAfterCompletion.CheckAlign = ContentAlignment.MiddleRight;
      this.cbExtraRefreshAfterCompletion.Location = new Point(81, 110);
      this.cbExtraRefreshAfterCompletion.Name = "cbExtraRefreshAfterCompletion";
      this.cbExtraRefreshAfterCompletion.Size = new Size(261, 17);
      this.cbExtraRefreshAfterCompletion.TabIndex = 4;
      this.cbExtraRefreshAfterCompletion.Text = "Initiate an extra refresh after the project completes";
      this.cbExtraRefreshAfterCompletion.UseVisualStyleBackColor = true;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(68, 200);
      this.label1.Name = "label1";
      this.label1.Size = new Size(253, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Name of Variable containing the path to the Error file";
      this.tbErrorFile.CharacterCasing = CharacterCasing.Upper;
      this.tbErrorFile.Location = new Point(327, 197);
      this.tbErrorFile.Name = "tbErrorFile";
      this.tbErrorFile.Size = new Size(128, 20);
      this.tbErrorFile.TabIndex = 6;
      this.tbErrorFile.TextChanged += new EventHandler(this.tbErrorFile_TextChanged);
      this.tbSummaryFile.CharacterCasing = CharacterCasing.Upper;
      this.tbSummaryFile.Location = new Point(327, 225);
      this.tbSummaryFile.Name = "tbSummaryFile";
      this.tbSummaryFile.Size = new Size(128, 20);
      this.tbSummaryFile.TabIndex = 8;
      this.tbSummaryFile.TextChanged += new EventHandler(this.tbSummaryFile_TextChanged);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(47, 228);
      this.label2.Name = "label2";
      this.label2.Size = new Size(274, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Name of Variable containing the path to the Summary file";
      this.cbMergeRevisions.AutoSize = true;
      this.cbMergeRevisions.CheckAlign = ContentAlignment.MiddleRight;
      this.cbMergeRevisions.Location = new Point(237, 153);
      this.cbMergeRevisions.Name = "cbMergeRevisions";
      this.cbMergeRevisions.Size = new Size(105, 17);
      this.cbMergeRevisions.TabIndex = 9;
      this.cbMergeRevisions.Text = "Merge Revisions";
      this.cbMergeRevisions.UseVisualStyleBackColor = true;
      this.btnSave.DialogResult = DialogResult.OK;
      this.btnSave.Enabled = false;
      this.btnSave.Location = new Point(517, 285);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(75, 23);
      this.btnSave.TabIndex = 10;
      this.btnSave.Text = "OK";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(517, 314);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 11;
      this.btnCancel.Text = "Exit";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnExit_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(604, 349);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnSave);
      this.Controls.Add((Control) this.cbMergeRevisions);
      this.Controls.Add((Control) this.tbSummaryFile);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.tbErrorFile);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.cbExtraRefreshAfterCompletion);
      this.Controls.Add((Control) this.lblAutoRefreshInterval);
      this.Controls.Add((Control) this.tbAutoRefreshInterval);
      this.Controls.Add((Control) this.cbGetIntervalFromReportParms);
      this.Controls.Add((Control) this.cbAutoRefresh);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (OptionsForm);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Ab Initio Log Viewer Default Options";
      this.FormClosing += new FormClosingEventHandler(this.OptionsForm_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
