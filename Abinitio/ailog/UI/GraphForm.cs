// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.GraphForm
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.Apps.Graph;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
  public class GraphForm : Form
  {
    private IContainer components = (IContainer) null;
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

    public GraphForm()
    {
      this.InitializeComponent();
      this.lbLines.DrawItem += new DrawItemEventHandler(this.lbLines_DrawItem);
      this.lbLines.SelectedIndexChanged += new EventHandler(this.lbLines_SelectedIndexChanged);
      this.graph.GraphPointChanged += new EventHandler(this.graph_GraphPointChanged);
    }

    public void Add(GraphLine graphLine)
    {
      this.graph.Graphlines.Add(graphLine);
      this.graph.SelectedGraphLineIndex = this.lbLines.Items.Add((object) graphLine);
    }

    private void lbLines_DrawItem(object sender, DrawItemEventArgs e)
    {
      GraphLine graphLine = (GraphLine) ((ListBox) sender).Items[e.Index];
      e.DrawBackground();
      Font font = new Font(e.Font, FontStyle.Regular);
      e.Graphics.DrawRectangle(new Pen(this.lbLines.BackColor), e.Bounds);
      using (Brush brush = (Brush) new SolidBrush(graphLine.LineColor))
        e.Graphics.DrawString(graphLine.LineInfo, font, brush, (RectangleF) e.Bounds);
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
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.graph = new ADTechnology.Apps.Graph.GraphControl();
            this.lbLines = new System.Windows.Forms.ListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.graph);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.lbLines);
            this.splitContainer1.Size = new System.Drawing.Size(907, 679);
            this.splitContainer1.SplitterDistance = 567;
            this.splitContainer1.TabIndex = 0;
            // 
            // graph
            // 
            this.graph.BackColor = System.Drawing.Color.Linen;
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.Location = new System.Drawing.Point(0, 0);
            this.graph.Name = "graph";
            this.graph.SelectedGraphLineIndex = -1;
            this.graph.SingleScale = false;
            this.graph.Size = new System.Drawing.Size(907, 567);
            this.graph.TabIndex = 1;
            // 
            // lbLines
            // 
            this.lbLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLines.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbLines.FormattingEnabled = true;
            this.lbLines.Location = new System.Drawing.Point(0, 0);
            this.lbLines.Name = "lbLines";
            this.lbLines.Size = new System.Drawing.Size(907, 108);
            this.lbLines.TabIndex = 0;
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 679);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GraphForm";
            this.Text = "Plotter";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

    }
  }
}
