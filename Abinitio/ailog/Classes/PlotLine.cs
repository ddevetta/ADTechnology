// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.PlotLine
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.Apps.Graph;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace ADTechnology.AbInitio.Classes
{
  internal class PlotLine
  {
    private GraphLine gl;
    private Project prj;
    private int phix;
    private Type type;
    private object obj;
    private Vertex vertex;
    private Flow flow;
    private Port port;
    private string column;

    public GraphLine GraphLine
    {
      get
      {
        return this.gl;
      }
    }

    public PlotLine()
    {
    }

    public PlotLine(Project project, int phaseIndex, Type type, object obj, string column)
    {
      this.gl = new GraphLine();
      this.type = type;
      this.prj = project;
      this.phix = phaseIndex;
      this.column = column;
      this.obj = obj;
      this.plot();
    }

    public PlotLine(
      Project project,
      int phaseIndex,
      Type type,
      object obj,
      string column,
      Color color)
    {
      this.gl = new GraphLine();
      this.type = type;
      this.prj = project;
      this.phix = phaseIndex;
      this.column = column;
      this.obj = obj;
      this.gl.LineColor = color;
      this.plot();
    }

    private void plot()
    {
      switch (this.type.ToString())
      {
        case "ADTechnology.AbInitio.Classes.Flow":
          this.flow = (Flow) this.obj;
          this.gl.LineInfo = this.flow.Name + " (" + this.column + ")";
          for (int index = this.prj.Phases[this.phix].PhaseRevisions.Count - 1; index > -1; --index)
          {
            PhaseRevision phaseRevision = this.prj.Phases[this.phix].PhaseRevisions[index];
            foreach (Flow flow in (Collection<Flow>) phaseRevision.Flows)
            {
              if (flow.Name == this.flow.Name)
              {
                switch (this.column)
                {
                  case "Skew":
                    this.gl.GraphPoints.Add(new GraphPoint((object) (long) flow.Skew, phaseRevision.ToString()));
                    break;
                  default:
                    throw new ApplicationException("This Flow column cannot be plotted.");
                }
              }
            }
          }
          break;
        case "ADTechnology.AbInitio.Classes.Port":
          this.port = (Port) this.obj;
          this.gl.LineInfo = this.port.VertexName + " (port " + this.port.Name + " " + this.column + ")";
          for (int index = this.prj.Phases[this.phix].PhaseRevisions.Count - 1; index > -1; --index)
          {
            Debug.WriteLine("Revision=" + index.ToString());
            PhaseRevision phaseRevision = this.prj.Phases[this.phix].PhaseRevisions[index];
            foreach (Vertex vertex in (Collection<Vertex>) phaseRevision.Vertexes)
            {
              if (vertex.Name == this.port.VertexName)
              {
                foreach (Port port in (CollectionBase) vertex.Ports)
                {
                  if (port.Name == this.port.Name)
                  {
                    switch (this.column)
                    {
                      case "Records":
                        this.gl.GraphPoints.Add(new GraphPoint((object) port.Records, phaseRevision.ToString()));
                        Debug.WriteLine("Plotted");
                        break;
                      case "Bytes":
                        this.gl.GraphPoints.Add(new GraphPoint((object) port.Bytes, phaseRevision.ToString()));
                        Debug.WriteLine("Plotted");
                        break;
                      default:
                        throw new ApplicationException("This Port column cannot be plotted.");
                    }
                  }
                }
              }
            }
          }
          break;
        case "ADTechnology.AbInitio.Classes.Vertex":
          this.vertex = (Vertex) this.obj;
          this.gl.LineInfo = this.vertex.Name + " (" + this.column + ")";
          for (int index = this.prj.Phases[this.phix].PhaseRevisions.Count - 1; index > -1; --index)
          {
            PhaseRevision phaseRevision = this.prj.Phases[this.phix].PhaseRevisions[index];
            foreach (Vertex vertex in (Collection<Vertex>) phaseRevision.Vertexes)
            {
              if (vertex.Name == this.vertex.Name)
              {
                switch (this.column)
                {
                  case "Skew":
                    this.gl.GraphPoints.Add(new GraphPoint((object) (long) vertex.Skew, phaseRevision.ToString()));
                    break;
                  case "CPU":
                    this.gl.GraphPoints.Add(new GraphPoint((object) (long) vertex.CPU, phaseRevision.ToString()));
                    break;
                  default:
                    throw new ApplicationException("This Vertex column cannot be plotted.");
                }
              }
            }
          }
          break;
        default:
          throw new ApplicationException("Unexpected type passed to PlotLine: expecting Port, Vertex or Flow.");
      }
      if (this.prj.Phases[this.phix].PhaseRevisions.Count < 2)
        throw new ApplicationException("The value does not have enough revision points to plot.\n\nThis may be caused by any of the following:\n    1) Only one revision has elapsed,\n    2) The log was not created with the AB_REPORT 'scroll' option in effect,  \n    3) The 'View->Merge Revisions' option is enabled.");
    }
  }
}
