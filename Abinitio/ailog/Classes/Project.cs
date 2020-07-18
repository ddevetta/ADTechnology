// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Project
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
  internal class Project
  {
    private Options opts;
    private ProjectStatus status;
    private DateTime start;
    private DateTime end;
    private PhaseList phases;

    public ProjectStatus Status
    {
      get
      {
        return this.status;
      }
      set
      {
        this.status = value;
      }
    }

    public DateTime Start
    {
      get
      {
        return this.start;
      }
      set
      {
        this.start = value;
      }
    }

    public DateTime End
    {
      get
      {
        return this.end;
      }
      set
      {
        this.end = value;
      }
    }

    public PhaseList Phases
    {
      get
      {
        return this.phases;
      }
    }

    public Project(Options options)
    {
      this.opts = options;
      this.status = ProjectStatus.NotStarted;
      this.start = DateTime.MinValue;
      this.phases = new PhaseList();
    }

    public Phase AddOrModifyPhase(string line)
    {
      string[] strArray = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length < 7 || strArray[3] != "Phase")
        return (Phase) null;
      int index = Convert.ToInt32(strArray[4]);
      if (index > this.phases.Count)
        throw new InvalidFormatException("A phase has been skipped numerically in the log file.");
      if (index == this.phases.Count)
        index = this.Phases.Add(new Phase());
      Phase phase = this.Phases[index];
      phase.PhaseNumber = index;
      return phase;
    }

    public override string ToString()
    {
      return string.Format("Project:Status={0},Start={1},End={2},Phases={3}", (object) this.status, (object) this.start, (object) this.end, (object) this.phases.Count);
    }
  }
}
