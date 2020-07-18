// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.GraphPoint
// Assembly: Graph, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: B35E77AB-CD37-4C35-91F6-BF2AAF358919
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Graph.dll

using System.Drawing;

namespace ADTechnology.Apps.Graph
{
  public class GraphPoint
  {
    public object Y;
    public string Information;
    public Point PhysicalPoint;

    public GraphPoint(object y, string information)
    {
      this.Y = y;
      this.Information = information;
    }
  }
}
