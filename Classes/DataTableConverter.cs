// Decompiled with JetBrains decompiler
// Type: ADTechnology.Classes.DataTableConverter
// Assembly: Classes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3691c68ab77a0ae2
// MVID: 309EFA5C-EC20-477A-A536-24A691034D41
// Assembly location: C:\Users\DaveAndTina\OneDrive - Sky\Projects\ldap..tion_0000000000000000_0001.0000_91e2ccba3687cdbf\Classes.dll

using System;
using System.Data;
using System.IO;
using System.Text;

namespace ADTechnology.Classes
{
  public class DataTableConverter
  {
    private string delim = ",";
    private string textdelim = "\"";
    private bool includeColumnHeader = true;
    private byte[] crlf = Encoding.UTF8.GetBytes("\r\n");
    private Stream stream;
    private int bytes_written;
    public DataTable Table;

    public Stream OutputStream
    {
      get
      {
        return this.stream;
      }
      set
      {
        this.stream = value;
      }
    }

    public string Delimiter
    {
      get
      {
        return this.delim;
      }
      set
      {
        this.delim = value;
      }
    }

    public string TextDelimiter
    {
      get
      {
        return this.textdelim;
      }
      set
      {
        this.textdelim = value;
      }
    }

    public bool IncludeColumnHeader
    {
      get
      {
        return this.includeColumnHeader;
      }
      set
      {
        this.includeColumnHeader = value;
      }
    }

    public DataTableConverter()
    {
    }

    public DataTableConverter(DataTable table)
    {
      this.Table = table;
    }

    public int Convert(DataTableConversionType type)
    {
      return this.convert(type);
    }

    public int Convert(DataTableConversionType type, string delimiter)
    {
      this.delim = delimiter;
      return this.convert(type);
    }

    private int convert(DataTableConversionType type)
    {
      if (this.Table == null)
        throw new Exception("Cannot convert: the 'DataTableConverter.Table' property is null.");
      if (this.OutputStream == null)
        throw new Exception("Cannot convert: the 'DataTableConverter.OutputStream' property is null.");
      this.bytes_written = 0;
      switch (type)
      {
        case DataTableConversionType.DelimitedText:
          this.conv_delimited();
          break;
        case DataTableConversionType.XML:
          this.conv_XML();
          break;
        case DataTableConversionType.HTML:
          this.conv_HTML();
          break;
        default:
          throw new Exception("The 'DataTableConversionType' specified is not supported.");
      }
      return this.bytes_written;
    }

    private void conv_XML()
    {
      new DataSet(nameof (DataTableConverter))
      {
        Tables = {
          this.Table
        }
      }.WriteXml(this.stream);
    }

    private void conv_HTML()
    {
      this.writeLine(" <html xmlns='http://www.w3.org/1999/xhtml'>\r\n<head></head>\r\n<body>");
      this.writeLine(" <table border='1px' cellpadding='5' cellspacing='0' style='border: solid 1px Silver; font-size: x-small;'>");
      if (this.includeColumnHeader)
      {
        this.writeLine("   <tr align='left' valign='top'>");
        foreach (DataColumn column in (InternalDataCollectionBase) this.Table.Columns)
          this.writeLine("     <td align='left' valign='top' style='font-size: small;'><b>" + column.ColumnName + "</b></td>");
        this.writeLine("   </tr>");
      }
      foreach (DataRow row in (InternalDataCollectionBase) this.Table.Rows)
      {
        this.writeLine("   <tr align='left' valign='top'>");
        foreach (DataColumn column in (InternalDataCollectionBase) this.Table.Columns)
        {
          string str;
          switch (row[column].GetType().ToString())
          {
            case "System.Int32":
              str = ((int) row[column]).ToString();
              break;
            case "System.DateTime":
              str = ((DateTime) row[column]).ToString();
              break;
            default:
              str = row[column] as string;
              break;
          }
          if (str == null || str.Length == 0)
            str = "&nbsp;";
          this.writeLine("     <td align='left' valign='top'>" + str + "</td>");
        }
        this.writeLine("   </tr>");
      }
      this.writeLine(" </table></body></html>");
    }

    private void conv_delimited()
    {
      string line1 = "";
      DateTime minValue = DateTime.MinValue;
      if (this.includeColumnHeader)
      {
        foreach (DataColumn column in (InternalDataCollectionBase) this.Table.Columns)
          line1 = line1 + this.delim + this.textdelim + column.ColumnName + this.textdelim;
        this.writeLine(line1);
      }
      foreach (DataRow row in (InternalDataCollectionBase) this.Table.Rows)
      {
        string line2 = "";
        foreach (DataColumn column in (InternalDataCollectionBase) this.Table.Columns)
        {
          switch (row[column].GetType().ToString())
          {
            case "System.Int32":
              int num = (int) row[column];
              line2 = line2 + this.delim + num.ToString();
              continue;
            case "System.DateTime":
              DateTime dateTime = (DateTime) row[column];
              line2 = line2 + this.delim + dateTime.ToString();
              continue;
            default:
              string str = row[column] as string;
              line2 = line2 + this.delim + this.textdelim + str + this.textdelim;
              continue;
          }
        }
        this.writeLine(line2);
      }
    }

    private void writeLine(string line)
    {
      if (line.Length < 2)
        return;
      byte[] bytes = Encoding.UTF8.GetBytes(line);
      this.stream.Write(bytes, 1, bytes.Length - 1);
      this.stream.Write(this.crlf, 0, this.crlf.Length);
      this.bytes_written += bytes.Length - 1 + this.crlf.Length;
    }
  }
}
