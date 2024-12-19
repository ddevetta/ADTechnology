// Decompiled with JetBrains decompiler
// Type: ADTechnology.AbInitio.UI.ProjectControl
// Assembly: ailog, Version=1.0.5827.22454, Culture=neutral, PublicKeyToken=null
// MVID: 579C0A01-7810-4FA2-81F0-9D716001FCE7
// Assembly location: C:\Users\DaveAndTina\Documents\Projects\ailog installation\Application Files\ailog_1_0_0_8\ailog.exe

using ADTechnology.AbInitio.Classes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
    public partial class ProjectControl : UserControl
    {
        private Project prj;

        public string FileName
        {
            get
            {
                return this.tbFileName.Text;
            }
            set
            {
                this.tbFileName.Text = value;
            }
        }

        internal Project Project
        {
            get
            {
                return this.prj;
            }
            set
            {
                this.prj = value;
            }
        }

        public event EventHandler SelectedPhaseChanged;

        protected virtual void OnSelectedPhaseChanged(EventArgs e)
        {
            if (this.SelectedPhaseChanged == null)
                return;
            this.SelectedPhaseChanged((object)this, e);
        }

        public event EventHandler SelectedRevisionChanged;

        protected virtual void OnSelectedRevisionChanged(EventArgs e)
        {
            if (this.SelectedRevisionChanged == null)
                return;
            this.SelectedRevisionChanged((object)this, e);
        }

        internal Phase SelectedPhase
        {
            get
            {
                return (Phase)this.cbPhase.SelectedValue;
            }
            set
            {
                this.cbPhase.SelectedValue = (object)value;
            }
        }

        internal int SelectedRevision
        {
            get
            {
                return this.cbRevision.SelectedIndex;
            }
            set
            {
                this.cbRevision.SelectedIndex = value;
            }
        }

        public ProjectControl()
        {
            this.InitializeComponent();
        }

        public void Initialise(string fileName)
        {
            tbFileName.Text = fileName;
            lblStatus.Text = lblStart.Text = lblEnd.Text = lblDuration.Text = lblPhases.Text = string.Empty;
            cbPhase.DataSource = null;
            cbRevision.DataSource = null;
            pbStatus.Image = null;
        }

        public void DataBind()
        {
            this.lblStatus.DataBindings.Clear();
            this.lblStart.DataBindings.Clear();
            this.lblEnd.DataBindings.Clear();
            this.lblDuration.DataBindings.Clear();
            this.lblPhases.DataBindings.Clear();
            if (this.prj == null)
                return;
            this.lblStatus.DataBindings.Add("Text", (object)this.prj.Status, (string)null);
            if (this.prj.Start > DateTime.MinValue)
                this.lblStart.DataBindings.Add("Text", (object)this.prj.Start, (string)null);
            else
                this.lblStart.Text = "";
            if (this.prj.End > DateTime.MinValue)
                this.lblEnd.DataBindings.Add("Text", (object)this.prj.End, (string)null);
            else
                this.lblEnd.Text = "";
            if (this.prj.End > DateTime.MinValue || this.prj.Status == ProjectStatus.Running)
            {
                DateTime dateTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                this.lblDuration.DataBindings.Add("Text", (object)((this.prj.End > DateTime.MinValue ? this.prj.End : dateTime) - this.prj.Start), (string)null);
            }
            else
                this.lblDuration.Text = "";
            this.lblPhases.DataBindings.Add("Text", (object)("(" + this.prj.Phases.Count.ToString() + " phases)"), (string)null);
            this.cbPhase.DataSource = (object)this.prj.Phases;
            if (this.prj.Status == ProjectStatus.Running)
                this.cbPhase.SelectedIndex = this.cbPhase.Items.Count - 1;
            this.pbStatus.Paint += new PaintEventHandler(this.pbStatus_Paint);
            this.pbStatus.Refresh();
        }

        private void pbStatus_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bitmap = Images.dot_notstarted;
            if (this.prj != null)
            {
                switch (this.prj.Status)
                {
                    case ProjectStatus.Running:
                        bitmap = Images.dot_running;
                        break;
                    case ProjectStatus.Ended:
                        bitmap = Images.dot_cmplt;
                        break;
                    case ProjectStatus.Error:
                        bitmap = Images.dot_failed;
                        break;
                }
            }
            Color pixel = bitmap.GetPixel(0, 0);
            bitmap.MakeTransparent(pixel);
            Rectangle rect = new Rectangle(0, 0, 12, 12);
            e.Graphics.DrawImage((Image)bitmap, rect);
        }

        private void cbPhase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbPhase.SelectedValue == null)
                return;
            this.cbRevision.DataSource = (object)((Phase)this.cbPhase.SelectedValue).PhaseRevisions;
            this.cbRevision.SelectedIndex = 0;
            this.OnSelectedPhaseChanged(e);
        }

        private void cbRevision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnSelectedRevisionChanged(e);
        }
    }
}
