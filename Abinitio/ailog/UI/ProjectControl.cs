// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.ProjectControl
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.AbInitio.Classes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
  public class ProjectControl : UserControl
  {
    private IContainer components = (IContainer) null;
    private Project prj;
    private Label label1;
    private TextBox tbFileName;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label lblStatus;
    private Label lblPhases;
    private Label label5;
    private ComboBox cbPhase;
    private Label label7;
    private TextBox lblStart;
    private TextBox lblEnd;
    private TextBox lblDuration;
    private PictureBox pbStatus;
    private Label lblRevision;
    private ComboBox cbRevision;

    public string FileName
    {
      get
      {
        return this.tbFileName.Text;
      }
      set
      {
        this.tbFileName.Text = value;
      }
    }

    internal Project Project
    {
      get
      {
        return this.prj;
      }
      set
      {
        this.prj = value;
      }
    }

    public event EventHandler SelectedPhaseChanged;

    protected virtual void OnSelectedPhaseChanged(EventArgs e)
    {
      if (this.SelectedPhaseChanged == null)
        return;
      this.SelectedPhaseChanged((object) this, e);
    }

    public event EventHandler SelectedRevisionChanged;

    protected virtual void OnSelectedRevisionChanged(EventArgs e)
    {
      if (this.SelectedRevisionChanged == null)
        return;
      this.SelectedRevisionChanged((object) this, e);
    }

    internal Phase SelectedPhase
    {
      get
      {
        return (Phase) this.cbPhase.SelectedValue;
      }
      set
      {
        this.cbPhase.SelectedValue = (object) value;
      }
    }

    internal int SelectedRevision
    {
      get
      {
        return this.cbRevision.SelectedIndex;
      }
      set
      {
        this.cbRevision.SelectedIndex = value;
      }
    }

    public ProjectControl()
    {
      this.InitializeComponent();
    }

    public void DataBind()
    {
      this.lblStatus.DataBindings.Clear();
      this.lblStart.DataBindings.Clear();
      this.lblEnd.DataBindings.Clear();
      this.lblDuration.DataBindings.Clear();
      this.lblPhases.DataBindings.Clear();
      if (this.prj == null)
        return;
      this.lblStatus.DataBindings.Add("Text", (object) this.prj.Status, (string) null);
      if (this.prj.Start > DateTime.MinValue)
        this.lblStart.DataBindings.Add("Text", (object) this.prj.Start, (string) null);
      else
        this.lblStart.Text = "";
      if (this.prj.End > DateTime.MinValue)
        this.lblEnd.DataBindings.Add("Text", (object) this.prj.End, (string) null);
      else
        this.lblEnd.Text = "";
      if (this.prj.End > DateTime.MinValue || this.prj.Status == ProjectStatus.Running)
      {
        DateTime dateTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        this.lblDuration.DataBindings.Add("Text", (object) ((this.prj.End > DateTime.MinValue ? this.prj.End : dateTime) - this.prj.Start), (string) null);
      }
      else
        this.lblDuration.Text = "";
      this.lblPhases.DataBindings.Add("Text", (object) ("(" + this.prj.Phases.Count.ToString() + " phases)"), (string) null);
      this.cbPhase.DataSource = (object) this.prj.Phases;
      if (this.prj.Status == ProjectStatus.Running)
        this.cbPhase.SelectedIndex = this.cbPhase.Items.Count - 1;
      this.pbStatus.Paint += new PaintEventHandler(this.pbStatus_Paint);
      this.pbStatus.Refresh();
    }

    private void pbStatus_Paint(object sender, PaintEventArgs e)
    {
      Bitmap bitmap = Images.dot_notstarted;
      switch (this.prj.Status)
      {
        case ProjectStatus.Running:
          bitmap = Images.dot_running;
          break;
        case ProjectStatus.Ended:
          bitmap = Images.dot_cmplt;
          break;
        case ProjectStatus.Error:
          bitmap = Images.dot_failed;
          break;
      }
      Color pixel = bitmap.GetPixel(0, 0);
      bitmap.MakeTransparent(pixel);
      Rectangle rect = new Rectangle(0, 0, 12, 12);
      e.Graphics.DrawImage((Image) bitmap, rect);
    }

    private void cbPhase_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cbPhase.SelectedValue == null)
        return;
      this.cbRevision.DataSource = (object) ((Phase) this.cbPhase.SelectedValue).PhaseRevisions;
      this.cbRevision.SelectedIndex = 0;
      this.OnSelectedPhaseChanged(e);
    }

    private void cbRevision_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.OnSelectedRevisionChanged(e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.tbFileName = new TextBox();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.lblStatus = new Label();
      this.lblPhases = new Label();
      this.label5 = new Label();
      this.cbPhase = new ComboBox();
      this.label7 = new Label();
      this.lblStart = new TextBox();
      this.lblEnd = new TextBox();
      this.lblDuration = new TextBox();
      this.pbStatus = new PictureBox();
      this.lblRevision = new Label();
      this.cbRevision = new ComboBox();
      ((ISupportInitialize) this.pbStatus).BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(5, 16);
      this.label1.Name = "label1";
      this.label1.Size = new Size(60, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Log name :";
      this.tbFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tbFileName.Location = new Point(93, 13);
      this.tbFileName.Multiline = true;
      this.tbFileName.Name = "tbFileName";
      this.tbFileName.ReadOnly = true;
      this.tbFileName.Size = new Size(659, 22);
      this.tbFileName.TabIndex = 1;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(5, 44);
      this.label2.Name = "label2";
      this.label2.Size = new Size(79, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Project Status :";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(194, 44);
      this.label3.Name = "label3";
      this.label3.Size = new Size(35, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Start :";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(375, 44);
      this.label4.Name = "label4";
      this.label4.Size = new Size(32, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "End :";
      this.lblStatus.AutoSize = true;
      this.lblStatus.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblStatus.Location = new Point(115, 44);
      this.lblStatus.MinimumSize = new Size(30, 0);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(30, 13);
      this.lblStatus.TabIndex = 7;
      this.lblPhases.AutoSize = true;
      this.lblPhases.Location = new Point(245, 69);
      this.lblPhases.MinimumSize = new Size(20, 0);
      this.lblPhases.Name = "lblPhases";
      this.lblPhases.Size = new Size(20, 13);
      this.lblPhases.TabIndex = 8;
      this.label5.AutoSize = true;
      this.label5.Location = new Point(5, 69);
      this.label5.Name = "label5";
      this.label5.Size = new Size(43, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Phase :";
      this.cbPhase.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbPhase.FormattingEnabled = true;
      this.cbPhase.Location = new Point(93, 66);
      this.cbPhase.Name = "cbPhase";
      this.cbPhase.Size = new Size(146, 21);
      this.cbPhase.TabIndex = 11;
      this.cbPhase.SelectedIndexChanged += new EventHandler(this.cbPhase_SelectedIndexChanged);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(562, 44);
      this.label7.Name = "label7";
      this.label7.Size = new Size(53, 13);
      this.label7.TabIndex = 12;
      this.label7.Text = "Duration :";
      this.lblStart.BorderStyle = BorderStyle.None;
      this.lblStart.Location = new Point(235, 44);
      this.lblStart.MinimumSize = new Size(20, 0);
      this.lblStart.Name = "lblStart";
      this.lblStart.ReadOnly = true;
      this.lblStart.Size = new Size(134, 13);
      this.lblStart.TabIndex = 14;
      this.lblEnd.BorderStyle = BorderStyle.None;
      this.lblEnd.Location = new Point(413, 44);
      this.lblEnd.MinimumSize = new Size(20, 0);
      this.lblEnd.Name = "lblEnd";
      this.lblEnd.ReadOnly = true;
      this.lblEnd.Size = new Size(134, 13);
      this.lblEnd.TabIndex = 15;
      this.lblDuration.BorderStyle = BorderStyle.None;
      this.lblDuration.Location = new Point(621, 44);
      this.lblDuration.MinimumSize = new Size(20, 0);
      this.lblDuration.Name = "lblDuration";
      this.lblDuration.ReadOnly = true;
      this.lblDuration.Size = new Size(91, 13);
      this.lblDuration.TabIndex = 16;
      this.pbStatus.Location = new Point(93, 44);
      this.pbStatus.Name = "pbStatus";
      this.pbStatus.Size = new Size(18, 18);
      this.pbStatus.TabIndex = 17;
      this.pbStatus.TabStop = false;
      this.lblRevision.AutoSize = true;
      this.lblRevision.Location = new Point(315, 69);
      this.lblRevision.Name = "lblRevision";
      this.lblRevision.Size = new Size(54, 13);
      this.lblRevision.TabIndex = 18;
      this.lblRevision.Text = "Revision :";
      this.cbRevision.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbRevision.FormattingEnabled = true;
      this.cbRevision.Location = new Point(375, 66);
      this.cbRevision.Name = "cbRevision";
      this.cbRevision.Size = new Size(377, 21);
      this.cbRevision.TabIndex = 19;
      this.cbRevision.SelectedIndexChanged += new EventHandler(this.cbRevision_SelectedIndexChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.cbRevision);
      this.Controls.Add((Control) this.lblRevision);
      this.Controls.Add((Control) this.pbStatus);
      this.Controls.Add((Control) this.lblDuration);
      this.Controls.Add((Control) this.lblEnd);
      this.Controls.Add((Control) this.lblStart);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.cbPhase);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.lblPhases);
      this.Controls.Add((Control) this.lblStatus);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.tbFileName);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (ProjectControl);
      this.Size = new Size(755, 107);
      ((ISupportInitialize) this.pbStatus).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
