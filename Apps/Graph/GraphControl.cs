// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.GraphControl
// Assembly: Graph, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: B35E77AB-CD37-4C35-91F6-BF2AAF358919
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Graph.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.Apps.Graph
{
  public class GraphControl : UserControl
  {
    private int focus = 12;
    private int currentLine = -1;
    private const int leftBorder = 12;
    private const int rightBorder = 6;
    private const int topBorder = 6;
    private const int bottomBorder = 50;
    private IContainer components;
    private GraphLineList graphLines;
    private int currentPoint;
    private bool singleScale;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Linen;
      this.Name = nameof (GraphControl);
      this.Size = new Size(680, 500);
      this.MouseDown += new MouseEventHandler(this.GraphControl_MouseDown);
      this.KeyDown += new KeyEventHandler(this.GraphControl_KeyDown);
      this.ResumeLayout(false);
    }

    public event EventHandler GraphPointChanged;

    protected virtual void OnGraphPointChanged(EventArgs e)
    {
      if (this.GraphPointChanged == null)
        return;
      this.GraphPointChanged((object) this, e);
    }

    public GraphLineList Graphlines
    {
      get
      {
        return this.graphLines;
      }
    }

    public int SelectedGraphLineIndex
    {
      get
      {
        return this.currentLine;
      }
      set
      {
        if (value < -1 || value >= this.graphLines.Count)
          return;
        this.currentLine = value;
        this.OnGraphPointChanged(new EventArgs());
      }
    }

    public int SelectedGraphPointIndex
    {
      get
      {
        return this.currentPoint;
      }
    }

    public bool SingleScale
    {
      get
      {
        return this.singleScale;
      }
      set
      {
        this.singleScale = value;
      }
    }

    public GraphControl()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.DoubleBuffered = true;
      this.graphLines = new GraphLineList();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Font font = new Font(FontFamily.GenericSansSerif, 10f);
      SolidBrush solidBrush = new SolidBrush(Color.SlateGray);
      Pen pen1 = new Pen((Brush) solidBrush);
      e.Graphics.DrawLine(pen1, 0, this.Height - 50, this.Width - 6, this.Height - 50);
      e.Graphics.DrawLine(pen1, 12, 0, 12, this.Height - 6);
      int num1 = 0;
      long num2 = long.MaxValue;
      long num3 = 0;
      foreach (GraphLine graphLine in (CollectionBase) this.graphLines)
      {
        graphLine.Min = long.MaxValue;
        foreach (GraphPoint graphPoint in (CollectionBase) graphLine.GraphPoints)
        {
          switch (graphLine.YType.ToString())
          {
            case "System.Int64":
              long y = (long) graphPoint.Y;
              if (y < graphLine.Min)
                graphLine.Min = y;
              if (y > graphLine.Max)
              {
                graphLine.Max = y;
                continue;
              }
              continue;
            default:
              throw new InvalidCastException("Cannot cast Y object to type of " + graphLine.YType.ToString());
          }
        }
        if (graphLine.Min < num2)
          num2 = graphLine.Min;
        if (graphLine.Max > num3)
          num3 = graphLine.Max;
        if (graphLine.GraphPoints.Count >= num1)
          num1 = graphLine.GraphPoints.Count - 1;
      }
      int num4 = -1;
      foreach (GraphLine graphLine in (CollectionBase) this.graphLines)
      {
        ++num4;
        if (graphLine.GraphPoints.Count >= 2)
        {
          float num5 = (float) (this.Width - 12 - 6) / (float) (graphLine.GraphPoints.Count - 1);
          Point[] points = new Point[graphLine.GraphPoints.Count];
          int index = 0;
          float num6 = 12f;
          foreach (GraphPoint graphPoint in (CollectionBase) graphLine.GraphPoints)
          {
            int y = graphLine.Max != graphLine.Min ? (int) ((long) (this.Height - 50) - (long) (this.Height - 50 - 6) * ((long) graphPoint.Y - graphLine.Min) / (graphLine.Max - graphLine.Min)) : this.Height - 50;
            points[index] = new Point((int) num6, y);
            graphPoint.PhysicalPoint = points[index];
            ++index;
            num6 += num5;
          }
          Pen pen2 = num4 != this.currentLine ? new Pen(graphLine.LineColor) : new Pen(graphLine.LineColor, 2f);
          e.Graphics.DrawLines(pen2, points);
        }
      }
      if (this.currentLine > -1)
      {
        GraphLine graphLine = this.graphLines[this.currentLine];
        GraphPoint graphPoint = graphLine.GraphPoints[this.currentPoint];
        string s = string.Format("{0} \nmin={1}, max={2}\n{3} = {4}", (object) graphLine.LineInfo, (object) graphLine.Min, (object) graphLine.Max, graphPoint.Y, (object) graphPoint.Information);
        e.Graphics.DrawString(s, font, (Brush) solidBrush, 14f, (float) ((double) this.Height - 50.0 + 2.0));
        e.Graphics.DrawEllipse(pen1, graphPoint.PhysicalPoint.X - this.focus / 2, graphPoint.PhysicalPoint.Y - this.focus / 2, this.focus, this.focus);
      }
      base.OnPaint(e);
    }

    private void GraphControl_MouseDown(object sender, MouseEventArgs e)
    {
      int num1 = e.X - this.focus / 2;
      int num2 = e.X + this.focus / 2;
      int num3 = e.Y - this.focus / 2;
      int num4 = e.Y + this.focus / 2;
      for (int index1 = 0; index1 < this.graphLines.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.graphLines[index1].GraphPoints.Count; ++index2)
        {
          if (this.graphLines[index1].GraphPoints[index2].PhysicalPoint.X >= num1 && this.graphLines[index1].GraphPoints[index2].PhysicalPoint.X <= num2 && (this.graphLines[index1].GraphPoints[index2].PhysicalPoint.Y >= num3 && this.graphLines[index1].GraphPoints[index2].PhysicalPoint.Y <= num4))
          {
            this.currentLine = index1;
            this.currentPoint = index2;
            this.OnGraphPointChanged((EventArgs) e);
          }
        }
      }
      this.Refresh();
    }

    private void GraphControl_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.currentLine < 0)
        return;
      switch (e.KeyCode)
      {
        case Keys.Left:
          --this.currentPoint;
          if (this.currentPoint < 0)
          {
            this.currentPoint = this.graphLines[this.currentLine].GraphPoints.Count - 1;
            break;
          }
          break;
        case Keys.Up:
          --this.currentLine;
          if (this.currentLine < 0)
            this.currentLine = this.graphLines.Count - 1;
          if (this.currentPoint >= this.graphLines[this.currentLine].GraphPoints.Count)
          {
            this.currentPoint = 0;
            break;
          }
          break;
        case Keys.Right:
          ++this.currentPoint;
          if (this.currentPoint == this.graphLines[this.currentLine].GraphPoints.Count)
          {
            this.currentPoint = 0;
            break;
          }
          break;
        case Keys.Down:
          ++this.currentLine;
          if (this.currentLine == this.graphLines.Count)
            this.currentLine = 0;
          if (this.currentPoint >= this.graphLines[this.currentLine].GraphPoints.Count)
          {
            this.currentPoint = 0;
            break;
          }
          break;
        default:
          return;
      }
      this.OnGraphPointChanged((EventArgs) e);
      this.Refresh();
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }
  }
}
