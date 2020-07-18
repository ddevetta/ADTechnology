// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Generic.PropertyComparer`1
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ADTechnology.AbInitio.Classes.Generic
{
  public class PropertyComparer<T> : IComparer<T>
  {
    private readonly IComparer comparer;
    private PropertyDescriptor propertyDescriptor;
    private int reverse;

    public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
    {
      this.propertyDescriptor = property;
      this.comparer = (IComparer) typeof (Comparer<>).MakeGenericType(property.PropertyType).InvokeMember("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, (object) null, (object[]) null);
      this.SetListSortDirection(direction);
    }

    public int Compare(T x, T y)
    {
      return this.reverse * this.comparer.Compare(this.propertyDescriptor.GetValue((object) x), this.propertyDescriptor.GetValue((object) y));
    }

    private void SetPropertyDescriptor(PropertyDescriptor descriptor)
    {
      this.propertyDescriptor = descriptor;
    }

    private void SetListSortDirection(ListSortDirection direction)
    {
      this.reverse = direction == ListSortDirection.Ascending ? 1 : -1;
    }

    public void SetPropertyAndDirection(PropertyDescriptor descriptor, ListSortDirection direction)
    {
      this.SetPropertyDescriptor(descriptor);
      this.SetListSortDirection(direction);
    }
  }
}
