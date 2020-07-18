// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.ExecutionParametersForm
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
  public class ExecutionParametersForm : Form
  {
    private IContainer components = (IContainer) null;
    private DataGridView dgMain;
    private DataGridViewTextBoxColumn Parameter;
    private DataGridViewTextBoxColumn ParameterValue;
    private ContextMenuStrip contextMenu;
    private ToolStripMenuItem copyToolStripMenuItem;

    internal ExecutionParametersForm(ExecutionParameters ep)
    {
      this.InitializeComponent();
      this.dgMain.DataSource = (object) ep;
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(((DataGridView) ((ContextMenuStrip) ((ToolStripItem) sender).Owner).SourceControl).SelectedCells[0].Value.ToString());
    }

    private void _CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right || e.ColumnIndex < 0 || e.RowIndex < 0)
        return;
      DataGridView dataGridView = (DataGridView) sender;
      dataGridView.CurrentCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExecutionParametersForm));
      this.dgMain = new DataGridView();
      this.Parameter = new DataGridViewTextBoxColumn();
      this.ParameterValue = new DataGridViewTextBoxColumn();
      this.contextMenu = new ContextMenuStrip(this.components);
      this.copyToolStripMenuItem = new ToolStripMenuItem();
      ((ISupportInitialize) this.dgMain).BeginInit();
      this.contextMenu.SuspendLayout();
      this.SuspendLayout();
      this.dgMain.AllowUserToAddRows = false;
      this.dgMain.AllowUserToDeleteRows = false;
      this.dgMain.AllowUserToOrderColumns = true;
      this.dgMain.AllowUserToResizeRows = false;
      this.dgMain.BorderStyle = BorderStyle.None;
      this.dgMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgMain.Columns.AddRange((DataGridViewColumn) this.Parameter, (DataGridViewColumn) this.ParameterValue);
      this.dgMain.ContextMenuStrip = this.contextMenu;
      gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle.BackColor = SystemColors.Window;
      gridViewCellStyle.Font = new Font("Lucida Console", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle.ForeColor = SystemColors.ControlText;
      gridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle.WrapMode = DataGridViewTriState.False;
      this.dgMain.DefaultCellStyle = gridViewCellStyle;
      this.dgMain.Dock = DockStyle.Fill;
      this.dgMain.Location = new Point(0, 0);
      this.dgMain.MultiSelect = false;
      this.dgMain.Name = "dgMain";
      this.dgMain.ReadOnly = true;
      this.dgMain.RowHeadersVisible = false;
      this.dgMain.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.dgMain.Size = new Size(942, 544);
      this.dgMain.TabIndex = 0;
      this.dgMain.CellMouseDown += new DataGridViewCellMouseEventHandler(this._CellMouseDown);
      this.Parameter.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
      this.Parameter.DataPropertyName = "Parameter";
      this.Parameter.HeaderText = "Parameter";
      this.Parameter.Name = "Parameter";
      this.Parameter.ReadOnly = true;
      this.Parameter.Width = 360;
      this.ParameterValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.ParameterValue.DataPropertyName = "Value";
      this.ParameterValue.HeaderText = "Value";
      this.ParameterValue.Name = "ParameterValue";
      this.ParameterValue.ReadOnly = true;
      this.ParameterValue.Width = 59;
      this.contextMenu.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.copyToolStripMenuItem
      });
      this.contextMenu.Name = "contextMenu";
      this.contextMenu.Size = new Size(111, 26);
      this.copyToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("copyToolStripMenuItem.Image");
      this.copyToolStripMenuItem.ImageTransparentColor = Color.White;
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new Size(110, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      this.copyToolStripMenuItem.Click += new EventHandler(this.copyToolStripMenuItem_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(942, 544);
      this.Controls.Add((Control) this.dgMain);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (ExecutionParametersForm);
      this.Text = "Execution Parameters";
      ((ISupportInitialize) this.dgMain).EndInit();
      this.contextMenu.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
