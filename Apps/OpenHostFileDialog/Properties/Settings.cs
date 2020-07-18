// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.Properties.Settings
// Assembly: OpenHostFileDialog1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: F8D282FE-1A5F-453A-946F-DF9521E5E170
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\OpenHostFileDialog1.dll

using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ADTechnology.Windows.Controls.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />")]
    public StringCollection Hosts
    {
      get
      {
        return (StringCollection) this[nameof (Hosts)];
      }
      set
      {
        this[nameof (Hosts)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" />")]
    public StringCollection Users
    {
      get
      {
        return (StringCollection) this[nameof (Users)];
      }
      set
      {
        this[nameof (Users)] = (object) value;
      }
    }

    private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
    {
    }

    private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
    {
    }
  }
}
