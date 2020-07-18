// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.CompositeName
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Collections;

namespace ADTechnology.AbInitio.Classes
{
  internal class CompositeName
  {
    private ArrayList names;

    public string[] Elements
    {
      get
      {
        return (string[]) this.names.ToArray(typeof (string));
      }
    }

    public int Depth
    {
      get
      {
        return this.names.Count;
      }
    }

    public CompositeName()
    {
      this.names = new ArrayList(1);
    }

    public CompositeName(string name)
    {
      this.names = new ArrayList(1);
      this.names.AddRange((ICollection) name.Split('.'));
    }

    public override string ToString()
    {
      string str = "";
      for (int index = 0; index < this.names.Count; ++index)
      {
        str += (string) this.names[index];
        if (index < this.names.Count - 1)
          str += "\r\n";
      }
      return str;
    }
  }
}
