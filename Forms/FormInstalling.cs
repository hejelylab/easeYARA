using Ionic.Zip;
using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        String lokiFileName;
        String yaraFileName;
        bool error = false;
        String procgovFileName;
        String lokiLatestVersion;
        bool isCalled = false;
        String yaraLatestVersion;
        String yaraZipFile;
        String procgovLatestVersion;

        public FormInstalling()
        {
            InitializeComponent();
            if (ScanDetails.isFromChooseDir)
            {
                if (ScanDetails.isScannerLocal)
                {
                    label3.Text = "Updating Loki";
                }
                bw = new BackgroundWorker();
                bw.DoWork += bw_DoWork;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                bw.RunWorkerAsync();
            }

            if (ScanDetails.isFromScanOptions)
            {
                var file = "";
                if (ScanDetails.scanner == "loki")
                {
                    file = Directory.GetFiles(ScanDetails.scannerDir, "procgov.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                }
                if (ScanDetails.scanner == "yara")
                {
                    file = Directory.GetFiles(ScanDetails.scannerDir, "procgov64.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                }
                if (ScanDetails.isCPULessThan50 && file == null)
                {
                    label3.Text = "Installing Process Governor";
                }
                else if (ScanDetails.isAddRules)
                {
                    label3.Text = "Adding External Rules";
                }

                else if (!ScanDetails.isAddRules && ScanDetails.scanner == "yara")
                {
                    label3.Text = "Creating index.yar in scanner directory";
                }
                else
                {
                    label3.Text = "Preparing Shares and Script";
                }

                bw2 = new BackgroundWorker();
                bw2.DoWork += bw_DoWork;
                bw2.RunWorkerCompleted += bw_RunWorkerCompleted;
                bw2.RunWorkerAsync();
            }
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sender.Equals(bw))
            {
                if (ScanDetails.isScannerLocal)
                {
                    LokiUpdate();
                }
                else if (ScanDetails.scanner == "loki")
                {

                    LokiDownload();
                    LokiUnzip();
                    LokiUpdate();
                }
                else
                {
                    YaraDownload();
                    YaraUnzip();
                }
            }

            else if (sender.Equals(bw2))
            {
                if (ScanDetails.isCPULessThan50)
                {
                    var file = "";
                    if (ScanDetails.scanner == "loki")
                    {
                        file = Directory.GetFiles(ScanDetails.scannerDir, "procgov.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    }
                    if (ScanDetails.scanner == "yara")
                    {
                        file = Directory.GetFiles(ScanDetails.scannerDir, "procgov64.exe", SearchOption.TopDirectoryOnly).FirstOrDefault();
                    }

                    if (file == null)
                    {
                        procgovDownload();
                    }
                }

                if (ScanDetails.scanner == "yara" && !ScanDetails.isAddRules)
                {
                    String f;
                    List<String> rulesNameList = new List<String>();
                    List<String> removedFiles = new List<String>();

                    var ext = new List<string> { "yar", "yara" };
                    if (File.Exists(ScanDetails.scannerDir + "\\" + "index.yar"))
                    {
                        File.Delete(ScanDetails.scannerDir + "\\" + "index.yar");
                    }
                    var myFiles = Directory.EnumerateFiles(ScanDetails.scannerDir, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s).TrimStart('.').ToLowerInvariant()));
                    List<string> list = new List<string>();
                    foreach (string filename in myFiles)
                    {
                        f = filename.Substring(filename.LastIndexOf('\\') + 1);

                        if (list.Count > 0 && rulesNameList.Contains(f))
                        {
                            removedFiles.Add(filename);
                        }
                        else
                        {
                            rulesNameList.Add(f);
                            list.Add("include \"" + ".\\" + Path.GetRelativePath(ScanDetails.scannerDir, filename) + "\"");
                        }

                    }
                    String[] listArray = list.ToArray();

                    File.WriteAllLines(ScanDetails.scannerDir + "\\index.yar", listArray);
                    if (removedFiles.Count > 0)
                    {
                        MessageBox.Show("The following file(s) won't be added to index.yar becuase there's already file with the same name: \n" + String.Join("\n", removedFiles));
                    }
                }

                if (ScanDetails.scanTarget == "remote")
                {
                    directorySetAccessControl(ScanDetails.scannerDir);
                    ScanDetails.yaraResultsDirectory = yaraResultsDirectoryCreation();
                    directorySetAccessControl(ScanDetails.yaraResultsDirectory);
                    if (ScanDetails.scanner == "loki" && (ScanDetails.isScanAllDrives || (ScanDetails.isScanDrive && ScanDetails.drivesList.Count == 1) || (ScanDetails.isScanSusDirectories && ScanDetails.susDirectories.Count == 1)))
                    {
                        if (ScanDetails.isScanAllDrives)
                        {
                            if (ScanDetails.isCPULessThan50)
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons --allhds --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons --noprocscan --allhds --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\loki.exe";
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
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons -p " + scannedDrive + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons --noprocscan -p " + scannedDrive + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\loki.exe";
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
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov.exe";
                                if (ScanDetails.isScanMemory)
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons -p " + "\"\"" + ScanDetails.susDirectories[0] + "\"\"" + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                                else
                                {
                                    ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons --noprocscan -p " + "\"\"" + ScanDetails.susDirectories[0] + "\"\"" + " --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"";
                                }
                            }
                            else
                            {
                                ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\loki.exe";
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

            while (lokiLatestVersion == null)
            {
                if (!isCalled)
                {
                    isCalled = true;
                    GetLokiLatestVersion();
                }
            }
            string lokiLatestVersionNum = lokiLatestVersion;
            while (char.IsLetter(lokiLatestVersionNum.ToCharArray()[0]))
            {
                lokiLatestVersionNum = lokiLatestVersionNum.Remove(0, 1);
            }


            string LOKIGITHUBURIString = "https://github.com/Neo23x0/Loki/releases/download/" + lokiLatestVersion + "/loki_" + lokiLatestVersion.Remove(0, 1) + ".zip";
            Uri LOKIGITHLOKIGITHUBURI = new Uri(LOKIGITHUBURIString);
            string remoteFilePathWithoutQueryLOKI = LOKIGITHLOKIGITHUBURI.GetLeftPart(UriPartial.Path);
            lokiFileName = System.IO.Path.GetFileName(remoteFilePathWithoutQueryLOKI);
            string LOKIlocalPath = ScanDetails.scannerDir + "\\" + lokiFileName;
            WebClient webClientLOKI = new WebClient();
            try
            {
                webClientLOKI.Proxy = WebRequest.GetSystemWebProxy();
            }
            catch (Exception e)
            {
                MessageBox.Show("System proxy wasn't identified");
                error = true;
            }
            try
            {
                webClientLOKI.DownloadFile(LOKIGITHUBURIString, LOKIlocalPath);

                webClientLOKI.Dispose();
            }
            catch (Exception internetAccessException)
            {
                MessageBox.Show("Please check if the system can access the internet to download LOKI scanner");
                error = true;
            }
        }
        private async void GetLokiLatestVersion()
        {
            try
            {
                GitHubClient client = new GitHubClient(new ProductHeaderValue("Neo23x0"));
                Release releases = await client.Repository.Release.GetLatest("Neo23x0", "Loki");
                lokiLatestVersion = releases.TagName;
            }
            catch (Exception e)
            {
                MessageBox.Show("LOKI latest release wasn't pulled");
                error = true;
            }
        }
        public void LokiUnzip()
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show("LOKI folder wasn't unzipped");
                error = true;
            }
        }
        public void LokiUpdate()
        {
            try
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
                if (System.Environment.OSVersion.Version.Major >= 6)
                {
                    process.StartInfo.Verb = "runas";
                }
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    Console.WriteLine(process.StandardOutput.ReadLine());
                }
                process.WaitForExit();

            }
            catch (Exception e)
            {
                MessageBox.Show("LOKI folder wasn't updated");
                error = true;
            }
        }
        public void YaraDownload()
        {
            while (yaraLatestVersion == null || yaraZipFile == null)
            {
                if (!isCalled)
                {
                    isCalled = true;
                    GetYaraLatestVersion();
                }
            }
            string YARAGITHUBURIString = "https://github.com/VirusTotal/yara/releases/download/" + yaraLatestVersion + "/" + yaraZipFile;
            Uri YARAGITHUBURI = new Uri(YARAGITHUBURIString);
            string remoteFilePathWithoutQuery = YARAGITHUBURI.GetLeftPart(UriPartial.Path);
            yaraFileName = Path.GetFileName(remoteFilePathWithoutQuery);
            string YARAlocalPath = ScanDetails.scannerDir + "\\" + yaraFileName;
            WebClient webClientYARA = new WebClient();
            try
            {
                webClientYARA.Proxy = WebRequest.GetSystemWebProxy();
            }
            catch (Exception e)
            {
                MessageBox.Show("System proxy wasn't identified");
                error = true;
            }
            try
            {
                webClientYARA.DownloadFile(YARAGITHUBURIString, YARAlocalPath);
                webClientYARA.Dispose();
            }
            catch (Exception internetAccessException)
            {
                MessageBox.Show("Please check if the system can access the internet to download YARA scanner");
                error = true;
            }
        }

        private async void GetYaraLatestVersion()
        {
            try
            {
                GitHubClient client = new GitHubClient(new ProductHeaderValue("VirusTotal"));
                Release releases = await client.Repository.Release.GetLatest("VirusTotal", "yara");
                IReadOnlyList<ReleaseAsset> assets = releases.Assets;

                foreach (ReleaseAsset a in assets)
                {
                    if (a.Name.EndsWith("win64.zip"))
                    {
                        yaraZipFile = a.Name;
                        yaraLatestVersion = releases.TagName;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("YARA latest release wasn't pulled");
                error = true;
            }
        }
        public void YaraUnzip()
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show("YARA folder wasn't unzipped");
                error = true;
            }
        }

        public void procgovDownload()
        {
            while (procgovLatestVersion == null)
            {
                if (!isCalled)
                {
                    isCalled = true;
                    GetProcgovLatestVersion();
                }
            }

            string procgovGITHUBURIString = "https://github.com/lowleveldesign/process-governor/releases/download/" + procgovLatestVersion + "/procgov.zip";
            Uri procgovGITHLOKIGITHUBURI = new Uri(procgovGITHUBURIString);
            string remoteFilePathWithoutQueryprocgov = procgovGITHLOKIGITHUBURI.GetLeftPart(UriPartial.Path);
            procgovFileName = System.IO.Path.GetFileName(remoteFilePathWithoutQueryprocgov);
            string procgovlocalPath = ScanDetails.scannerDir + "\\" + procgovFileName;
            WebClient webClientPROCGOV = new WebClient();
            try
            {
                webClientPROCGOV.Proxy = WebRequest.GetSystemWebProxy();
            }
            catch (Exception e)
            {
                MessageBox.Show("System proxy wasn't identified");
                error = true;
            }
            try
            {
                webClientPROCGOV.DownloadFile(procgovGITHUBURIString, procgovlocalPath);
                webClientPROCGOV.Dispose();
            }
            catch (Exception internetAccessException)
            {
                MessageBox.Show("Please check if the system can access the internet to download process-gvoernor");
                error = true;
            }
            procgovUnzip();
        }

        private async void GetProcgovLatestVersion()
        {
            try
            {
                GitHubClient client = new GitHubClient(new ProductHeaderValue("lowleveldesign"));
                Release releases = await client.Repository.Release.GetLatest("lowleveldesign", "process-governor");
                procgovLatestVersion = releases.TagName;
            }
            catch (Exception e)
            {
                MessageBox.Show("LOKI latest release wasn't pulled");
                error = true;
            }
        }

        public async void procgovUnzip()
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string procgovlocalPath = ScanDetails.scannerDir + "\\" + procgovFileName;
                using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(procgovlocalPath))
                {
                    foreach (Ionic.Zip.ZipEntry entry in zip1)
                    {
                        entry.Extract(ScanDetails.scannerDir, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("process-governor folder wasn't unzipped");
                error = true;
            }
        }
        private void directorySetAccessControl(string folderName)
        {
            DirectoryInfo dInfo = new DirectoryInfo(folderName);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.SetAccessRuleProtection(true, false);
            var administratorsGroupAccount = new NTAccount("Administrators");
            dSecurity.AddAccessRule(new FileSystemAccessRule(administratorsGroupAccount, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));

            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
            var currentUserAccount = new NTAccount(currentUser.Name);
            dSecurity.AddAccessRule(new FileSystemAccessRule(currentUserAccount, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));

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

            if (ScanDetails.isFromChooseDir)
            {
                ScanDetails.isFromChooseDir = false;
                OpenNextForm(new Forms.FormScanOptions());
            }

            if (ScanDetails.isFromScanOptions)
            {
                ScanDetails.isFromScanOptions = false;
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

