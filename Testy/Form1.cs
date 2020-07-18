using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ADTechnology.Apps.OpenHostFileDialog;

namespace Testy
{
    public partial class Form1 : Form
    {
        public OpenHostFileDialog ohfd;

        public Form1()
        {
            InitializeComponent();
            ohfd = new OpenHostFileDialog();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (ohfd.ShowDialog(this) == DialogResult.OK)
                this.tbName.Text = ohfd.FileName;
        }
    }
}
