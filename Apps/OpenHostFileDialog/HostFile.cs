// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.HostFile
// Assembly: OpenHostFileDialog1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: F8D282FE-1A5F-453A-946F-DF9521E5E170
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\OpenHostFileDialog1.dll

namespace ADTechnology.Apps.OpenHostFileDialog
{
  public class HostFile
  {
    private string host;
    private string filename;
    private string path;

    public string Host
    {
      get
      {
        return this.host;
      }
      set
      {
        this.host = value;
      }
    }

    public string FileName
    {
      get
      {
        return this.filename;
      }
      set
      {
        this.filename = value;
      }
    }

    public string Path
    {
      get
      {
        return this.path;
      }
      set
      {
        this.path = value;
      }
    }

    public override string ToString()
    {
      if (this.host == null || this.filename == null || this.path == null)
        return "(unknown)";
      return (this.host == null ? "" : "//" + this.host) + (this.path == null ? "" : this.path) + (this.filename == null ? "" : "/" + this.filename);
    }
  }
}
