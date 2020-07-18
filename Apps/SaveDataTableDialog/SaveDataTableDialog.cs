// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.SaveDataTableDialog
// Assembly: SaveDataTableDialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e5c8ad4bf3538416
// MVID: 8E9AD6D9-9826-485B-8F80-3147D34B95DE
// Assembly location: C:\Users\DaveAndTina\OneDrive - Sky\Projects\ldap..tion_0000000000000000_0001.0000_91e2ccba3687cdbf\SaveDataTableDialog.exe

using ADTechnology.Classes;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ADTechnology.Apps.SaveDataTable
{
  [ToolboxItem("Dialogs")]
  public class SaveDataTableDialog
  {
    private string[] filters = new string[5]
    {
      "Tab-delimited text (*.txt)|*.txt",
      "Comma-separated values (*.csv)|*.csv",
      "Pipe-delimited text (*.dat)|*.dat",
      "HTML (*.html)|*.html",
      "Xml (*.xml)|*.xml"
    };
    private DataTableConversionType[] conversionTypes = new DataTableConversionType[5]
    {
      DataTableConversionType.DelimitedText,
      DataTableConversionType.DelimitedText,
      DataTableConversionType.DelimitedText,
      DataTableConversionType.HTML,
      DataTableConversionType.XML
    };
    private string[] delims = new string[5]
    {
      "\t",
      ",",
      "|",
      ",",
      ","
    };
    private SaveFileDialog sfd;
    private DataTableConverter dtc;
    private DataTable dt;

    public DataTable Table
    {
      get
      {
        return this.dt;
      }
      set
      {
        this.dt = value;
      }
    }

    public Stream OutputStream
    {
      get
      {
        return this.dtc.OutputStream;
      }
      set
      {
        this.dtc.OutputStream = value;
      }
    }

    public bool AddExtension
    {
      get
      {
        return this.sfd.AddExtension;
      }
      set
      {
        this.sfd.AddExtension = value;
      }
    }

    public bool CheckFileExists
    {
      get
      {
        return this.sfd.CheckFileExists;
      }
      set
      {
        this.sfd.CheckFileExists = value;
      }
    }

    public bool CheckPathExists
    {
      get
      {
        return this.sfd.CheckPathExists;
      }
      set
      {
        this.sfd.CheckPathExists = value;
      }
    }

    public string FileName
    {
      get
      {
        return this.sfd.FileName;
      }
      set
      {
        this.sfd.FileName = value;
      }
    }

    public string[] FileNames
    {
      get
      {
        return this.sfd.FileNames;
      }
    }

    public int FilterIndex
    {
      get
      {
        return this.sfd.FilterIndex;
      }
      set
      {
        this.sfd.FilterIndex = value;
      }
    }

    public string InitialDirectory
    {
      get
      {
        return this.sfd.InitialDirectory;
      }
      set
      {
        this.sfd.InitialDirectory = value;
      }
    }

    public bool OverwritePrompt
    {
      get
      {
        return this.sfd.OverwritePrompt;
      }
      set
      {
        this.sfd.OverwritePrompt = value;
      }
    }

    public bool RestoreDirectory
    {
      get
      {
        return this.sfd.RestoreDirectory;
      }
      set
      {
        this.sfd.RestoreDirectory = value;
      }
    }

    public bool ShowHelp
    {
      get
      {
        return this.sfd.ShowHelp;
      }
      set
      {
        this.sfd.ShowHelp = value;
      }
    }

    public bool SupportMultiDottedExtensions
    {
      get
      {
        return this.sfd.SupportMultiDottedExtensions;
      }
      set
      {
        this.sfd.SupportMultiDottedExtensions = value;
      }
    }

    public string Title
    {
      get
      {
        return this.sfd.Title;
      }
      set
      {
        this.sfd.Title = value;
      }
    }

    public bool ValidateNames
    {
      get
      {
        return this.sfd.ValidateNames;
      }
      set
      {
        this.sfd.ValidateNames = value;
      }
    }

    public Stream OpenFile()
    {
      return this.sfd.OpenFile();
    }

    public DialogResult ShowDialog()
    {
      return this.sfd.ShowDialog();
    }

    public DialogResult ShowDialog(IWin32Window owner)
    {
      return this.sfd.ShowDialog(owner);
    }

    public SaveDataTableDialog()
    {
      this.initialise();
    }

    public SaveDataTableDialog(DataTable table)
    {
      this.initialise();
      this.dt = table;
    }

    public int Convert()
    {
      if (this.sfd.FilterIndex < 1)
        return 0;
      this.dtc.Table = this.dt;
      int index = this.sfd.FilterIndex - 1;
      return this.dtc.Convert(this.conversionTypes[index], this.delims[index]);
    }

    private void initialise()
    {
      this.sfd = new SaveFileDialog();
      this.sfd.Filter = string.Join("|", this.filters);
      this.sfd.FilterIndex = 0;
      this.dtc = new DataTableConverter();
    }
  }
}
