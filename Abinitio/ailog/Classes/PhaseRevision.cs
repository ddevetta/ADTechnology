// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.PhaseRevision
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
    internal class PhaseRevision
    {
        private PhaseStatus status;
        private DateTime statusTime;
        private int seconds;
        private VertexList vertexes;
        private FlowList flows;

        public PhaseStatus Status
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

        public DateTime StatusTime
        {
            get
            {
                return this.statusTime;
            }
            set
            {
                this.statusTime = value;
            }
        }

        public int Seconds
        {
            get
            {
                return this.seconds;
            }
            set
            {
                this.seconds = value;
            }
        }

        public VertexList Vertexes
        {
            get
            {
                return this.vertexes;
            }
        }

        public FlowList Flows
        {
            get
            {
                return this.flows;
            }
        }

        public PhaseRevision()
        {
            this.status = PhaseStatus.Started;
            this.statusTime = DateTime.MinValue;
            this.seconds = 0;
            this.vertexes = new VertexList();
            this.flows = new FlowList();
        }

        public PhaseRevision(PhaseStatus status, DateTime statusTime, int seconds)
        {
            this.status = status;
            this.statusTime = statusTime;
            this.seconds = seconds;
            this.vertexes = new VertexList();
            this.flows = new FlowList();
        }

        public Vertex AddOrModifyVertex(string line)
        {
            string[] strArray = line.Split("[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length < 3)
                throw new InvalidFormatException("An expected vertex line did not contain the CPU time, Status, Skew and Vertex name.");
            string vertexName = strArray[2].Substring(6);
            Vertex vertex = this.vertexes.Find(vertexName);
            if (vertex == null)
            {
                vertex = new Vertex(vertexName);
                this.vertexes.Add(vertex);
            }
            vertex.CPU = Convert.ToDouble(strArray[0].Trim());
            vertex.Skew = Convert.ToDouble(strArray[2].Substring(0, 4));
            vertex.Status.Parse(strArray[1]);
            if (vertex.StartTime == DateTime.MinValue)
                vertex.StartTime = this.StatusTime;
            if (vertex.EndTime == DateTime.MinValue && vertex.Status.Running == 0)
                vertex.EndTime = this.StatusTime;
            return vertex;
        }

        public Flow AddOrModifyFlow(string line)
        {
            Port port = (Port)null;
            string[] strArray = line.Substring(56).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 3)
                throw new InvalidFormatException("An expected flow line did not contain the names of the flow, vertex and port.");
            Flow flow = this.flows.Find(strArray[0]);
            if (flow == null)
            {
                flow = new Flow(strArray[0]);
                this.flows.Add(flow);
            }
            flow.Skew = Convert.ToDouble(line.Substring(50, 4));
            flow.Status.Parse(line.Substring(34, 16));
            port = flow.AddOrModifyPort(line, this.vertexes.Find(strArray[1]), strArray[2]);
            return flow;
        }

        public override string ToString()
        {
            return string.Format("{0}  {1}.  {2} seconds,  {3} vertexes,  {4} flows", (object)this.status, (object)this.statusTime, (object)this.seconds, (object)this.vertexes.Count, (object)this.flows.Count);
        }
    }
}
