// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.EncryptedStringException
// Assembly: Classes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: CE1BA639-CFBF-46AF-9C7E-ED92C98B418E
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\Classes.dll

using System;

namespace ADTechnology.Classes
{
  public class EncryptedStringException : ApplicationException
  {
    public EncryptedStringException()
    {
    }

    public EncryptedStringException(string message)
      : base(message)
    {
    }

    public EncryptedStringException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
