// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.EncryptedString
// Assembly: Classes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: CE1BA639-CFBF-46AF-9C7E-ED92C98B418E
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Classes.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ADTechnology.Classes
{
  public class EncryptedString
  {
    private string key2 = "ddv";
    private string val;
    private string encval;

    public EncryptedString(string value)
    {
      this.val = this.encval = value;
    }

    public string Value
    {
      get
      {
        return this.val;
      }
      set
      {
        this.val = this.encval = value;
      }
    }

    public string EncryptedValue
    {
      get
      {
        return this.encval;
      }
      set
      {
        this.encval = value;
      }
    }

    public string Key2
    {
      get
      {
        return this.key2;
      }
      set
      {
        this.key2 = value;
      }
    }

    public static implicit operator EncryptedString(string newval)
    {
      return new EncryptedString(newval);
    }

    public static implicit operator string(EncryptedString es)
    {
      return es.Value;
    }

    public string Encrypt(string key)
    {
      MD5 md5 = MD5.Create();
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.KeySize = 128;
      rijndaelManaged.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
      rijndaelManaged.IV = md5.ComputeHash(Encoding.UTF8.GetBytes(this.key2));
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write);
      cryptoStream.Write(Encoding.UTF8.GetBytes(this.val), 0, Encoding.UTF8.GetBytes(this.val).Length);
      cryptoStream.Close();
      this.encval = Convert.ToBase64String(memoryStream.ToArray());
      memoryStream.Close();
      return this.encval;
    }

    public string Decrypt(string key)
    {
      try
      {
        MD5 md5 = MD5.Create();
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.KeySize = 128;
        rijndaelManaged.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
        rijndaelManaged.IV = md5.ComputeHash(Encoding.UTF8.GetBytes(this.key2));
        MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(this.encval));
        CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Read);
        StreamReader streamReader = new StreamReader((Stream) cryptoStream);
        this.val = streamReader.ReadLine();
        streamReader.Close();
        cryptoStream.Close();
        memoryStream.Close();
      }
      catch (CryptographicException ex)
      {
        throw new EncryptedStringException("The value cannot be decrypted using the key supplied. The decryption key is invalid.", (Exception) ex);
      }
      return this.val;
    }
  }
}
