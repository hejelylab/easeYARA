using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormScanTarget : Form
    {
        public FormScanTarget()
        {
            InitializeComponent();
        }
        private void btnLocalScan_Click(object sender, EventArgs e)
        {
            ScanDetails.scanTarget = "local";
            OpenNextForm(new Forms.FormChooseScanner());
        }
        private void btnPrepareRemoteScan_Click(object sender, EventArgs e)
        {
            if (!GeneralFunctions.IsAdministrator())
            {
                MessageBox.Show("You need to have admin privileges to proceed.");
                return;
            }
            ScanDetails.scanTarget = "remote";
            OpenNextForm(new Forms.FormChooseScanner());
        }
        private void OpenNextForm(Form nextForm)
        {
            nextForm.TopLevel = false;
            nextForm.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(nextForm);
            nextForm.BringToFront();
            nextForm.Show();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            ScanDetails.scanTarget = "";
            this.Close();
        }
        
    }
}
