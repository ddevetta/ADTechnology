// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.PhaseControl
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
  public class PhaseControl : UserControl
  {
    private int revision = -1;
    private bool isVertexSorted = false;
    private bool isFlowSorted = false;
    private IContainer components = (IContainer) null;
    private Phase ph;
    private PropertyDescriptor sortVertexProp;
    private ListSortDirection sortVertexDir;
    private PropertyDescriptor sortFlowProp;
    private ListSortDirection sortFlowDir;
    private SplitContainer splitContainer1;
    private TabControl tabControl1;
    private TabPage tabVertexes;
    private TabPage tabFlows;
    private DataGridView dgVertexes;
    private DataGridView dgFlows;
    private TabControl tabControl2;
    private TabPage tabPage1;
    private DataGridView dgPorts;
    private ContextMenuStrip contextMenu;
    private ToolStripMenuItem copyToolStripMenuItem;
    private DataGridViewTextBoxColumn PortName;
    private DataGridViewTextBoxColumn Records;
    private DataGridViewTextBoxColumn Bytes;
    private DataGridViewTextBoxColumn PortFlowName;
    private DataGridViewTextBoxColumn PortVertexName;
    private ToolStripMenuItem addToPlotterToolStripMenuItem;
    private DataGridViewTextBoxColumn Status;
    private DataGridViewTextBoxColumn VertexName;
    private DataGridViewTextBoxColumn CPU;
    private DataGridViewTextBoxColumn Skew;
    private DataGridViewTextBoxColumn VertexStart;
    private DataGridViewTextBoxColumn VertexEnd;
    private DataGridViewTextBoxColumn Duration;
    private DataGridViewTextBoxColumn colOrder;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn FlowName;
    private DataGridViewTextBoxColumn Order;

    internal Phase Phase
    {
      get
      {
        return this.ph;
      }
      set
      {
        this.ph = value;
        if (this.revision <= -1)
          return;
        this.linkPhase();
      }
    }

    internal int Revision
    {
      get
      {
        return this.revision;
      }
      set
      {
        this.revision = value;
        if (this.ph == null)
          return;
        this.linkPhase();
      }
    }

    public event AddToPlotterEventHandler AddToPlotterSelected;

    protected virtual void OnAddToPlotterSelected(AddToPlotterEventArgs e)
    {
      if (this.AddToPlotterSelected == null)
        return;
      this.AddToPlotterSelected((object) this, e);
    }

    public PhaseControl()
    {
      this.InitializeComponent();
      this.Dock = DockStyle.Fill;
      this.dgVertexes.AutoGenerateColumns = this.dgFlows.AutoGenerateColumns = this.dgPorts.AutoGenerateColumns = false;
    }

    private void linkPhase()
    {
      int index1 = 0;
      int index2 = 0;
      if (this.dgVertexes.SelectedCells.Count > 0)
      {
        index1 = this.dgVertexes.SelectedCells[0].ColumnIndex;
        index2 = this.dgVertexes.SelectedCells[0].RowIndex;
      }
      this.dgVertexes.DataSource = (object) this.ph.PhaseRevisions[this.revision].Vertexes;
      if (index1 >= this.dgVertexes.Columns.Count)
        index1 = 0;
      if (index2 >= this.dgVertexes.Rows.Count)
        index2 = 0;
      if (index2 < this.dgVertexes.Rows.Count)
        this.dgVertexes[index1, index2].Selected = true;
      if (this.isVertexSorted)
        this.ph.PhaseRevisions[this.revision].Vertexes.Sort(this.sortVertexProp, this.sortVertexDir);
      else
        this.ph.PhaseRevisions[this.revision].Vertexes.Sort("Order");
      this.ph.PhaseRevisions[this.revision].Vertexes.ListChanged += new ListChangedEventHandler(this.Vertexes_ListChanged);
      int index3 = 0;
      int index4 = 0;
      if (this.dgFlows.SelectedCells.Count > 0)
      {
        index3 = this.dgFlows.SelectedCells[0].ColumnIndex;
        index4 = this.dgFlows.SelectedCells[0].RowIndex;
      }
      this.dgFlows.DataSource = (object) this.ph.PhaseRevisions[this.revision].Flows;
      if (this.dgFlows.Rows.Count > 0)
      {
        if (index3 >= this.dgFlows.Columns.Count)
          index3 = 0;
        if (index4 >= this.dgFlows.Rows.Count)
          index4 = 0;
        this.dgFlows[index3, index4].Selected = true;
      }
      if (this.isFlowSorted)
        this.ph.PhaseRevisions[this.revision].Flows.Sort(this.sortFlowProp, this.sortFlowDir);
      else
        this.ph.PhaseRevisions[this.revision].Flows.Sort("Order");
      this.ph.PhaseRevisions[this.revision].Flows.ListChanged += new ListChangedEventHandler(this.Flows_ListChanged);
      this.tabControl1_Selected((object) this, new TabControlEventArgs((TabPage) null, 0, TabControlAction.Selected));
    }

    private void Vertexes_ListChanged(object sender, ListChangedEventArgs e)
    {
      this.sortVertexDir = this.ph.PhaseRevisions[this.revision].Vertexes.SortDirection;
      this.sortVertexProp = this.ph.PhaseRevisions[this.revision].Vertexes.SortProperty;
      this.isVertexSorted = this.ph.PhaseRevisions[this.revision].Vertexes.IsSorted;
    }

    private void Flows_ListChanged(object sender, ListChangedEventArgs e)
    {
      this.sortFlowDir = this.ph.PhaseRevisions[this.revision].Flows.SortDirection;
      this.sortFlowProp = this.ph.PhaseRevisions[this.revision].Flows.SortProperty;
      this.isFlowSorted = this.ph.PhaseRevisions[this.revision].Flows.IsSorted;
    }

    private void dgVertexes_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgVertexes.SelectedCells.Count > 0)
        this.dgPorts.DataSource = (object) ((Vertex) this.dgVertexes.SelectedCells[0].OwningRow.DataBoundItem).Ports;
      this.dgPorts.CurrentCell = (DataGridViewCell) null;
    }

    private void dgFlows_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgFlows.SelectedCells.Count > 0)
        this.dgPorts.DataSource = (object) ((Flow) this.dgFlows.SelectedCells[0].OwningRow.DataBoundItem).Ports;
      this.dgPorts.CurrentCell = (DataGridViewCell) null;
    }

    private void tabControl1_Selected(object sender, TabControlEventArgs e)
    {
      switch (this.tabControl1.SelectedTab.Name)
      {
        case "tabVertexes":
          this.dgPorts.Columns["PortVertexName"].Visible = false;
          this.dgPorts.Columns["PortFlowName"].Visible = true;
          this.dgVertexes_SelectionChanged(sender, (EventArgs) e);
          this.dgVertexes.Select();
          break;
        case "tabFlows":
          this.dgPorts.Columns["PortVertexName"].Visible = true;
          this.dgPorts.Columns["PortFlowName"].Visible = false;
          this.dgFlows_SelectionChanged(sender, (EventArgs) e);
          this.dgFlows.Select();
          break;
      }
      this.dgPorts.CurrentCell = (DataGridViewCell) null;
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Clipboard.SetText((string) ((DataGridView) ((ContextMenuStrip) ((ToolStripItem) sender).Owner).SourceControl).SelectedCells[0].Value);
    }

    private void _CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right || e.ColumnIndex < 0 || e.RowIndex < 0)
        return;
      DataGridView dataGridView = (DataGridView) sender;
      dataGridView.CurrentCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
    }

    private void dgPorts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex < 0 || e.RowIndex < 0)
        return;
      DataGridView dataGridView = (DataGridView) sender;
      switch (dataGridView.Columns[e.ColumnIndex].Name)
      {
        case "PortFlowName":
          this.findFlow(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
          break;
        case "PortVertexName":
          this.findVertex(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
          break;
      }
    }

    private void findVertex(string p)
    {
      for (int index = 0; index < this.dgVertexes.Rows.Count; ++index)
      {
        if (this.dgVertexes.Rows[index].Cells["VertexName"].Value.ToString() == p)
        {
          this.tabControl1.SelectedIndex = 0;
          this.dgVertexes.CurrentCell = this.dgVertexes.Rows[index].Cells["VertexName"];
          break;
        }
      }
    }

    private void findFlow(string p)
    {
      for (int index = 0; index < this.dgFlows.Rows.Count; ++index)
      {
        if (this.dgFlows.Rows[index].Cells["FlowName"].Value.ToString() == p)
        {
          this.tabControl1.SelectedIndex = 1;
          this.dgFlows.CurrentCell = this.dgFlows.Rows[index].Cells["FlowName"];
          break;
        }
      }
    }

    private void dgVertexes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.ColumnIndex != this.dgVertexes.Columns["VertexName"].Index || e.Value == null)
        return;
      DataGridViewCell cell = this.dgVertexes.Rows[e.RowIndex].Cells[e.ColumnIndex];
      cell.ToolTipText = ((Vertex) cell.OwningRow.DataBoundItem).VertexName.ToString();
    }

    private void dgFlows_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.ColumnIndex != this.dgFlows.Columns["FlowName"].Index || e.Value == null)
        return;
      DataGridViewCell cell = this.dgFlows.Rows[e.RowIndex].Cells[e.ColumnIndex];
      cell.ToolTipText = ((Flow) cell.OwningRow.DataBoundItem).FlowName.ToString();
    }

    private void addToPlotterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        DataGridViewCell selectedCell = ((DataGridView) ((ContextMenuStrip) ((ToolStripItem) sender).Owner).SourceControl).SelectedCells[0];
        this.OnAddToPlotterSelected(new AddToPlotterEventArgs(selectedCell.OwningRow.DataBoundItem.GetType(), selectedCell.OwningRow.DataBoundItem, selectedCell.OwningColumn.Name));
      }
      catch
      {
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
      this.components = (IContainer) new Container();
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PhaseControl));
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle8 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle9 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle10 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle11 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle12 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle13 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle14 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle15 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle16 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle17 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle18 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle19 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle20 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle21 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle22 = new DataGridViewCellStyle();
      this.splitContainer1 = new SplitContainer();
      this.tabControl1 = new TabControl();
      this.tabVertexes = new TabPage();
      this.dgVertexes = new DataGridView();
      this.contextMenu = new ContextMenuStrip(this.components);
      this.copyToolStripMenuItem = new ToolStripMenuItem();
      this.addToPlotterToolStripMenuItem = new ToolStripMenuItem();
      this.tabFlows = new TabPage();
      this.dgFlows = new DataGridView();
      this.tabControl2 = new TabControl();
      this.tabPage1 = new TabPage();
      this.dgPorts = new DataGridView();
      this.PortName = new DataGridViewTextBoxColumn();
      this.Records = new DataGridViewTextBoxColumn();
      this.Bytes = new DataGridViewTextBoxColumn();
      this.PortFlowName = new DataGridViewTextBoxColumn();
      this.PortVertexName = new DataGridViewTextBoxColumn();
      this.Status = new DataGridViewTextBoxColumn();
      this.VertexName = new DataGridViewTextBoxColumn();
      this.CPU = new DataGridViewTextBoxColumn();
      this.Skew = new DataGridViewTextBoxColumn();
      this.VertexStart = new DataGridViewTextBoxColumn();
      this.VertexEnd = new DataGridViewTextBoxColumn();
      this.Duration = new DataGridViewTextBoxColumn();
      this.colOrder = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.FlowName = new DataGridViewTextBoxColumn();
      this.Order = new DataGridViewTextBoxColumn();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabVertexes.SuspendLayout();
      ((ISupportInitialize) this.dgVertexes).BeginInit();
      this.contextMenu.SuspendLayout();
      this.tabFlows.SuspendLayout();
      ((ISupportInitialize) this.dgFlows).BeginInit();
      this.tabControl2.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((ISupportInitialize) this.dgPorts).BeginInit();
      this.SuspendLayout();
      this.splitContainer1.Dock = DockStyle.Fill;
      this.splitContainer1.Location = new Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1.Controls.Add((Control) this.tabControl1);
      this.splitContainer1.Panel2.Controls.Add((Control) this.tabControl2);
      this.splitContainer1.Size = new Size(737, 397);
      this.splitContainer1.SplitterDistance = 442;
      this.splitContainer1.TabIndex = 0;
      this.tabControl1.Controls.Add((Control) this.tabVertexes);
      this.tabControl1.Controls.Add((Control) this.tabFlows);
      this.tabControl1.Dock = DockStyle.Fill;
      this.tabControl1.HotTrack = true;
      this.tabControl1.Location = new Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(442, 397);
      this.tabControl1.TabIndex = 0;
      this.tabControl1.Selected += new TabControlEventHandler(this.tabControl1_Selected);
      this.tabVertexes.Controls.Add((Control) this.dgVertexes);
      this.tabVertexes.Location = new Point(4, 22);
      this.tabVertexes.Name = "tabVertexes";
      this.tabVertexes.Padding = new Padding(3);
      this.tabVertexes.Size = new Size(434, 371);
      this.tabVertexes.TabIndex = 0;
      this.tabVertexes.Text = "Vertexes";
      this.tabVertexes.UseVisualStyleBackColor = true;
      this.dgVertexes.AllowUserToAddRows = false;
      this.dgVertexes.AllowUserToDeleteRows = false;
      this.dgVertexes.AllowUserToOrderColumns = true;
      this.dgVertexes.AllowUserToResizeRows = false;
      this.dgVertexes.BorderStyle = BorderStyle.None;
      gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle1.BackColor = SystemColors.Control;
      gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle1.ForeColor = SystemColors.WindowText;
      gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
      this.dgVertexes.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
      this.dgVertexes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgVertexes.Columns.AddRange((DataGridViewColumn) this.Status, (DataGridViewColumn) this.VertexName, (DataGridViewColumn) this.CPU, (DataGridViewColumn) this.Skew, (DataGridViewColumn) this.VertexStart, (DataGridViewColumn) this.VertexEnd, (DataGridViewColumn) this.Duration, (DataGridViewColumn) this.colOrder);
      this.dgVertexes.ContextMenuStrip = this.contextMenu;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Window;
      gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = SystemColors.ControlText;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
      this.dgVertexes.DefaultCellStyle = gridViewCellStyle2;
      this.dgVertexes.Dock = DockStyle.Fill;
      this.dgVertexes.Location = new Point(3, 3);
      this.dgVertexes.MultiSelect = false;
      this.dgVertexes.Name = "dgVertexes";
      this.dgVertexes.ReadOnly = true;
      this.dgVertexes.RowHeadersVisible = false;
      this.dgVertexes.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.dgVertexes.Size = new Size(428, 365);
      this.dgVertexes.TabIndex = 0;
      this.dgVertexes.CellMouseDown += new DataGridViewCellMouseEventHandler(this._CellMouseDown);
      this.dgVertexes.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgVertexes_CellFormatting);
      this.dgVertexes.SelectionChanged += new EventHandler(this.dgVertexes_SelectionChanged);
      this.contextMenu.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.copyToolStripMenuItem,
        (ToolStripItem) this.addToPlotterToolStripMenuItem
      });
      this.contextMenu.Name = "contextMenu";
      this.contextMenu.Size = new Size(153, 48);
      this.copyToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("copyToolStripMenuItem.Image");
      this.copyToolStripMenuItem.ImageTransparentColor = Color.White;
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new Size(152, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      this.copyToolStripMenuItem.Click += new EventHandler(this.copyToolStripMenuItem_Click);
      this.addToPlotterToolStripMenuItem.Name = "addToPlotterToolStripMenuItem";
      this.addToPlotterToolStripMenuItem.Size = new Size(152, 22);
      this.addToPlotterToolStripMenuItem.Text = "Add to Plotter";
      this.addToPlotterToolStripMenuItem.Click += new EventHandler(this.addToPlotterToolStripMenuItem_Click);
      this.tabFlows.Controls.Add((Control) this.dgFlows);
      this.tabFlows.Location = new Point(4, 22);
      this.tabFlows.Name = "tabFlows";
      this.tabFlows.Padding = new Padding(3);
      this.tabFlows.Size = new Size(434, 371);
      this.tabFlows.TabIndex = 1;
      this.tabFlows.Text = "Flows";
      this.tabFlows.UseVisualStyleBackColor = true;
      this.dgFlows.AllowUserToAddRows = false;
      this.dgFlows.AllowUserToDeleteRows = false;
      this.dgFlows.AllowUserToOrderColumns = true;
      this.dgFlows.AllowUserToResizeRows = false;
      this.dgFlows.BorderStyle = BorderStyle.None;
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle3.BackColor = SystemColors.Control;
      gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle3.ForeColor = SystemColors.WindowText;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.dgFlows.ColumnHeadersDefaultCellStyle = gridViewCellStyle3;
      this.dgFlows.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgFlows.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn2, (DataGridViewColumn) this.FlowName, (DataGridViewColumn) this.Order);
      this.dgFlows.ContextMenuStrip = this.contextMenu;
      gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle4.BackColor = SystemColors.Window;
      gridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle4.ForeColor = SystemColors.ControlText;
      gridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle4.WrapMode = DataGridViewTriState.False;
      this.dgFlows.DefaultCellStyle = gridViewCellStyle4;
      this.dgFlows.Dock = DockStyle.Fill;
      this.dgFlows.Location = new Point(3, 3);
      this.dgFlows.MultiSelect = false;
      this.dgFlows.Name = "dgFlows";
      this.dgFlows.ReadOnly = true;
      gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle5.BackColor = SystemColors.Control;
      gridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle5.ForeColor = SystemColors.WindowText;
      gridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle5.WrapMode = DataGridViewTriState.True;
      this.dgFlows.RowHeadersDefaultCellStyle = gridViewCellStyle5;
      this.dgFlows.RowHeadersVisible = false;
      this.dgFlows.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.dgFlows.Size = new Size(428, 365);
      this.dgFlows.TabIndex = 0;
      this.dgFlows.CellMouseDown += new DataGridViewCellMouseEventHandler(this._CellMouseDown);
      this.dgFlows.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgFlows_CellFormatting);
      this.dgFlows.SelectionChanged += new EventHandler(this.dgFlows_SelectionChanged);
      this.tabControl2.Controls.Add((Control) this.tabPage1);
      this.tabControl2.Dock = DockStyle.Fill;
      this.tabControl2.Location = new Point(0, 0);
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new Size(291, 397);
      this.tabControl2.TabIndex = 0;
      this.tabPage1.Controls.Add((Control) this.dgPorts);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(283, 371);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Ports";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.dgPorts.AllowUserToAddRows = false;
      this.dgPorts.AllowUserToDeleteRows = false;
      this.dgPorts.AllowUserToOrderColumns = true;
      this.dgPorts.AllowUserToResizeRows = false;
      this.dgPorts.BorderStyle = BorderStyle.None;
      gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle6.BackColor = SystemColors.Control;
      gridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle6.ForeColor = SystemColors.WindowText;
      gridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle6.WrapMode = DataGridViewTriState.True;
      this.dgPorts.ColumnHeadersDefaultCellStyle = gridViewCellStyle6;
      this.dgPorts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgPorts.Columns.AddRange((DataGridViewColumn) this.PortName, (DataGridViewColumn) this.Records, (DataGridViewColumn) this.Bytes, (DataGridViewColumn) this.PortFlowName, (DataGridViewColumn) this.PortVertexName);
      this.dgPorts.ContextMenuStrip = this.contextMenu;
      gridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle7.BackColor = SystemColors.Window;
      gridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle7.ForeColor = SystemColors.ControlText;
      gridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle7.WrapMode = DataGridViewTriState.False;
      this.dgPorts.DefaultCellStyle = gridViewCellStyle7;
      this.dgPorts.Dock = DockStyle.Fill;
      this.dgPorts.Location = new Point(3, 3);
      this.dgPorts.MultiSelect = false;
      this.dgPorts.Name = "dgPorts";
      this.dgPorts.ReadOnly = true;
      gridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle8.BackColor = SystemColors.Control;
      gridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle8.ForeColor = SystemColors.WindowText;
      gridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle8.WrapMode = DataGridViewTriState.True;
      this.dgPorts.RowHeadersDefaultCellStyle = gridViewCellStyle8;
      this.dgPorts.RowHeadersVisible = false;
      this.dgPorts.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.dgPorts.Size = new Size(277, 365);
      this.dgPorts.TabIndex = 0;
      this.dgPorts.CellDoubleClick += new DataGridViewCellEventHandler(this.dgPorts_CellDoubleClick);
      this.dgPorts.CellMouseDown += new DataGridViewCellMouseEventHandler(this._CellMouseDown);
      this.PortName.DataPropertyName = "Name";
      gridViewCellStyle9.Padding = new Padding(3, 0, 0, 0);
      this.PortName.DefaultCellStyle = gridViewCellStyle9;
      this.PortName.HeaderText = "Port";
      this.PortName.Name = "PortName";
      this.PortName.ReadOnly = true;
      this.PortName.Width = 50;
      this.Records.DataPropertyName = "Records";
      gridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight;
      gridViewCellStyle10.Format = "N0";
      gridViewCellStyle10.NullValue = (object) null;
      this.Records.DefaultCellStyle = gridViewCellStyle10;
      this.Records.HeaderText = "Records";
      this.Records.Name = "Records";
      this.Records.ReadOnly = true;
      this.Records.Width = 80;
      this.Bytes.DataPropertyName = "Bytes";
      gridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight;
      gridViewCellStyle11.Format = "N0";
      gridViewCellStyle11.NullValue = (object) null;
      this.Bytes.DefaultCellStyle = gridViewCellStyle11;
      this.Bytes.HeaderText = "Bytes";
      this.Bytes.Name = "Bytes";
      this.Bytes.ReadOnly = true;
      this.Bytes.Width = 90;
      this.PortFlowName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.PortFlowName.DataPropertyName = "FlowName";
      gridViewCellStyle12.Padding = new Padding(3, 0, 0, 0);
      this.PortFlowName.DefaultCellStyle = gridViewCellStyle12;
      this.PortFlowName.HeaderText = "Connecting Flow";
      this.PortFlowName.Name = "PortFlowName";
      this.PortFlowName.ReadOnly = true;
      this.PortFlowName.Width = 111;
      this.PortVertexName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.PortVertexName.DataPropertyName = "VertexName";
      gridViewCellStyle13.Padding = new Padding(3, 0, 0, 0);
      this.PortVertexName.DefaultCellStyle = gridViewCellStyle13;
      this.PortVertexName.HeaderText = "Connecting Vertex";
      this.PortVertexName.Name = "PortVertexName";
      this.PortVertexName.ReadOnly = true;
      this.PortVertexName.Width = 119;
      this.Status.DataPropertyName = "Status";
      gridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.Status.DefaultCellStyle = gridViewCellStyle14;
      this.Status.HeaderText = "Status";
      this.Status.Name = "Status";
      this.Status.ReadOnly = true;
      this.Status.Width = 50;
      this.VertexName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.VertexName.DataPropertyName = "Name";
      gridViewCellStyle15.Padding = new Padding(3, 0, 0, 0);
      this.VertexName.DefaultCellStyle = gridViewCellStyle15;
      this.VertexName.HeaderText = "Vertex Name";
      this.VertexName.Name = "VertexName";
      this.VertexName.ReadOnly = true;
      this.VertexName.Width = 93;
      this.CPU.DataPropertyName = "CPU";
      gridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleRight;
      gridViewCellStyle16.Format = "N3";
      gridViewCellStyle16.NullValue = (object) null;
      this.CPU.DefaultCellStyle = gridViewCellStyle16;
      this.CPU.HeaderText = "CPU Time";
      this.CPU.Name = "CPU";
      this.CPU.ReadOnly = true;
      this.CPU.Width = 70;
      this.Skew.DataPropertyName = "Skew";
      gridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle17.Format = "##0\"%\"";
      gridViewCellStyle17.NullValue = (object) null;
      this.Skew.DefaultCellStyle = gridViewCellStyle17;
      this.Skew.HeaderText = "Skew";
      this.Skew.Name = "Skew";
      this.Skew.ReadOnly = true;
      this.Skew.Width = 40;
      this.VertexStart.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.VertexStart.DataPropertyName = "StartTime";
      gridViewCellStyle18.Format = "T";
      gridViewCellStyle18.NullValue = (object) null;
      this.VertexStart.DefaultCellStyle = gridViewCellStyle18;
      this.VertexStart.HeaderText = "Start";
      this.VertexStart.Name = "VertexStart";
      this.VertexStart.ReadOnly = true;
      this.VertexStart.Width = 54;
      this.VertexEnd.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.VertexEnd.DataPropertyName = "EndTime";
      gridViewCellStyle19.Format = "T";
      this.VertexEnd.DefaultCellStyle = gridViewCellStyle19;
      this.VertexEnd.HeaderText = "End";
      this.VertexEnd.Name = "VertexEnd";
      this.VertexEnd.ReadOnly = true;
      this.VertexEnd.Width = 51;
      this.Duration.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.Duration.DataPropertyName = "Duration";
      this.Duration.HeaderText = "Duration";
      this.Duration.Name = "Duration";
      this.Duration.ReadOnly = true;
      this.Duration.Width = 72;
      this.colOrder.DataPropertyName = "Order";
      this.colOrder.HeaderText = "Order";
      this.colOrder.Name = "colOrder";
      this.colOrder.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.DataPropertyName = "Status";
      gridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.dataGridViewTextBoxColumn1.DefaultCellStyle = gridViewCellStyle20;
      this.dataGridViewTextBoxColumn1.HeaderText = "Status";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Width = 80;
      this.dataGridViewTextBoxColumn2.DataPropertyName = "Skew";
      gridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle21.Format = "##0\"%\"";
      this.dataGridViewTextBoxColumn2.DefaultCellStyle = gridViewCellStyle21;
      this.dataGridViewTextBoxColumn2.HeaderText = "Skew";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 40;
      this.FlowName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.FlowName.DataPropertyName = "Name";
      gridViewCellStyle22.Padding = new Padding(3, 0, 0, 0);
      this.FlowName.DefaultCellStyle = gridViewCellStyle22;
      this.FlowName.HeaderText = "Flow Name";
      this.FlowName.Name = "FlowName";
      this.FlowName.ReadOnly = true;
      this.FlowName.Width = 85;
      this.Order.DataPropertyName = "Order";
      this.Order.HeaderText = "Order";
      this.Order.Name = "Order";
      this.Order.ReadOnly = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.splitContainer1);
      this.Name = nameof (PhaseControl);
      this.Size = new Size(737, 397);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabVertexes.ResumeLayout(false);
      ((ISupportInitialize) this.dgVertexes).EndInit();
      this.contextMenu.ResumeLayout(false);
      this.tabFlows.ResumeLayout(false);
      ((ISupportInitialize) this.dgFlows).EndInit();
      this.tabControl2.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      ((ISupportInitialize) this.dgPorts).EndInit();
      this.ResumeLayout(false);
    }
  }
}
