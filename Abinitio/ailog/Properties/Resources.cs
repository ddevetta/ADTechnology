// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.Properties.Resources
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ADTechnology.AbInitio.Properties
{
  [CompilerGenerated]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) ADTechnology.AbInitio.Properties.Resources.resourceMan, (object) null))
          ADTechnology.AbInitio.Properties.Resources.resourceMan = new ResourceManager("ADTechnology.AbInitio.Properties.Resources", typeof (ADTechnology.AbInitio.Properties.Resources).Assembly);
        return ADTechnology.AbInitio.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return ADTechnology.AbInitio.Properties.Resources.resourceCulture;
      }
      set
      {
        ADTechnology.AbInitio.Properties.Resources.resourceCulture = value;
      }
    }
  }
}
