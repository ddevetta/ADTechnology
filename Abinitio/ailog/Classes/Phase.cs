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

        public PhaseRevision AddOrModifyPhaseRevision(string line)
        {
            string[] strArray = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int secs = Convert.ToInt32(strArray[6].Remove(0, 1));
            DateTime statusTime = DateTime.ParseExact(strArray[0] + strArray[1] + strArray[2], "MMMddHH:mm:ss", (IFormatProvider)null);

            PhaseRevision totRev, newRev;

            // Add 'Total' revision if first time
            if (this.phaseRevisions.Count == 0)
            {
                totRev = new PhaseRevision(PhaseStatus.Total, statusTime, secs);
                totRev.Status = PhaseStatus.Total;
                this.phaseRevisions.Add(totRev);
            }
            else
                totRev = this.phaseRevisions[0];

            // Add revision
            int index = 1;
            while (index < this.phaseRevisions.Count && this.phaseRevisions[index].Seconds != secs)
                ++index;

            if (index == this.phaseRevisions.Count)
            {
                newRev = new PhaseRevision();
                this.phaseRevisions.Add(newRev);
            }
            else
                newRev = this.phaseRevisions[index];

            newRev.StatusTime = DateTime.ParseExact(strArray[0] + strArray[1] + strArray[2], "MMMddHH:mm:ss", (IFormatProvider)null);
            switch (strArray[5])
            {
                case "started":
                    newRev.Status = PhaseStatus.Started;
                    break;
                case "running":
                    newRev.Status = PhaseStatus.Running;
                    break;
                case "ended":
                    newRev.Status = PhaseStatus.Ended;
                    break;
                case "error":
                    newRev.Status = PhaseStatus.Error;
                    break;
                default:
                    throw new InvalidFormatException("Unrecognised phase status - " + strArray[5]);
            }
            newRev.Seconds = secs;

            // Update total revision
            totRev.StatusTime = newRev.StatusTime;
            totRev.Seconds = newRev.Seconds;

            return newRev;
        }

        public override string ToString()
        {
            return string.Format("Phase {0} : {1} revisions", (object)this.phase_n, (object)this.phaseRevisions.Count);
        }
    }
}
