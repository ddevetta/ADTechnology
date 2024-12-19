using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
    public partial class FileBrowserControl : TabControlFrame
    {
        private bool startFromTop = false;
        private bool incompleteFileRead = false;
        private AutoCompleteStringCollection searchList = new AutoCompleteStringCollection();

        public FileBrowserControl()
        {
            InitializeComponent();
            this.btnSearch.BackgroundImage = Images.search;
            this.btnSearch.BackgroundImageLayout = ImageLayout.Stretch;
            this.tbSearch.AutoCompleteCustomSource = searchList;
        }

        public string FileName
        {
            get { return tbFileName.Text; }
            set
            {
                if (value == null || value == string.Empty)
                    this.tbFileName.Text = "(AILOG: No File Specified)";
                else
                    this.tbFileName.Text = value;
            }
        }

        public void Fill(string fileName, string fileData)
        {
            this.FileName = fileName;
            rtbData.Text = fileData;
        }

        public void Fill(string fileName, StreamReader str)
        {
            string incompleteTag = "<<< AILOG read buffer full.";
            int bufsz = 1048576;
            int readq = 0;
            char[] buf = new char[bufsz];

            this.FileName = fileName;

            readq = str.ReadBlock(buf, 0, bufsz);

            rtbData.SuspendLayout();

            if (str.EndOfStream) // Whole file fits into buf
            {
                rtbData.Text = new string(buf);
                this.incompleteFileRead = false;
            }
            else
            {
                this.incompleteFileRead = true;
                rtbData.Text = new string(buf) + incompleteTag;
                rtbData.SelectionStart = rtbData.TextLength - incompleteTag.Length;
                rtbData.SelectionLength = incompleteTag.Length;
                rtbData.SelectionColor = Color.Red;
                rtbData.Select(0, 0);
                lblPosition.Text = "(incomplete)";
            }

            str.Close();
            rtbData.ResumeLayout();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchText();
        }

        private void rtbData_SelectionChanged(object sender, EventArgs e)
        {
            lblPosition.Text = " pos " + (rtbData.SelectionStart + 1).ToString("N0") 
                + " - line " + (rtbData.GetLineFromCharIndex(rtbData.SelectionStart) + 1).ToString("N0") + " of " + rtbData.Lines.Count().ToString("N0");
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                searchText();
        }

        private void searchText()
        {
            if (tbSearch.Text.Length == 0 || rtbData.TextLength == 0)
                return;

            this.SuspendLayout();
            if (startFromTop)
                rtbData.Select(0, 0);
            int ix = rtbData.Find(tbSearch.Text, rtbData.SelectionStart + 1, RichTextBoxFinds.None);
            if (ix > -1)
            {
                if (!searchList.Contains(tbSearch.Text))
                    searchList.Insert(0, tbSearch.Text);
                rtbData.Select(ix, tbSearch.Text.Length);
            }
            else
            {
                if (!startFromTop)
                {
                    lblPosition.Text = "Text not found to the end - press again to start from the beginning";
                    startFromTop = true;
                    return;
                }
            }
            rtbData.ScrollToCaret();

            startFromTop = false;
        }
    }
}
