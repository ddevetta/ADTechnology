// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.ListViewColumnSorter
// Assembly: OpenHostFileDialog1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: F8D282FE-1A5F-453A-946F-DF9521E5E170
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\OpenHostFileDialog1.dll

using System;
using System.Collections;
using System.Windows.Forms;

namespace ADTechnology.Apps.OpenHostFileDialog
{
  public class ListViewColumnSorter : IComparer
  {
    private int ColumnToSort;
    private SortOrder OrderOfSort;
    private CaseInsensitiveComparer ObjectCompare;

    public ListViewColumnSorter()
    {
      this.ColumnToSort = 0;
      this.OrderOfSort = SortOrder.None;
      this.ObjectCompare = new CaseInsensitiveComparer();
    }

    public int Compare(object x, object y)
    {
      ListViewItem listViewItem1 = (ListViewItem) x;
      ListViewItem listViewItem2 = (ListViewItem) y;
      string text1 = listViewItem1.SubItems[this.ColumnToSort].Text;
      string text2 = listViewItem2.SubItems[this.ColumnToSort].Text;
      int num;
      switch (this.ColumnToSort)
      {
        case 0:
          num = this.ObjectCompare.Compare((listViewItem1.SubItems[3].Text + text1), (listViewItem2.SubItems[3].Text + text2));
          break;
        case 1:
          num = this.ObjectCompare.Compare(long.Parse(text1), long.Parse(text2));
          break;
        case 2:
          num = this.ObjectCompare.Compare(listViewItem1.SubItems[4].Text, listViewItem2.SubItems[4].Text);
          break;
        default:
          num = this.ObjectCompare.Compare(text1, text2);
          break;
      }
      if (this.OrderOfSort == SortOrder.Ascending)
        return num;
      if (this.OrderOfSort == SortOrder.Descending)
        return -num;
      return 0;
    }

    public int SortColumn
    {
      set
      {
        this.ColumnToSort = value;
      }
      get
      {
        return this.ColumnToSort;
      }
    }

    public SortOrder Order
    {
      set
      {
        this.OrderOfSort = value;
      }
      get
      {
        return this.OrderOfSort;
      }
    }
  }
}
