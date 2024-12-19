using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ADTechnology.Apps.Graph;

namespace ADTechnology.AbInitio.UI
{
    public partial class GraphContainerControl : TabControlFrame
    {
        private SplitContainer splitContainer1;
        private GraphControl graph;
        private ListBox lbLines;

        public GraphLineList GraphLines
        {
            get
            {
                return this.graph.Graphlines;
            }
        }
        public GraphContainerControl()
        {
            InitializeComponent();
            this.lbLines.DrawItem += new DrawItemEventHandler(this.lbLines_DrawItem);
            this.lbLines.SelectedIndexChanged += new EventHandler(this.lbLines_SelectedIndexChanged);
            this.graph.GraphPointChanged += new EventHandler(this.graph_GraphPointChanged);
        }

        public void AddLine(GraphLine graphLine)
        {
            this.graph.Graphlines.Add(graphLine);
            this.graph.SelectedGraphLineIndex = this.lbLines.Items.Add(graphLine);
            this.graph.Focus();
        }

        public void ClearGraph()
        {
            this.graph.Graphlines.Clear();
            this.graph.SelectedGraphLineIndex = -1;
            this.graph.Refresh();
            this.lbLines.Items.Clear();
            this.lbLines.Refresh();
        }

        private void lbLines_DrawItem(object sender, DrawItemEventArgs e)
        {
            GraphLine graphLine = (GraphLine)((ListBox)sender).Items[e.Index];
            e.DrawBackground();
            Font font = new Font(e.Font, FontStyle.Regular);
            e.Graphics.DrawRectangle(new Pen(this.lbLines.BackColor), e.Bounds);
            using (Brush brush = new SolidBrush(graphLine.LineColor))
                e.Graphics.DrawString(graphLine.LineInfo, font, brush, (RectangleF)e.Bounds);
        }

        private void graph_GraphPointChanged(object sender, EventArgs e)
        {
            this.lbLines.SelectedIndex = this.graph.SelectedGraphLineIndex;
            this.lbLines.Refresh();
        }

        private void lbLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.graph.SelectedGraphLineIndex = this.lbLines.SelectedIndex;
            this.lbLines.Refresh();
            this.graph.Refresh();
            this.graph.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearGraph();
        }
    }
}
