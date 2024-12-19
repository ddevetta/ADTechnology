using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADTechnology.AbInitio.UI
{
    public partial class TabControlFrame : UserControl
    {

        public event CloseTabEventHandler CloseTabClicked;

        public TabControlFrame()
        {
            InitializeComponent();
        }

        public string TabName { get; set;  }

        public string ToolTipText { get; set; }

        protected virtual void OnCloseTabClicked(EventArgs e)
        {
            if (this.CloseTabClicked == null)
                return;
            this.CloseTabClicked((object)this, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.OnCloseTabClicked(e);
        }
    }
}
