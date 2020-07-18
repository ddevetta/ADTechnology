// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.Options
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ADTechnology.AbInitio.Classes
{
  [Serializable]
  internal class Options : ISerializable
  {
    private bool autoRefresh = true;
    private bool getIntervalFromReportParms = false;
    private int interval = 60;
    private bool extraRefreshAfterCompletion = true;
    private bool mergeRevisions = false;
    private string errorvar = "AI_ERROR_FILE";
    private string summvar = "AI_SUMMARY_FILE";

    public bool AutoRefresh
    {
      get
      {
        return this.autoRefresh;
      }
      set
      {
        this.autoRefresh = value;
        this.OnOptionsChanged(new EventArgs());
      }
    }

    public bool GetIntervalFromReportParms
    {
      get
      {
        return this.getIntervalFromReportParms;
      }
      set
      {
        this.getIntervalFromReportParms = value;
        this.OnOptionsChanged(new EventArgs());
      }
    }

    public int Interval
    {
      get
      {
        return this.interval;
      }
      set
      {
        this.interval = value;
        this.OnOptionsChanged(new EventArgs());
      }
    }

    public bool ExtraRefreshAfterCompletion
    {
      get
      {
        return this.extraRefreshAfterCompletion;
      }
      set
      {
        this.extraRefreshAfterCompletion = value;
        this.OnOptionsChanged(new EventArgs());
      }
    }

    public bool MergeRevisions
    {
      get
      {
        return this.mergeRevisions;
      }
      set
      {
        this.mergeRevisions = value;
        this.OnOptionsChanged(new EventArgs());
      }
    }

    public string ErrorVariable
    {
      get
      {
        return this.errorvar;
      }
      set
      {
        this.errorvar = value;
        this.OnOptionsChanged(new EventArgs());
      }
    }

    public string SummaryVariable
    {
      get
      {
        return this.summvar;
      }
      set
      {
        this.summvar = value;
        this.OnOptionsChanged(new EventArgs());
      }
    }

    public event EventHandler OptionsChanged;

    public Options()
    {
    }

    public Options(SerializationInfo info, StreamingContext context)
    {
      this.autoRefresh = info.GetBoolean(nameof (autoRefresh));
      this.getIntervalFromReportParms = info.GetBoolean(nameof (getIntervalFromReportParms));
      this.interval = info.GetInt32(nameof (interval));
      this.extraRefreshAfterCompletion = info.GetBoolean(nameof (extraRefreshAfterCompletion));
      this.mergeRevisions = info.GetBoolean(nameof (mergeRevisions));
      this.errorvar = info.GetString(nameof (errorvar));
      this.summvar = info.GetString(nameof (summvar));
    }

    public void Serialize(string file)
    {
      FileStream fileStream = new FileStream(file, FileMode.Create);
      new BinaryFormatter().Serialize((Stream) fileStream, (object) this);
      fileStream.Close();
    }

    public static Options Deserialize(string file)
    {
      FileStream fileStream = (FileStream) null;
      Options options;
      try
      {
        fileStream = new FileStream(file, FileMode.Open);
        options = (Options) new BinaryFormatter().Deserialize((Stream) fileStream);
      }
      catch
      {
        return new Options();
      }
      finally
      {
        fileStream?.Close();
      }
      return options;
    }

    protected virtual void OnOptionsChanged(EventArgs e)
    {
      if (this.OptionsChanged == null)
        return;
      this.OptionsChanged((object) this, e);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.SetType(this.GetType());
      info.AddValue("autoRefresh", this.autoRefresh);
      info.AddValue("getIntervalFromReportParms", this.getIntervalFromReportParms);
      info.AddValue("interval", this.interval);
      info.AddValue("extraRefreshAfterCompletion", this.extraRefreshAfterCompletion);
      info.AddValue("mergeRevisions", this.mergeRevisions);
      info.AddValue("errorvar", (object) this.errorvar);
      info.AddValue("summvar", (object) this.summvar);
    }
  }
}
