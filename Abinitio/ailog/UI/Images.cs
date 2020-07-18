// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.Images
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ADTechnology.AbInitio.UI
{
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Images
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Images()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Images.resourceMan, (object) null))
          Images.resourceMan = new ResourceManager("ADTechnology.AbInitio.UI.Images", typeof (Images).Assembly);
        return Images.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Images.resourceCulture;
      }
      set
      {
        Images.resourceCulture = value;
      }
    }

    internal static Bitmap dot_cmplt
    {
      get
      {
        return (Bitmap) Images.ResourceManager.GetObject(nameof (dot_cmplt), Images.resourceCulture);
      }
    }

    internal static Bitmap dot_failed
    {
      get
      {
        return (Bitmap) Images.ResourceManager.GetObject(nameof (dot_failed), Images.resourceCulture);
      }
    }

    internal static Bitmap dot_notstarted
    {
      get
      {
        return (Bitmap) Images.ResourceManager.GetObject(nameof (dot_notstarted), Images.resourceCulture);
      }
    }

    internal static Bitmap dot_running
    {
      get
      {
        return (Bitmap) Images.ResourceManager.GetObject(nameof (dot_running), Images.resourceCulture);
      }
    }
  }
}
