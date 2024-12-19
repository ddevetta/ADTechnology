// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.AboutForm
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
  public class AboutForm : Form
  {
    private IContainer components = (IContainer) null;
    private PictureBox pbADT;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label lblCompany;
    private Label lblBuild;
    private Label lblVersion;
    private Label lblProduct;
    private Label label5;
    private Label lblDescription;
    private Button btnClose;
    private Label label6;
    private LinkLabel llbEmail;
    private LinkLabel llbEvaluationForm;

    public AboutForm()
    {
      this.InitializeComponent();
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      this.lblVersion.Text = executingAssembly.GetName().Version.ToString();
      DateTime dateTime = new DateTime(2000, 1, 1);
      dateTime = dateTime.AddDays((double) executingAssembly.GetName().Version.Build);
      this.lblBuild.Text = dateTime.ToShortDateString();
      this.lblCompany.Text = ((AssemblyCompanyAttribute) executingAssembly.GetCustomAttributes(typeof (AssemblyCompanyAttribute), false)[0]).Company;
      this.lblProduct.Text = ((AssemblyProductAttribute) executingAssembly.GetCustomAttributes(typeof (AssemblyProductAttribute), false)[0]).Product;
      this.lblDescription.Text = ((AssemblyDescriptionAttribute) executingAssembly.GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false)[0]).Description;
      this.llbEmail.Text = ((AssemblyConfigurationAttribute) executingAssembly.GetCustomAttributes(typeof (AssemblyConfigurationAttribute), false)[0]).Configuration;
      this.llbEmail.Links.Add(0, this.llbEmail.Text.Length, (object) ("mailto:" + this.llbEmail.Text));
      this.Text = "About " + this.lblDescription.Text;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void llbEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(e.Link.LinkData.ToString());
    }

    private void llbEvaluationForm_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        Process.Start("ailog 1.0 evaluation.xlsx");
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, "Error accessing evaluation form:\r\n\r\n" + ex.Message, "Error");
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
      this.pbADT = new PictureBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.lblCompany = new Label();
      this.lblBuild = new Label();
      this.lblVersion = new Label();
      this.lblProduct = new Label();
      this.label5 = new Label();
      this.lblDescription = new Label();
      this.btnClose = new Button();
      this.label6 = new Label();
      this.llbEmail = new LinkLabel();
      this.llbEvaluationForm = new LinkLabel();
      ((ISupportInitialize) this.pbADT).BeginInit();
      this.SuspendLayout();
      this.pbADT.Dock = DockStyle.Top;
      this.pbADT.Location = new Point(0, 0);
      this.pbADT.Name = "pbADT";
      this.pbADT.Size = new Size(422, 84);
      this.pbADT.TabIndex = 0;
      this.pbADT.TabStop = false;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(78, 119);
      this.label1.Name = "label1";
      this.label1.Size = new Size(57, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Company :";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(85, 144);
      this.label2.Name = "label2";
      this.label2.Size = new Size(50, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Product :";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(87, 194);
      this.label3.Name = "label3";
      this.label3.Size = new Size(48, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Version :";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(73, 219);
      this.label4.Name = "label4";
      this.label4.Size = new Size(62, 13);
      this.label4.TabIndex = 4;
      this.label4.Text = "Build Date :";
      this.lblCompany.AutoSize = true;
      this.lblCompany.Location = new Point(141, 119);
      this.lblCompany.Name = "lblCompany";
      this.lblCompany.Size = new Size(13, 13);
      this.lblCompany.TabIndex = 5;
      this.lblCompany.Text = "?";
      this.lblBuild.AutoSize = true;
      this.lblBuild.Location = new Point(142, 219);
      this.lblBuild.Name = "lblBuild";
      this.lblBuild.Size = new Size(13, 13);
      this.lblBuild.TabIndex = 6;
      this.lblBuild.Text = "?";
      this.lblVersion.AutoSize = true;
      this.lblVersion.Location = new Point(142, 194);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new Size(13, 13);
      this.lblVersion.TabIndex = 7;
      this.lblVersion.Text = "?";
      this.lblProduct.AutoSize = true;
      this.lblProduct.Location = new Point(141, 144);
      this.lblProduct.Name = "lblProduct";
      this.lblProduct.Size = new Size(13, 13);
      this.lblProduct.TabIndex = 8;
      this.lblProduct.Text = "?";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(94, 169);
      this.label5.Name = "label5";
      this.label5.Size = new Size(41, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Name :";
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new Point(142, 169);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new Size(13, 13);
      this.lblDescription.TabIndex = 10;
      this.lblDescription.Text = "?";
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.DialogResult = DialogResult.Cancel;
      this.btnClose.Location = new Point(335, 302);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 11;
      this.btnClose.Text = "OK";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.label6.AutoSize = true;
      this.label6.Location = new Point(98, 244);
      this.label6.Name = "label6";
      this.label6.Size = new Size(37, 13);
      this.label6.TabIndex = 12;
      this.label6.Text = "email :";
      this.llbEmail.AutoSize = true;
      this.llbEmail.Location = new Point(142, 244);
      this.llbEmail.Name = "llbEmail";
      this.llbEmail.Size = new Size(13, 13);
      this.llbEmail.TabIndex = 13;
      this.llbEmail.TabStop = true;
      this.llbEmail.Text = "?";
      this.llbEmail.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llbEmail_LinkClicked);
      this.llbEvaluationForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.llbEvaluationForm.AutoSize = true;
      this.llbEvaluationForm.Location = new Point(52, 307);
      this.llbEvaluationForm.Name = "llbEvaluationForm";
      this.llbEvaluationForm.Size = new Size(83, 13);
      this.llbEvaluationForm.TabIndex = 14;
      this.llbEvaluationForm.TabStop = true;
      this.llbEvaluationForm.Text = "Evaluation Form";
      this.llbEvaluationForm.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llbEvaluationForm_LinkClicked);
      this.AcceptButton = (IButtonControl) this.btnClose;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnClose;
      this.ClientSize = new Size(422, 337);
      this.Controls.Add((Control) this.llbEvaluationForm);
      this.Controls.Add((Control) this.llbEmail);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.lblDescription);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.lblProduct);
      this.Controls.Add((Control) this.lblVersion);
      this.Controls.Add((Control) this.lblBuild);
      this.Controls.Add((Control) this.lblCompany);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.pbADT);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.HelpButton = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "About";
      ((ISupportInitialize) this.pbADT).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
