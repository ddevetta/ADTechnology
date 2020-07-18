// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Phase
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
  internal class Phase
  {
    private int phase_n;
    private PhaseRevisionList phaseRevisions;

    public int PhaseNumber
    {
      get
      {
        return this.phase_n;
      }
      set
      {
        this.phase_n = value;
      }
    }

    public PhaseRevisionList PhaseRevisions
    {
      get
      {
        return this.phaseRevisions;
      }
    }

    public Phase()
    {
      this.phase_n = 0;
      this.phaseRevisions = new PhaseRevisionList();
    }

    public Phase(int phaseNumber)
    {
      this.phase_n = phaseNumber;
      this.phaseRevisions = new PhaseRevisionList();
    }

    public PhaseRevision AddOrModifyPhaseRevision(string line, bool mergeRevisions)
    {
      string[] strArray = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      int int32 = Convert.ToInt32(strArray[6].Remove(0, 1));
      int index = 0;
      if (!mergeRevisions)
      {
        while (index < this.phaseRevisions.Count && this.phaseRevisions[index].Seconds != int32)
          ++index;
      }
      PhaseRevision PhaseRevision;
      if (index == this.phaseRevisions.Count)
      {
        PhaseRevision = new PhaseRevision();
        this.phaseRevisions.Add(PhaseRevision);
      }
      else
        PhaseRevision = this.phaseRevisions[index];
      PhaseRevision.StatusTime = DateTime.ParseExact(strArray[0] + strArray[1] + strArray[2], "MMMddHH:mm:ss", (IFormatProvider) null);
      switch (strArray[5])
      {
        case "started":
          PhaseRevision.Status = PhaseStatus.Started;
          break;
        case "running":
          PhaseRevision.Status = PhaseStatus.Running;
          break;
        case "ended":
          PhaseRevision.Status = PhaseStatus.Ended;
          break;
        case "error":
          PhaseRevision.Status = PhaseStatus.Error;
          break;
        default:
          throw new InvalidFormatException("Unrecognised phase status - " + strArray[5]);
      }
      PhaseRevision.Seconds = int32;
      return PhaseRevision;
    }

    public override string ToString()
    {
      return string.Format("Phase {0} : {1} revisions", (object) this.phase_n, (object) this.phaseRevisions.Count);
    }
  }
}
