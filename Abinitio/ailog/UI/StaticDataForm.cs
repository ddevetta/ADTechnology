// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.StaticDataForm
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
  public class StaticDataForm : Form
  {
    private IContainer components = (IContainer) null;
    private RichTextBox rtbData;

    internal StaticDataForm(string data, string header)
    {
      this.InitializeComponent();
      this.Text = header;
      this.rtbData.Text = data;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StaticDataForm));
      this.rtbData = new RichTextBox();
      this.SuspendLayout();
      this.rtbData.AutoWordSelection = true;
      this.rtbData.BorderStyle = BorderStyle.None;
      this.rtbData.Dock = DockStyle.Fill;
      this.rtbData.Font = new Font("Lucida Console", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.rtbData.Location = new Point(0, 0);
      this.rtbData.Name = "rtbData";
      this.rtbData.ReadOnly = true;
      this.rtbData.Size = new Size(862, 346);
      this.rtbData.TabIndex = 0;
      this.rtbData.Text = "";
      this.rtbData.WordWrap = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(862, 346);
      this.Controls.Add((Control) this.rtbData);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (StaticDataForm);
      this.Text = "Static readonly";
      this.ResumeLayout(false);
    }
  }
}
