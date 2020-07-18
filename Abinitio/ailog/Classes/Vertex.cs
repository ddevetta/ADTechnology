// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Vertex
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
  internal class Vertex
  {
    private int order = 0;
    private int[] flows = new int[3];
    private string name;
    private VertexStatus status;
    private double cpu;
    private double skew;
    private DateTime start;
    private DateTime end;
    private PortList ports;
    public CompositeName VertexName;

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
        this.VertexName = new CompositeName(this.name);
      }
    }

    public VertexStatus Status
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

    public double CPU
    {
      get
      {
        return this.cpu;
      }
      set
      {
        this.cpu = value;
      }
    }

    public double Skew
    {
      get
      {
        return this.skew;
      }
      set
      {
        this.skew = value;
      }
    }

    public DateTime StartTime
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

    public DateTime EndTime
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

    public TimeSpan Duration
    {
      get
      {
        if (this.end > DateTime.MinValue)
          return this.end - this.start;
        return TimeSpan.Zero;
      }
    }

    public bool StartPoint
    {
      get
      {
        return this.flows[0] == 0;
      }
    }

    public bool EndPoint
    {
      get
      {
        return this.flows[1] == 0 && this.flows[2] == 0;
      }
    }

    public int Order
    {
      get
      {
        return this.order;
      }
      set
      {
        this.order = value;
      }
    }

    public PortList Ports
    {
      get
      {
        return this.ports;
      }
    }

    public Vertex(string vertexName)
    {
      this.name = vertexName;
      this.status = new VertexStatus();
      this.cpu = 0.0;
      this.skew = 0.0;
      this.start = this.end = DateTime.MinValue;
      this.ports = new PortList();
      this.VertexName = new CompositeName(this.name);
    }

    public override string ToString()
    {
      return string.Format("Vertex:Name={0},NameDepth={5},Status={1},CPU={2:F3},Skew={3:F1}%,Ports={4}", (object) this.name, (object) this.status.ToString(), (object) this.CPU, (object) this.skew, (object) this.ports.Count, (object) this.VertexName.Depth);
    }

    public void AddIfNotExists(Port pt)
    {
      if (this.ports.Exists(pt))
        return;
      this.ports.Add(pt);
      ++this.flows[(int) pt.Direction];
    }
  }
}
