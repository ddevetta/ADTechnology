// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.GraphLine
// Assembly: Graph, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: B35E77AB-CD37-4C35-91F6-BF2AAF358919
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Graph.dll

using System;
using System.Drawing;

namespace ADTechnology.Apps.Graph
{
  public class GraphLine
  {
    private Type yType = typeof (long);
    private Color lineColor = Color.Black;
    private string lineInfo = "";
    private long min;
    private long max;
    private GraphPointList graphPoints;

    public long Min
    {
      get
      {
        return this.min;
      }
      set
      {
        this.min = value;
      }
    }

    public long Max
    {
      get
      {
        return this.max;
      }
      set
      {
        this.max = value;
      }
    }

    public Type YType
    {
      get
      {
        return this.yType;
      }
    }

    public Color LineColor
    {
      get
      {
        return this.lineColor;
      }
      set
      {
        this.lineColor = value;
      }
    }

    public string LineInfo
    {
      get
      {
        return this.lineInfo;
      }
      set
      {
        this.lineInfo = value;
      }
    }

    public GraphPointList GraphPoints
    {
      get
      {
        return this.graphPoints;
      }
    }

    public GraphLine()
    {
      this.graphPoints = new GraphPointList();
    }

    public GraphLine(Type yType, Color lineColor, string lineInfo)
    {
      this.graphPoints = new GraphPointList();
      this.yType = yType;
      this.lineColor = lineColor;
      this.lineInfo = lineInfo;
    }
  }
}
