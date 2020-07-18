// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.Form1
// Assembly: SaveDataTableDialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e5c8ad4bf3538416
// MVID: 8E9AD6D9-9826-485B-8F80-3147D34B95DE
// Assembly location: C:\Users\DaveAndTina\OneDrive - Sky\Projects\ldap..tion_0000000000000000_0001.0000_91e2ccba3687cdbf\SaveDataTableDialog.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.Apps.SaveDataTable
{
  public class Form1 : Form
  {
    private SaveDataTableDialog sdtd;
    private IContainer components;
    private Button button1;
    private DataGridView dataGridView1;
    private Label label1;

    public Form1()
    {
      this.InitializeComponent();
      DataTable table = new DataTable("sampleTable");
      table.Columns.AddRange(new DataColumn[2]
      {
        new DataColumn("Code"),
        new DataColumn("Description")
      });
      table.Rows.Add((object) "A", (object) "description for A");
      table.Rows.Add((object) "B", (object) "description for B\r\nover 2 lines");
      table.Rows.Add((object) "C");
      this.dataGridView1.DataSource = (object) table;
      this.sdtd = new SaveDataTableDialog(table);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.sdtd.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      try
      {
        this.sdtd.OutputStream = this.sdtd.OpenFile();
        int num = this.sdtd.Convert();
        this.sdtd.OutputStream.Close();
        this.label1.Text = string.Format("{0} bytes written.", (object) num);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
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
      this.button1 = new Button();
      this.dataGridView1 = new DataGridView();
      this.label1 = new Label();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button1.Location = new Point(205, 169);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Save";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Dock = DockStyle.Top;
      this.dataGridView1.Location = new Point(0, 0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(292, 150);
      this.dataGridView1.TabIndex = 1;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 174);
      this.label1.Name = "label1";
      this.label1.Size = new Size(0, 13);
      this.label1.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 208);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.dataGridView1);
      this.Controls.Add((Control) this.button1);
      this.Name = nameof (Form1);
      this.Text = nameof (Form1);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
