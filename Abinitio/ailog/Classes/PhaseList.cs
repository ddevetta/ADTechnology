// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.PhaseList
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Collections;

namespace ADTechnology.AbInitio.Classes
{
  internal class PhaseList : CollectionBase
  {
    public Phase this[int index]
    {
      get
      {
        return (Phase) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(Phase Phase)
    {
      return this.List.Add((object) Phase);
    }

    public void Remove(Phase Phase)
    {
      this.List.Remove((object) Phase);
    }
  }
}
