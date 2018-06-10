using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSoftLib
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnCaptchaSolver_Click(object sender, EventArgs e)
        {
            OpenForm(new FromCaptchaSolver());
        }

        public void OpenForm(Form Win)
        {
            this.Visible = false;
            Win.ShowDialog();
            this.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmImageCompare());
        }
    }
}
