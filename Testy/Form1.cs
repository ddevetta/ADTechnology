using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ADTechnology.Apps.OpenHostFileDialog;
using ADTechnology.Apps;
using System.Xml;
using System.IO;

//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

using ADTechnology.Classes;
using Testy.Properties;
using System.Security;
using System.Collections;
using System.Globalization;

namespace Testy
{
    public partial class Form1 : Form
    {
        public OpenHostFileDialog ohfd;
        public SaveDataTableDialog sdtd;
        DataSet ds;

        const int region_q = 2;
        TextBox[] tbHeadings = new TextBox[region_q];
        TextBox[] tbSubHeadings = new TextBox[region_q];
        TextBox[] tbTabs = new TextBox[region_q];
        DataGrid[] dgData = new DataGrid[region_q];
        DataGridView[] dv = new DataGridView[region_q];

        public Form1()
        {
            InitializeComponent();

            this.tabControl.SelectedIndex = Properties.Settings.Default.TabSelection;

            ohfd = new OpenHostFileDialog();
            sdtd = new SaveDataTableDialog();
            //sdtd.ExportDataTypes = new ExportDataType[] { ExportDataType.Csv, ExportDataType.Xlsx };
            sdtd.FilterIndex = 0;

            cbRenderAs.DataSource = ExportData.TypeFilters;
            cbRenderAs.DisplayMember = "Display";
            cbRenderAs.ValueMember = "Value";
            cbRenderAs.SelectedIndex = 0;

            ds = CreateTestData();

            int x = 40;
            int y = 36;
            for (int i = 0; i < region_q; i++)
            {
                tbHeadings[i] = new TextBox();
                tbHeadings[i].Text = "Heading " + (i + 1).ToString();
                tbHeadings[i].Location = new Point(x, y);

                tbSubHeadings[i] = new TextBox();
                tbSubHeadings[i].Location = new Point(x, y + 24);

                tbTabs[i] = new TextBox();
                tbTabs[i].Text = "Tab " + (i + 1).ToString();
                tbTabs[i].Location = new Point(x + 130, y);

                //dgData[i] = new DataGrid();
                //dgData[i].DataSource = ds.Tables[i];
                dv[i] = new DataGridView();
                dv[i].DataSource = ds.Tables[i];
                dv[i].Location = new Point(x, y + 48);
                dv[i].Width = 550;
                dv[i].Height = 120;
                dv[i].AutoGenerateColumns = true;

                this.tabPage2.Controls.Add(tbHeadings[i]);
                this.tabPage2.Controls.Add(tbTabs[i]);
                this.tabPage2.Controls.Add(tbSubHeadings[i]);
                this.tabPage2.Controls.Add(dv[i]);
                y = y + 180;
            }

            cbDBAOption.DataSource = Enum.GetNames(typeof(OracleDBAPrivilege));
            cbDBAOption.SelectedIndex = 0;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (ohfd.ShowDialog(this) == DialogResult.OK)
                this.tbName.Text = ohfd.FileName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.sdtd.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            long saved = 0;

            ds.Tables[0].ExtendedProperties.Clear();
            if (this.tbHeadings[0].Text != string.Empty)
                ds.Tables[0].ExtendedProperties.Add("heading", this.tbHeadings[0].Text);
            if (this.tbSubHeadings[0].Text != string.Empty)
                ds.Tables[0].ExtendedProperties.Add("subheading", this.tbSubHeadings[0].Text);
            if (this.tbTabs[0].Text != string.Empty)
                ds.Tables[0].ExtendedProperties.Add("tabname", this.tbTabs[0].Text);
            ds.Tables[1].ExtendedProperties.Clear();
            if (this.tbHeadings[1].Text != string.Empty)
                ds.Tables[1].ExtendedProperties.Add("heading", this.tbHeadings[1].Text);
            if (this.tbSubHeadings[1].Text != string.Empty)
                ds.Tables[1].ExtendedProperties.Add("subheading", this.tbSubHeadings[1].Text);
            if (this.tbTabs[1].Text != string.Empty)
                ds.Tables[1].ExtendedProperties.Add("tabname", this.tbTabs[1].Text);

            try
            {
                saved = this.sdtd.Save(ds);
                this.label1.Text = saved.ToString("n0") + " bytes written.";
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            label1.Text = cbRenderAs.SelectedValue.ToString();

            RenderDataTable rdt = new RenderDataTable();
            rdt.Render(ds);
        }

        private DataSet CreateTestData()
        {
            this.ds = new DataSet();
            DataTable dt1 = new DataTable("Customer");
            DataTable dt2 = new DataTable("Order");

            dt1.Columns.AddRange(new DataColumn[] { new DataColumn("customer_id", typeof(decimal)), new DataColumn("name", typeof(string)) });
            dt1.Columns[1].Caption = "Customer Name";

            dt2.Columns.AddRange(new DataColumn[] { new DataColumn("customer_id", typeof(decimal)), new DataColumn("order_id", typeof(int)), new DataColumn("product", typeof(string)), new DataColumn("purchase_date", typeof(DateTime)), new DataColumn("purchase_price", typeof(double)) });
            dt2.Columns[3].Caption = "Date";
            dt2.Columns[4].Caption = "Price";

            dt1.Rows.Add(1001, "Dave");
            dt1.Rows.Add(1002, "Ken");
            dt1.Rows.Add(1003, "Barbie");

            dt2.Rows.Add(1001, 1, "Potato Ricer", DateTime.Now, 14.99);
            dt2.Rows.Add(1003, 1, "Pink Chevvy", DateTime.Now, 27000);

            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            return ds;
        }

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet("queryresults");
            //string connect = "Data Source=" + tbDb.Text + ";Persist Security Info=True;User ID=" + tbUser.Text + ";Password=" + tbPassword.Text + ";";
            string connect = "Data Source=" + tbDb.Text + ";";
            System.Security.SecureString secpw = new System.Security.SecureString();
            Array.ForEach(tbPassword.Text.ToCharArray(), secpw.AppendChar);
            secpw.MakeReadOnly();
            OracleCredential cred = new OracleCredential(tbUser.Text, secpw, (OracleDBAPrivilege)Enum.Parse(typeof(OracleDBAPrivilege), cbDBAOption.SelectedItem.ToString()));
            try
            {
                OracleConnection conn = new OracleConnection(connect, cred);
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = tbQuery.Text.TrimEnd(';');
                cmd.CommandType = CommandType.Text;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error from call to DB\r" + ex.Message + ((ex.InnerException != null) ? ("\r" + ex.InnerException.Message) : ""));
                return;
            }

            // Give nice column headings
            TextInfo txt = new CultureInfo("en-US", false).TextInfo;
            foreach (DataColumn c in ds.Tables[0].Columns)
                c.Caption = txt.ToTitleCase(c.ColumnName.Replace("_", " ").ToLower());

            if (this.sdtd.ShowDialog(this) != DialogResult.OK)
                return;
            long saved = 0;

            try
            {
                saved = this.sdtd.Save(ds);
                this.label1.Text = saved.ToString("n0") + " bytes written.";
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TabSelection = tabControl.SelectedIndex;
            Properties.Settings.Default.Save();
        }
    }
}
