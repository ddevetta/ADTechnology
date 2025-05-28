using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using ADTechnology.Classes;
using System.Diagnostics;
using Microsoft.Win32;

namespace ADTechnology.Apps
{
    /// <summary>
    /// Class which will take a DataSet containing one or more DataTables, and will use <see cref="ExportData"/> to create an export file of type <see cref="ExportDataType"/>, 
    /// then will use the installed machine file association to Open the 'rendered' file.
    /// The file is created using the system's default Temp space.
    /// Because the app used to open the file is invoked from the local machine, it doesn't make sense to invoke this function as a server process.
    /// To serve a rendered file as a web page download, use <see cref="ExportData"/> directly then push the resultant stream to the HTTP response.
    /// 
    /// The class will attempt to find an 'OpenReadOnly' action first; this is to prevent the opened document from being 'saved' back to temp.
    /// If no read-only action found, then it will use the default open action for the file type (extension).
    /// </summary>
    public class RenderDataTable
    {
        /// <summary>
        /// Gets or sets the type of the data that is to be rendered.
        /// </summary>
        /// <value>
        /// The type of the rendered data.
        /// </value>
        public ExportDataType DataType { get; set; }

        /// <summary>
        /// Gets or sets the dataset containing the DataTable(s) to be rendered.
        /// </summary>
        /// <value>
        /// The dataset.
        /// </value>
        public DataSet Dataset { get; set; }

        /// <summary>
        /// Gets or sets the temporary file retention period (seconds).
        /// The default is 10 seconds, just enough for the associated program to open the file
        /// </summary>
        /// <value>
        /// The temporary file retention period (seconds).
        /// </value>
        public int TempFileRetention { get; set; } = 10;

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">A dataset has not been supplied.</exception>
        public int Render()
        {
            if (Dataset == null)
                throw new Exception("A dataset has not been supplied.");

            return this._render();
        }

        /// <summary>
        /// Renders the specified dataset.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <returns></returns>
        public int Render(DataSet dataset)
        {
            this.Dataset = dataset;
            return this.Render();
        }

        /// <summary>
        /// Renders the specified data type.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <returns></returns>
        public int Render(ExportDataType dataType)
        {
            this.DataType = dataType;
            return this.Render();
        }

        /// <summary>
        /// Renders the specified dataset ti the specified type.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <returns></returns>
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
