using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;

using ADTechnology.Classes;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace ADTechnology.Apps
{
    public class RenderDataTable
    {
        public ExportDataType DataType { get; set; }

        public DataSet Dataset { get; set; }

        public int TempFileRetention { get; set; } = 10;

        public int Render()
        {
            if (Dataset == null)
                throw new Exception("A dataset has not been supplied.");

            return this._render();
        }

        public int Render(DataSet dataset)
        {
            this.Dataset = dataset;
            return this.Render();
        }

        public int Render(ExportDataType dataType)
        {
            this.DataType = dataType;
            return this.Render();
        }

        public int Render(DataSet dataset, ExportDataType dataType)
        {
            this.Dataset = dataset;
            this.DataType = dataType;
            return this.Render();
        }

        private int _render()
        {
            int rc = 0;

            ExportData ed = new ExportData(this.Dataset);
            ed.DataType = this.DataType;

            string tempFile = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), this.DataType.ToString().ToLower()));
            Console.WriteLine(tempFile);

            using (FileStream fs = new FileStream(tempFile, FileMode.Append))
            {
                // Save the file to temp
                ed.WriteToStream(fs);
                fs.Close();
                ed.Close();
            }

            // Find a suitable command to open the temp file with, preferably read-only.
            string processCommand = string.Empty;
            string parms = string.Empty;

            // Get Default file action (from registry). If not found then exception

            FileAssociation fa = new FileAssociation();
            FileAssociationType typ = fa.Find(Path.GetExtension(tempFile));

            if (typ == null)
                throw new ApplicationException("Unable to render - Generated file does not have an associated default app defined on this machine.");

            if (typ.Actions != null)
            {
                // If there's an assigned read-only action then use that
                foreach (ClassAction action in typ.Actions)
                    if (action.Action.ToLower().Contains("readonly"))
                    {
                        processCommand = action.Command;
                        parms = action.Parms;
                    }

                // If not then use the default action if there is one
                if (processCommand == string.Empty && !string.IsNullOrEmpty(typ.DefaultShellAction))
                {
                    foreach (ClassAction action in typ.Actions)
                        if (action.Action == typ.DefaultShellAction)
                        {
                            processCommand = action.Command;
                            parms = action.Parms;
                        }
                }

                // If no default action defined, then find an 'Open' action
                if (processCommand == string.Empty)
                {
                    foreach (ClassAction action in typ.Actions)
                        if (action.Action.ToLower() == "open")
                        {
                            processCommand = action.Command;
                            parms = action.Parms;
                        }
                }
            }

            // If no suitable action was found then try opening the file by name and let the OS sort it out
            if (processCommand == string.Empty)
            {
                Process.Start(tempFile);
            }
            else
            {
                if (parms == string.Empty)
                    parms = tempFile;
                else
                {
                    if (parms.Contains("%1"))
                        parms = parms.Replace("%1", tempFile);
                    else
                        parms = parms + " " + tempFile;
                }
                Process.Start(processCommand, parms);
            }

            //// Open Excel in read-only mode, all others open by Process.Start
            //if (this.DataType == ExportDataType.Xlsx || this.DataType == ExportDataType.Csv)
            //{
            //    Excel.Application xcel = new Excel.Application();
            //    xcel.Workbooks.Open(Filename: tempFile, ReadOnly: true);
            //    xcel.Visible = true;
            //    File.Delete(tempFile);

            //}
            //else
            //{
            //    Process.Start(tempFile);
            //}

            //strm.Seek(0, SeekOrigin.Begin);
            //StreamReader sr = new StreamReader(strm);
            //fs.Write(sr.ReadToEnd());
            //SpreadsheetDocument doc;

            return rc;
        }
    }
}
