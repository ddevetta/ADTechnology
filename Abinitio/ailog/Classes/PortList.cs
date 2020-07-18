// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.PortList
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Collections;

namespace ADTechnology.AbInitio.Classes
{
  internal class PortList : CollectionBase
  {
    public Port this[int index]
    {
      get
      {
        return (Port) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(Port port)
    {
      int index = 0;
      while (index < this.List.Count && this[index].CompareTo(port) <= 0)
        ++index;
      this.List.Insert(index, (object) port);
      return index;
    }

    public void Remove(Port port)
    {
      this.List.Remove((object) port);
    }

    public bool Exists(Port port)
    {
      return this.List.IndexOf((object) port) > -1;
    }

    public Port Find(string portName)
    {
      foreach (Port port in (IEnumerable) this.List)
      {
        if (port.Name == portName)
          return port;
      }
      return (Port) null;
    }
  }
}
