using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;

using System.IO.Packaging;  // OpenXml zippers (reference WindowsBase)
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ADTechnology.Classes
{
    /// <summary>
    /// Supported export data types (formats).
    /// </summary>
    public enum ExportDataType
    {
         Xlsx,
         Csv, 
         Txt,
         Dat,
         Html,
         Xml
    }

    /// <summary>Class of the static readonly ExportDataTypeEntry array.</summary>
    public class ExportDataTypeEntry
    {
        public ExportDataTypeEntry() { }
        public ExportDataTypeEntry(ExportDataType dataType, string display, string value, string contentType)
        {
            DataType = dataType;
            Display = display;
            Value = value;
            ContentType = contentType;
        }

        /// <summary>The ExportDataType of this entry</summary>
        public ExportDataType DataType { get; }
        
        /// <summary>Gets the display value used for drop-down lists.</summary>
        public string Display { get; }
        
        /// <summary>Gets the value (file filter) used for drop-down lists.</summary>
        public string Value { get; }
        
        /// <summary>Gets the mime-type of the content, for writing to http stream.</summary>
        public string ContentType { get; }
    }

    /// <summary>Class for Exporting data from a DataTable to a stream in various data formats.</summary>
    public class ExportData
    {
        private DataSet ds;
        private DataTable dt;
        private Stream strm;
        private ExportDataType typ = ExportDataType.Csv;    // default to csv
        private bool noColumnHeaders = false;

        private System.Drawing.Font fontStd = new System.Drawing.Font("Tahoma", 10);                    // Standard font
        private System.Drawing.Font fontBld = new System.Drawing.Font("Tahoma", 10, FontStyle.Bold);    // Bold font
        private System.Drawing.Font fontHdr = new System.Drawing.Font("Tahoma", 12, FontStyle.Bold);    // Heading Line font

        /// <summary>
        /// Supported data types dropdown entries (static, readonly).
        /// </summary>
        public static readonly ExportDataTypeEntry[] TypeFilters =
        {
            new ExportDataTypeEntry(ExportDataType.Xlsx, "Excel (*.xlsx)", "*.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"),
            new ExportDataTypeEntry(ExportDataType.Csv, "Comma-separated values (*.csv)", "*.csv", "text/csv"),
            new ExportDataTypeEntry(ExportDataType.Txt, "Tab-delimited text (*.txt)", "*.txt", "text/octet"),
            new ExportDataTypeEntry(ExportDataType.Dat, "Pipe-delimited text (*.dat)", "*.dat", "text/dat"),
            new ExportDataTypeEntry(ExportDataType.Html, "HTML (*.html)", "*.html", "text/html"),
            new ExportDataTypeEntry(ExportDataType.Xml, "Xml (*.xml)", "*.xml", "text/xml")
        };

        /// <summary>Gets or sets the format type of the Export data.</summary>
        /// <value>The format type required.</value>
        public ExportDataType DataType
        {
            get { return typ; }
            set { typ = value; }
        }

        /// <summary>
        /// The Data Set containing the table(s) to be exported.
        /// </summary>
        public DataSet ExportDataSet
        {
            get { return ds; }
            set
            {
                ds = value;
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
        }

        /// <summary>
        /// The DataTable to be exported if only exporting one table. Can use this or specify an ExportDataSet containing one (or more) tables to export.
        /// </summary>
        public DataTable ExportDataTable
        {
            get { return dt; }
            set
            {
                dt = value;
                ds = new DataSet();
                ds.Tables.Add(dt);
            }
        }

        /// <summary>
        /// If set to true, suppresses render of column-headers to the output. Default is false.
        /// </summary>
        public bool SuppressColumnHeaders
        {
            get { return noColumnHeaders; } 
            set { noColumnHeaders = value; }
        }

        /// <summary>
        /// Returns the mime content-type related to the ExportDataType selected, for use in building mime headers.
        /// </summary>
        public string ContentType
        {
            get { return ExportData.TypeFilters[(int)typ].ContentType; }
        }

        /// <summary>
        /// Gets the file extension related to the selected ExportDataType.
        /// </summary>
        public string DataTypeExtension
        {
            get { return "." + typ.ToString().ToLower(); }
        }

        public ExportData()
        {
        }

        /// <summary>
        /// Constructor initialising the ExportDataSet property.
        /// </summary>
        /// <param name="dataSet">The DataSet containing table(s) to be exported.</param>
        /// <exception cref="Exception">Export DataSet is Null</exception>
        public ExportData(DataSet dataSet)
        {
            if (dataSet == null)
                throw new Exception("Export DataSet is Null");

            ds = dataSet;
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
        }

        /// <summary>
        /// Constructor initialising the ExportDataTable property.
        /// </summary>
        /// <param name="dataTable"></param>
        public ExportData(DataTable dataTable)
        {
            dt = dataTable;
            ds = new DataSet();
            ds.Tables.Add(dt);
        }

        /// <summary>
        /// The main methot which will serialise the table(s) into the ExportDataType format out to a supplied stream.
        /// </summary>
        /// <param name="stream">The Stream which will be used to write the formatted data.</param>
        /// <returns></returns>
        /// <exception cref="Exception">ExportDataTable is Null or the ExportDataSet is empty.</exception>
        /// <exception cref="Exception">The supplied stream is null or cannot be written to.</exception>
        public long WriteToStream(Stream stream)
        {
            if (dt == null)
                throw new Exception("ExportDataTable is null or the ExportDataSet is empty.");
            if (stream == null || !stream.CanWrite)
                throw new Exception("The supplied stream is null or cannot be written to.");

            strm = stream;

            switch (this.typ)
            {
                // OpenXml
                case ExportDataType.Xlsx:
                    doXlsx();
                    break;
                // Html
                case ExportDataType.Html:
                    doHtml();
                    break;
                // Xml
                case ExportDataType.Xml:
                    doXml();
                    break;
                // Csv
                case ExportDataType.Csv:
                    doDelimited(",");
                    break;
                // Txt
                case ExportDataType.Txt:
                    doDelimited("\t");
                    break;
                // Dat
                case ExportDataType.Dat:
                    doDelimited("|");
                    break;
                // Not defined
                default:
                    doError("Exporter error - selected type is not supported.");
                    break;
            }

            return strm.Length;

        }

        /// <summary>
        /// Flushes, closes and disposes of the stream.
        /// </summary>
        public void Close()
        {
            if (strm != null && strm.CanSeek)
            {
                strm.Flush();
                strm.Close();
                strm.Dispose();
            }
        }

        private void doError(string msg)
        {
            StreamWriter stw = new StreamWriter(strm);
            stw.WriteLine(msg);
        }

        private void doDelimited(string delimiter)
        {
            // csv = comma-separated, txt = tab delimited, dat = pipe delimited

            StreamWriter stw = new StreamWriter(strm);

            foreach (DataTable dt in ds.Tables)
            {
                string colHeaders = string.Empty;
                string formatMask = string.Empty;

                int x = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.Caption == null)
                        colHeaders += (delimiter + "\"" + c.ColumnName + "\"");
                    else
                        colHeaders += (delimiter + "\"" + c.Caption + "\"");

                    switch (c.DataType.Name)
                    {
                        case "String":
                            formatMask += (delimiter + "\"{" + x.ToString() + "}\"");
                            break;
                        case "DateTime":
                            formatMask += (delimiter + "{" + x.ToString() + ":d}");
                            break;
                        case "Decimal":
                        case "Int16":
                        case "Int32":
                            if (delimiter == ",")
                                formatMask += (delimiter + "{" + x.ToString() + ":#0}"); // no thousands-separators for csv
                            else
                                formatMask += (delimiter + "{" + x.ToString() + ":#,#0}");
                            break;
                        case "Single":
                        case "Double":
                            if (delimiter == ",")
                                formatMask += (delimiter + "{" + x.ToString() + ":#0.00}"); // no thousands-separators for csv
                            else
                                formatMask += (delimiter + "{" + x.ToString() + ":#,#0.00}");
                            break;
                        default:
                            formatMask += (delimiter + "{" + x.ToString() + "}");
                            break;
                    }
                    x++;
                }

                stw.WriteLine(colHeaders.Trim(delimiter.ToCharArray()));

                formatMask = formatMask.Trim(delimiter.ToCharArray());

                foreach (DataRow r in dt.Rows)
                    stw.WriteLine(string.Format(formatMask, r.ItemArray));
            }
            stw.Flush();
        }

        private void doXml()
        {
            // to xml

            ds.WriteXml(strm, XmlWriteMode.WriteSchema);
        }

        private void doHtml()
        {

            // to html

            StreamWriter stw = new StreamWriter(strm);

            StringWriter sr = new StringWriter();
            System.Web.UI.Html32TextWriter HtmlTw = new System.Web.UI.Html32TextWriter(sr);
            foreach (DataTable dt in ds.Tables)
            {
                System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();
                gv.DataSource = dt;
                gv.DataBind();
                gv.RenderControl(HtmlTw);
            }
            stw.Write(sr.ToString());
            stw.Flush();
        }

        private void doXlsx()
        {
            const double minNumberColWidth = 12D;
            const double maxColWidth = 120D;

            MemoryStream ms = new MemoryStream();
            SpreadsheetDocument doc = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook);

            // Add part
            WorkbookPart wbpart = doc.AddWorkbookPart();
            Workbook workbook = wbpart.Workbook = new Workbook();
            workbook.Append(new BookViews(new WorkbookView()));

            // Add stylesheet
            WorkbookStylesPart wbsp = wbpart.AddNewPart<WorkbookStylesPart>();
            Stylesheet ss = wbsp.Stylesheet = GenerateStylesheet();
            Sheets sheets = new Sheets();

            ss.Save();

            string prevTabName = "null";
            WorksheetPart sheetPart = null;
            SheetData sheetData = null;
            Worksheet worksheet = null;

            double freezeRows = 0D;
            int colCount = 0;
            double[] colWidth = new double[1000];
            CellValues[] colDataType = new CellValues[1000];
            UInt32[] colStyleIndex = new UInt32[1000];

            UInt32 sheetId = 1;
            foreach (DataTable d in ds.Tables)
            {
                dt = d;

                // Create a new worksheet if this is the first query, or on change of tab name
                // This provides a mechanism of 'stacking' results on the same tab - give them the same tab name.
                string tabName = (dt.ExtendedProperties["tabname"] == null) ? dt.TableName : dt.ExtendedProperties["tabname"].ToString();

                if (worksheet == null || prevTabName == null || tabName != prevTabName)
                {
                    // Complete previous tab
                    if (sheetData != null)
                    {
                        PostPreviousSheet(sheetPart, sheetData, worksheet, freezeRows, colCount, colWidth);
                    }
                    sheetPart = wbpart.AddNewPart<WorksheetPart>();
                    sheetData = new SheetData();
                    worksheet = new Worksheet(sheetData);
                    sheetPart.Worksheet = worksheet;

                    freezeRows = 0D;

                    // Append a new worksheet and associate it with the workbook.
                    Sheet sheet = new Sheet();
                    sheet.Id = doc.WorkbookPart.GetIdOfPart(sheetPart);
                    sheet.SheetId = sheetId++;
                    sheet.Name = tabName;
                    sheets.Append(sheet);

                    colWidth.Initialize();
                    colDataType.Initialize();
                    colStyleIndex.Initialize(); 
                }
                else  // If continuing on the same page, leave a blank line before rendering this result set
                {
                    sheetData.Append(new Row());
                }

                // Create Heading if present
                if (dt.ExtendedProperties["heading"] != null)
                {
                    Row headingRow = new Row();
                    Cell headingCell = new Cell();
                    headingCell.DataType = CellValues.String;
                    headingCell.CellValue = new CellValue(dt.ExtendedProperties["heading"].ToString());
                    headingCell.StyleIndex = 2;  // Heading style
                    headingRow.Append(headingCell);
                    headingRow.Height = fontHdr.Size * 2;
                    headingRow.CustomHeight = true;
                    sheetData.Append(headingRow);
                    if (tabName != prevTabName)
                        freezeRows += 1D;
                }

                // Create SubHeading if present
                if (dt.ExtendedProperties["subheading"] != null)
                {
                    Row headingRow = new Row();
                    Cell headingCell = new Cell();
                    headingCell.DataType = CellValues.String;
                    headingCell.CellValue = new CellValue(dt.ExtendedProperties["subheading"].ToString());
                    headingCell.StyleIndex = 1;  // Bold style
                    headingRow.Append(headingCell);
                    headingRow.Height = fontBld.Size * 2;
                    headingRow.CustomHeight = true;
                    sheetData.Append(headingRow);
                    if (tabName != prevTabName)
                        freezeRows += 1D;
                }

                Graphics graphics = Graphics.FromImage(new Bitmap(2000, 200));

                // Save column format array and create column header if required
                Row headerRow = new Row();

                colCount = dt.Columns.Count;
                for (int x = 0; x < colCount; x++)
                {
                    Cell headerCell = new Cell();
                    headerCell.DataType = CellValues.String;
                    if (dt.Columns[x].Caption == null)
                        headerCell.CellValue = new CellValue(dt.Columns[x].ColumnName);
                    else
                        headerCell.CellValue = new CellValue(dt.Columns[x].Caption);

                    switch (dt.Columns[x].DataType.Name)
                    {
                        case "Decimal":
                        case "Int16":
                        case "Int32":
                            headerCell.StyleIndex = 6;          // Bold rightalign
                            colStyleIndex[x] = 3;               // rightalign
                            colDataType[x] = CellValues.Number;
                            colWidth[x] = minNumberColWidth;    // Pre-load the minimum decimal column width
                            break;
                        case "Single":
                        case "Double":
                            headerCell.StyleIndex = 6;          // Bold rightalign
                            colStyleIndex[x] = 7;               // rightalign 2 decimal places
                            colDataType[x] = CellValues.Number;
                            colWidth[x] = minNumberColWidth;    // Pre-load the minimum decimal column width
                            break;
                        case "DateTime":
                            headerCell.StyleIndex = 1;          // Bold leftalign
                            colStyleIndex[x] = 4;               // Date
                            colDataType[x] = CellValues.String;
                            break;
                        default:
                            headerCell.StyleIndex = 1;          // Bold leftalign
                            colStyleIndex[x] = 5;               // for some reason index=0 comes out as 'general' regardless of what's loaded in occurrence 0
                            colDataType[x] = CellValues.String;
                            break;
                    }

                    if (!this.noColumnHeaders)
                    {
                        colWidth[x] = maxWidth(colWidth[x], maxColWidth, headerCell.CellValue.Text, fontBld, graphics);
                        headerRow.Append(headerCell);
                    }
                }
                if (!this.noColumnHeaders)
                {
                    headerRow.Height = fontBld.Size * 2;
                    headerRow.CustomHeight = true;
                    sheetData.Append(headerRow);
                    if (tabName != prevTabName)
                        freezeRows += 1D;
                }

                // Detail rows
                for (int y = 0; y < dt.Rows.Count; y++)
                {
                    Row dataRow = new Row();
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        Cell dataCell = new Cell();
                        dataCell.StyleIndex = colStyleIndex[x];
                        dataCell.DataType = colDataType[x];
                        dataCell.CellValue = new CellValue(dt.Rows[y][x].ToString().Replace(" 00:00:00", ""));
                        dataRow.Append(dataCell);
                        if (x < colWidth.Length && y < 50) // sample first 50 rows only
                            colWidth[x] = maxWidth(colWidth[x], maxColWidth, dataCell.CellValue.Text, fontStd, graphics);
                    }
                    sheetData.Append(dataRow);
                }
                prevTabName = tabName;
            }

            PostPreviousSheet(sheetPart, sheetData, worksheet, freezeRows, colCount, colWidth);

            workbook.AppendChild<Sheets>(sheets);
            workbook.Save();
            doc.Dispose();

            ms.WriteTo(strm);

            ms.Close();
            ms.Dispose();
        }

        private static void PostPreviousSheet(WorksheetPart sheetPart, SheetData sheetData, Worksheet worksheet, double freezeRows, int colCount, double[] colWidth)
        {
            // Freeze header row(s)
            SheetViews sheetViews1 = new SheetViews();
            SheetView sheetView1 = new SheetView() { WorkbookViewId = (UInt32Value)0U };
            string selectedCell = "A" + ((int)(freezeRows + 1)).ToString();
            Pane pane1 = new Pane() { VerticalSplit = freezeRows, TopLeftCell = selectedCell, ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen };
            Selection selection1 = new Selection() { Pane = PaneValues.BottomLeft, ActiveCell = selectedCell, SequenceOfReferences = new ListValue<StringValue>() { InnerText = selectedCell } };
            sheetView1.Append(pane1);
            sheetView1.Append(selection1);
            sheetViews1.Append(sheetView1);
            worksheet.InsertBefore(sheetViews1, sheetData);

            // Add the columns with their calculated widths
            Columns cols = new Columns();
            for (uint x = 1; x <= colCount; x++)
            {
                Column col = new Column() { Min = (UInt32Value)x, Max = (UInt32Value)x, Width = (DoubleValue)colWidth[x - 1], BestFit = true, CustomWidth = true };
                cols.Append(col);
            }
            worksheet.InsertBefore(cols, sheetData);
        }

        private Stylesheet GenerateStylesheet()
        {
            // This code is generated by the OpenXML Productivity tool. It's a hell of a lot of stuff just to get bold headings but there you go.
            // and you need the borders and fills too, even though they're not used. 

            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)3U };

            // Index 0 - standard font
            DocumentFormat.OpenXml.Spreadsheet.Font font1 = new DocumentFormat.OpenXml.Spreadsheet.Font();
            font1.Append(new FontSize() { Val = fontStd.Size });
            font1.Append(new FontName() { Val = fontStd.Name });

            // Index 1 - bold font
            DocumentFormat.OpenXml.Spreadsheet.Font font2 = new DocumentFormat.OpenXml.Spreadsheet.Font();
            font2.Append(new Bold() { Val = true });
            font2.Append(new FontSize() { Val = fontBld.Size });
            font2.Append(new FontName() { Val = fontBld.Name });

            // Index 2 - bold heading font
            DocumentFormat.OpenXml.Spreadsheet.Font font3 = new DocumentFormat.OpenXml.Spreadsheet.Font();
            font3.Append(new Bold() { Val = true });
            font3.Append(new FontSize() { Val = fontHdr.Size });
            font3.Append(new FontName() { Val = fontHdr.Name });

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);

            Fills fills1 = new Fills() { Count = (UInt32Value)2U };

            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };

            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };

            fill2.Append(patternFill2);

            fills1.Append(fill1);
            fills1.Append(fill2);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder1);
            border1.Append(rightBorder1);
            border1.Append(topBorder1);
            border1.Append(bottomBorder1);
            border1.Append(diagonalBorder1);
            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat fmt = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };
            cellStyleFormats1.Append(fmt);

            NumberingFormats numberingFormats1 = new NumberingFormats() { Count = (UInt32Value)1U };
            NumberingFormat numberingFormat1 = new NumberingFormat() { NumberFormatId = (UInt32Value)164U, FormatCode = "#,##0.00" };
            numberingFormats1.Append(numberingFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)7U };
            // Style Index 0 - text, standard
            CellFormat cellFormat0 = new CellFormat() { NumberFormatId = (UInt32Value)49U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
            // Style Index 1 - text, bold
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)49U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true };
            // Style Index 2 - text, heading
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)49U, FontId = (UInt32Value)2U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true };
            // Style Index 3 - number, standard, rightalign, comma thousands
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)3U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
            cellFormat3.Append(new Alignment() { Horizontal = HorizontalAlignmentValues.Right });
            // Style Index 4 - date, standard
            //CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)22U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
            // Style Index 5 - text, standard
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)49U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
            // Style Index 6 - bold, rightalign
            CellFormat cellFormat6 = new CellFormat() { NumberFormatId = (UInt32Value)49U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true, ApplyFont = true };
            cellFormat6.Append(new Alignment() { Horizontal = HorizontalAlignmentValues.Right });
            // Style Index 7 - number, standard, rightalign, comma thousands, currency
            CellFormat cellFormat7 = new CellFormat() { NumberFormatId = (UInt32Value)164U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyNumberFormat = true };
            cellFormat7.Append(new Alignment() { Horizontal = HorizontalAlignmentValues.Right });

            cellFormats1.Append(cellFormat0);
            cellFormats1.Append(cellFormat1);
            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);
            cellFormats1.Append(cellFormat7);

            //CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            //CellStyle cellStyle1 = new CellStyle() { Name = "General", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };
            //cellStyles1.Append(cellStyle1);

            stylesheet1.Append(numberingFormats1);
            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            //stylesheet1.Append(cellStyles1);
            return stylesheet1;
        }


        private double maxWidth(double width, double max, string text, System.Drawing.Font font, Graphics graphics)
        {
            double d = Math.Truncate((graphics.MeasureString(text, font).Width + font.Size) / (font.Size - 3) * 256) / 256;
            if (d > max)
                return max;
            else
                if (d > width)
                return d;
            else
                return width;
        }
    }
}
