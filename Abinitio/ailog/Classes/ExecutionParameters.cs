// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.ExecutionParameters
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Data;

namespace ADTechnology.AbInitio.Classes
{
  internal class ExecutionParameters : DataTable
  {
    public ExecutionParameters()
    {
      this.TableName = nameof (ExecutionParameters);
      this.Columns.Add("Parameter", typeof (string));
      this.Columns.Add("Value", typeof (string));
    }

    public void Add(string key, string value)
    {
      this.Rows.Add((object) key, (object) value);
    }

    public void Add(string[] pair)
    {
      this.Rows.Add((object[]) pair);
    }
  }
}
