using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
    public partial class PhaseControl
    {
        private IContainer components = (IContainer)null;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhaseControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabVertexes = new System.Windows.Forms.TabPage();
            this.dgVertexes = new System.Windows.Forms.DataGridView();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VertexName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Skew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VertexStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VertexEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToPlotterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabFlows = new System.Windows.Forms.TabPage();
            this.dgFlows = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgPorts = new System.Windows.Forms.DataGridView();
            this.PortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Records = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PortFlowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PortVertexName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabVertexes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVertexes)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.tabFlows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFlows)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPorts)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(746, 409);
            this.splitContainer1.SplitterDistance = 447;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabVertexes);
            this.tabControl1.Controls.Add(this.tabFlows);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 409);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabVertexes
            // 
            this.tabVertexes.Controls.Add(this.dgVertexes);
            this.tabVertexes.Location = new System.Drawing.Point(4, 22);
            this.tabVertexes.Name = "tabVertexes";
            this.tabVertexes.Padding = new System.Windows.Forms.Padding(3);
            this.tabVertexes.Size = new System.Drawing.Size(439, 383);
            this.tabVertexes.TabIndex = 0;
            this.tabVertexes.Text = "Vertexes";
            this.tabVertexes.UseVisualStyleBackColor = true;
            // 
            // dgVertexes
            // 
            this.dgVertexes.AllowUserToAddRows = false;
            this.dgVertexes.AllowUserToDeleteRows = false;
            this.dgVertexes.AllowUserToOrderColumns = true;
            this.dgVertexes.AllowUserToResizeRows = false;
            this.dgVertexes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgVertexes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgVertexes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgVertexes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.VertexName,
            this.CPU,
            this.Skew,
            this.VertexStart,
            this.VertexEnd,
            this.Duration,
            this.colOrder});
            this.dgVertexes.ContextMenuStrip = this.contextMenu;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgVertexes.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgVertexes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVertexes.Location = new System.Drawing.Point(3, 3);
            this.dgVertexes.MultiSelect = false;
            this.dgVertexes.Name = "dgVertexes";
            this.dgVertexes.ReadOnly = true;
            this.dgVertexes.RowHeadersVisible = false;
            this.dgVertexes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgVertexes.Size = new System.Drawing.Size(433, 377);
            this.dgVertexes.TabIndex = 0;
            this.dgVertexes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgVertexes_CellFormatting);
            this.dgVertexes.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._CellMouseDown);
            this.dgVertexes.SelectionChanged += new System.EventHandler(this.dgVertexes_SelectionChanged);
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle2;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 50;
            // 
            // VertexName
            // 
            this.VertexName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VertexName.DataPropertyName = "Name";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.VertexName.DefaultCellStyle = dataGridViewCellStyle3;
            this.VertexName.HeaderText = "Vertex Name";
            this.VertexName.Name = "VertexName";
            this.VertexName.ReadOnly = true;
            this.VertexName.Width = 93;
            // 
            // CPU
            // 
            this.CPU.DataPropertyName = "CPU";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N3";
            dataGridViewCellStyle4.NullValue = null;
            this.CPU.DefaultCellStyle = dataGridViewCellStyle4;
            this.CPU.HeaderText = "CPU Time";
            this.CPU.Name = "CPU";
            this.CPU.ReadOnly = true;
            this.CPU.Width = 70;
            // 
            // Skew
            // 
            this.Skew.DataPropertyName = "Skew";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "##0\"%\"";
            dataGridViewCellStyle5.NullValue = null;
            this.Skew.DefaultCellStyle = dataGridViewCellStyle5;
            this.Skew.HeaderText = "Skew";
            this.Skew.Name = "Skew";
            this.Skew.ReadOnly = true;
            this.Skew.Width = 40;
            // 
            // VertexStart
            // 
            this.VertexStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VertexStart.DataPropertyName = "StartTime";
            dataGridViewCellStyle6.Format = "T";
            dataGridViewCellStyle6.NullValue = null;
            this.VertexStart.DefaultCellStyle = dataGridViewCellStyle6;
            this.VertexStart.HeaderText = "Start";
            this.VertexStart.Name = "VertexStart";
            this.VertexStart.ReadOnly = true;
            this.VertexStart.Width = 54;
            // 
            // VertexEnd
            // 
            this.VertexEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VertexEnd.DataPropertyName = "EndTime";
            dataGridViewCellStyle7.Format = "T";
            this.VertexEnd.DefaultCellStyle = dataGridViewCellStyle7;
            this.VertexEnd.HeaderText = "End";
            this.VertexEnd.Name = "VertexEnd";
            this.VertexEnd.ReadOnly = true;
            this.VertexEnd.Width = 51;
            // 
            // Duration
            // 
            this.Duration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Duration.DataPropertyName = "Duration";
            this.Duration.HeaderText = "Duration";
            this.Duration.Name = "Duration";
            this.Duration.ReadOnly = true;
            this.Duration.Width = 72;
            // 
            // colOrder
            // 
            this.colOrder.DataPropertyName = "Order";
            this.colOrder.HeaderText = "Order";
            this.colOrder.Name = "colOrder";
            this.colOrder.ReadOnly = true;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.addToPlotterToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(149, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // addToPlotterToolStripMenuItem
            // 
            this.addToPlotterToolStripMenuItem.Name = "addToPlotterToolStripMenuItem";
            this.addToPlotterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.addToPlotterToolStripMenuItem.Text = "Add to Plotter";
            this.addToPlotterToolStripMenuItem.Click += new System.EventHandler(this.addToPlotterToolStripMenuItem_Click);
            // 
            // tabFlows
            // 
            this.tabFlows.Controls.Add(this.dgFlows);
            this.tabFlows.Location = new System.Drawing.Point(4, 22);
            this.tabFlows.Name = "tabFlows";
            this.tabFlows.Padding = new System.Windows.Forms.Padding(3);
            this.tabFlows.Size = new System.Drawing.Size(434, 371);
            this.tabFlows.TabIndex = 1;
            this.tabFlows.Text = "Flows";
            this.tabFlows.UseVisualStyleBackColor = true;
            // 
            // dgFlows
            // 
            this.dgFlows.AllowUserToAddRows = false;
            this.dgFlows.AllowUserToDeleteRows = false;
            this.dgFlows.AllowUserToOrderColumns = true;
            this.dgFlows.AllowUserToResizeRows = false;
            this.dgFlows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFlows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgFlows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgFlows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.FlowName,
            this.Order});
            this.dgFlows.ContextMenuStrip = this.contextMenu;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgFlows.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgFlows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFlows.Location = new System.Drawing.Point(3, 3);
            this.dgFlows.MultiSelect = false;
            this.dgFlows.Name = "dgFlows";
            this.dgFlows.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFlows.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgFlows.RowHeadersVisible = false;
            this.dgFlows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgFlows.Size = new System.Drawing.Size(428, 365);
            this.dgFlows.TabIndex = 0;
            this.dgFlows.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgFlows_CellFormatting);
            this.dgFlows.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._CellMouseDown);
            this.dgFlows.SelectionChanged += new System.EventHandler(this.dgFlows_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Status";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn1.HeaderText = "Status";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Skew";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "##0\"%\"";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn2.HeaderText = "Skew";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 40;
            // 
            // FlowName
            // 
            this.FlowName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FlowName.DataPropertyName = "Name";
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.FlowName.DefaultCellStyle = dataGridViewCellStyle12;
            this.FlowName.HeaderText = "Flow Name";
            this.FlowName.Name = "FlowName";
            this.FlowName.ReadOnly = true;
            this.FlowName.Width = 85;
            // 
            // Order
            // 
            this.Order.DataPropertyName = "Order";
            this.Order.HeaderText = "Order";
            this.Order.Name = "Order";
            this.Order.ReadOnly = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(295, 409);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgPorts);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(287, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ports";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgPorts
            // 
            this.dgPorts.AllowUserToAddRows = false;
            this.dgPorts.AllowUserToDeleteRows = false;
            this.dgPorts.AllowUserToOrderColumns = true;
            this.dgPorts.AllowUserToResizeRows = false;
            this.dgPorts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPorts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgPorts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgPorts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PortName,
            this.Records,
            this.Bytes,
            this.PortFlowName,
            this.PortVertexName});
            this.dgPorts.ContextMenuStrip = this.contextMenu;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPorts.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPorts.Location = new System.Drawing.Point(3, 3);
            this.dgPorts.MultiSelect = false;
            this.dgPorts.Name = "dgPorts";
            this.dgPorts.ReadOnly = true;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPorts.RowHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgPorts.RowHeadersVisible = false;
            this.dgPorts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgPorts.Size = new System.Drawing.Size(281, 377);
            this.dgPorts.TabIndex = 0;
            this.dgPorts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPorts_CellDoubleClick);
            this.dgPorts.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._CellMouseDown);
            // 
            // PortName
            // 
            this.PortName.DataPropertyName = "Name";
            dataGridViewCellStyle16.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.PortName.DefaultCellStyle = dataGridViewCellStyle16;
            this.PortName.HeaderText = "Port";
            this.PortName.Name = "PortName";
            this.PortName.ReadOnly = true;
            this.PortName.Width = 50;
            // 
            // Records
            // 
            this.Records.DataPropertyName = "Records";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "N0";
            dataGridViewCellStyle17.NullValue = null;
            this.Records.DefaultCellStyle = dataGridViewCellStyle17;
            this.Records.HeaderText = "Records";
            this.Records.Name = "Records";
            this.Records.ReadOnly = true;
            this.Records.Width = 80;
            // 
            // Bytes
            // 
            this.Bytes.DataPropertyName = "Bytes";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "N0";
            dataGridViewCellStyle18.NullValue = null;
            this.Bytes.DefaultCellStyle = dataGridViewCellStyle18;
            this.Bytes.HeaderText = "Bytes";
            this.Bytes.Name = "Bytes";
            this.Bytes.ReadOnly = true;
            this.Bytes.Width = 90;
            // 
            // PortFlowName
            // 
            this.PortFlowName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PortFlowName.DataPropertyName = "FlowName";
            dataGridViewCellStyle19.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.PortFlowName.DefaultCellStyle = dataGridViewCellStyle19;
            this.PortFlowName.HeaderText = "Connecting Flow";
            this.PortFlowName.Name = "PortFlowName";
            this.PortFlowName.ReadOnly = true;
            this.PortFlowName.Width = 111;
            // 
            // PortVertexName
            // 
            this.PortVertexName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PortVertexName.DataPropertyName = "VertexName";
            dataGridViewCellStyle20.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.PortVertexName.DefaultCellStyle = dataGridViewCellStyle20;
            this.PortVertexName.HeaderText = "Connecting Vertex";
            this.PortVertexName.Name = "PortVertexName";
            this.PortVertexName.ReadOnly = true;
            this.PortVertexName.Width = 119;
            // 
            // PhaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PhaseControl";
            this.Size = new System.Drawing.Size(746, 409);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabVertexes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgVertexes)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.tabFlows.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFlows)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPorts)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
