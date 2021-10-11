using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB04_01
{
    public partial class frmSearchForm : Form
    {
        public frmSearchForm()
        {
            InitializeComponent();
            CenterToParent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmSearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }
    }
}
