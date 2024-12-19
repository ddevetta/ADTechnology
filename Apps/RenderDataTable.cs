using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;

using ADTechnology.Classes;
using System.IO;

namespace ADTechnology.Apps
{
    public class RenderDataTable
    {
        public ExportDataType dataType { get; set; }

        public DataSet dataset { get; set; }

        public int Render()
        {
            if (dataset == null)
                throw new Exception("A dataset has not been supplied.");

            return this._render();
        }

        public int Render(DataSet dataset)
        {
            this.dataset = dataset;
            return this.Render();
        }

        public int Render(ExportDataType dataType)
        {
            this.dataType = dataType;
            return this.Render();
        }

        public int Render(DataSet dataset, ExportDataType dataType)
        {
            this.dataset = dataset;
            this.dataType = dataType;
            return this.Render();
        }

        private int _render()
        {
            int rc = 0;

            
            ExportData ed = new ExportData(dataset);
            using (MemoryStream ms = new MemoryStream())
            {
                ed.DataType = ExportDataType.Xlsx;
                //this.ed.WriteToStream(strm);

                ed.WriteToStream(ms);

                //strm.Seek(0, SeekOrigin.Begin);
                //StreamReader sr = new StreamReader(strm);
                //fs.Write(sr.ReadToEnd());
                //SpreadsheetDocument doc;

                ms.Close();
            }
            ed.Close();
            return rc;
        }
    }
}
