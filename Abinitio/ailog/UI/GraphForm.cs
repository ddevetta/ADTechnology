// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.GraphForm
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.Apps;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GraphForm));
      this.splitContainer1 = new SplitContainer();
      this.graph = new GraphControl();
      this.lbLines = new ListBox();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      this.splitContainer1.Dock = DockStyle.Fill;
      this.splitContainer1.Location = new Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = Orientation.Horizontal;
      this.splitContainer1.Panel1.Controls.Add((Control) this.graph);
      this.splitContainer1.Panel2.AutoScroll = true;
      this.splitContainer1.Panel2.Controls.Add((Control) this.lbLines);
      this.splitContainer1.Size = new Size(700, 598);
      this.splitContainer1.SplitterDistance = 500;
      this.splitContainer1.TabIndex = 0;
      this.graph.BackColor = Color.Linen;
      this.graph.Dock = DockStyle.Fill;
      this.graph.Location = new Point(0, 0);
      this.graph.Name = "graph";
      this.graph.SelectedGraphLineIndex = -1;
      this.graph.Size = new Size(700, 500);
      this.graph.TabIndex = 1;
      this.lbLines.Dock = DockStyle.Fill;
      this.lbLines.DrawMode = DrawMode.OwnerDrawFixed;
      this.lbLines.FormattingEnabled = true;
      this.lbLines.Location = new Point(0, 0);
      this.lbLines.Name = "lbLines";
      this.lbLines.Size = new Size(700, 82);
      this.lbLines.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(700, 598);
      this.Controls.Add((Control) this.splitContainer1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (GraphForm);
      this.Text = "Plotter";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
