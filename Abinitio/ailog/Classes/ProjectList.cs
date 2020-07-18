// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.ProjectList
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Collections;

namespace ADTechnology.AbInitio.Classes
{
  internal class ProjectList : CollectionBase
  {
    public Project this[int index]
    {
      get
      {
        return (Project) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(Project Project)
    {
      return this.List.Add((object) Project);
    }

    public void Remove(Project Project)
    {
      this.List.Remove((object) Project);
    }
  }
}
