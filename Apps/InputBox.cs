using System.Windows.Forms;

namespace ADTechnology.Apps
{
    public partial class InputBox : Form
    {
        /// <summary>
        /// Gets or sets the value of the input text box
        /// </summary>
        public string Value
        {
            get { return this.inputValue.Text; }
            set { this.inputValue.Text = value; }
        }

        public InputBox()
        {
            InitializeComponent();
        }

        public InputBox(string label, string value)
        {
            InitializeComponent();
            this.inputLabel.Text = label;
            this.inputValue.Text = value;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
