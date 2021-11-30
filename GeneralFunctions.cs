using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
namespace easeYARA
{
    class GeneralFunctions
    {
        static bool cancel = false;
        public static bool AddExternalRules()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (Ionic.Zip.ZipFile archive = new Ionic.Zip.ZipFile(ScanDetails.addRulesFileDir))
            {
                bool passwordProtectedZIP = IsPasswordProtectedZipFile(ScanDetails.addRulesFileDir);
                if (passwordProtectedZIP)
                {
                    string[] promptValue;
                    promptValue = ShowDialog(true);
                    var textBoxValue = promptValue[0];
                    bool isPasswordCorrect = false;

                    while (!isPasswordCorrect)
                    {
                        if (cancel)
                        {
                            cancel = false;
                            return false;
                        }
                        if (Ionic.Zip.ZipFile.CheckZipPassword(archive.Name, textBoxValue))
                        {
                            isPasswordCorrect = true;
                        }
                        else
                        {
                            promptValue = ShowDialog(false);
                            textBoxValue = promptValue[0];
                        }
                    }
                    Console.WriteLine(textBoxValue);
                    archive.Password = textBoxValue.ToString();
                    archive.Encryption = Ionic.Zip.EncryptionAlgorithm.WinZipAes256;
                    archive.StatusMessageTextWriter = Console.Out;
                }
                string yaraRulesPath = "";
                if (ScanDetails.scanner == "loki")
                {
                    yaraRulesPath = ScanDetails.scannerDir + "\\signature-base\\yara\\";

                    if (!Directory.Exists(ScanDetails.scannerDir + "\\signature-base"))
                    {
                        Directory.SetCurrentDirectory(ScanDetails.scannerDir);
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = true;
                        startInfo.RedirectStandardOutput = true;
                        startInfo.FileName = ScanDetails.scannerDir + "\\loki-upgrader.exe";
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        process.StartInfo = startInfo;
                        process.Start();
                        while (!process.StandardOutput.EndOfStream)
                        {
                            Console.WriteLine(process.StandardOutput.ReadLine());
                        }
                        process.WaitForExit();
                    }

                    if (passwordProtectedZIP)
                    {
                        try
                        {
                            archive.ExtractAll(yaraRulesPath, Ionic.Zip.ExtractExistingFileAction.Throw);
                        }
                        catch (Ionic.Zip.ZipException e)
                        {
                            if (e is Ionic.Zip.BadPasswordException)
                            {
                                MessageBox.Show("Incorrect provided password, Copying File will be skipped");
                                return false;
                            }
                            else
                            {
                                MessageBox.Show("File with the same name already exists, Copying File will be skipped");
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            System.IO.Compression.ZipFile.ExtractToDirectory(ScanDetails.addRulesFileDir, yaraRulesPath);
                        }
                        catch (System.IO.IOException e)
                        {
                            MessageBox.Show("File with the same name already exists, Copying File will be skipped");
                        }
                    }
                    copyFilesANDRemoveSourceDirectory(yaraRulesPath, Path.GetFileNameWithoutExtension(ScanDetails.addRulesFileDir));
                }
                if (ScanDetails.scanner == "yara")
                {
                    yaraRulesPath = ScanDetails.scannerDir + "\\";
                    if (passwordProtectedZIP)
                    {
                        try
                        {
                            archive.ExtractAll(ScanDetails.scannerDir, Ionic.Zip.ExtractExistingFileAction.Throw);
                        }
                        catch (Ionic.Zip.ZipException e)
                        {
                            if (e is Ionic.Zip.BadPasswordException)
                            {
                                MessageBox.Show("Incorrect provided password, Copying File will be skipped");
                            }
                            else
                            {
                                MessageBox.Show("File with the same name already exists, Copying File will be skipped");
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            System.IO.Compression.ZipFile.ExtractToDirectory(ScanDetails.addRulesFileDir, yaraRulesPath);
                        }
                        catch (System.IO.IOException e)
                        {
                            MessageBox.Show("File with the same name already exists, Copying File will be skipped");
                        }
                    }
                    copyFilesANDRemoveSourceDirectory(yaraRulesPath, Path.GetFileNameWithoutExtension(ScanDetails.addRulesFileDir));

                }
            }
            return true;
        }
        public static bool IsPasswordProtectedZipFile(string path)
        {
            int encryptedEntries = 0;
            using (var zip = Ionic.Zip.ZipFile.Read(path))
            {
                foreach (var e in zip)
                {
                    if (e.UsesEncryption)
                    {
                        Console.WriteLine("Entry {0} uses encryption", e.FileName);
                        encryptedEntries++;
                    }
                }
            }
            return (encryptedEntries > 0);
        }
        public static void copyFilesANDRemoveSourceDirectory(string yaraRulesPath, string passwordProtectedZIPFileName)
        {
            string sourceDir = yaraRulesPath + passwordProtectedZIPFileName;
            string targetDir = yaraRulesPath;
            try
            {
                string[] yaraFiles = Directory.GetFiles(sourceDir, "*.yar");
                foreach (string f in yaraFiles)
                {
                    string fName = f.Substring(sourceDir.Length + 1);
                    try
                    {
                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(targetDir, fName), true);
                    }
                    catch (IOException copyError)
                    {
                        Console.WriteLine(copyError.Message);
                    }
                }
                foreach (string f in yaraFiles)
                {
                    File.Delete(f);
                    Directory.Delete(sourceDir, true);
                }
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }
        public static void copyYARARulesFilenames()
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

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static string[] ShowDialog(bool isFirstTime)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen
            };
            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 40, Top = 20, Width = 200, Text = "Enter Zip File Password" };
            TextBox textBox2 = new TextBox() { Left = 40, Top = 50, Width = 200 };
            System.Windows.Forms.Label lblIncorrect = new System.Windows.Forms.Label() { Left = 40, Top = 110, Text = "Incorrect Password" };
            lblIncorrect.ForeColor = System.Drawing.Color.Tomato;
            if (isFirstTime)
            {
                lblIncorrect.Visible = false;
            }
            else
            {
                lblIncorrect.Visible = true;
            }
            Button confirmation = new Button() { Text = "Ok", Left = 40, Width = 90, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            Button btnCancel = new Button() { Text = "Cancel", Left = 150, Width = 90, Top = 80, DialogResult = DialogResult.OK };
            btnCancel.Click += (sender, e) => { cancel = true; prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox2);
            prompt.Controls.Add(lblIncorrect);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(btnCancel);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? new string[] { textBox2.Text } : null;
        }
    }
}
