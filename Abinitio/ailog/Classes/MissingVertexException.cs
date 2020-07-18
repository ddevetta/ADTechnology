// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Classes.MissingVertexException
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System;

namespace ADTechnology.AbInitio.Classes
{
  internal class MissingVertexException : ApplicationException
  {
    public string VertexName;

    public MissingVertexException(string message, string vertexName)
      : base(message)
    {
      this.VertexName = vertexName;
    }
  }
}
