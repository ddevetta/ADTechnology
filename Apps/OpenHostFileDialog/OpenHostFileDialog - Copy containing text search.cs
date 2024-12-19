// Decompiled with JetBrains decompiler
// Type: ADTechnology.Windows.Controls.OpenHostFileDialog
// Assembly: OpenHostFileDialog1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=e9ef268d26c5f334
// MVID: F8D282FE-1A5F-453A-946F-DF9521E5E170
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_7\OpenHostFileDialog1.dll

using ADTechnology.Classes;
using ADTechnology.Windows.Controls.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace ADTechnology.Apps
{
    public partial class OpenHostFileDialog : Form
    {

        private HostFile hostfile;
        private FtpShell fc;
        private string fileName;
        private string lastHost = "";
        private string lastUser = "";
        private string lastPath = "";
        private AutoCompleteStringCollection acHosts;
        private AutoCompleteStringCollection acUsers;
        private ListViewColumnSorter lvwColumnSorter;
        private OpenHostFileDialogLocations ohfl;
        private bool keepConnectionOpen = true;

        ArrayList fileArray;
        string selFile = "";

        System.Timers.Timer oneShotTimer;

        public string Host
        {
            get
            {
                return this.tbHost.Text;
            }
            set
            {
                this.tbHost.Text = value;
            }
        }

        public string User
        {
            get
            {
                return this.tbUser.Text;
            }
            set
            {
                this.tbUser.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return this.tbPassword.Text;
            }
            set
            {
                this.tbPassword.Text = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        public HostFile HostFile
        {
            get
            {
                return this.hostfile;
            }
        }

        public FtpShell FTPConnection
        {
            get
            {
                return this.fc;
            }
        }

        public bool KeppConnectionOpen
        {
            get
            {
                return this.keepConnectionOpen;
            }
            set
            { 
                this.keepConnectionOpen = value;
            }
        }

        public OpenHostFileDialog()
        {
            this.InitializeComponent();
            this.acHosts = new AutoCompleteStringCollection();
            this.acUsers = new AutoCompleteStringCollection();
            this.tbHost.AutoCompleteCustomSource = this.acHosts;
            this.tbUser.AutoCompleteCustomSource = this.acUsers;
            this.getAutoComplete();

            this.lvwColumnSorter = new ListViewColumnSorter();
            this.lvFiles.ListViewItemSorter = (IComparer)this.lvwColumnSorter;
            this.lvwColumnSorter.SortColumn = 2;                // Default to Modified date...
            this.lvwColumnSorter.Order = SortOrder.Descending;  // .. descending
            this.lvFiles.Columns[2].ImageIndex = 3;

            oneShotTimer = new System.Timers.Timer(500);
            oneShotTimer.AutoReset = false;
            oneShotTimer.Elapsed += new ElapsedEventHandler(OnTimedKeyUp);
        }

        public long OpenStream()
        {
            if (this.fc == null)
                throw new Exception("Not connected.");
            return this.fc.OpenStream(this.fileName);
        }

        public void CloseStream()
        {
            this.fc.CloseStream();
        }

        private void getAutoComplete()
        {
            if (Settings.Default.Hosts != null)
            {
                string[] array = new string[Settings.Default.Hosts.Count];
                Settings.Default.Hosts.CopyTo(array, 0);
                this.acHosts.AddRange(array);
            }
            if (Settings.Default.Users == null)
                return;
            string[] array1 = new string[Settings.Default.Users.Count];
            Settings.Default.Users.CopyTo(array1, 0);
            this.acUsers.AddRange(array1);
        }

        private void tbHost_Validating(object sender, CancelEventArgs e)
        {
            if (this.tbHost.Text.Trim() == "")
            {
                this.epHost.SetError((Control)sender, "Enter host name.");
                this.tbHost.Select();
                e.Cancel = true;
            }
            else
            {
                this.epHost.Clear();
                e.Cancel = false;
            }
        }

        private void tbUser_Validating(object sender, CancelEventArgs e)
        {
            if (this.tbUser.Text.Trim() == "")
            {
                this.epUser.SetError((Control)sender, "Enter user name, or 'anonymous' if the host accepts anonymous connections.");
                this.tbUser.Select();
                e.Cancel = true;
            }
            else
            {
                this.epUser.Clear();
                e.Cancel = false;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren(ValidationConstraints.Enabled | ValidationConstraints.ImmediateChildren))
                return;

            this.connect();
        }

        private bool connect()
        {
            return this.connect(tbHost.Text, tbUser.Text, tbPassword.Text);
        }

        private bool connect(string host, string user, string password)
        {
            this.hostfile = new HostFile();
            this.fc = new FtpShell();
            this.lvFiles.SelectedIndices.Clear();
            this.lvFiles.Items.Clear();

            this.lblPath.Enabled = this.tbPath.Enabled = this.lblFile.Enabled = this.tbFile.Enabled = this.btnRefresh.Enabled = this.btnCdup.Enabled = this.lvFiles.Enabled = false ;

            this.tellThem("Connecting...");
            try
            {
                this.fc.Connect(host, user, password);
            }
            catch (Exception ex)
            {
                this.fc.Disconnect();
                this.tellThem("Connection failed.");
                MessageBox.Show((IWin32Window)this, ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return this.fc.IsConnected;
            }

            if (this.tbPath.Text.Length > 0)
                this.chdir(this.tbPath.Text);
            else
                this.tbPath.Text = this.pwd();

            this.lblPath.Enabled = this.tbPath.Enabled = this.lblFile.Enabled = this.tbFile.Enabled = this.btnRefresh.Enabled = this.btnCdup.Enabled = this.lvFiles.Enabled = true;
            
            if (!this.acHosts.Contains(this.tbHost.Text))
            {
                this.acHosts.Add(this.tbHost.Text);
                Settings.Default.Hosts.Add(this.tbHost.Text);
            }
            if (!this.acUsers.Contains(this.tbUser.Text))
            {
                this.acUsers.Add(this.tbUser.Text);
                Settings.Default.Users.Add(this.tbUser.Text);
            }
            this.lastHost = this.hostfile.Host = this.tbHost.Text;
            this.lastUser = this.tbUser.Text;

            Cursor.Current = Cursors.Default;
            return this.fc.IsConnected;
        }

        private void dir()
        {
            this.lvFiles.SelectedIndices.Clear();
            this.btnOpen.Enabled = false;
            this.tellThem("Retrieving directory listing...");
            try
            {
                this.fileArray = this.fc.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show((IWin32Window)this, "Error retrieving directory information : " + ex.Message + "\r\n\r\n" + ex.StackTrace, "FTP Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            filter("");

            lvFiles.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            if (lvFiles.Columns[0].Width < this.Width / 3)
                lvFiles.Columns[0].Width = this.Width / 3;

            this.lastPath = this.tbPath.Text = this.pwd();
            this.tbFile.Text = "";
            this.tellThem(this.tbUser.Text + "@" + this.tbHost.Text + " (" + this.fc.ServerVersion + ")");
            Cursor.Current = Cursors.Default;
        }

        private delegate void filterDelegate(string filterText);

        private void filter(string filterText)
        {
            this.lvFiles.SuspendLayout();
            this.lvFiles.Items.Clear();
            this.lvFiles.SelectedIndexChanged -= new System.EventHandler(this.lvFiles_SelectedIndexChanged);

            object[] parms = new object[] 
            { 
                this.lvFiles, this.fileArray, filterText, this.selFile, this.btnOpen 
            };

            updateListDelegate call = new updateListDelegate(updateList);
            this.lvFiles.BeginInvoke(call, parms);
        }

        private delegate void updateListDelegate(ListView lv, ArrayList fileList, string filterText, string selectedFile, Button openButton);

        public void updateList(ListView lv, ArrayList fileList, string filterText, string selectedFile, Button openButton)
        {
            ListViewItem added;
            System.Diagnostics.Debug.WriteLine("UpdateList");
            openButton.Enabled = false;
            for (int x = 0; x < fileArray.Count; x++)
            {
                string[] items = (string[])fileList[x];
                if (filterText == ""                    // Blank filter...
                    || items[0].Contains(filterText))   // ... or filename contains filter text
                {
                    added = lv.Items.Add(new ListViewItem(items));
                    added.ImageIndex = !(items[3] == "D") ? (!(items[3] == "L") ? -1 : 1) : 0;
                    if (selectedFile == items[0])
                    {
                        added.Selected = true;
                        if (items[0] == filterText)
                            openButton.Enabled = true;
                    }
                }
            }

            if (lv.SelectedIndices.Count > 0)
                lv.EnsureVisible(lv.SelectedIndices[0]);
            lv.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
        }

        private void chdir(string dir)
        {
            try
            {
                this.fc.ChangeDir(dir);
            }
            catch (Exception ex)
            {
                if (!this.fc.IsConnected)
                {
                    if (this.connect())
                        this.chdir(dir);
                    return;
                }
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            this.dir();
        }

        private void cdup()
        {
            try
            {
                this.fc.CdUp();
            }
            catch
            {
                if (!this.fc.IsConnected)
                {
                    if (this.connect())
                        this.cdup();
                }
                return;
            }
            this.dir();
        }

        private string pwd()
        {
            string dir = null;
            try
            {
                dir = this.fc.GetWorkingDirectory();
            }
            catch (Exception ex)
            {
                if (!this.fc.IsConnected)
                {
                    if (this.connect())
                        dir = this.pwd();
                    return dir;
                }
                MessageBox.Show((IWin32Window)this, "Error retrieving current working directory : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return dir;
        }

        private void OpenHostFileDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.keepConnectionOpen 
                && this.fc != null
                && this.fc.IsConnected)
            {
                this.tellThem("Closing connection...");
                try
                {
                    this.fc.Disconnect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            this.saveAutoCorrect();
        }

        private void saveAutoCorrect()
        {
            Settings.Default.Save();
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selFile = "";
            if (this.lvFiles.SelectedIndices.Count < 1)
                return;
            if (this.lvFiles.SelectedItems[0].SubItems[3].Text == "F")
                this.tbFile.Text = this.selFile = this.lvFiles.SelectedItems[0].Text;
            this.btnOpen.Enabled = true;
            this.AcceptButton = (IButtonControl)this.btnOpen;
        }

        private void lvFiles_Click(object sender, EventArgs e)
        {
            if (this.lvFiles.SelectedItems.Count > 0)
                this.tbFile.Text = lvFiles.SelectedItems[0].SubItems[0].Text;
        }

        private void lvFiles_DoubleClick(object sender, EventArgs e)
        {
            this.btnOpen_Click(sender, e);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.lvFiles.SelectedIndices.Count < 1 && this.tbFile.Text.Trim() == "")
                return;
            if (this.lvFiles.SelectedIndices.Count > 0)
            {
                switch (this.lvFiles.SelectedItems[0].SubItems[3].Text)
                { 
                    case "D":
                        this.chdir(this.lvFiles.SelectedItems[0].Text);
                        break;
                    case "F":
                        this.hostfile.Path = this.tbPath.Text;
                        this.hostfile.FileName = this.tbFile.Text;
                        this.fileName = this.tbPath.Text + (this.tbPath.Text.Length > 0 ? this.tbPath.Text.Substring(0, 1) : "") + this.tbFile.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    default:
                        return;
                }
            }
        }

        private void _host_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = (IButtonControl)this.btnConnect;
        }

        private void btnCdup_Click(object sender, EventArgs e)
        {
            this.cdup();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.chdir(this.tbPath.Text);
        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = (IButtonControl)this.btnRefresh;
        }

        private string tellThem(string stuff)
        {
            string text = this.toolStripStatus.Text;
            this.toolStripStatus.Text = stuff;
            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;
            return text;
        }

        private void lvFiles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lvwColumnSorter.SortColumn > -1)
            {
                this.lvFiles.Columns[this.lvwColumnSorter.SortColumn].ImageIndex = -1;
                this.lvFiles.Columns[this.lvwColumnSorter.SortColumn].ImageKey = null;
            }
            if (e.Column == this.lvwColumnSorter.SortColumn)
            {
                if (this.lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    this.lvwColumnSorter.Order = SortOrder.Descending;
                    this.lvFiles.Columns[e.Column].ImageIndex = 3;
                }
                else
                {
                    this.lvwColumnSorter.Order = SortOrder.Ascending;
                    this.lvFiles.Columns[e.Column].ImageIndex = 2;
                }
            }
            else
            {
                this.lvwColumnSorter.SortColumn = e.Column;
                this.lvwColumnSorter.Order = SortOrder.Ascending;
                this.lvFiles.Columns[e.Column].ImageIndex = 2;
            }
            this.lvFiles.Sort();
        }

        private void btnLocs_Click(object sender, EventArgs e)
        {
            if (this.ohfl == null)
                this.ohfl = new OpenHostFileDialogLocations();

            this.ohfl.Host = this.tbHost.Text;
            this.ohfl.User = this.tbUser.Text;
            this.ohfl.Pass = this.tbPassword.Text;
            this.ohfl.Path = this.tbPath.Text;

            if (this.ohfl.ShowDialog((IWin32Window)this) == DialogResult.OK)
            {
                this.tbHost.Text = this.ohfl.Host;
                this.tbUser.Text = this.ohfl.User;
                this.tbPassword.Text = this.ohfl.Pass;
                this.tbPath.Text = this.ohfl.Path;
                if (this.connect(this.ohfl.Host, this.ohfl.User, this.ohfl.Pass))
                {
                    this.lastHost = this.ohfl.Host;
                    this.lastUser = this.ohfl.User;
                }
            }
        }

        private void OpenHostFileDialog_Activated(object sender, EventArgs e)
        {
            if (lvFiles.SelectedIndices.Count > 0)
                lvFiles.EnsureVisible(lvFiles.SelectedIndices[0]);
        }

        private void tbFile_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbFile.Text.Length == 1 || tbFile.Text.Length == 2)  // Don't filter 1 or 2 chars
                return;

            if (oneShotTimer.Enabled)
                return;

            oneShotTimer.Start();
        }

        private void OnTimedKeyUp(Object source, ElapsedEventArgs e)
        {
            oneShotTimer.Enabled = false;
            filterDelegate call = new filterDelegate(filter);
            this.Invoke(call, new object[] { tbFile.Text } );
        }

        private void lvFiles_Resize(object sender, EventArgs e)
        {
            lvFiles.Columns[0].Width = lvFiles.Width - lvFiles.Columns[1].Width - lvFiles.Columns[2].Width;

        }
    }
}
