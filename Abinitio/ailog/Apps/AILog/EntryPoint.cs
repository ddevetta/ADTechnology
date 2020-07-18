// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Apps.AILog.EntryPoint
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.AbInitio.UI;
using System;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.Apps.AILog
{
  internal static class EntryPoint
  {
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(args.Length != 0 ? (Form) new Mainform(args[0]) : (Form) new Mainform());
    }
  }
}
