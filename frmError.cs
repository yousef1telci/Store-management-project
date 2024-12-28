using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stores_Management
{
    public partial class frmError : Form
    {
        public frmError(string message)
        {
            InitializeComponent();
            lblErrorTxt.Text = message;
            lblErrorTxt.Location = new Point((this.Width - lblErrorTxt.Width) / 2, 90);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
