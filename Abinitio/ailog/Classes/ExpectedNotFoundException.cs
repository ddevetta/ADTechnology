// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.ExpectedNotFoundException
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
  internal class ExpectedNotFoundException : ApplicationException
  {
    public int LineNumber;

    public ExpectedNotFoundException(string message, int lineNumber)
      : base(message)
    {
      this.LineNumber = lineNumber;
    }

    public ExpectedNotFoundException(string message, Exception innerException, int lineNumber)
      : base(message, innerException)
    {
      this.LineNumber = lineNumber;
    }
  }
}
