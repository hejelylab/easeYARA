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
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormInstalling : Form
    {
        BackgroundWorker bw;
        BackgroundWorker bw2;
        BackgroundWorker bw3;
        String lokiFileName;
        String yaraFileName;
        bool error = false;
        String procgovFileName;

        public FormInstalling()
        {
            InitializeComponent();
            if (ScanDetails.isFromChooseDir)
            {
                bw = new BackgroundWorker();
                bw.DoWork += bw_DoWork;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                bw.RunWorkerAsync();
                //ScanDetails.isFromChooseDir = false;
            }

            if ( ScanDetails.isFromScanOptions)
            {
                var file = Directory.GetFiles(ScanDetails.scannerDir, "procgov64.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (ScanDetails.isCPULessThan50 && file == null)
                {
                    label3.Text = "Installing Process Governor";
                }
                else if (ScanDetails.isAddRules)
                {
                    label3.Text = "Adding External Rules";
                }
                else
                {
                    label3.Text = "Preparing Shares and Script";
                }
                bw2 = new BackgroundWorker();
                bw2.DoWork += bw_DoWork;
                bw2.RunWorkerCompleted += bw_RunWorkerCompleted;
                bw2.RunWorkerAsync();
                //ScanDetails.isFromScanOptions = false;
            }               
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sender.Equals(bw))
            {
                if (ScanDetails.scanner == "loki")
                {
                    try
                    {
                        LokiDownload();
                        LokiUnzip();
                        LokiUpdate();
                    }
                    catch (Exception internetAccessException)
                    {
                        MessageBox.Show("Please check if the system can access the internet to download LOKI scanner");
                        error = true;
                    }
                }
                else
                {
                    try
                    {
                        YaraDownload();
                        YaraUnzip();
                    }
                    catch (Exception internetAccessException)
                    {
                        MessageBox.Show("Please check if the system can access the internet to download YARA scanner");
                        error = true;
                    }
                }
            }

            else if (sender.Equals(bw2))
            {
                if (ScanDetails.isCPULessThan50)
                {
                    var file = Directory.GetFiles(ScanDetails.scannerDir, "procgov64.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    if (file == null)
                    {
                        procgovDownload();
                    }
                }
                if (ScanDetails.isAddRules)
                {
                    if (ScanDetails.scanner == "loki")
                    {
                        GeneralFunctions.AddExternalRules();
                    }
                    else
                    {
                        GeneralFunctions.AddExternalRules();
                        if (!GeneralFunctions.IsAdministrator() && File.Exists(ScanDetails.scannerDir + "\\" + "index.yar"))
                        {
                            MessageBox.Show("Scanner directory contains index.yar. Either remove it manually or run the application as administrator ");
                            return;
                        }
                        GeneralFunctions.copyYARARulesFilenames();
                    }
                }

                if (ScanDetails.scanTarget == "remote")
                {
                    directorySetAccessControl(ScanDetails.scannerDir);
                    ScanDetails.yaraResultsDirectory = yaraResultsDirectoryCreation();
                    //MessageBox.Show(ScanDetails.yaraResultsDirectory);
                    directorySetAccessControl(ScanDetails.yaraResultsDirectory);
                    if (ScanDetails.scanner == "loki" && (ScanDetails.isScanAllDrives || (ScanDetails.isScanDrive && ScanDetails.drivesList.Count == 1) || (ScanDetails.isScanSusDirectories && ScanDetails.susDirectories.Count == 1)))
                    {
                        ScanDetails.scannerDirUNCPath = ScanDetails.scannerDirUNCPath.Replace(@"\", "/");
                        ScanDetails.yaraResultsDirectoryUNCPath = ScanDetails.yaraResultsDirectoryUNCPath.Replace(@"\", "/");
                        if (ScanDetails.isScanAllDrives)
                        {
                            if (ScanDetails.isCPULessThan50)
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/procgov.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/loki.exe --allreasons --allhds --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/loki.exe --allreasons --noprocscan --allhds --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/loki.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " --allreasons --csv " + "--allhds --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath;
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " --allreasons --noprocscan --csv " + "--allhds --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath;
                                }
                            }
                        }
                        else if (ScanDetails.isScanDrive && ScanDetails.drivesList.Count == 1)
                        {
                            string scannedDrive = ScanDetails.drivesList[0] + ":\\";
                            if (ScanDetails.isCPULessThan50)
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/procgov.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/loki.exe --allreasons -p " + scannedDrive + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/loki.exe --allreasons --noprocscan -p " + scannedDrive + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/loki.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " --allreasons -p " + scannedDrive + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath;
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " --allreasons --noprocscan -p " + scannedDrive + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath;
                                }
                            }
                        }
                        else
                        {
                            if (ScanDetails.isCPULessThan50)
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/procgov.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/loki.exe --allreasons -p " + "\"\"" + ScanDetails.susDirectories[0] + "\"\"" + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/loki.exe --allreasons --noprocscan -p " + "\"\"" + ScanDetails.susDirectories[0] + "\"\"" + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/loki.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " --allreasons -p " + "\"" + ScanDetails.susDirectories[0] + "\"" + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath;
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " --allreasons --noprocscan -p " + "\"" + ScanDetails.susDirectories[0] + "\"" + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath;
                                }
                            }
                        }
                    }
                    else if (ScanDetails.scanner == "yara" && ((ScanDetails.isScanDrive && ScanDetails.drivesList.Count == 1) || (ScanDetails.isScanSusDirectories && ScanDetails.susDirectories.Count == 1)))
                    {
                        ScanDetails.scannerDirUNCPath = ScanDetails.scannerDirUNCPath.Replace(@"\", "/");
                        ScanDetails.yaraResultsDirectoryUNCPath = ScanDetails.yaraResultsDirectoryUNCPath.Replace(@"\", "/");
                        if (ScanDetails.isScanDrive && ScanDetails.drivesList.Count == 1)
                        {
                            string scannedDrive = ScanDetails.drivesList[0] + ":\\";
                            if (ScanDetails.isCPULessThan50)
                            {

                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/procgov64.exe";
                                ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/yara64.exe " + ScanDetails.scannerDirUNCPath + "/" + "index.yar" + " -r " + "\"" + scannedDrive + "\\\"\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "/%COMPUTERNAME%_" + ScanDetails.drivesList[0] + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/yara64.exe";
                                ScanDetails.generatedCommandArguments = ScanDetails.scannerDirUNCPath + "/" + "index.yar" + " -r " + "\"" + scannedDrive + "\\\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "/%COMPUTERNAME%_" + ScanDetails.drivesList[0] + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            }
                        }
                        else
                        {
                            if (ScanDetails.isCPULessThan50)
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/procgov64.exe";
                                ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "/yara64.exe " + ScanDetails.scannerDirUNCPath + "/" + "index.yar" + " -r " + "\"\"" + ScanDetails.susDirectories[0] + "\"\"\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "/%COMPUTERNAME%" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "/yara64.exe";
                                ScanDetails.generatedCommandArguments = ScanDetails.scannerDirUNCPath + "/" + "index.yar" + " -r " + "\"" + ScanDetails.susDirectories[0] + "\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "/%COMPUTERNAME%" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            }
                        }
                    }
                    else
                    {
                        ScanDetails.generatedCommand = "Please click on Generate Batch Script.";
                        ScanDetails.generatedCommandArguments = "This will generate the desired script to the same directory which contains your Scanner (yara64.exe/loki.exe)";
                    }

                }

            }
               
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (error)
            {
                this.Close();
            }
            prgrsbarInstall.Style = ProgressBarStyle.Continuous;
            prgrsbarInstall.Value = 100;
            lblPleaseWait.Text = "Installation Completed";
            btnNext.Visible = true;
        }
        public void LokiDownload()
        {
            string LOKIGITHUBURIString = "https://github.com/Neo23x0/Loki/releases/download/v0.44.2/loki_0.44.2.zip";
            Uri LOKIGITHLOKIGITHUBURI = new Uri(LOKIGITHUBURIString);
            string remoteFilePathWithoutQueryLOKI = LOKIGITHLOKIGITHUBURI.GetLeftPart(UriPartial.Path);
            lokiFileName = System.IO.Path.GetFileName(remoteFilePathWithoutQueryLOKI);
            string LOKIlocalPath = ScanDetails.scannerDir + "\\" + lokiFileName;
            WebClient webClientLOKI = new WebClient();
            webClientLOKI.Proxy = WebRequest.GetSystemWebProxy();
            webClientLOKI.DownloadFile(LOKIGITHUBURIString, LOKIlocalPath);
            
            webClientLOKI.Dispose();
        }
        public void LokiUnzip()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string LOKIlocalPath = ScanDetails.scannerDir + "\\" + lokiFileName;
            using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(LOKIlocalPath))
            {
                foreach (Ionic.Zip.ZipEntry entry in zip1)
                {
                    entry.Extract(ScanDetails.scannerDir, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            ScanDetails.scannerDir = ScanDetails.scannerDir + "\\" + "loki";
        }
        public void LokiUpdate()
        {
            Directory.SetCurrentDirectory(ScanDetails.scannerDir);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = ScanDetails.scannerDir + "\\loki.exe"; 
            startInfo.Arguments = " --update";
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                Console.WriteLine(process.StandardOutput.ReadLine());
            }
            process.WaitForExit();
        }
        public void YaraDownload()
        {
            string YARAGITHUBURIString = "https://github.com/VirusTotal/yara/releases/download/v4.1.1/yara-v4.1.1-1635-win64.zip";
            Uri YARAGITHUBURI = new Uri(YARAGITHUBURIString);
            string remoteFilePathWithoutQuery = YARAGITHUBURI.GetLeftPart(UriPartial.Path);
            yaraFileName = Path.GetFileName(remoteFilePathWithoutQuery);
            string YARAlocalPath = ScanDetails.scannerDir + "\\" + yaraFileName;
            WebClient webClientYARA = new WebClient();
            webClientYARA.Proxy = WebRequest.GetSystemWebProxy();
            webClientYARA.DownloadFile(YARAGITHUBURIString, YARAlocalPath);
            webClientYARA.Dispose();
        }
        public void YaraUnzip()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string YARAlocalPath = ScanDetails.scannerDir + "\\" + yaraFileName;
            using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(YARAlocalPath))
            {
                foreach (Ionic.Zip.ZipEntry entry in zip1)
                {
                    entry.Extract(ScanDetails.scannerDir, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        public void procgovDownload()
        {
            string procgovGITHUBURIString = "https://github.com/lowleveldesign/process-governor/releases/download/2.9-1/procgov.zip";
            Uri procgovGITHLOKIGITHUBURI = new Uri(procgovGITHUBURIString);
            string remoteFilePathWithoutQueryprocgov = procgovGITHLOKIGITHUBURI.GetLeftPart(UriPartial.Path);
            procgovFileName = System.IO.Path.GetFileName(remoteFilePathWithoutQueryprocgov);
            string procgovlocalPath = ScanDetails.scannerDir + "\\" + procgovFileName;
            WebClient webClientLOKI = new WebClient();
            webClientLOKI.Proxy = WebRequest.GetSystemWebProxy();
            webClientLOKI.DownloadFile(procgovGITHUBURIString, procgovlocalPath);
            webClientLOKI.Dispose();
            procgovUnzip();
        }
        public async void procgovUnzip()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string procgovlocalPath = ScanDetails.scannerDir + "\\" + procgovFileName;
            using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(procgovlocalPath))
            {
                foreach (Ionic.Zip.ZipEntry entry in zip1)
                {
                    // if this extract entries directly in the same folder with no creation of loki folder. Create it.
                    // if it generates an error, use txtbxDirectory.Text instead of scannerDirectory
                    entry.Extract(ScanDetails.scannerDir, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
        private void directorySetAccessControl(string folderName)
        {
            DirectoryInfo dInfo = new DirectoryInfo(folderName);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.SetAccessRuleProtection(true, false);
            var administratorsGroupAccount = new NTAccount("Administrators");
            dSecurity.AddAccessRule(new FileSystemAccessRule(administratorsGroupAccount, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
            var AuthenticatedUsersAccounts = new NTAccount("Authenticated Users");
            dSecurity.RemoveAccessRuleAll(new FileSystemAccessRule(AuthenticatedUsersAccounts, FileSystemRights.FullControl, AccessControlType.Allow));
            if (folderName == ScanDetails.scannerDir)
            {
                dSecurity.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsersAccounts, FileSystemRights.ReadAndExecute, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
                shareFolder(ScanDetails.scannerDir, dInfo.Name, "This is YARA Scanner Share");
                ScanDetails.scannerDirUNCPath = "\\\\" + ScanDetails.computerName + "\\" + dInfo.Name;
            }
            if ((dInfo.FullName).ToString().Contains("YARAScanResults"))
            {
                dSecurity.AddAccessRule(new FileSystemAccessRule(AuthenticatedUsersAccounts, FileSystemRights.Write | FileSystemRights.ReadAttributes, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
                shareFolder((dInfo.FullName).ToString(), dInfo.Name, "This is YARA Scan Results Share");
                ScanDetails.yaraResultsDirectoryUNCPath = "\\\\" + ScanDetails.computerName + "\\" + dInfo.Name;
            }
        }
        private void shareFolder(string FolderPath, string ShareName, string Description)
        {
            try
            {
                ManagementClass managementClass = new ManagementClass("Win32_Share");
                ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
                ManagementBaseObject outParams;
                inParams["Description"] = Description;
                inParams["Name"] = ShareName;
                inParams["Path"] = FolderPath;
                inParams["Type"] = 0x0; // Disk Drive
                NTAccount authenticatedUsersAccount = new NTAccount(null, "AUTHENTICATED USERS");
                SecurityIdentifier authenticatedUserssid = (SecurityIdentifier)authenticatedUsersAccount.Translate(typeof(SecurityIdentifier));
                byte[] authenticatedUserssidArray = new byte[authenticatedUserssid.BinaryLength];
                authenticatedUserssid.GetBinaryForm(authenticatedUserssidArray, 0);
                ManagementObject authenticateUsers = new ManagementClass("Win32_Trustee");
                authenticateUsers["Domain"] = null;
                authenticateUsers["Name"] = "AUTHENTICATED USERS";
                authenticateUsers["SID"] = authenticatedUserssidArray;
                ManagementObject daclAuthenticatedUsers = new ManagementClass("Win32_Ace");
                daclAuthenticatedUsers["AccessMask"] = 1245631;
                //daclAuthenticatedUsers["AccessMask"] = 1179817;
                daclAuthenticatedUsers["AceFlags"] = 3;
                daclAuthenticatedUsers["AceType"] = 0;
                daclAuthenticatedUsers["Trustee"] = authenticateUsers;
                ManagementObject securityDescriptor = new ManagementClass("Win32_SecurityDescriptor");
                securityDescriptor["ControlFlags"] = 4; //SE_DACL_PRESENT 
                securityDescriptor["DACL"] = new object[] { daclAuthenticatedUsers };
                inParams["Access"] = securityDescriptor;
                outParams = managementClass.InvokeMethod("Create", inParams, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error, couldn't shrare folders as intended");
            }
        }
        private string yaraResultsDirectoryCreation()
        {
            string path = ScanDetails.scannerDir;
            DirectoryInfo parentDir = Directory.GetParent(path);
            string yaraResultsDirectory = parentDir + "\\YARAScanResults";
            try
            {
                if (Directory.Exists(yaraResultsDirectory))
                {
                    Console.WriteLine("That path exists already.");
                }
                DirectoryInfo di = Directory.CreateDirectory(yaraResultsDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return yaraResultsDirectory;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {

            if(ScanDetails.isFromChooseDir)
            {
                OpenNextForm(new Forms.FormScanOptions());
            }

            if (ScanDetails.isFromScanOptions)
            {
                if (ScanDetails.scanTarget.Equals("local"))
                {
                    OpenNextForm(new Forms.FormScanning());
                }
                else
                {
                    OpenNextForm(new Forms.FormGenerateScript());
                }
            }
            
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
