using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormScanOptions : Form
    {
        bool isEdit = false;
        bool clearOptions = false;
        bool addRulesSuccess = false;

        public FormScanOptions()
        {
            InitializeComponent();
            foreach (var item in ScanDetails.susDirectories)
            {
                if (!rchtxbxSusDirectories.Lines.Contains(item))
                {
                    rchtxbxSusDirectories.AppendText(item + "\r\n");
                }

            }
            if (ScanDetails.scanTarget.Equals("local"))
            {
                chklstDrives.Visible = true;
                if (chklstDrives.Items.Count == 0)
                {
                    List<string> localDrives = ScanDetails.getLocalDrives();
                    foreach (var item in localDrives)
                    {
                        chklstDrives.Items.Add(item.Substring(0, 1));
                    }
                }
            }
            else
            {
                txtbxDrivesLetters.Visible = true;
                lblComment1.Visible = true;
                btnScan.Text = "Next";
            }

            if (ScanDetails.scanner == "yara")
            {
                chkbxScanMemory.Visible = false;
                chkbxUseLessCPU.Location = new System.Drawing.Point(chkbxUseLessCPU.Location.X, chkbxUseLessCPU.Location.Y - 29);
                label5.Location = new System.Drawing.Point(label5.Location.X, label5.Location.Y - 29);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            ScanDetails.isScanAllDrives = false;
            ScanDetails.isScanDrive = false;
            ScanDetails.drivesList.Clear();
            ScanDetails.isScanSusDirectories = false;
            ScanDetails.isAddRules = false;
            ScanDetails.addRulesFileDir = null;
            ScanDetails.isScanMemory = false;
            ScanDetails.isCPULessThan50 = false;
            this.Close();
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            bool proceed = false;

            if (ScanDetails.scanner == "yara" && !GeneralFunctions.IsAdministrator() && File.Exists(ScanDetails.scannerDir + "\\" + "index.yar"))
            {
                MessageBox.Show("Scanner directory contains index.yar. Either remove it manually or run the application as administrator ");
                return;
            }
            if (ScanDetails.scanner == "yara" && GeneralFunctions.IsAdministrator() && File.Exists(ScanDetails.scannerDir + "\\" + "index.yar"))
            {
                File.Delete(ScanDetails.scannerDir + "\\" + "index.yar");
            }

            if (ScanDetails.isScanDrive)
            {
                if (ScanDetails.scanTarget.Equals("local"))
                {
                    if (chklstDrives.CheckedItems.Count != 0)
                    {
                        foreach (object itemChecked in chklstDrives.CheckedItems)
                        {
                            ScanDetails.drivesList.Add(itemChecked.ToString());
                        }
                        proceed = true;
                    }
                    else
                    {
                        MessageBox.Show("Please select at least 1 drive to scan");
                        return;
                    }
                }
                else
                {
                    if (Regex.IsMatch(txtbxDrivesLetters.Text, @"(^[a-zA-Z]$|^[a-zA-Z]((\s*,\s*)[a-zA-Z])+$)"))
                    {
                        ScanDetails.drivesList = txtbxDrivesLetters.Text.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                        String x = "";
                        proceed = true;
                    }
                    else
                    {
                        if (txtbxDrivesLetters.Text.Length == 0)
                        {
                            MessageBox.Show("Please write at least one drive letter");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Please follow the right pattern to write the letters: A,B,C");
                            return;
                        }
                    }
                }
            }
            else if (ScanDetails.isScanSusDirectories)
            {
                ScanDetails.susDirectories.Clear();
                ScanDetails.susDirectories2.Clear();
                ScanDetails.susDirectories2 = this.rchtxbxSusDirectories.Text.Split('\n').ToList();
                foreach (String directory in ScanDetails.susDirectories2)
                {
                    if (ScanDetails.scanTarget.Equals("local") && Directory.Exists(directory) && !ScanDetails.susDirectories.Contains(directory))
                    {
                        string directory2 = directory.TrimEnd(new[] { '/', '\\' });
                        ScanDetails.susDirectories.Add(directory2);
                    }
                    if (ScanDetails.scanTarget.Equals("remote") && !ScanDetails.susDirectories.Contains(directory))
                    {
                        string directory2 = directory.TrimEnd(new[] { '/', '\\' });
                        if (!string.IsNullOrEmpty(directory2))
                        {
                            ScanDetails.susDirectories.Add(directory2);
                        }
                    }
                }
                proceed = true;
            }
            else if (ScanDetails.isScanAllDrives)
            {
                proceed = true;
            }
            else
            {
                MessageBox.Show("Please choose one of the required fields");
                return;
            }
            if (ScanDetails.isAddRules)
            {
                if (!File.Exists(ScanDetails.addRulesFileDir))
                {
                    MessageBox.Show("Please select a valid file");
                    return;
                }
                else
                {
                    if (ScanDetails.scanner == "loki")
                    {
                        addRulesSuccess = GeneralFunctions.AddExternalRules();
                        if (!addRulesSuccess)
                            return;
                    }
                    else
                    {
                        addRulesSuccess = GeneralFunctions.AddExternalRules();
                        if (!addRulesSuccess)
                            return;

                        if (!GeneralFunctions.IsAdministrator() && File.Exists(ScanDetails.scannerDir + "\\" + "index.yar"))
                        {
                            MessageBox.Show("Scanner directory contains index.yar. Either remove it manually or run the application as administrator ");
                            return;
                        }
                        GeneralFunctions.copyYARARulesFilenames();
                    }
                    proceed = true;
                }
            }


            if (proceed)
            {
                if (ScanDetails.scanner == "yara" && !ScanDetails.isCPULessThan50 && !ScanDetails.isAddRules)
                {
                    var ext = new List<string> { "yar", "yara" };
                    var myFiles = Directory.EnumerateFiles(ScanDetails.scannerDir, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s).TrimStart('.').ToLowerInvariant()));
                    if (!myFiles.Any())
                    {
                        MessageBox.Show("Please add YARA rules files to scanner directory");
                        return;
                    }

                    ScanDetails.isFromScanOptions = true;
                    OpenNextForm(new Forms.FormInstalling());

                }
                else if (ScanDetails.scanTarget.Equals("local") && ScanDetails.isCPULessThan50)
                {
                    var file = "";
                    if (ScanDetails.scanner == "loki")
                    {
                        file = Directory.GetFiles(ScanDetails.scannerDir, "procgov.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    }
                    else if (ScanDetails.scanner == "yara")
                    {
                        file = Directory.GetFiles(ScanDetails.scannerDir, "procgov64.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    }
                    if (file == null)
                    {
                        ScanDetails.isFromScanOptions = true;
                        OpenNextForm(new Forms.FormInstalling());
                    }
                    else if (ScanDetails.isAddRules)
                    {
                        ScanDetails.isFromScanOptions = true;
                        OpenNextForm(new Forms.FormInstalling());
                    }
                    else if (ScanDetails.scanner == "yara")
                    {
                        ScanDetails.isFromScanOptions = true;
                        OpenNextForm(new Forms.FormInstalling());
                    }
                    else
                    {
                        OpenNextForm(new Forms.FormScanning());
                    }

                }
                else if (ScanDetails.scanTarget.Equals("local") && ScanDetails.isAddRules)
                {
                    ScanDetails.isFromScanOptions = true;
                    OpenNextForm(new Forms.FormInstalling());
                }
                else if (ScanDetails.scanTarget.Equals("local") && !ScanDetails.isAddRules && !ScanDetails.isCPULessThan50)
                {
                    ScanDetails.isFromScanOptions = true;
                    OpenNextForm(new Forms.FormScanning());
                }
                else
                {
                    ScanDetails.isFromScanOptions = true;
                    OpenNextForm(new Forms.FormInstalling());
                }
                clearOptions = true;
                ClearSelections();
            }
        }

        private void ClearSelections()
        {
            clearOptions = true;
            rbScanAllDrives.Checked = false;
            rbScanDrive.Checked = false;
            chklstDrives.ClearSelected();
            foreach (int i in chklstDrives.CheckedIndices)
            {
                chklstDrives.SetItemCheckState(i, CheckState.Unchecked);
            }

            chklstDrives.Enabled = false;
            txtbxDrivesLetters.Text = "";
            txtbxDrivesLetters.Enabled = false;
            rbScanSusDirectories.Checked = false;
            lblEdit.Visible = false;
            rchtxbxSusDirectories.Enabled = false;
            chkbxAddExternalRules.Checked = false;
            txtbxAddExternalRules.Text = "";
            txtbxAddExternalRules.Enabled = false;
            btnBrowseAddExternalRules.Enabled = false;
            chkbxScanMemory.Checked = false;
            chkbxUseLessCPU.Checked = false;
            clearOptions = false;
            addRulesSuccess = false;

        }
        private void btnBrowseScanDirectory_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.BackColor = btn.Enabled ? Color.FromArgb(126, 140, 224) : Color.FromArgb(58, 60, 66);
            btn.ForeColor = btn.Enabled ? Color.WhiteSmoke : Color.Gray;
        }
        private void btnBrowseAddRules_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.BackColor = btn.Enabled ? Color.FromArgb(126, 140, 224) : Color.FromArgb(58, 60, 66);
            btn.ForeColor = btn.Enabled ? Color.WhiteSmoke : Color.Gray;
        }
        private void btnBrowseAddExternalRules_Click(object sender, EventArgs e)
        {
            using (openFileDialog1)
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "zip files (*.zip)|*.zip";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtbxAddExternalRules.Text = openFileDialog1.FileName;
                    ScanDetails.addRulesFileDir = txtbxAddExternalRules.Text;
                }
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
        private void rbScanDrive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbScanDrive.Checked)
            {
                if (ScanDetails.scanTarget.Equals("local"))
                {
                    chklstDrives.Enabled = true;
                }
                else
                {
                    txtbxDrivesLetters.Enabled = true;
                }
                ScanDetails.isScanDrive = true;
            }
            else if (!clearOptions)
            {
                chklstDrives.Enabled = false;
                txtbxDrivesLetters.Enabled = false;
                ScanDetails.isScanDrive = false;
            }
        }
        private void chkbxAddExternalRules_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxAddExternalRules.Checked)
            {
                txtbxAddExternalRules.Enabled = true;
                btnBrowseAddExternalRules.Enabled = true;
                ScanDetails.isAddRules = true;
            }
            else if (!clearOptions)
            {
                txtbxAddExternalRules.Enabled = false;
                btnBrowseAddExternalRules.Enabled = false;
                ScanDetails.isAddRules = false;
            }
        }
        private void chkbxScanMemory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxScanMemory.Checked)
            {
                ScanDetails.isScanMemory = true;
            }
            else if (!clearOptions)
            {
                ScanDetails.isScanMemory = false;
            }
        }
        private void chkbxUseLessCPU_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxUseLessCPU.Checked)
            {
                ScanDetails.isCPULessThan50 = true;
            }
            else if (!clearOptions)
            {
                ScanDetails.isCPULessThan50 = false;
            }
        }
        private void rbScanAllDrives_CheckedChanged(object sender, EventArgs e)
        {
            if (rbScanAllDrives.Checked)
            {
                ScanDetails.isScanAllDrives = true;
            }
            else if (!clearOptions)
            {
                ScanDetails.isScanAllDrives = false;
            }
        }
        private void rbScanSusDirectories_CheckedChanged(object sender, EventArgs e)
        {
            if (rbScanSusDirectories.Checked)
            {
                ScanDetails.isScanSusDirectories = true;
                lblEdit.Visible = true;
            }
            else if (!clearOptions)
            {
                ScanDetails.isScanSusDirectories = false;
                lblEdit.Visible = false;
            }
        }
        private void lblEdit_Click(object sender, EventArgs e)
        {
            isEdit = !isEdit;
            if (isEdit)
            {
                rchtxbxSusDirectories.Enabled = true;
            }
            else
            {
                rchtxbxSusDirectories.Enabled = false;
            }
        }
    }
}
