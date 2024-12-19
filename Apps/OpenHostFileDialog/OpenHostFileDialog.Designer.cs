
using ADTechnology.Classes;
using ADTechnology.Windows.Controls.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.Apps.OpenHostFileDialog
{
    public partial class OpenHostFileDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private TextBox tbHost;
        private Label label1;
        private TextBox tbUser;
        private Label label2;
        private TextBox tbPassword;
        private Label label3;
        private Button btnConnect;
        private Label lblPath;
        private TextBox tbPath;
        private Button btnCancel;
        private Button btnOpen;
        private Button btnCdup;
        private ErrorProvider epHost;
        private ErrorProvider epUser;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripStatus;
        private ListView lvFiles;
        private ColumnHeader colFileName;
        private ColumnHeader colFileSize;
        private ColumnHeader colModified;
        private ColumnHeader colFileType;
        private ImageList imageList;
        private Button btnRefresh;
        private Label lblFile;
        private TextBox tbFile;
        private Button btnLocs;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenHostFileDialog));
            this.tbHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnCdup = new System.Windows.Forms.Button();
            this.epHost = new System.Windows.Forms.ErrorProvider(this.components);
            this.epUser = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModifiedSorter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btnLocs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.epHost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epUser)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbHost
            // 
            this.tbHost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbHost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbHost.Location = new System.Drawing.Point(89, 12);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(160, 20);
            this.tbHost.TabIndex = 0;
            this.tbHost.TextChanged += new System.EventHandler(this._host_TextChanged);
            this.tbHost.Validating += new System.ComponentModel.CancelEventHandler(this.tbHost_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Host :";
            // 
            // tbUser
            // 
            this.tbUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbUser.Location = new System.Drawing.Point(89, 39);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 20);
            this.tbUser.TabIndex = 1;
            this.tbUser.TextChanged += new System.EventHandler(this._host_TextChanged);
            this.tbUser.Validating += new System.ComponentModel.CancelEventHandler(this.tbUser_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "User :";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(89, 64);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.TextChanged += new System.EventHandler(this._host_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password :";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(695, 64);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(74, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Enabled = false;
            this.lblPath.Location = new System.Drawing.Point(24, 105);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(35, 13);
            this.lblPath.TabIndex = 21;
            this.lblPath.Text = "Path :";
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.Enabled = false;
            this.tbPath.Location = new System.Drawing.Point(89, 102);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(600, 20);
            this.tbPath.TabIndex = 4;
            this.tbPath.TextChanged += new System.EventHandler(this.tbPath_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(695, 510);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(695, 481);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(74, 23);
            this.btnOpen.TabIndex = 9;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnCdup
            // 
            this.btnCdup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCdup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCdup.BackgroundImage")));
            this.btnCdup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCdup.Enabled = false;
            this.btnCdup.Location = new System.Drawing.Point(725, 99);
            this.btnCdup.Name = "btnCdup";
            this.btnCdup.Size = new System.Drawing.Size(24, 24);
            this.btnCdup.TabIndex = 6;
            this.btnCdup.UseVisualStyleBackColor = true;
            this.btnCdup.Click += new System.EventHandler(this.btnCdup_Click);
            // 
            // epHost
            // 
            this.epHost.ContainerControl = this;
            // 
            // epUser
            // 
            this.epUser.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(781, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // lvFiles
            // 
            this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colFileSize,
            this.colModified,
            this.colFileType,
            this.colModifiedSorter});
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.HideSelection = false;
            this.lvFiles.Location = new System.Drawing.Point(89, 129);
            this.lvFiles.MultiSelect = false;
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(680, 347);
            this.lvFiles.SmallImageList = this.imageList;
            this.lvFiles.TabIndex = 7;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvFiles_ColumnClick);
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
            this.lvFiles.Click += new System.EventHandler(this.lvFiles_Click);
            this.lvFiles.DoubleClick += new System.EventHandler(this.lvFiles_DoubleClick);
            this.lvFiles.Resize += new System.EventHandler(this.lvFiles_Resize);
            // 
            // colFileName
            // 
            this.colFileName.Text = "Name";
            this.colFileName.Width = 450;
            // 
            // colFileSize
            // 
            this.colFileSize.Text = "Size";
            this.colFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colFileSize.Width = 80;
            // 
            // colModified
            // 
            this.colModified.Text = "Modified";
            this.colModified.Width = 120;
            // 
            // colFileType
            // 
            this.colFileType.Text = "Type";
            this.colFileType.Width = 0;
            // 
            // colModifiedSorter
            // 
            this.colModifiedSorter.Text = "ModSort";
            this.colModifiedSorter.Width = 0;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "XpFolder18.bmp");
            this.imageList.Images.SetKeyName(1, "XpMove24_2.bmp");
            this.imageList.Images.SetKeyName(2, "SortUp.bmp");
            this.imageList.Images.SetKeyName(3, "SortDown.bmp");
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(695, 99);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(24, 24);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblFile
            // 
            this.lblFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFile.AutoSize = true;
            this.lblFile.Enabled = false;
            this.lblFile.Location = new System.Drawing.Point(24, 485);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(29, 13);
            this.lblFile.TabIndex = 25;
            this.lblFile.Text = "File :";
            // 
            // tbFile
            // 
            this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFile.Enabled = false;
            this.tbFile.Location = new System.Drawing.Point(89, 482);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(600, 20);
            this.tbFile.TabIndex = 24;
            this.tbFile.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFile_KeyUp);
            // 
            // btnLocs
            // 
            this.btnLocs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocs.CausesValidation = false;
            this.btnLocs.Location = new System.Drawing.Point(695, 10);
            this.btnLocs.Name = "btnLocs";
            this.btnLocs.Size = new System.Drawing.Size(75, 23);
            this.btnLocs.TabIndex = 26;
            this.btnLocs.Text = "Locations...";
            this.btnLocs.UseVisualStyleBackColor = true;
            this.btnLocs.Click += new System.EventHandler(this.btnLocs_Click);
            // 
            // OpenHostFileDialog
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(781, 562);
            this.Controls.Add(this.btnLocs);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCdup);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbHost);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "OpenHostFileDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Open from Host";
            this.Activated += new System.EventHandler(this.OpenHostFileDialog_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenHostFileDialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.epHost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epUser)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ColumnHeader colModifiedSorter;
    }
}