// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.HostLocationFile
// Assembly: Classes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: CE1BA639-CFBF-46AF-9C7E-ED92C98B418E
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Classes.dll

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ADTechnology.Classes
{
  [Serializable]
  public class HostLocationFile
  {
    private string appname;
    [XmlArrayItem("LocationName")]
    [XmlArray("LocationList")]
    public HostLocationList HostLocations;

    [XmlElement]
    public string ApplicationName
    {
      get
      {
        return this.appname;
      }
      set
      {
        this.appname = value;
      }
    }

    public HostLocationFile()
    {
      this.HostLocations = new HostLocationList();
      this.appname = this.GetType().ToString();
    }

    public HostLocationFile(string applicationName)
    {
      this.HostLocations = new HostLocationList();
      this.appname = applicationName;
    }

    public string WriteXml()
    {
      MemoryStream memoryStream = new MemoryStream();
      new XmlSerializer(this.GetType()).Serialize((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) new UTF8Encoding(true)), (object) this);
      memoryStream.Position = 0L;
      StreamReader streamReader = new StreamReader((Stream) memoryStream);
      string end = streamReader.ReadToEnd();
      streamReader.Close();
      return end;
    }

    public static HostLocationFile ReadXml(Stream xml)
    {
      return (HostLocationFile) new XmlSerializer(typeof (HostLocationFile)).Deserialize(xml);
    }
  }
}
