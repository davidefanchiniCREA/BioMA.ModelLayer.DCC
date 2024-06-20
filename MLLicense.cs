using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRA.ModelLayer.DCC
{
    public partial class MLLicense : Form
    {
       
        public MLLicense()
        {
            InitializeComponent();
            // Show license acceptance if not done before
            if (!File.Exists("Lutil.lsf"))
            {
                chkIdoNotAccept.Visible = false;
                chkLicense.Visible = false;
                this.ControlBox = true;
            }
            
        }

        private void chkLicense_CheckedChanged(object sender, EventArgs e)
        {
            // If license is accepted the hidden file is deleted - see constructor
            if (File.Exists("Lutil.lsf"))
            {
                File.Delete("Lutil.lsf");
                this.Close();
                DeveloperData dd = new DeveloperData();
                dd.ShowDialog();
            }
        }

        private void chkIdoNotAccept_CheckedChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
