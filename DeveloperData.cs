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
    public partial class DeveloperData : Form
    {
        public DeveloperData()
        {
            InitializeComponent();
            LoadDeveloperData();
        }

        private DataSet developerData = new DataSet();
        private const string devdataFile = "AuthorSettings.xml";

        private void LoadDeveloperData()
        {
            if (File.Exists(devdataFile))
            {
                developerData.ReadXml(devdataFile);
                txtNameLastname.Text = developerData.Tables[0].Rows[0][0].ToString();
                txtEmail.Text = developerData.Tables[0].Rows[0][1].ToString();
                txtInstitution.Text = developerData.Tables[0].Rows[0][2].ToString();
                txtURL.Text = developerData.Tables[0].Rows[0][3].ToString();
            }
        }

        private void btnExitDeveloperData_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveDeveloperData_Click(object sender, EventArgs e)
        {
            developerData.Tables[0].Rows[0][0] = txtNameLastname.Text;
            developerData.Tables[0].Rows[0][1] = txtEmail.Text;
            developerData.Tables[0].Rows[0][2] = txtInstitution.Text;
            developerData.Tables[0].Rows[0][3] = txtURL.Text;
            developerData.WriteXml(devdataFile);
            Close();
        }
    }
}
