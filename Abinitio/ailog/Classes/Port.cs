// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Port
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
  internal class Port : IComparable<Port>
  {
    private double averageRate = 0.0;
    private double lastRate = 0.0;
    private string name;
    private long records;
    private long bytes;
    private Vertex vertex;
    private Flow flow;
    private PortDirection portDir;

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public long Records
    {
      get
      {
        return this.records;
      }
      set
      {
        this.records = value;
      }
    }

    public long Bytes
    {
      get
      {
        return this.bytes;
      }
      set
      {
        this.bytes = value;
      }
    }

    public double AverageRate
    {
      get
      {
        return this.averageRate;
      }
      set
      {
        this.averageRate = value;
      }
    }

    public double LastRate
    {
      get
      {
        return this.lastRate;
      }
      set
      {
        this.lastRate = value;
      }
    }

    public Vertex Vertex
    {
      get
      {
        return this.vertex;
      }
      set
      {
        this.vertex = value;
      }
    }

    public Flow Flow
    {
      get
      {
        return this.flow;
      }
      set
      {
        this.flow = value;
      }
    }

    public string VertexName
    {
      get
      {
        return this.vertex.Name;
      }
    }

    public string FlowName
    {
      get
      {
        return this.flow.Name;
      }
    }

    public PortDirection Direction
    {
      get
      {
        return this.portDir;
      }
    }

    public Port(string name)
    {
      this.name = name;
      this.bytes = this.records = 0L;
      if (name.Contains("unused") || name.Contains("desel"))
        this.portDir = PortDirection.Additional;
      else if (name.StartsWith("in") || name.StartsWith("ctl"))
        this.portDir = PortDirection.In;
      else
        this.portDir = PortDirection.Out;
    }

    public override string ToString()
    {
      return string.Format("Port:Name={0},Records={2:D0},Bytes={1:D0},ConnectingVertex={3}({5}),ToFlow={4}", (object) this.name, (object) this.records, (object) this.bytes, (object) this.vertex.Name, (object) this.flow.Name, (object) this.portDir.ToString());
    }

    public int CompareTo(Port other)
    {
      return (this.portDir - other.Direction) * 1024 + this.name.CompareTo(other.Name);
    }
  }
}
