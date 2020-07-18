// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.IO.LogFile
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.AbInitio.Classes;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;

namespace ADTechnology.AbInitio.IO
{
  internal class LogFile
  {
    private int line_q = 0;
    private long byte_q = 0;
    private long mod_q = 0;
    private long logsize = 0;
    private long ticksize = 0;
    private Project prj;
    private Options opts;
    private StreamReader lfile;
    private string startup;
    private ExecutionParameters ep;
    private string shutdown;
    private string logFile;
    private string errorFile;
    private string summFile;
    private AB_REPORT reportOptions;

    public Project Project
    {
      get
      {
        return this.prj;
      }
    }

    public string Startup
    {
      get
      {
        return this.startup;
      }
    }

    public ExecutionParameters ExecutionParameters
    {
      get
      {
        return this.ep;
      }
    }

    public string Shutdown
    {
      get
      {
        return this.shutdown;
      }
    }

    public string LogFileName
    {
      get
      {
        return this.logFile;
      }
      set
      {
        this.logFile = value;
      }
    }

    public string ErrorFile
    {
      get
      {
        return this.errorFile;
      }
    }

    public string SummaryFile
    {
      get
      {
        return this.summFile;
      }
    }

    public AB_REPORT ReportOptions
    {
      get
      {
        return this.reportOptions;
      }
    }

    public StreamReader LogFileStreamReader
    {
      get
      {
        return this.lfile;
      }
    }

    public LogFile()
    {
      this.ep = new ExecutionParameters();
    }

    public Project Parse(Options options, StreamReader logfile, long logfilesize)
    {
      this.opts = options;
      this.lfile = logfile;
      this.logsize = logfilesize;
      this.ticksize = this.logsize / 10L + 1L;
      return this.parse();
    }

    public event ProgressTickEventHandler ProgressTick;

    protected virtual void OnProgressTick(ProgressTickEventArgs e)
    {
      if (this.ProgressTick == null)
        return;
      this.ProgressTick((object) this, e);
    }

