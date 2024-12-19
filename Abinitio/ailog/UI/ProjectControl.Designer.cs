using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
    public partial class ProjectControl
    {
        private IContainer components = null;
        private Label label1;
        private TextBox tbFileName;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblStatus;
        private Label lblPhases;
        private Label label5;
        private ComboBox cbPhase;
        private Label label7;
        private TextBox lblStart;
        private TextBox lblEnd;
        private TextBox lblDuration;
        private PictureBox pbStatus;
        private Label lblRevision;
        private ComboBox cbRevision;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPhases = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPhase = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.TextBox();
            this.lblEnd = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.TextBox();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            this.lblRevision = new System.Windows.Forms.Label();
            this.cbRevision = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log name :";
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(124, 16);
            this.tbFileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbFileName.Multiline = true;
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(942, 26);
            this.tbFileName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project Status :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(259, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Start :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(500, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "End :";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(153, 54);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.MinimumSize = new System.Drawing.Size(40, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 17);
            this.lblStatus.TabIndex = 7;
            // 
            // lblPhases
            // 
            this.lblPhases.AutoSize = true;
            this.lblPhases.Location = new System.Drawing.Point(327, 85);
            this.lblPhases.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhases.MinimumSize = new System.Drawing.Size(27, 0);
            this.lblPhases.Name = "lblPhases";
            this.lblPhases.Size = new System.Drawing.Size(27, 17);
            this.lblPhases.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Phase :";
            // 
            // cbPhase
            // 
            this.cbPhase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhase.FormattingEnabled = true;
            this.cbPhase.Location = new System.Drawing.Point(124, 81);
            this.cbPhase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbPhase.Name = "cbPhase";
            this.cbPhase.Size = new System.Drawing.Size(193, 24);
            this.cbPhase.TabIndex = 11;
            this.cbPhase.SelectedIndexChanged += new System.EventHandler(this.cbPhase_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(749, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Duration :";
            // 
            // lblStart
            // 
            this.lblStart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblStart.Location = new System.Drawing.Point(313, 54);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblStart.MinimumSize = new System.Drawing.Size(27, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.ReadOnly = true;
            this.lblStart.Size = new System.Drawing.Size(179, 15);
            this.lblStart.TabIndex = 14;
            // 
            // lblEnd
            // 
            this.lblEnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblEnd.Location = new System.Drawing.Point(551, 54);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblEnd.MinimumSize = new System.Drawing.Size(27, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.ReadOnly = true;
            this.lblEnd.Size = new System.Drawing.Size(179, 15);
            this.lblEnd.TabIndex = 15;
            // 
            // lblDuration
            // 
            this.lblDuration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblDuration.Location = new System.Drawing.Point(828, 54);
            this.lblDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblDuration.MinimumSize = new System.Drawing.Size(27, 0);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.ReadOnly = true;
            this.lblDuration.Size = new System.Drawing.Size(121, 15);
            this.lblDuration.TabIndex = 16;
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(124, 54);
            this.pbStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(24, 22);
            this.pbStatus.TabIndex = 17;
            this.pbStatus.TabStop = false;
            // 
            // lblRevision
            // 
            this.lblRevision.AutoSize = true;
            this.lblRevision.Location = new System.Drawing.Point(420, 85);
            this.lblRevision.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRevision.Name = "lblRevision";
            this.lblRevision.Size = new System.Drawing.Size(70, 17);
            this.lblRevision.TabIndex = 18;
            this.lblRevision.Text = "Revision :";
            // 
            // cbRevision
            // 
            this.cbRevision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRevision.FormattingEnabled = true;
            this.cbRevision.Location = new System.Drawing.Point(500, 81);
            this.cbRevision.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbRevision.Name = "cbRevision";
            this.cbRevision.Size = new System.Drawing.Size(501, 24);
            this.cbRevision.TabIndex = 19;
            this.cbRevision.SelectedIndexChanged += new System.EventHandler(this.cbRevision_SelectedIndexChanged);
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbRevision);
            this.Controls.Add(this.lblRevision);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbPhase);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPhases);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(1081, 134);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
