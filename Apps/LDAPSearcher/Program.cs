// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Apps.LDAPSearcher.Program
// Assembly: LDAPSearcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5AF5FD7-65F5-4CFF-9BBF-D18F59F6CCDD
// Assembly location: C:\Users\DaveAndTina\OneDrive - Sky\Projects\ldap..tion_0000000000000000_0001.0000_91e2ccba3687cdbf\LDAPSearcher.exe

using System;
using System.Windows.Forms;

namespace ADTechnology.Apps.LDAPSearcher
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new LDAPSearcherForm());
    }
  }
}
