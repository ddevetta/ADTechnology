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
            this.cbAutoRefresh = new System.Windows.Forms.CheckBox();
            this.cbGetIntervalFromReportParms = new System.Windows.Forms.CheckBox();
            this.tbAutoRefreshInterval = new System.Windows.Forms.TextBox();
            this.lblAutoRefreshInterval = new System.Windows.Forms.Label();
            this.cbExtraRefreshAfterCompletion = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbErrorFile = new System.Windows.Forms.TextBox();
            this.tbSummaryFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbAutoRefresh
            // 
            this.cbAutoRefresh.AutoSize = true;
            this.cbAutoRefresh.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAutoRefresh.Location = new System.Drawing.Point(170, 46);
            this.cbAutoRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAutoRefresh.Name = "cbAutoRefresh";
            this.cbAutoRefresh.Size = new System.Drawing.Size(287, 21);
            this.cbAutoRefresh.TabIndex = 0;
            this.cbAutoRefresh.Text = "Auto-refresh Started or Running projects";
            this.cbAutoRefresh.UseVisualStyleBackColor = true;
            this.cbAutoRefresh.CheckedChanged += new System.EventHandler(this.cbAutoRefresh_CheckedChanged);
            // 
            // cbGetIntervalFromReportParms
            // 
            this.cbGetIntervalFromReportParms.AutoSize = true;
            this.cbGetIntervalFromReportParms.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbGetIntervalFromReportParms.Location = new System.Drawing.Point(92, 75);
            this.cbGetIntervalFromReportParms.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbGetIntervalFromReportParms.Name = "cbGetIntervalFromReportParms";
            this.cbGetIntervalFromReportParms.Size = new System.Drawing.Size(365, 21);
            this.cbGetIntervalFromReportParms.TabIndex = 1;
            this.cbGetIntervalFromReportParms.Text = "Get Auto-refresh interval from the project parameters";
            this.cbGetIntervalFromReportParms.UseVisualStyleBackColor = true;
            this.cbGetIntervalFromReportParms.CheckedChanged += new System.EventHandler(this.cbGetIntervalFromReportParms_CheckedChanged);
            // 
            // tbAutoRefreshInterval
            // 
            this.tbAutoRefreshInterval.Location = new System.Drawing.Point(437, 104);
            this.tbAutoRefreshInterval.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAutoRefreshInterval.MaxLength = 3;
            this.tbAutoRefreshInterval.Name = "tbAutoRefreshInterval";
            this.tbAutoRefreshInterval.Size = new System.Drawing.Size(40, 22);
            this.tbAutoRefreshInterval.TabIndex = 2;
            this.tbAutoRefreshInterval.TextChanged += new System.EventHandler(this.tbAutoRefreshInterval_TextChanged);
            this.tbAutoRefreshInterval.Validating += new System.ComponentModel.CancelEventHandler(this.tbAutoRefreshInterval_Validating);
            // 
            // lblAutoRefreshInterval
            // 
            this.lblAutoRefreshInterval.AutoSize = true;
            this.lblAutoRefreshInterval.Location = new System.Drawing.Point(225, 107);
            this.lblAutoRefreshInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoRefreshInterval.Name = "lblAutoRefreshInterval";
            this.lblAutoRefreshInterval.Size = new System.Drawing.Size(204, 17);
            this.lblAutoRefreshInterval.TabIndex = 3;
            this.lblAutoRefreshInterval.Text = "Auto-refresh interval (seconds)";
            // 
            // cbExtraRefreshAfterCompletion
            // 
            this.cbExtraRefreshAfterCompletion.AutoSize = true;
            this.cbExtraRefreshAfterCompletion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbExtraRefreshAfterCompletion.Location = new System.Drawing.Point(110, 134);
            this.cbExtraRefreshAfterCompletion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbExtraRefreshAfterCompletion.Name = "cbExtraRefreshAfterCompletion";
            this.cbExtraRefreshAfterCompletion.Size = new System.Drawing.Size(347, 21);
            this.cbExtraRefreshAfterCompletion.TabIndex = 4;
            this.cbExtraRefreshAfterCompletion.Text = "Initiate an extra refresh after the project completes";
            this.cbExtraRefreshAfterCompletion.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 200);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name of Variable containing the path to the Error file";
            // 
            // tbErrorFile
            // 
            this.tbErrorFile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbErrorFile.Location = new System.Drawing.Point(437, 197);
            this.tbErrorFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbErrorFile.Name = "tbErrorFile";
            this.tbErrorFile.Size = new System.Drawing.Size(169, 22);
            this.tbErrorFile.TabIndex = 6;
            this.tbErrorFile.TextChanged += new System.EventHandler(this.tbErrorFile_TextChanged);
            // 
            // tbSummaryFile
            // 
            this.tbSummaryFile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbSummaryFile.Location = new System.Drawing.Point(437, 232);
            this.tbSummaryFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSummaryFile.Name = "tbSummaryFile";
            this.tbSummaryFile.Size = new System.Drawing.Size(169, 22);
            this.tbSummaryFile.TabIndex = 8;
            this.tbSummaryFile.TextChanged += new System.EventHandler(this.tbSummaryFile_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 235);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(367, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Name of Variable containing the path to the Summary file";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(657, 297);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(657, 332);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(774, 378);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbSummaryFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbErrorFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbExtraRefreshAfterCompletion);
            this.Controls.Add(this.lblAutoRefreshInterval);
            this.Controls.Add(this.tbAutoRefreshInterval);
            this.Controls.Add(this.cbGetIntervalFromReportParms);
            this.Controls.Add(this.cbAutoRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Ab Initio Log Viewer Default Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
  }
}
