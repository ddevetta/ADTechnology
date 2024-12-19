// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.SaveDataTableDialog
// Assembly: SaveDataTableDialog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e5c8ad4bf3538416
// MVID: 8E9AD6D9-9826-485B-8F80-3147D34B95DE
// Assembly location: C:\Users\DaveAndTina\OneDrive - Sky\Projects\ldap..tion_0000000000000000_0001.0000_91e2ccba3687cdbf\SaveDataTableDialog.exe

using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ADTechnology.Classes;

namespace ADTechnology.Apps
{
    [ToolboxItem("Dialogs")]
    public class SaveDataTableDialog
    {
        private ExportDataType[] selectedTypes;

        private SaveFileDialog sfd;
        private ExportData ed;
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

        public DataSet ExportDataSet
        {
            get
            {
                return this.ed.ExportDataSet;
            }
            set
            {
                this.ed.ExportDataSet = value;
            }
        }

        /// <summary>
        /// An array of ExportDataTypes available to the user.
        /// They will be presented in the order of the array.
        /// </summary>
        public ExportDataType[] ExportDataTypes
        {
            get
            {
                return this.selectedTypes;
            }
            set
            {
                this.selectedTypes = value;
                setFilters();
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

        public long Save()
        {
            if (this.sfd.FilterIndex < 1)
                return 0;

            long bytesWritten = 0;
            using (StreamWriter fs = new StreamWriter(this.sfd.FileName))
            {
                ed.DataType = ExportData.TypeFilters[this.sfd.FilterIndex - 1].DataType;

                bytesWritten = this.ed.WriteToStream(fs.BaseStream);
                fs.Close();
            }
            ed.Close();

            return bytesWritten;
        }

        public long Save(DataSet ExportDataSet)
        {
            this.ed.ExportDataSet = ExportDataSet;
            return this.Save();
        }

        public long Save(DataTable ExportDataTable)
        {
            this.ed.ExportDataTable = ExportDataTable;
            return this.Save();
        }

        private void initialise()
        {
            this.sfd = new SaveFileDialog();
            this.ed = new ExportData();
            this.setFilters();
        }

        private void setFilters()
        {
            string[] filters;
            if (this.selectedTypes == null)
            {
                filters = new string[ExportData.TypeFilters.Length];
                for (int y = 0; y < ExportData.TypeFilters.Length; y++)
                    filters[y] = ExportData.TypeFilters[y].Display + "|" + ExportData.TypeFilters[y].Value;
            }
            else
            {
                filters = new string[this.selectedTypes.Length];
                for (int x = 0; x < this.selectedTypes.Length; x++)
                {
                    for (int y = 0; y < ExportData.TypeFilters.Length; y++)
                        if (ExportData.TypeFilters[y].DataType.Equals(selectedTypes[x]))
                            filters[x] = ExportData.TypeFilters[y].Display + "|" + ExportData.TypeFilters[y].Value;
                }
            }
            this.sfd.Filter = string.Join("|", filters);
            this.sfd.FilterIndex = 0;
        }
    }
}
