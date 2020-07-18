// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.AddToPlotterEventArgs
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.UI
{
  public class AddToPlotterEventArgs : EventArgs
  {
    public Type ObjectType;
    public object Object;
    public string Column;

    public AddToPlotterEventArgs(Type type, object obj, string column)
    {
      this.ObjectType = type;
      this.Object = obj;
      this.Column = column;
    }

    public override string ToString()
    {
      return "Type=" + this.ObjectType.ToString() + ", Column=" + this.Column;
    }
  }
}
