namespace Testy
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRender = new System.Windows.Forms.Button();
            this.cbRenderAs = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbDBAOption = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRunQuery = new System.Windows.Forms.Button();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbDb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvFA = new System.Windows.Forms.DataGridView();
            this.dgFA3 = new System.Windows.Forms.DataGridView();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgFA2 = new System.Windows.Forms.DataGridView();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FAError = new System.Windows.Forms.Label();
            this.btnFAAll = new System.Windows.Forms.Button();
            this.btnFAOne = new System.Windows.Forms.Button();
            this.tbFileExtension = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.userControl11 = new Testy.UserControl1();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFA3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFA2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your name : ";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(119, 9);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(165, 20);
            this.tbName.TabIndex = 1;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(305, 6);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Hit me";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(515, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save As ...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(597, 32);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(90, 23);
            this.btnRender.TabIndex = 5;
            this.btnRender.Text = "Render As ...";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // cbRenderAs
            // 
            this.cbRenderAs.DropDownWidth = 160;
            this.cbRenderAs.FormattingEnabled = true;
            this.cbRenderAs.Location = new System.Drawing.Point(683, 34);
            this.cbRenderAs.Name = "cbRenderAs";
            this.cbRenderAs.Size = new System.Drawing.Size(21, 21);
            this.cbRenderAs.TabIndex = 6;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1222, 673);
            this.tabControl.TabIndex = 7;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnGo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbName);
            this.tabPage1.Controls.Add(this.userControl11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1214, 647);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OpenHostDialog";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.cbRenderAs);
            this.tabPage2.Controls.Add(this.btnRender);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1214, 647);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ExportData";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cbDBAOption);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.btnRunQuery);
            this.tabPage3.Controls.Add(this.tbQuery);
            this.tabPage3.Controls.Add(this.tbPassword);
            this.tabPage3.Controls.Add(this.tbUser);
            this.tabPage3.Controls.Add(this.tbDb);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1214, 647);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ExportData (query)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbDBAOption
            // 
            this.cbDBAOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDBAOption.FormattingEnabled = true;
            this.cbDBAOption.Location = new System.Drawing.Point(318, 48);
            this.cbDBAOption.Name = "cbDBAOption";
            this.cbDBAOption.Size = new System.Drawing.Size(121, 21);
            this.cbDBAOption.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "DBA Option :";
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Location = new System.Drawing.Point(115, 428);
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(75, 23);
            this.btnRunQuery.TabIndex = 8;
            this.btnRunQuery.Text = "Run";
            this.btnRunQuery.UseVisualStyleBackColor = true;
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // tbQuery
            // 
            this.tbQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQuery.Location = new System.Drawing.Point(115, 102);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbQuery.Size = new System.Drawing.Size(1033, 299);
            this.tbQuery.TabIndex = 7;
            this.tbQuery.Text = "select * from hr.emp_details_view;";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(115, 75);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 6;
            this.tbPassword.Text = "capet0wn";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(115, 48);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 20);
            this.tbUser.TabIndex = 5;
            this.tbUser.Text = "ISR";
            // 
            // tbDb
            // 
            this.tbDb.Location = new System.Drawing.Point(115, 22);
            this.tbDb.Name = "tbDb";
            this.tbDb.Size = new System.Drawing.Size(100, 20);
            this.tbDb.TabIndex = 4;
            this.tbDb.Text = "DDVORA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Query :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "User :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Database :";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer1);
            this.tabPage4.Controls.Add(this.FAError);
            this.tabPage4.Controls.Add(this.btnFAAll);
            this.tabPage4.Controls.Add(this.btnFAOne);
            this.tabPage4.Controls.Add(this.tbFileExtension);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1214, 647);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "FileAssociation";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 81);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvFA);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgFA3);
            this.splitContainer1.Panel2.Controls.Add(this.dgFA2);
            this.splitContainer1.Size = new System.Drawing.Size(1208, 563);
            this.splitContainer1.SplitterDistance = 783;
            this.splitContainer1.TabIndex = 6;
            // 
            // dgvFA
            // 
            this.dgvFA.AllowUserToAddRows = false;
            this.dgvFA.AllowUserToDeleteRows = false;
            this.dgvFA.AllowUserToOrderColumns = true;
            this.dgvFA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFA.Location = new System.Drawing.Point(0, 0);
            this.dgvFA.Name = "dgvFA";
            this.dgvFA.ReadOnly = true;
            this.dgvFA.Size = new System.Drawing.Size(783, 563);
            this.dgvFA.TabIndex = 4;
            this.dgvFA.SelectionChanged += new System.EventHandler(this.dgvFA_SelectionChanged);
            // 
            // dgFA3
            // 
            this.dgFA3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgFA3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFA3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Action,
            this.Command});
            this.dgFA3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFA3.Location = new System.Drawing.Point(0, 316);
            this.dgFA3.MultiSelect = false;
            this.dgFA3.Name = "dgFA3";
            this.dgFA3.ReadOnly = true;
            this.dgFA3.RowHeadersVisible = false;
            this.dgFA3.Size = new System.Drawing.Size(421, 247);
            this.dgFA3.TabIndex = 1;
            // 
            // Action
            // 
            this.Action.DataPropertyName = "Action";
            this.Action.FillWeight = 10F;
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            // 
            // Command
            // 
            this.Command.DataPropertyName = "Command";
            this.Command.FillWeight = 29.91806F;
            this.Command.HeaderText = "Command";
            this.Command.Name = "Command";
            this.Command.ReadOnly = true;
            // 
            // dgFA2
            // 
            this.dgFA2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgFA2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFA2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Key,
            this.Value});
            this.dgFA2.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgFA2.Location = new System.Drawing.Point(0, 0);
            this.dgFA2.Name = "dgFA2";
            this.dgFA2.ReadOnly = true;
            this.dgFA2.RowHeadersVisible = false;
            this.dgFA2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgFA2.Size = new System.Drawing.Size(421, 316);
            this.dgFA2.TabIndex = 0;
            // 
            // Key
            // 
            this.Key.DataPropertyName = "Key";
            this.Key.FillWeight = 20F;
            this.Key.HeaderText = "Field";
            this.Key.MinimumWidth = 100;
            this.Key.Name = "Key";
            this.Key.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            this.Value.FillWeight = 80F;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // FAError
            // 
            this.FAError.AutoSize = true;
            this.FAError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FAError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FAError.Location = new System.Drawing.Point(305, 31);
            this.FAError.Name = "FAError";
            this.FAError.Size = new System.Drawing.Size(0, 13);
            this.FAError.TabIndex = 5;
            // 
            // btnFAAll
            // 
            this.btnFAAll.Location = new System.Drawing.Point(203, 52);
            this.btnFAAll.Name = "btnFAAll";
            this.btnFAAll.Size = new System.Drawing.Size(75, 23);
            this.btnFAAll.TabIndex = 3;
            this.btnFAAll.Text = "Find All";
            this.btnFAAll.UseVisualStyleBackColor = true;
            this.btnFAAll.Click += new System.EventHandler(this.btnFAAll_Click);
            // 
            // btnFAOne
            // 
            this.btnFAOne.Location = new System.Drawing.Point(203, 26);
            this.btnFAOne.Name = "btnFAOne";
            this.btnFAOne.Size = new System.Drawing.Size(75, 23);
            this.btnFAOne.TabIndex = 2;
            this.btnFAOne.Text = "Find";
            this.btnFAOne.UseVisualStyleBackColor = true;
            this.btnFAOne.Click += new System.EventHandler(this.btnFAOne_Click);
            // 
            // tbFileExtension
            // 
            this.tbFileExtension.Location = new System.Drawing.Point(97, 28);
            this.tbFileExtension.Name = "tbFileExtension";
            this.tbFileExtension.Size = new System.Drawing.Size(100, 20);
            this.tbFileExtension.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Extension : ";
            // 
            // userControl11
            // 
            this.userControl11.Location = new System.Drawing.Point(119, 47);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(150, 150);
            this.userControl11.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 697);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "My Pub";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFA3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFA2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnGo;
        private UserControl1 userControl11;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.ComboBox cbRenderAs;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbDb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDBAOption;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox tbFileExtension;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvFA;
        private System.Windows.Forms.Button btnFAAll;
        private System.Windows.Forms.Button btnFAOne;
        private System.Windows.Forms.Label FAError;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgFA2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridView dgFA3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Command;
    }
}

