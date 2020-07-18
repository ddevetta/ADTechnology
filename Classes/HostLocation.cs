// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.HostLocation
// Assembly: Classes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: CE1BA639-CFBF-46AF-9C7E-ED92C98B418E
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Classes.dll

using System;
using System.Xml.Serialization;

namespace ADTechnology.Classes
{
  [Serializable]
  public class HostLocation
  {
    private EncryptedString pass = (EncryptedString) "";
    private string name;
    private string host;
    private string user;
    private string path;

    [XmlText]
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

    [XmlAttribute]
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

    [XmlAttribute]
    public string User
    {
      get
      {
        return this.user;
      }
      set
      {
        this.user = value;
      }
    }

    [XmlIgnore]
    public string Password
    {
      get
      {
        return (string) this.pass;
      }
      set
      {
        this.pass = (EncryptedString) value;
      }
    }

    [XmlAttribute]
    public string EncryptedPassword
    {
      get
      {
        return this.pass.EncryptedValue;
      }
      set
      {
        this.pass.EncryptedValue = value;
      }
    }

    [XmlAttribute]
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

    public HostLocation()
    {
    }

    public HostLocation(string name, string host, string user, string password, string path)
    {
      this.name = name;
      this.host = host;
      this.user = user;
      this.pass = (EncryptedString) password;
      this.path = path;
    }

    public string Encrypt(string key)
    {
      return this.pass.Encrypt(key);
    }

    public string Encrypt(string key, string key2)
    {
      this.pass.Key2 = key2;
      return this.pass.Encrypt(key);
    }

    public string Decrypt(string key)
    {
      return this.pass.Decrypt(key);
    }

    public string Decrypt(string key, string key2)
    {
      this.pass.Key2 = key2;
      return this.pass.Decrypt(key);
    }
  }
}
