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

        private DataSet ds;

        public Form1()
        {
            this.InitializeComponent();
            DataTable table = new DataTable("sampleTable");
            table.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("Code"),
                new DataColumn("Description"),
                new DataColumn("Count", typeof(Decimal))
            });
            table.Rows.Add((object)"A", (object)"description for A", 12D);
            table.Rows.Add((object)"B", (object)"description for B\r\nover 2 lines", null);
            table.Rows.Add((object)"C", DBNull.Value, 1234567);
            table.ExtendedProperties.Add("heading", "Very little Table");
            table.ExtendedProperties.Add("subheading", "A little subheading");
            this.dataGridView1.DataSource = (object)table;
            ds = new DataSet("dataSet");
            ds.Tables.Add(table);

            this.sdtd = new SaveDataTableDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = "";
            Application.DoEvents();

            if (this.sdtd.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            long saved = 0;

            try
            {
                saved = this.sdtd.Save(ds);
                this.label1.Text = saved.ToString("n0") + " bytes written.";
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(286, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(373, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 206);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Test SaveDataTableDialog";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
