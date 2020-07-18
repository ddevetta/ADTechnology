// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.GraphPointList
// Assembly: Graph, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: B35E77AB-CD37-4C35-91F6-BF2AAF358919
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Graph.dll

using System.Collections;

namespace ADTechnology.Apps.Graph
{
  public class GraphPointList : CollectionBase
  {
    public GraphPoint this[int index]
    {
      get
      {
        return (GraphPoint) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(GraphPoint graphPoint)
    {
      return this.List.Add((object) graphPoint);
    }

    public void Remove(GraphPoint graphPoint)
    {
      this.List.Remove((object) graphPoint);
    }
  }
}
