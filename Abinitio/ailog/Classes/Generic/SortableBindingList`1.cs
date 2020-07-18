// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Generic.SortableBindingList`1
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ADTechnology.AbInitio.Classes.Generic
{
  public class SortableBindingList<T> : BindingList<T>
  {
    private readonly Dictionary<Type, PropertyComparer<T>> comparers;
    private bool isSorted;
    private ListSortDirection listSortDirection;
    private PropertyDescriptor propertyDescriptor;

    public SortableBindingList()
      : base((IList<T>) new List<T>())
    {
      this.comparers = new Dictionary<Type, PropertyComparer<T>>();
    }

    public SortableBindingList(IEnumerable<T> enumeration)
      : base((IList<T>) new List<T>(enumeration))
    {
      this.comparers = new Dictionary<Type, PropertyComparer<T>>();
    }

    protected override bool SupportsSortingCore
    {
      get
      {
        return true;
      }
    }

    protected override bool IsSortedCore
    {
      get
      {
        return this.isSorted;
      }
    }

    protected override PropertyDescriptor SortPropertyCore
    {
      get
      {
        return this.propertyDescriptor;
      }
    }

    protected override ListSortDirection SortDirectionCore
    {
      get
      {
        return this.listSortDirection;
      }
    }

    protected override bool SupportsSearchingCore
    {
      get
      {
        return true;
      }
    }

    protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
    {
      List<T> items = (List<T>) this.Items;
      Type propertyType = property.PropertyType;
      PropertyComparer<T> propertyComparer;
      if (!this.comparers.TryGetValue(propertyType, out propertyComparer))
      {
        propertyComparer = new PropertyComparer<T>(property, direction);
        this.comparers.Add(propertyType, propertyComparer);
      }
      propertyComparer.SetPropertyAndDirection(property, direction);
      items.Sort((IComparer<T>) propertyComparer);
      this.propertyDescriptor = property;
      this.listSortDirection = direction;
      this.isSorted = true;
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
      this.isSorted = false;
      this.propertyDescriptor = base.SortPropertyCore;
      this.listSortDirection = base.SortDirectionCore;
      this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override int FindCore(PropertyDescriptor property, object key)
    {
      int count = this.Count;
      for (int index = 0; index < count; ++index)
      {
        T obj = this[index];
        if (property.GetValue((object) obj).Equals(key))
          return index;
      }
      return -1;
    }
  }
}
