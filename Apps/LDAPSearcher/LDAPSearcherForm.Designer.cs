namespace ADTechnology.Apps.LDAPSearcher
{
    partial class LDAPSearcherForm 
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.saveResultsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.rowCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.copyWithHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) this.dgResult).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) this.error).BeginInit();
            this.statusStrip.SuspendLayout();
            ///
            /// deSearcher
            /// 
            this.deSearcher.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.deSearcher.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.deSearcher.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.deSearcher.Sort.PropertyName = "samaccountname";
            ///
            /// label1
            ///
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(36, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search  for";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.tbValue.Location = new System.Drawing.Point(516, 46);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(147, 20);
            this.tbValue.TabIndex = 0;
            this.btnGo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.error.SetIconAlignment((System.Windows.Forms.Control) this.btnGo, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.error.SetIconPadding((System.Windows.Forms.Control) this.btnGo, 4);
            this.btnGo.Location = new System.Drawing.Point(774, 44);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Search";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            this.dgResult.AllowUserToAddRows = false;
            this.dgResult.AllowUserToDeleteRows = false;
            this.dgResult.AllowUserToOrderColumns = true;
            this.dgResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.error.SetIconAlignment((System.Windows.Forms.Control) this.dgResult, System.Windows.Forms.ErrorIconAlignment.TopLeft);
            this.dgResult.Location = new System.Drawing.Point(6, 99);
            this.dgResult.Name = "dgResult";
            this.dgResult.ReadOnly = true;
            this.dgResult.RowHeadersVisible = false;
            this.dgResult.Size = new System.Drawing.Size(852, 194);
            this.dgResult.TabIndex = 3;
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.cbOperator);
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.label3);
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.cbType);
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.cbColumn);
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.btnGo);
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.tbValue);
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.label1);
            this.panel1.Controls.Add((System.Windows.Forms.Control) this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 93);
            this.panel1.TabIndex = 0;
            this.cbOperator.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Items.AddRange(new object[4]
            {
            (object) "starts with",
            (object) "contains",
            (object) "equals",
            (object) "in"
            });
            this.cbOperator.Location = new System.Drawing.Point(415, 46);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(95, 21);
            this.cbOperator.TabIndex = 10;
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(228, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "where";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbType.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[3]
            {
            (object) "Person",
            (object) "Group",
            (object) "Computer"
            });
            this.cbType.Location = new System.Drawing.Point(101, 46);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 21);
            this.cbType.TabIndex = 8;
            this.cbColumn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.cbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn.FormattingEnabled = true;
            this.cbColumn.Location = new System.Drawing.Point(270, 46);
            this.cbColumn.MaxDropDownItems = 20;
            this.cbColumn.Name = "cbColumn";
            this.cbColumn.Size = new System.Drawing.Size(139, 21);
            this.cbColumn.TabIndex = 7;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
            {
            (System.Windows.Forms.ToolStripItem) this.fileToolStripMenuItem,
            (System.Windows.Forms.ToolStripItem) this.editToolStripMenuItem,
            (System.Windows.Forms.ToolStripItem) this.helpToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip";
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3]
            {
            (System.Windows.Forms.ToolStripItem) this.saveResultsAsToolStripMenuItem,
            (System.Windows.Forms.ToolStripItem) this.toolStripSeparator1,
            (System.Windows.Forms.ToolStripItem) this.exitToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.saveResultsAsToolStripMenuItem.Name = "saveResultsAsToolStripMenuItem";
            this.saveResultsAsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveResultsAsToolStripMenuItem.Text = "&Save Results as ...";
            this.saveResultsAsToolStripMenuItem.Click += new System.EventHandler(this.saveResultsAsToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.editToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
            {
            (System.Windows.Forms.ToolStripItem) this.copyGridToolStripMenuItem,
            (System.Windows.Forms.ToolStripItem) this.copyWithHeaderToolStripMenuItem
            });
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.copyGridToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyGridToolStripMenuItem.Name = "copyGridToolStripMenuItem";
            this.copyGridToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control;
            this.copyGridToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.copyGridToolStripMenuItem.Text = "&Copy Results";
            this.copyGridToolStripMenuItem.Click += new System.EventHandler(this.copyGridToolStripMenuItem_Click);
            this.helpToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
            {
            (System.Windows.Forms.ToolStripItem) this.aboutToolStripMenuItem
            });
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.error.ContainerControl = (System.Windows.Forms.ContainerControl) this;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
            {
            (System.Windows.Forms.ToolStripItem) this.rowCountLabel,
            (System.Windows.Forms.ToolStripItem) this.statusLabel1
            });
            this.statusStrip.Location = new System.Drawing.Point(6, 293);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(852, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 4;
            this.rowCountLabel.Name = "rowCountLabel";
            this.rowCountLabel.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.rowCountLabel.Size = new System.Drawing.Size(6, 17);
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.statusLabel1.Size = new System.Drawing.Size(6, 17);
            this.copyWithHeaderToolStripMenuItem.Name = "copyWithHeaderToolStripMenuItem";
            this.copyWithHeaderToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyWithHeaderToolStripMenuItem.Text = "Copy with &Header";
            this.copyWithHeaderToolStripMenuItem.Click += new System.EventHandler(this.copyGridWithHeaderToolStripMenuItem_Click);
            this.AcceptButton = (System.Windows.Forms.IButtonControl) this.btnGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 318);
            this.Controls.Add(this.dgResult);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LDAPSearcherForm";
            this.Padding = new System.Windows.Forms.Padding(6, 6, 6, 3);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "LDAP Searcher";
            ((System.ComponentModel.ISupportInitialize) this.dgResult).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) this.error).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.DataGridView dgResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider error;
        private System.Windows.Forms.ComboBox cbColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel rowCountLabel;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveResultsAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyWithHeaderToolStripMenuItem;
    }
}
