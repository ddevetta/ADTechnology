using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ADTechnology.AbInitio.Classes;

namespace ADTechnology.AbInitio.UI
{
    public partial class ParametersBrowserControl : TabControlFrame
    {

        internal ExecutionParameters ExecutionParams { get; set; }

        public ParametersBrowserControl()
        {
            InitializeComponent();
            this.pbSearch.BackgroundImage = Images.search;
            this.pbSearch.BackgroundImageLayout = ImageLayout.Stretch;
        }

        internal void Fill(ExecutionParameters executionParams)
        {
            tbFileName.Text = "Execution Parameters";
            ExecutionParams = executionParams;
            dgParms.DataSource = executionParams;
        }

        private void searchText()
        {
            string rowFilter = null;

            if (tbSearch.TextLength > 0)
                rowFilter = "Parameter like '%" + tbSearch.Text + "%' or Value like '%" + tbSearch.Text + "%'";

            ((DataTable)dgParms.DataSource).DefaultView.RowFilter = rowFilter;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            searchText();
        }
    }
}
