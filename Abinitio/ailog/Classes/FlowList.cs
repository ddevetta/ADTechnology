// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.FlowList
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.AbInitio.Classes.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ADTechnology.AbInitio.Classes
{
  internal class FlowList : SortableBindingList<Flow>
  {
    public PropertyDescriptor SortProperty
    {
      get
      {
        return this.SortPropertyCore;
      }
    }

    public ListSortDirection SortDirection
    {
      get
      {
        return this.SortDirectionCore;
      }
    }

    public bool IsSorted
    {
      get
      {
        return this.IsSortedCore;
      }
    }

    public Flow Find(string flowName)
    {
      foreach (Flow flow in (Collection<Flow>) this)
      {
        if (flow.Name == flowName)
          return flow;
      }
      return (Flow) null;
    }

    public void Sort(PropertyDescriptor sortProperty, ListSortDirection direction)
    {
      this.ApplySortCore(sortProperty, direction);
    }

    public void Sort(string columnName)
    {
      if (this.Count < 1)
        return;
      this.ApplySortCore(TypeDescriptor.GetProperties((object) this[0])[columnName], ListSortDirection.Ascending);
    }
  }
}
