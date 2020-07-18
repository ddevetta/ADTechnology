// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.AB_REPORT
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.Diagnostics;

namespace ADTechnology.AbInitio.Classes
{
  internal class AB_REPORT
  {
    private bool flows = true;
    private bool processes = true;
    private int interval = 1;
    private string file = "";
    private string summary = "";
    private bool scroll = true;
    private bool spillage = false;
    private bool totals = false;
    private string skew = "";
    private bool filePercentages = false;
    private bool errorCodes = false;
    private bool showFolding = false;
    private bool splitCpu = false;
    private bool tableFlows = false;
    private bool verbose = false;

    public bool Flows
    {
      get
      {
        return this.flows;
      }
    }

    public bool Processes
    {
      get
      {
        return this.processes;
      }
    }

    public int Interval
    {
      get
      {
        return this.interval;
      }
    }

    public string File
    {
      get
      {
        return this.file;
      }
    }

    public string Summary
    {
      get
      {
        return this.summary;
      }
    }

    public bool Scroll
    {
      get
      {
        return this.scroll;
      }
    }

    public bool Spillage
    {
      get
      {
        return this.spillage;
      }
    }

    public bool Totals
    {
      get
      {
        return this.totals;
      }
    }

    public string Skew
    {
      get
      {
        return this.skew;
      }
    }

    public bool FilePercentages
    {
      get
      {
        return this.filePercentages;
      }
    }

    public bool ErrorCodes
    {
      get
      {
        return this.errorCodes;
      }
    }

    public bool ShowFolding
    {
      get
      {
        return this.showFolding;
      }
    }

    public bool SplitCpu
    {
      get
      {
        return this.splitCpu;
      }
    }

    public bool TableFlows
    {
      get
      {
        return this.tableFlows;
      }
    }

    public bool Verbose
    {
      get
      {
        return this.verbose;
      }
    }

    public AB_REPORT()
    {
    }

    public AB_REPORT(string parmValue)
    {
      this.Parse(parmValue);
    }

    public void Parse(string parmValue)
    {
      foreach (string message in parmValue.Replace("'", "").Split((char[]) null))
      {
        Debug.WriteLine(message);
        string[] strArray = message.Split('=');
        switch (strArray[0])
        {
          case "flows":
          case "monitor":
            this.flows = true;
            if (strArray.Length > 1)
            {
              int.TryParse(strArray[1], out this.interval);
              break;
            }
            break;
          case "processes":
          case "times":
            this.processes = true;
            if (strArray.Length > 1)
            {
              int.TryParse(strArray[1], out this.interval);
              break;
            }
            break;
          case "scroll":
            if (strArray.Length > 1 && strArray[1] == "false")
            {
              this.scroll = false;
              break;
            }
            break;
          case "interval":
            if (strArray.Length > 1)
            {
              int.TryParse(strArray[1], out this.interval);
              break;
            }
            break;
          case "file":
            if (strArray.Length > 1)
            {
              this.file = strArray[1];
              break;
            }
            break;
          case "skew":
            if (strArray.Length > 1)
            {
              this.skew = strArray[1];
              break;
            }
            break;
          case "summary":
            if (strArray.Length > 1)
            {
              this.summary = strArray[1];
              break;
            }
            break;
          case "spillage":
            this.spillage = true;
            break;
          case "totals":
            this.totals = true;
            break;
          case "file-percentages":
            this.filePercentages = true;
            break;
          case "error-codes":
            this.errorCodes = true;
            break;
          case "show-folding":
            this.showFolding = true;
            break;
          case "split-cpu":
            this.splitCpu = true;
            break;
          case "table-flows":
            this.tableFlows = true;
            break;
          case "verbose":
            this.verbose = true;
            break;
        }
      }
    }

    public override string ToString()
    {
      return string.Format("report flows            : {0}\nreport processes        : {1}\ninterval                : {2}\nscroll                  : {3}\n------------------------\nreport file path        : {4}\nsummary file path       : {5}\n------------------------\nshow disk spillage      : {6}\nprint totalline         : {7}\nskew threshold values   : {8}\nshow input file flow %  : {9}\nshow error codes        : {10}\nshow component folding  : {11}\nsplit cpu into user/sys : {12}\nshow lookup table flows : {13}\nverbose error reporting : {14}\n", (object) this.flows, (object) this.processes, (object) this.interval, (object) this.scroll, (object) this.file, (object) this.summary, (object) this.spillage, (object) this.totals, (object) this.skew, (object) this.filePercentages, (object) this.errorCodes, (object) this.showFolding, (object) this.splitCpu, (object) this.tableFlows, (object) this.verbose);
    }
  }
}
