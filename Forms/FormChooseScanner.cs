using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormChooseScanner : Form
    {
        public FormChooseScanner()
        {
            InitializeComponent();
        }
        private void btnYaraScanner_Click(object sender, EventArgs e)
        {
            ScanDetails.scanner = "yara";
            OpenNextForm(new Forms.FormChooseScannerDirectory());
            
        }
        private void btnLokiScanner_Click(object sender, EventArgs e)
        {
            ScanDetails.scanner = "loki";
            OpenNextForm(new Forms.FormChooseScannerDirectory());
        }
        private void btnHaveScanner_Click(object sender, EventArgs e)
        {
            ScanDetails.isScannerLocal = true;
            OpenNextForm(new Forms.FormChooseScannerDirectory());
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            ScanDetails.isScannerLocal = false;
            ScanDetails.scanner = "";
            this.Close();
        }
        private void OpenNextForm(Form nextForm)
        {
            nextForm.TopLevel = false;
            nextForm.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(nextForm);
            nextForm.BringToFront();
            nextForm.Show();
        }
    }
}
