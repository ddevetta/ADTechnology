// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Apps.LDAPSearcher.LDAPSearcherForm
// Assembly: LDAPSearcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5AF5FD7-65F5-4CFF-9BBF-D18F59F6CCDD
// Assembly location: C:\Users\DaveAndTina\OneDrive - Sky\Projects\ldap..tion_0000000000000000_0001.0000_91e2ccba3687cdbf\LDAPSearcher.exe

using ADTechnology.Classes;
using ADTechnology.Apps;
using ADTechnology.Apps.SaveDataTable;

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
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
        private ToolStripMenuItem domainToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyToolStripMenuItem1;
        private ToolStripMenuItem copyLineToolStripMenuItem;
        private ToolStripMenuItem copyWithHeaderToolStripMenuItem;

        public LDAPSearcherForm()
        {
            this.InitializeComponent();

            this.domain = Properties.Settings.Default.domain;

            initialiseSearcher();

        }

        private void initialiseSearcher()
        {
            this.rowCountLabel.Text = string.Empty;
            this.btnGo.Enabled = this.tbValue.Enabled = false;

            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;

            if (this.domain == null || this.domain == string.Empty)
            {
                try
                {
                    Domain dm = Domain.GetComputerDomain();
                    this.domain = dm.Name;
					
                    Properties.Settings.Default.domain = this.domain;
                    Properties.Settings.Default.Save();
                }
                catch (ActiveDirectoryObjectNotFoundException adex)
                {
                    this.rowCountLabel.Text = adex.Message + " (Specify a known domain in File->Domain)";
                    return;
                }
                catch (Exception ex)
                {
                    this.rowCountLabel.Text = "Error retrieving domain - " + ex.Message;
                    return;
                }
            }
            else
            {
                try
                {
                    PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain);
                    if (pc.Name == null)
                        return;
                }
                catch (Exception ex)
                {
                    this.rowCountLabel.Text = ex.Message + " (Specify a valid domain in File->Domain)";
                    return;
                }
            }

            deSearcher.SearchRoot = new DirectoryEntry("LDAP://" + domain);
            this.deSearcher.PropertiesToLoad.AddRange(this.propertiesToLoad);
            this.cbColumn.Items.AddRange((object[])this.propertiesToLoad);
            this.deSearcher.SizeLimit = 1000;

            this.btnGo.Enabled = this.tbValue.Enabled = true;
            this.cbType.SelectedIndex = this.cbOperator.SelectedIndex = this.cbColumn.SelectedIndex = 0;
            this.tbValue.Focus();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {

            this.error.Clear();
            this.rowCountLabel.Text = this.statusLabel1.Text = "";
            this.dgResult.DataSource = (object)null;
            this.tbValue.Text = this.tbValue.Text.Trim();
            if (this.tbValue.Text.Length == 0)
            {
                this.error.SetError((Control)this.tbValue, "Please enter some search criteria.");
            }
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.dgResult.DataSource = (object)this.createDt(this.cbType.Text, this.cbColumn.Text, this.cbOperator.SelectedIndex, this.tbValue.Text);
                    if (this.dgResult.Rows.Count == 0)
                    {
                        this.error.SetError((Control)this.tbValue, "No entries found.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.error.SetError((Control)this.tbValue, ex.Message);
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                this.rowCountLabel.Text = this.dgResult.Rows.Count.ToString() + (this.dgResult.Rows.Count == 1 ? " row." : " rows.");
            }
        }

        private DataTable createDt(string objectCategory, string column, int operatorIndex, string value)
        {
            string dom = this.deSearcher.SearchRoot.Properties["name"][0].ToString().ToUpper();
            DataTable dataTable = initialiseDt();
            string[] strArray1;
            if (operatorIndex == 3)
                strArray1 = value.Split(this.in_delimiters, StringSplitOptions.RemoveEmptyEntries);
            else
                strArray1 = new string[1] { value };
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
                    row["domain"] = dom;
                    ResultPropertyCollection properties = searchResult.Properties;
                    foreach (string propertyName in (IEnumerable)properties.PropertyNames)
                        row[propertyName] = (object)properties[propertyName][0].ToString();
                    dataTable.Rows.Add(row);
                }
            }
            return dataTable;
        }

        private DataTable initialiseDt()
        {
            DataTable dataTable = new DataTable("ldap");
            dataTable.Columns.Add("Domain");
            foreach (string columnName in this.deSearcher.PropertiesToLoad)
                dataTable.Columns.Add(columnName);
            return dataTable;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgResult.SelectedCells.Count > 0)
                Clipboard.SetData(DataFormats.Text, this.dgResult.SelectedCells[0].FormattedValue);
        }

        private void copyGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgResult.SelectedCells.Count > 0)
                this.copyResults(this.dgResult.SelectedCells[0].RowIndex, false);
        }

        private void copyGridWithHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.copyResults(-1, true);
        }

        private void saveResultsAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveResults();
        }

        private void dgResult_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
                this.dgResult.CurrentCell = this.dgResult[e.ColumnIndex, e.RowIndex];
        }

        private void domainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox box = new InputBox("Enter domain name :", domain);
            if (box.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.domain = this.domain = box.Value;
                Properties.Settings.Default.Save();

                this.initialiseSearcher();
            }
        }

        private void copyResults(int row, bool includeColumnHeader)
        {
            DataTable dt;

            if (this.dgResult.DataSource == null)
            {
                int num = (int)MessageBox.Show("Nothing to copy!");
            }
            else
            {
                if (row > -1)
                {
                    dt = new DataTable();  // if Copying a Line, create a table with just the selected row
                    dt = initialiseDt();
                    DataRow newrow = dt.NewRow();
                    newrow.ItemArray = ((DataRowView)this.dgResult.Rows[row].DataBoundItem).Row.ItemArray;
                    dt.Rows.Add(newrow);
                }
                else
                    dt = (DataTable)this.dgResult.DataSource;  // else copy the whole grid
                
                try
                {
                    DataTableConverter dataTableConverter = new DataTableConverter(dt);
                    dataTableConverter.OutputStream = (Stream)new MemoryStream();
                    dataTableConverter.IncludeColumnHeader = includeColumnHeader;
                    dataTableConverter.Convert(DataTableConversionType.DelimitedText, "\t");
                    Clipboard.SetData(DataFormats.Text, (object)Encoding.UTF8.GetString(((MemoryStream)dataTableConverter.OutputStream).ToArray()));
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
                int num1 = (int)MessageBox.Show("Nothing to save!");
            }
            else
            {
                if (this.sdtd == null)
                    this.sdtd = new SaveDataTableDialog();
                try
                {
                    if (this.sdtd.ShowDialog((IWin32Window)this) != DialogResult.OK)
                        return;
                    this.sdtd.Save((DataTable)this.dgResult.DataSource);
                }
                catch (Exception ex)
                {
                    int num2 = (int)MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LDAPSearcherForm));
            this.deSearcher = new System.DirectoryServices.DirectorySearcher();
            this.label1 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.dgResult = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.cbColumn = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.domainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveResultsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyWithHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.rowCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // deSearcher
            // 
            this.deSearcher.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.deSearcher.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.deSearcher.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.deSearcher.Sort.PropertyName = "samaccountname";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(36, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search  for";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbValue
            // 
            this.tbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbValue.Location = new System.Drawing.Point(516, 46);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(147, 20);
            this.tbValue.TabIndex = 0;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.error.SetIconAlignment(this.btnGo, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.error.SetIconPadding(this.btnGo, 4);
            this.btnGo.Location = new System.Drawing.Point(774, 44);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Search";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // dgResult
            // 
            this.dgResult.AllowUserToAddRows = false;
            this.dgResult.AllowUserToDeleteRows = false;
            this.dgResult.AllowUserToOrderColumns = true;
            this.dgResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResult.ContextMenuStrip = this.contextMenuStrip1;
            this.dgResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.error.SetIconAlignment(this.dgResult, System.Windows.Forms.ErrorIconAlignment.TopLeft);
            this.dgResult.Location = new System.Drawing.Point(6, 99);
            this.dgResult.MultiSelect = false;
            this.dgResult.Name = "dgResult";
            this.dgResult.ReadOnly = true;
            this.dgResult.RowHeadersVisible = false;
            this.dgResult.Size = new System.Drawing.Size(852, 194);
            this.dgResult.TabIndex = 3;
            this.dgResult.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgResult_CellMouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbOperator);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbType);
            this.panel1.Controls.Add(this.cbColumn);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Controls.Add(this.tbValue);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 93);
            this.panel1.TabIndex = 0;
            // 
            // cbOperator
            // 
            this.cbOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Items.AddRange(new object[] {
            "starts with",
            "contains",
            "equals",
            "in"});
            this.cbOperator.Location = new System.Drawing.Point(415, 46);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(95, 21);
            this.cbOperator.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(228, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "where";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbType
            // 
            this.cbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Person",
            "Group",
            "Computer"});
            this.cbType.Location = new System.Drawing.Point(101, 46);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 21);
            this.cbType.TabIndex = 8;
            // 
            // cbColumn
            // 
            this.cbColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn.FormattingEnabled = true;
            this.cbColumn.Location = new System.Drawing.Point(270, 46);
            this.cbColumn.MaxDropDownItems = 20;
            this.cbColumn.Name = "cbColumn";
            this.cbColumn.Size = new System.Drawing.Size(139, 21);
            this.cbColumn.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.domainToolStripMenuItem,
            this.saveResultsAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // domainToolStripMenuItem
            // 
            this.domainToolStripMenuItem.Name = "domainToolStripMenuItem";
            this.domainToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.domainToolStripMenuItem.Text = "Domain ...";
            this.domainToolStripMenuItem.Click += new System.EventHandler(this.domainToolStripMenuItem_Click);
            // 
            // saveResultsAsToolStripMenuItem
            // 
            this.saveResultsAsToolStripMenuItem.Name = "saveResultsAsToolStripMenuItem";
            this.saveResultsAsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveResultsAsToolStripMenuItem.Text = "&Save Results as ...";
            this.saveResultsAsToolStripMenuItem.Click += new System.EventHandler(this.saveResultsAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copyGridToolStripMenuItem,
            this.copyWithHeaderToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyGridToolStripMenuItem
            // 
            this.copyGridToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyGridToolStripMenuItem.Name = "copyGridToolStripMenuItem";
            this.copyGridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.copyGridToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyGridToolStripMenuItem.Text = "Copy &Line";
            this.copyGridToolStripMenuItem.Click += new System.EventHandler(this.copyGridToolStripMenuItem_Click);
            // 
            // copyWithHeaderToolStripMenuItem
            // 
            this.copyWithHeaderToolStripMenuItem.Name = "copyWithHeaderToolStripMenuItem";
            this.copyWithHeaderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.copyWithHeaderToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyWithHeaderToolStripMenuItem.Text = "Copy &Results";
            this.copyWithHeaderToolStripMenuItem.Click += new System.EventHandler(this.copyGridWithHeaderToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rowCountLabel,
            this.statusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(6, 293);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(852, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 4;
            // 
            // rowCountLabel
            // 
            this.rowCountLabel.Name = "rowCountLabel";
            this.rowCountLabel.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.rowCountLabel.Size = new System.Drawing.Size(6, 17);
            // 
            // statusLabel1
            // 
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.statusLabel1.Size = new System.Drawing.Size(6, 17);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1,
            this.copyLineToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 48);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem1.Text = "&Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyLineToolStripMenuItem
            // 
            this.copyLineToolStripMenuItem.Name = "copyLineToolStripMenuItem";
            this.copyLineToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyLineToolStripMenuItem.Text = "Copy &Line";
            this.copyLineToolStripMenuItem.Click += new System.EventHandler(this.copyGridToolStripMenuItem_Click);
            // 
            // LDAPSearcherForm
            // 
            this.AcceptButton = this.btnGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 318);
            this.Controls.Add(this.dgResult);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LDAPSearcherForm";
            this.Padding = new System.Windows.Forms.Padding(6, 6, 6, 3);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "LDAP Searcher";
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
