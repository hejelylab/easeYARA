using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormScanCompleted : Form
    {
        public FormScanCompleted()
        {
            InitializeComponent();
            if (ScanDetails.scanner == "yara")
            {
                btnViewResults.Visible = false;
            }
        }
        private void btnReturnToMainScreen_Click(object sender, EventArgs e)
        {
            ScanDetails.scanTarget = "";
            ScanDetails.scanner = "";
            ScanDetails.scannerDir = "";
            ScanDetails.scannerDirUNCPath = "";
            ScanDetails.isScannerLocal = false;
            ScanDetails.isScanAllDrives = false;
            ScanDetails.isScanDrive = false;
            ScanDetails.drivesList.Clear();
            ScanDetails.isScanSusDirectories = false;
            ScanDetails.susDirectories2.Clear();
            ScanDetails.scanDirectory = "";
            ScanDetails.isAddRules = false;
            ScanDetails.addRulesFileDir = "";
            ScanDetails.isScanMemory = false;
            ScanDetails.isCPULessThan50 = false;
            ScanDetails.yaraResultsDirectory = "";
            ScanDetails.yaraResultsDirectoryUNCPath = "";
            ScanDetails.computerName = "";
            ScanDetails.generatedCommand = "";
            ScanDetails.generatedCommandArguments = "";
            ScanDetails.statisticsFilesDirs.Clear();
            ScanDetails.scanResultFilesDirs.Clear();
            ScanDetails.isFromChooseDir = false;
            ScanDetails.isFromScanOptions = false;

            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {

                if (f.Name != "MainForm")
                    f.Close();
            }
        }
        private void OpenNextForm(Form nextForm)
        {
            nextForm.TopLevel = false;
            nextForm.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(nextForm);
            nextForm.BringToFront();
            nextForm.Show();
        }

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            foreach (String dir in ScanDetails.scanResultFilesDirs)
            {
                ScanDetails.statisticsFilesDirs.Add(dir);
            }
            OpenNextForm(new Forms.FormStatistics());

        }

        private void btnReturnToOptions_Click(object sender, EventArgs e)
        {
            ScanDetails.isScanAllDrives = false;
            ScanDetails.isScanDrive = false;
            ScanDetails.drivesList.Clear();
            ScanDetails.isScanSusDirectories = false;
            ScanDetails.susDirectories2.Clear();
            ScanDetails.scanDirectory = "";
            ScanDetails.isAddRules = false;
            ScanDetails.addRulesFileDir = "";
            ScanDetails.isScanMemory = false;
            ScanDetails.isCPULessThan50 = false;
            ScanDetails.scanResultFilesDirs.Clear();
            this.Close();
        }
    }
}
