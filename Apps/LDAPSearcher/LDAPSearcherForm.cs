// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Apps.LDAPSearcher.LDAPSearcherForm
// Assembly: LDAPSearcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5AF5FD7-65F5-4CFF-9BBF-D18F59F6CCDD
// Assembly location: C:\Users\DaveAndTina\OneDrive - Sky\Projects\ldap..tion_0000000000000000_0001.0000_91e2ccba3687cdbf\LDAPSearcher.exe

using ADTechnology.Classes;
using ADTechnology.Apps.SaveDataTable;

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ADTechnology.Apps.LDAPSearcher
{
  public class LDAPSearcherForm : Form
  {
    private string[] propertiesToLoad = new string[12]
    {
      "SamAccountName",
      "Name",
      "TelephoneNumber",
      "Mail",
      "Title",
      "Company",
      "Department",
      "StreetAddress",
      "L",
      "PostalCode",
      "Co",
      "ADsPath"
    };
    private char[] in_delimiters = new char[4]
    {
      ' ',
      ',',
      ';',
      '\t'
    };
    private string domain = "";
    private SaveDataTableDialog sdtd;
    private IContainer components;
    private DirectorySearcher deSearcher;
    private Label label1;
    private TextBox tbValue;
    private Button btnGo;
    private DataGridView dgResult;
    private Panel panel1;
    private ErrorProvider error;
    private ComboBox cbColumn;
    private Label label3;
    private ComboBox cbType;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel rowCountLabel;
    private ComboBox cbOperator;
    private ToolStripStatusLabel statusLabel1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem copyGridToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem aboutToolStripMenuItem;
    private ToolStripMenuItem saveResultsAsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem copyWithHeaderToolStripMenuItem;

    public LDAPSearcherForm()
    {
      this.InitializeComponent();
      this.deSearcher.PropertiesToLoad.AddRange(this.propertiesToLoad);
      this.cbColumn.Items.AddRange((object[]) this.propertiesToLoad);
      this.domain = this.deSearcher.SearchRoot.Properties["name"][0].ToString().ToUpper();
      this.cbType.SelectedIndex = this.cbOperator.SelectedIndex = this.cbColumn.SelectedIndex = 0;
    }

    private void btnGo_Click(object sender, EventArgs e)
    {
      this.error.Clear();
      this.rowCountLabel.Text = this.statusLabel1.Text = "";
      this.dgResult.DataSource = (object) null;
      this.tbValue.Text = this.tbValue.Text.Trim();
      if (this.tbValue.Text.Length == 0)
      {
        this.error.SetError((Control) this.tbValue, "Please enter some search criteria.");
      }
      else
      {
        try
        {
          this.Cursor = Cursors.WaitCursor;
          this.dgResult.DataSource = (object) this.createDt(this.cbType.Text, this.cbColumn.Text, this.cbOperator.SelectedIndex, this.tbValue.Text);
          if (this.dgResult.Rows.Count == 0)
          {
            this.error.SetError((Control) this.tbValue, "No entries found.");
            return;
          }
        }
        catch (Exception ex)
        {
          this.error.SetError((Control) this.tbValue, ex.Message);
          return;
        }
        finally
        {
          this.Cursor = Cursors.Default;
        }
        this.rowCountLabel.Text = this.dgResult.Rows.Count.ToString() + (this.dgResult.Rows.Count == 1 ? " row." : " rows.");
      }
    }

    private DataTable createDt(
      string objectCategory,
      string column,
      int operatorIndex,
      string value)
    {
      DataTable dataTable = new DataTable("ldap");
      dataTable.Columns.Add("Domain");
      foreach (string columnName in this.deSearcher.PropertiesToLoad)
        dataTable.Columns.Add(columnName);
      string[] strArray1;
      if (operatorIndex == 3)
        strArray1 = value.Split(this.in_delimiters, StringSplitOptions.RemoveEmptyEntries);
      else
        strArray1 = new string[1]{ value };
      foreach (string str1 in strArray1)
      {
        DirectorySearcher deSearcher = this.deSearcher;
        ToolStripStatusLabel statusLabel1 = this.statusLabel1;
        string[] strArray2 = new string[9]
        {
          "(&(objectcategory=",
          objectCategory,
          ")(",
          column,
          "=",
          operatorIndex == 1 ? "*" : "",
          str1,
          operatorIndex < 2 ? "*" : "",
          "))"
        };
        string str2;
        string str3 = str2 = string.Concat(strArray2);
        statusLabel1.Text = str2;
        string str4 = str3;
        deSearcher.Filter = str4;
        foreach (SearchResult searchResult in this.deSearcher.FindAll())
        {
          DataRow row = dataTable.NewRow();
          row["domain"] = (object) this.domain;
          ResultPropertyCollection properties = searchResult.Properties;
          foreach (string propertyName in (IEnumerable) properties.PropertyNames)
            row[propertyName] = (object) properties[propertyName][0].ToString();
          dataTable.Rows.Add(row);
        }
      }
      return dataTable;
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void copyGridToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.copyResults(false);
    }

    private void copyGridWithHeaderToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.copyResults(true);
    }

    private void saveResultsAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.saveResults();
    }

    private void copyResults(bool includeColumnHeader)
    {
      if (this.dgResult.DataSource == null)
      {
        int num = (int) MessageBox.Show("Nothing to copy!");
      }
      else
      {
        try
        {
          DataTableConverter dataTableConverter = new DataTableConverter((DataTable) this.dgResult.DataSource);
          dataTableConverter.OutputStream = (Stream) new MemoryStream();
          dataTableConverter.IncludeColumnHeader = includeColumnHeader;
          dataTableConverter.Convert(DataTableConversionType.DelimitedText, "\t");
          Clipboard.SetData(DataFormats.Text, (object) Encoding.UTF8.GetString(((MemoryStream) dataTableConverter.OutputStream).ToArray()));
        }
        catch
        {
        }
      }
    }

    private void saveResults()
    {
      if (this.dgResult.DataSource == null)
      {
        int num1 = (int) MessageBox.Show("Nothing to save!");
      }
      else
      {
        if (this.sdtd == null)
          this.sdtd = new SaveDataTableDialog((DataTable) this.dgResult.DataSource);
        try
        {
          if (this.sdtd.ShowDialog((IWin32Window) this) != DialogResult.OK)
            return;
          this.sdtd.OutputStream = this.sdtd.OpenFile();
          this.sdtd.Convert();
          this.sdtd.OutputStream.Close();
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.deSearcher = new DirectorySearcher();
      this.label1 = new Label();
      this.tbValue = new TextBox();
      this.btnGo = new Button();
      this.dgResult = new DataGridView();
      this.panel1 = new Panel();
      this.cbOperator = new ComboBox();
      this.label3 = new Label();
      this.cbType = new ComboBox();
      this.cbColumn = new ComboBox();
      this.menuStrip1 = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.saveResultsAsToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.editToolStripMenuItem = new ToolStripMenuItem();
      this.copyGridToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.aboutToolStripMenuItem = new ToolStripMenuItem();
      this.error = new ErrorProvider(this.components);
      this.statusStrip = new StatusStrip();
      this.rowCountLabel = new ToolStripStatusLabel();
      this.statusLabel1 = new ToolStripStatusLabel();
      this.copyWithHeaderToolStripMenuItem = new ToolStripMenuItem();
      ((ISupportInitialize) this.dgResult).BeginInit();
      this.panel1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      ((ISupportInitialize) this.error).BeginInit();
      this.statusStrip.SuspendLayout();
      this.SuspendLayout();
      this.deSearcher.ClientTimeout = TimeSpan.Parse("-00:00:01");
      this.deSearcher.ServerPageTimeLimit = TimeSpan.Parse("-00:00:01");
      this.deSearcher.ServerTimeLimit = TimeSpan.Parse("-00:00:01");
      this.deSearcher.Sort.PropertyName = "samaccountname";
      this.label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.label1.Location = new Point(36, 46);
      this.label1.Name = "label1";
      this.label1.Size = new Size(59, 21);
      this.label1.TabIndex = 3;
      this.label1.Text = "Search  for";
      this.label1.TextAlign = ContentAlignment.MiddleLeft;
      this.tbValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.tbValue.Location = new Point(516, 46);
      this.tbValue.Name = "tbValue";
      this.tbValue.Size = new Size(147, 20);
      this.tbValue.TabIndex = 0;
      this.btnGo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.error.SetIconAlignment((Control) this.btnGo, ErrorIconAlignment.MiddleLeft);
      this.error.SetIconPadding((Control) this.btnGo, 4);
      this.btnGo.Location = new Point(774, 44);
      this.btnGo.Name = "btnGo";
      this.btnGo.Size = new Size(75, 23);
      this.btnGo.TabIndex = 4;
      this.btnGo.Text = "Search";
      this.btnGo.UseVisualStyleBackColor = true;
      this.btnGo.Click += new EventHandler(this.btnGo_Click);
      this.dgResult.AllowUserToAddRows = false;
      this.dgResult.AllowUserToDeleteRows = false;
      this.dgResult.AllowUserToOrderColumns = true;
      this.dgResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      this.dgResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgResult.Dock = DockStyle.Fill;
      this.error.SetIconAlignment((Control) this.dgResult, ErrorIconAlignment.TopLeft);
      this.dgResult.Location = new Point(6, 99);
      this.dgResult.Name = "dgResult";
      this.dgResult.ReadOnly = true;
      this.dgResult.RowHeadersVisible = false;
      this.dgResult.Size = new Size(852, 194);
      this.dgResult.TabIndex = 3;
      this.panel1.Controls.Add((Control) this.cbOperator);
      this.panel1.Controls.Add((Control) this.label3);
      this.panel1.Controls.Add((Control) this.cbType);
      this.panel1.Controls.Add((Control) this.cbColumn);
      this.panel1.Controls.Add((Control) this.btnGo);
      this.panel1.Controls.Add((Control) this.tbValue);
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Controls.Add((Control) this.menuStrip1);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(6, 6);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(852, 93);
      this.panel1.TabIndex = 0;
      this.cbOperator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.cbOperator.FormattingEnabled = true;
      this.cbOperator.Items.AddRange(new object[4]
      {
        (object) "starts with",
        (object) "contains",
        (object) "equals",
        (object) "in"
      });
      this.cbOperator.Location = new Point(415, 46);
      this.cbOperator.Name = "cbOperator";
      this.cbOperator.Size = new Size(95, 21);
      this.cbOperator.TabIndex = 10;
      this.label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.label3.Location = new Point(228, 46);
      this.label3.Name = "label3";
      this.label3.Size = new Size(36, 21);
      this.label3.TabIndex = 9;
      this.label3.Text = "where";
      this.label3.TextAlign = ContentAlignment.MiddleLeft;
      this.cbType.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.cbType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbType.FormattingEnabled = true;
      this.cbType.Items.AddRange(new object[3]
      {
        (object) "Person",
        (object) "Group",
        (object) "Computer"
      });
      this.cbType.Location = new Point(101, 46);
      this.cbType.Name = "cbType";
      this.cbType.Size = new Size(121, 21);
      this.cbType.TabIndex = 8;
      this.cbColumn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.cbColumn.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbColumn.FormattingEnabled = true;
      this.cbColumn.Location = new Point(270, 46);
      this.cbColumn.MaxDropDownItems = 20;
      this.cbColumn.Name = "cbColumn";
      this.cbColumn.Size = new Size(139, 21);
      this.cbColumn.TabIndex = 7;
      this.menuStrip1.GripStyle = ToolStripGripStyle.Visible;
      this.menuStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.editToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(852, 24);
      this.menuStrip1.TabIndex = 11;
      this.menuStrip1.Text = "menuStrip";
      this.fileToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.saveResultsAsToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.exitToolStripMenuItem
      });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      this.saveResultsAsToolStripMenuItem.Name = "saveResultsAsToolStripMenuItem";
      this.saveResultsAsToolStripMenuItem.Size = new Size(164, 22);
      this.saveResultsAsToolStripMenuItem.Text = "&Save Results as ...";
      this.saveResultsAsToolStripMenuItem.Click += new EventHandler(this.saveResultsAsToolStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(161, 6);
      this.exitToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new Size(164, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.TextImageRelation = TextImageRelation.Overlay;
      this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
      this.editToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.copyGridToolStripMenuItem,
        (ToolStripItem) this.copyWithHeaderToolStripMenuItem
      });
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new Size(39, 20);
      this.editToolStripMenuItem.Text = "&Edit";
      this.copyGridToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.copyGridToolStripMenuItem.Name = "copyGridToolStripMenuItem";
      this.copyGridToolStripMenuItem.ShortcutKeys = Keys.C | Keys.Control;
      this.copyGridToolStripMenuItem.Size = new Size(184, 22);
      this.copyGridToolStripMenuItem.Text = "&Copy Results";
      this.copyGridToolStripMenuItem.Click += new EventHandler(this.copyGridToolStripMenuItem_Click);
      this.helpToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.aboutToolStripMenuItem
      });
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new Size(116, 22);
      this.aboutToolStripMenuItem.Text = "About...";
      this.error.ContainerControl = (ContainerControl) this;
      this.statusStrip.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.rowCountLabel,
        (ToolStripItem) this.statusLabel1
      });
      this.statusStrip.Location = new Point(6, 293);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new Size(852, 22);
      this.statusStrip.SizingGrip = false;
      this.statusStrip.TabIndex = 4;
      this.rowCountLabel.Name = "rowCountLabel";
      this.rowCountLabel.Padding = new Padding(0, 0, 6, 0);
      this.rowCountLabel.Size = new Size(6, 17);
      this.statusLabel1.Name = "statusLabel1";
      this.statusLabel1.Padding = new Padding(0, 0, 6, 0);
      this.statusLabel1.Size = new Size(6, 17);
      this.copyWithHeaderToolStripMenuItem.Name = "copyWithHeaderToolStripMenuItem";
      this.copyWithHeaderToolStripMenuItem.Size = new Size(169, 22);
      this.copyWithHeaderToolStripMenuItem.Text = "Copy with &Header";
      this.copyWithHeaderToolStripMenuItem.Click += new EventHandler(this.copyGridWithHeaderToolStripMenuItem_Click);
      this.AcceptButton = (IButtonControl) this.btnGo;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(864, 318);
      this.Controls.Add((Control) this.dgResult);
      this.Controls.Add((Control) this.statusStrip);
      this.Controls.Add((Control) this.panel1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = nameof (LDAPSearcherForm);
      this.Padding = new Padding(6, 6, 6, 3);
      this.SizeGripStyle = SizeGripStyle.Show;
      this.Text = "LDAP Searcher";
      ((ISupportInitialize) this.dgResult).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((ISupportInitialize) this.error).EndInit();
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
