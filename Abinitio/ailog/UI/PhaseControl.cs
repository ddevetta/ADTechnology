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
    public partial class PhaseControl : UserControl
    {
        private int revision = -1;
        private bool isVertexSorted = false;
        private bool isFlowSorted = false;
        private Phase ph;

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

        internal TabPage SelectVertexTab()
        {
            int ix = tabControl1.TabPages.IndexOfKey("tabVertexes");
            if (ix > -1)
            {
                tabControl1.SelectedIndex = ix;
                return tabControl1.TabPages[ix];
            }
            return null;
        }

        public event AddToPlotterEventHandler AddToPlotterSelected;

        public void AddOrReplaceTab(string tabName, string toolTipText, TabControlFrame frame)
        {
            TabPage tab;
            int ix = this.tabControl1.TabPages.IndexOfKey(toolTipText);
            if (ix > -1)
            {
                tab = this.tabControl1.TabPages[ix];
                tab.Controls.Clear();
            }
            else
            {
                tab = new TabPage(tabName);
                tab.Name = toolTipText;
                this.tabControl1.TabPages.Add(tab);
            }
            frame.Dock = DockStyle.Fill;
            frame.TabName = tabName;
            frame.ToolTipText = tab.ToolTipText = toolTipText;
            tab.Controls.Add(frame);
            this.tabControl1.SelectedTab = tab;
            frame.CloseTabClicked += new CloseTabEventHandler(_CloseTabClicked);
        }

        protected void _CloseTabClicked(object sender, EventArgs e)
        {
            TabControlFrame frame = (TabControlFrame)sender;
            this.tabControl1.TabPages.RemoveByKey(frame.ToolTipText);
        }

        public void Clear()
        {
            this.dgVertexes.DataSource = this.dgFlows.DataSource = this.dgPorts.DataSource = null;
            this.tabControl1.TabPages.Clear();
            this.tabControl1.TabPages.Add(this.tabVertexes);
            this.tabControl1.TabPages.Add(this.tabFlows);
        }

        protected virtual void OnAddToPlotterSelected(AddToPlotterEventArgs e)
        {
            if (this.AddToPlotterSelected == null)
                return;
            this.AddToPlotterSelected((object)this, e);
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
            this.dgVertexes.DataSource = (object)this.ph.PhaseRevisions[this.revision].Vertexes;
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
            this.dgFlows.DataSource = (object)this.ph.PhaseRevisions[this.revision].Flows;
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
            this.tabControl1_Selected((object)this, new TabControlEventArgs((TabPage)null, 0, TabControlAction.Selected));
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
                this.dgPorts.DataSource = (object)((Vertex)this.dgVertexes.SelectedCells[0].OwningRow.DataBoundItem).Ports;
            this.dgPorts.CurrentCell = (DataGridViewCell)null;
        }

        private void dgFlows_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgFlows.SelectedCells.Count > 0)
                this.dgPorts.DataSource = (object)((Flow)this.dgFlows.SelectedCells[0].OwningRow.DataBoundItem).Ports;
            this.dgPorts.CurrentCell = (DataGridViewCell)null;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (this.tabControl1.TabPages.Count == 0)
                return;

            switch (this.tabControl1.SelectedTab.Name)
            {
                case "tabVertexes":
                    this.splitContainer1.Panel2Collapsed = false;
                    this.dgPorts.Columns["PortVertexName"].Visible = false;
                    this.dgPorts.Columns["PortFlowName"].Visible = true;
                    this.dgVertexes_SelectionChanged(sender, (EventArgs)e);
                    this.dgVertexes.Select();
                    break;
                case "tabFlows":
                    this.splitContainer1.Panel2Collapsed = false;
                    this.dgPorts.Columns["PortVertexName"].Visible = true;
                    this.dgPorts.Columns["PortFlowName"].Visible = false;
                    this.dgFlows_SelectionChanged(sender, (EventArgs)e);
                    this.dgFlows.Select();
                    break;
                default:
                    this.splitContainer1.Panel2Collapsed = true;
                    break;
            }
            this.dgPorts.CurrentCell = (DataGridViewCell)null;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText((string)((DataGridView)((ContextMenuStrip)((ToolStripItem)sender).Owner).SourceControl).SelectedCells[0].Value.ToString());
        }

        private void _CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            DataGridView dataGridView = (DataGridView)sender;
            dataGridView.CurrentCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        private void dgPorts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            DataGridView dataGridView = (DataGridView)sender;
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
            cell.ToolTipText = ((Vertex)cell.OwningRow.DataBoundItem).VertexName.ToString();
        }

        private void dgFlows_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != this.dgFlows.Columns["FlowName"].Index || e.Value == null)
                return;
            DataGridViewCell cell = this.dgFlows.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ToolTipText = ((Flow)cell.OwningRow.DataBoundItem).FlowName.ToString();
        }

        private void addToPlotterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell selectedCell = ((DataGridView)((ContextMenuStrip)((ToolStripItem)sender).Owner).SourceControl).SelectedCells[0];
                this.OnAddToPlotterSelected(new AddToPlotterEventArgs(selectedCell.OwningRow.DataBoundItem.GetType(), selectedCell.OwningRow.DataBoundItem, selectedCell.OwningColumn.Name));
            }
            catch
            {
            }
        }
    }
}
