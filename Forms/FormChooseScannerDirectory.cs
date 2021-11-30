using System;
using System.IO;
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
            else
            {
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
                    }

                }
            }
            else
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtbxScannerDir.Text = folderBrowserDialog1.SelectedPath;
                }
            }
        }
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (ScanDetails.isScannerLocal)
            {
                if (!File.Exists(txtbxScannerDir.Text))
                {
                    MessageBox.Show("Please choose scanner executable: loki.exe or yara64.exe");
                    return;
                }

                if (txtbxScannerDir.Text.EndsWith("loki.exe"))
                {
                    ScanDetails.scanner = "loki";
                    ScanDetails.scannerDir = txtbxScannerDir.Text.Substring(0, txtbxScannerDir.Text.LastIndexOf("\\loki.exe"));

                    DialogResult dialogResult = MessageBox.Show("Would you like to update Loki and its signatures?", "Loki Update", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ScanDetails.isFromChooseDir = true;
                        OpenNextForm(new Forms.FormInstalling());
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        OpenNextForm(new Forms.FormScanOptions());
                    }
                }

                else if (txtbxScannerDir.Text.EndsWith("yara64.exe"))
                {
                    ScanDetails.scanner = "yara";
                    ScanDetails.scannerDir = txtbxScannerDir.Text.Substring(0, txtbxScannerDir.Text.LastIndexOf("\\yara64.exe"));

                    OpenNextForm(new Forms.FormScanOptions());
                }
                else
                {
                    MessageBox.Show("Please choose scanner executable: loki.exe or yara64.exe");
                    return;
                }
            }

            else
            {
                if (!Directory.Exists(txtbxScannerDir.Text))
                {
                    MessageBox.Show("Please select a valid directory");
                    return;
                }

                ScanDetails.scannerDir = txtbxScannerDir.Text;
                ScanDetails.isFromChooseDir = true;
                OpenNextForm(new Forms.FormInstalling());
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
    }
}