    private Project parse()
    {
      if (this.lfile == null || !this.lfile.BaseStream.CanRead)
        throw new InvalidLogFileStreamException("The log file stream cannot be read.");
      this.prj = new Project(this.opts);
      this.byte_q = 0L;
      this.line_q = 0;
      this.prj.Status = ProjectStatus.NotStarted;
      string str1 = this.readNextLine();
      if (str1 == null || str1 != LogFile.Expects.FirstLine)
        throw new ExpectedNotFoundException("The log file was not recognised as a valid Ab Initio log.", this.line_q);
      this.ep.Rows.Clear();
      this.startup = this.shutdown = "";
      string str2 = this.readNextLine();
      char[] separator = new char[1]{ '=' };
      for (; str2 != null && str2 != LogFile.Expects.EndOfInitialisation; str2 = this.readNextLine())
      {
        string[] pair = str2.Split(separator, 2);
        this.ep.Add(pair);
        if (pair[0] == this.opts.ErrorVariable)
          this.errorFile = pair[1];
        else if (pair[0] == this.opts.SummaryVariable)
          this.summFile = pair[1];
        else if (pair[0] == "AB_REPORT")
        {
          this.reportOptions = new AB_REPORT();
          this.reportOptions.Parse(pair[1]);
          if (this.opts.GetIntervalFromReportParms)
            this.opts.Interval = this.reportOptions.Interval;
        }
      }
      if (str2 == null)
        throw new ExpectedNotFoundException("End of data was reached before the end of the execution environment section was reached.", this.line_q);
      string str3 = this.readNextLine();
      int num = 0;
      for (; str3 != null && str3 != LogFile.Expects.DashLine; str3 = this.readNextLine())
      {
        ++num;
        LogFile logFile = this;
        logFile.startup = logFile.startup + str3 + "\r\n";
      }
      if (str3 == null)
        throw new ExpectedNotFoundException("End of data was reached before the end of the startup section was reached.", this.line_q);
      PhaseRevision phaseRevision1 = (PhaseRevision) null;
      Vertex vertex1 = (Vertex) null;
      Flow flow1 = (Flow) null;
      string line1 = this.readNextLine();
      while (line1 != null)
      {
        Phase phase1;
        try
        {
          phase1 = this.prj.AddOrModifyPhase(line1);
        }
        catch (Exception ex)
        {
          throw new ExpectedNotFoundException(ex.Message, ex, this.line_q);
        }
        if (phase1 == null)
        {
          do
          {
            LogFile logFile = this;
            logFile.shutdown = logFile.shutdown + line1 + "\r\n";
            line1 = this.readNextLine();
          }
          while (line1 != null);
          break;
        }
        Phase phase2 = phase1;
        try
        {
          phaseRevision1 = phase2.AddOrModifyPhaseRevision(line1, this.opts.MergeRevisions);
        }
        catch (Exception ex)
        {
          throw new ExpectedNotFoundException(ex.Message, ex, this.line_q);
        }
        if (this.prj.Start == DateTime.MinValue && phaseRevision1 != null)
          this.prj.Start = phaseRevision1.StatusTime;
        string str4 = this.readNextLine();
        if (str4 == LogFile.Expects.VertexHeader || str4 == LogFile.Expects.VertexHeader2)
        {
          for (string line2 = this.readNextLine(); line2 != null && line2 != LogFile.Expects.DashLine; line2 = this.readNextLine())
          {
            try
            {
              vertex1 = phaseRevision1.AddOrModifyVertex(line2);
            }
            catch (Exception ex)
            {
              throw new ExpectedNotFoundException(ex.Message, ex, this.line_q);
            }
          }
        }
        line1 = this.readNextLine();
        if (line1.Substring(0, 60) == LogFile.Expects.FlowHeader)
        {
          for (line1 = this.readNextLine(); line1 != null && line1 != LogFile.Expects.DashLine; line1 = this.readNextLine())
          {
            try
            {
              flow1 = phaseRevision1.AddOrModifyFlow(line1);
            }
            catch (Exception ex)
            {
              throw new ExpectedNotFoundException(ex.Message, ex, this.line_q);
            }
          }
        }
        while (line1 == LogFile.Expects.DashLine)
          line1 = this.readNextLine();
      }
      if (phaseRevision1 != null)
      {
        this.prj.Status = this.prj.Phases.Count <= 1 || phaseRevision1.Status != PhaseStatus.Started ? (ProjectStatus) phaseRevision1.Status : ProjectStatus.Running;
        if (this.prj.Status == ProjectStatus.Ended || this.prj.Status == ProjectStatus.Error)
          this.prj.End = phaseRevision1.StatusTime;
      }
      foreach (Phase phase in (CollectionBase) this.prj.Phases)
      {
        foreach (PhaseRevision phaseRevision2 in (CollectionBase) phase.PhaseRevisions)
        {
          foreach (Flow flow2 in (Collection<Flow>) phaseRevision2.Flows)
          {
            foreach (Port port in (CollectionBase) flow2.Ports)
            {
              Vertex vertex2 = phaseRevision2.Vertexes.Find(port.VertexName);
              if (vertex2 == null)
                throw new MissingVertexException("A vertex referenced in a flow was not found.", port.VertexName);
              vertex2.AddIfNotExists(port);
            }
          }
        }
      }
      foreach (Phase phase in (CollectionBase) this.prj.Phases)
      {
        foreach (PhaseRevision phaseRevision2 in (CollectionBase) phase.PhaseRevisions)
        {
          int ord = 1;
          foreach (Flow flow2 in (Collection<Flow>) phaseRevision2.Flows)
          {
            if (flow2.StartPoint)
              this.setOrder(flow2, ref ord);
          }
          foreach (Vertex vertex2 in (Collection<Vertex>) phaseRevision2.Vertexes)
          {
            if (vertex2.StartPoint)
              this.setOrder(vertex2, ref ord);
          }
        }
      }
      return this.prj;
    }

    private void setOrder(Vertex v, ref int ord)
    {
      if (v.Order == 0)
      {
        v.Order = ord;
        ++ord;
      }
      foreach (Port port in (CollectionBase) v.Ports)
      {
        if (port.Direction != PortDirection.In)
          this.setOrder(port.Flow, ref ord);
      }
    }

    private void setOrder(Flow f, ref int ord)
    {
      if (f.Order == 0)
      {
        f.Order = ord;
        ++ord;
      }
      foreach (Port port in (CollectionBase) f.Ports)
      {
        if (port.Direction == PortDirection.In)
          this.setOrder(port.Vertex, ref ord);
      }
    }

    private string readNextLine()
    {
      string str = this.lfile.ReadLine();
      if (str == null)
        return (string) null;
      ++this.line_q;
      this.byte_q += (long) (str.Length + 1);
      if (this.ticksize > 0L)
      {
        this.mod_q += (long) (str.Length + 1);
        if (this.mod_q > this.ticksize)
        {
          this.OnProgressTick(new ProgressTickEventArgs(this.byte_q));
          this.mod_q = 0L;
        }
      }
      return str;
    }

    private sealed class Expects
    {
      public static string FirstLine = "#vvvvvvvvvvvvvvvvvv Execution Environment vvvvvvvvvvvvvvvvvv#";
      public static string EndOfInitialisation = "#^^^^^^^^^^^^^^^^^^ Execution Environment ^^^^^^^^^^^^^^^^^^#";
      public static string DashLine = "--------------------------------------------------------------------------------";
      public static string VertexHeader = "                        CPU Time  Status Skew Vertex";
      public static string VertexHeader2 = "                         CPU Time  Status Skew Vertex";
      public static string FlowHeader = "       Data Bytes         Records      Status      Skew Flow";
    }
  }
}
