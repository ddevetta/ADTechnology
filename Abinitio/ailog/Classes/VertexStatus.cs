// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.VertexStatus
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
    internal class VertexStatus : IComparable<VertexStatus>
    {
        private int running;
        private int completed;

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

        public VertexStatus()
        {
            this.running = this.completed = 0;
        }

        public VertexStatus(int running, int completed)
        {
            this.running = running;
            this.completed = completed;
        }

        public void Parse(string p)
        {
            string[] parts = p.Split(':');
            if (parts.Length < 2)
                throw new InvalidFormatException("Malformed vertex status.");
            int.TryParse(parts[0], out this.running);
            int.TryParse(parts[1], out this.completed);
        }

        public override string ToString()
        {
            return string.Format("[{0,2:##}:{1,2:##}]", (object)this.running, (object)this.completed);
        }

        public int CompareTo(VertexStatus other)
        {
            return (this.completed - other.Completed) * 1024 + (this.running - other.Running);
        }
    }
}
