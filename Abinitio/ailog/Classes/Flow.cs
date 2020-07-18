// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Flow
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

namespace ADTechnology.AbInitio.Classes
{
  internal class Flow
  {
    private int order = 0;
    private int[] flows = new int[3];
    private string name;
    private FlowStatus status;
    private double skew;
    private PortList ports;
    public CompositeName FlowName;

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
        this.FlowName = new CompositeName(value);
      }
    }

    public FlowStatus Status
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

    public bool StartPoint
    {
      get
      {
        return this.flows[1] == 0 && this.flows[2] == 0;
      }
    }

    public bool EndPoint
    {
      get
      {
        return this.flows[0] == 0;
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

    public Flow(string flowName)
    {
      this.name = flowName;
      this.status = new FlowStatus();
      this.skew = 0.0;
      this.ports = new PortList();
      this.FlowName = new CompositeName(this.name);
    }

    public Port AddOrModifyPort(string line, Vertex vertex, string portName)
    {
      Port port = this.ports.Find(portName);
      if (port == null)
      {
        port = new Port(portName);
        this.ports.Add(port);
      }
      long result;
      long.TryParse(line.Substring(0, 17).Replace(",", ""), out result);
      port.Bytes = result;
      long.TryParse(line.Substring(17, 16).Replace(",", ""), out result);
      port.Records = result;
      port.Vertex = vertex;
      port.Flow = this;
      ++this.flows[(int) port.Direction];
      return port;
    }

    public override string ToString()
    {
      return string.Format("Flow:Name={0},Status={1},Skew={2:F1}%,Ports={3}", (object) this.name, (object) this.status.ToString(), (object) this.skew, (object) this.ports.Count);
    }
  }
}
