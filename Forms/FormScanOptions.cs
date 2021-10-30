using Ionic.Zip;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormScanOptions : Form
    {
        bool isEdit = false;
        string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        //String procgovFileName;
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

            //if (ScanDetails.isCPULessThan50)
            //{
            //    var file = Directory.GetFiles(ScanDetails.scannerDir, "procgov64.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
            //    if (file == null)
            //    {
            //        procgovDownload();
                    
            //    }
            //}


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
                    if (Directory.Exists(directory) && !ScanDetails.susDirectories.Contains(directory))
                    {
                        string directory2 = directory.TrimEnd(new[] { '/', '\\' });
                        ScanDetails.susDirectories.Add(directory2);
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
                    proceed = true;
                }
            }


            if (proceed)
            {
                if (ScanDetails.scanTarget.Equals("local") && !ScanDetails.isCPULessThan50 && !ScanDetails.isAddRules)
                {
                    OpenNextForm(new Forms.FormScanning());
                }
                else if (ScanDetails.scanTarget.Equals("local") && ScanDetails.isCPULessThan50)
                {
                    var file = Directory.GetFiles(ScanDetails.scannerDir, "procgov64.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    if (file == null)
                    {
                        ScanDetails.isFromScanOptions = true;
                        OpenNextForm(new Forms.FormInstalling());
                    }
                    else if(ScanDetails.isAddRules)
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
                else
                { // Move this to FormInstalling 
                    ScanDetails.isFromScanOptions = true;
                    OpenNextForm(new Forms.FormInstalling());

                }
            }
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
            else
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
            else
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
            else
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
            else
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
            else
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
            else
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


        //public void procgovDownload()
        //{
        //    string procgovGITHUBURIString = "https://github.com/lowleveldesign/process-governor/releases/download/2.9-1/procgov.zip";
        //    Uri procgovGITHLOKIGITHUBURI = new Uri(procgovGITHUBURIString);
        //    string remoteFilePathWithoutQueryprocgov = procgovGITHLOKIGITHUBURI.GetLeftPart(UriPartial.Path);
        //    procgovFileName = System.IO.Path.GetFileName(remoteFilePathWithoutQueryprocgov);
        //    string procgovlocalPath = ScanDetails.scannerDir + "\\" + procgovFileName;  
        //    WebClient webClientLOKI = new WebClient();
        //    webClientLOKI.Proxy = WebRequest.GetSystemWebProxy();
        //    webClientLOKI.DownloadFile(procgovGITHUBURIString, procgovlocalPath);
        //    webClientLOKI.Dispose();
        //    procgovUnzip();
        //}
        //public async void procgovUnzip()
        //{
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //    string procgovlocalPath = ScanDetails.scannerDir + "\\" + procgovFileName;
        //    using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(procgovlocalPath))
        //    {
        //        foreach (Ionic.Zip.ZipEntry entry in zip1)
        //        {
        //            // if this extract entries directly in the same folder with no creation of loki folder. Create it.
        //            // if it generates an error, use txtbxDirectory.Text instead of scannerDirectory
        //            entry.Extract(ScanDetails.scannerDir, ExtractExistingFileAction.OverwriteSilently);
        //        }
        //    }
        //}
        //private void directorySetAccessControl(string folderName)
        //{
        //    DirectoryInfo dInfo = new DirectoryInfo(folderName);
        //    DirectorySecurity dSecurity = dInfo.GetAccessControl();
        //    dSecurity.SetAccessRuleProtection(true, false);
        //    var administratorsGroupAccount = new NTAccount("Administrators");
        //    dSecurity.AddAccessRule(new FileSystemAccessRule(administratorsGroupAccount, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
        //    dInfo.SetAccessControl(dSecurity);
        //    var AuthenticatedUsersAccounts = new NTAccount("Authenticated Users");
        //    dSecurity.RemoveAccessRuleAll(new FileSystemAccessRule(AuthenticatedUsersAccounts, FileSystemRights.FullControl, AccessControlType.Allow));
        //    if (folderName == ScanDetails.scannerDir)
        //    {
        //        dSecurity.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsersAccounts, FileSystemRights.ReadAndExecute, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
        //        dInfo.SetAccessControl(dSecurity);
        //        shareFolder(ScanDetails.scannerDir, dInfo.Name, "This is YARA Scanner Share");
        //        ScanDetails.scannerDirUNCPath = "\\\\" + ScanDetails.computerName + "\\" + dInfo.Name;
        //    }
        //    if ((dInfo.FullName).ToString().Contains("YARAScanResults"))
        //    {
        //        dSecurity.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsersAccounts, FileSystemRights.Write | FileSystemRights.ReadAttributes, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
        //        dInfo.SetAccessControl(dSecurity);
        //        shareFolder((dInfo.FullName).ToString(), dInfo.Name, "This is YARA Scan Results Share");
        //        ScanDetails.yaraResultsDirectoryUNCPath = "\\\\" + ScanDetails.computerName + "\\" + dInfo.Name;
        //    }
        //}
        //private void shareFolder(string FolderPath, string ShareName, string Description)
        //{
        //    try
        //    {
        //        ManagementClass managementClass = new ManagementClass("Win32_Share");
        //        ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
        //        ManagementBaseObject outParams;
        //        inParams["Description"] = Description;
        //        inParams["Name"] = ShareName;
        //        inParams["Path"] = FolderPath;
        //        inParams["Type"] = 0x0; // Disk Drive
        //        NTAccount authenticatedUsersAccount = new NTAccount(null, "AUTHENTICATED USERS");
        //        SecurityIdentifier authenticatedUserssid = (SecurityIdentifier)authenticatedUsersAccount.Translate(typeof(SecurityIdentifier));
        //        byte[] authenticatedUserssidArray = new byte[authenticatedUserssid.BinaryLength];
        //        authenticatedUserssid.GetBinaryForm(authenticatedUserssidArray, 0);
        //        ManagementObject authenticateUsers = new ManagementClass("Win32_Trustee");
        //        authenticateUsers["Domain"] = null;
        //        authenticateUsers["Name"] = "AUTHENTICATED USERS";
        //        authenticateUsers["SID"] = authenticatedUserssidArray;
        //        ManagementObject daclAuthenticatedUsers = new ManagementClass("Win32_Ace");
        //        daclAuthenticatedUsers["AccessMask"] = 1245631;
        //        //daclAuthenticatedUsers["AccessMask"] = 1179817;
        //        daclAuthenticatedUsers["AceFlags"] = 3;
        //        daclAuthenticatedUsers["AceType"] = 0;
        //        daclAuthenticatedUsers["Trustee"] = authenticateUsers;
        //        ManagementObject securityDescriptor = new ManagementClass("Win32_SecurityDescriptor");
        //        securityDescriptor["ControlFlags"] = 4; //SE_DACL_PRESENT 
        //        securityDescriptor["DACL"] = new object[] { daclAuthenticatedUsers };
        //        inParams["Access"] = securityDescriptor;
        //        outParams = managementClass.InvokeMethod("Create", inParams, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "error, couldn't shrare folders as intended");
        //    }
        //}
        //private string yaraResultsDirectoryCreation()
        //{
        //    MessageBox.Show("hello1");
        //    string path = ScanDetails.scannerDir;
        //    DirectoryInfo parentDir = Directory.GetParent(path);
        //    string yaraResultsDirectory = parentDir + "\\YARAScanResults";
        //    MessageBox.Show(path);
        //    MessageBox.Show(parentDir.ToString());
        //    MessageBox.Show(yaraResultsDirectory);
        //    try
        //    {
        //        if (Directory.Exists(yaraResultsDirectory))
        //        {
        //            MessageBox.Show("hello2");
        //            Console.WriteLine("That path exists already.");
        //        }
        //        DirectoryInfo di = Directory.CreateDirectory(yaraResultsDirectory);
        //        MessageBox.Show("hello3");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("The process failed: {0}", e.ToString());
        //    }
        //    return yaraResultsDirectory;
        //}
    }
}
