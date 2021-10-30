using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormChooseScannerDirectory : Form
    {
        public FormChooseScannerDirectory()
        {
            InitializeComponent();
            if (ScanDetails.scanTarget.Equals("local"))
            {
                if (ScanDetails.isScannerLocal)
                {
                    lblChooseScannerDirPnl.Text = "Choose the scanner exe (loki.exe or yara64.exe): ";
                    btnInstall.Text = "Next";
                }
            }
            else
            {
                lblComment1.Visible = true;
                lblComment2.Visible = true;
                if (ScanDetails.isScannerLocal)
                {
                    lblChooseScannerDirPnl.Text = "Choose the scanner exe (loki.exe or yara64.exe): ";
                    btnInstall.Text = "Next";
                }
            }
            
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            ScanDetails.scannerDir = "";
            if (ScanDetails.isScannerLocal)
            {
                ScanDetails.isScannerLocal = false;
                ScanDetails.scanner = "";
            }
            else{
                lblChooseScannerDirPnl.Text = "Choose Directory";
                btnInstall.Text = "Install";
            }
            this.Close();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ScanDetails.isScannerLocal)
            {
                using (openFileDialog1)
                {
                    openFileDialog1.InitialDirectory = "c:\\";
                    openFileDialog1.Filter = "exe files (*.exe)|*.exe";
                    openFileDialog1.FilterIndex = 2;
                    openFileDialog1.RestoreDirectory = true;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        txtbxScannerDir.Text = openFileDialog1.FileName;
                        if (Path.GetFileName(openFileDialog1.FileName) == "loki.exe")
                        {
                            ScanDetails.scanner = "loki";
                            ScanDetails.scannerDir = Path.GetDirectoryName(openFileDialog1.FileName);
                            //ScanDetails.scannerDir = txtbxScannerDir.Text.Substring(0, txtbxScannerDir.Text.LastIndexOf("\\loki.exe"));
                        }
                        else if (Path.GetFileName(openFileDialog1.FileName) == "yara64.exe")
                        {
                            ScanDetails.scanner = "yara";
                            ScanDetails.scannerDir = Path.GetDirectoryName(openFileDialog1.FileName);
                            //ScanDetails.scannerDir = txtbxScannerDir.Text.Substring(0, txtbxScannerDir.Text.LastIndexOf("\\yara64.exe")); ;
                        }
                        else
                        {
                            ScanDetails.scanner = Path.GetFileName(openFileDialog1.FileName);
                            MessageBox.Show("Please choose scanner executable: loki.exe or yara64.exe");
                        }
                    }

                }
            }
            else
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtbxScannerDir.Text = folderBrowserDialog1.SelectedPath;
                    ScanDetails.scannerDir = txtbxScannerDir.Text;
                }
            }
        }
        private void btnInstall_Click(object sender, EventArgs e)
        {
            //if (ScanDetails.isScannerLocal && ScanDetails.scanner == "loki")
            //{
            //    ScanDetails.scannerDir = txtbxScannerDir.Text.Substring(0, txtbxScannerDir.Text.LastIndexOf("\\loki.exe"));
            //}
            
            //if (ScanDetails.isScannerLocal && ScanDetails.scanner == "yara")
            //{
            //    ScanDetails.scannerDir = txtbxScannerDir.Text.Substring(0, txtbxScannerDir.Text.LastIndexOf("\\yara64.exe"));
            //}

            //if (!ScanDetails.isScannerLocal)
            //{
            //    ScanDetails.scannerDir = txtbxScannerDir.Text;
            //}

            //MessageBox.Show(ScanDetails.scannerDir);

            if (ScanDetails.isScannerLocal && (!ScanDetails.scanner.Equals("loki") && !ScanDetails.scanner.Equals("yara")))
            {
                MessageBox.Show("Please choose scanner executable: loki.exe or yara64.exe");
            }
            else
            {
                if (ScanDetails.isScannerLocal)
                {
                    if (ScanDetails.scanner.Equals("loki"))
                    {
                        DialogResult dialogResult = MessageBox.Show("Would you like to update Loki and its signatures?", "Loki Update", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            runLokiUpdate();
                            OpenNextForm(new Forms.FormScanOptions());
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            OpenNextForm(new Forms.FormScanOptions());
                        }
                    }
                    else
                    {
                        OpenNextForm(new Forms.FormScanOptions());
                    }
                }
                else
                {
                    ScanDetails.isFromChooseDir = true;
                    OpenNextForm(new Forms.FormInstalling());
                }
            }
            
        }
        private void runLokiUpdate()
        {
            Directory.SetCurrentDirectory(ScanDetails.scannerDir);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = ScanDetails.scannerDir + "\\loki.exe";
            startInfo.Arguments = " --update";
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            if (System.Environment.OSVersion.Version.Major >= 6)
            {
                process.StartInfo.Verb = "runas";
            }
    
            process.Start();
            process.WaitForExit();
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
