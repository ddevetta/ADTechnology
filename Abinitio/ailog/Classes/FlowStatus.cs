// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.FlowStatus
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
  internal class FlowStatus : IComparable<FlowStatus>
  {
    private int notStarted;
    private int running;
    private int completed;

    public int NotStarted
    {
      get
      {
        return this.notStarted;
      }
      set
      {
        this.notStarted = value;
      }
    }

    public int Running
    {
      get
      {
        return this.running;
      }
      set
      {
        this.running = value;
      }
    }

    public int Completed
    {
      get
      {
        return this.completed;
      }
      set
      {
        this.completed = value;
      }
    }

    public bool IsComplete
    {
      get
      {
        return this.completed > 0 && this.running == 0 && this.notStarted == 0;
      }
    }

    public FlowStatus()
    {
      this.notStarted = this.running = this.completed = 0;
    }

    public FlowStatus(int notStarted, int running, int completed)
    {
      this.notStarted = notStarted;
      this.running = running;
      this.completed = completed;
    }

    public void Parse(string p)
    {
      int.TryParse(p.Substring(1, 4), out this.notStarted);
      int.TryParse(p.Substring(6, 4), out this.running);
      int.TryParse(p.Substring(11, 4), out this.completed);
    }

    public override string ToString()
    {
      return string.Format("[{0,4:####}:{1,4:####}:{2,4:####}]", (object) this.notStarted, (object) this.running, (object) this.completed);
    }

    public int CompareTo(FlowStatus other)
    {
      return (this.completed - other.completed) * 1048576 + (this.running - other.running) * 1024 + (this.notStarted - other.notStarted);
    }
  }
}
