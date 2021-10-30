using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using easeYARA.Forms;
namespace easeYARA
{
    class GeneralFunctions
    {
        public static void AddExternalRules()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (Ionic.Zip.ZipFile archive = new Ionic.Zip.ZipFile(ScanDetails.addRulesFileDir))
            {
                bool passwordProtectedZIP = IsPasswordProtectedZipFile(ScanDetails.addRulesFileDir);
                if (passwordProtectedZIP)
                {
                    string[] promptValue;
                    if (ScanDetails.isScannerLocal)
                    {
                        promptValue = FormScanning.ShowDialog();
                    }
                    else
                    {
                        promptValue = FormGenerateScript.ShowDialog();
                    }
                    var textBoxValue = promptValue[0];
                    Console.WriteLine(textBoxValue);
                    archive.Password = textBoxValue.ToString();
                    archive.Encryption = Ionic.Zip.EncryptionAlgorithm.WinZipAes256; // the default: you might need to select the proper value here
                    archive.StatusMessageTextWriter = Console.Out;
                }
                string yaraRulesPath = "";
                if (ScanDetails.scanner == "loki")
                {
                    yaraRulesPath = ScanDetails.scannerDir + "\\signature-base\\yara\\";
                    if (passwordProtectedZIP)
                    {
                        try
                        {
                            archive.ExtractAll(yaraRulesPath, Ionic.Zip.ExtractExistingFileAction.Throw);
                        }
                        catch (Ionic.Zip.ZipException e)
                        {
                            MessageBox.Show("File with the same name already exists, Copying File will be skipped");
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
                            MessageBox.Show("File with the same name already exists, Copying File will be skipped");
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
                    Directory.Delete(sourceDir);
                }
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }
        public static void copyYARARulesFilenames()
        {
            var ext = new List<string> { "yar", "yara" };
            if (File.Exists(ScanDetails.scannerDir + "\\" + "index.yar"))
            {
                File.Delete(ScanDetails.scannerDir + "\\" + "index.yar");
            }
            var myFiles = Directory.EnumerateFiles(ScanDetails.scannerDir, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s).TrimStart('.').ToLowerInvariant()));
            List<string> list = new List<string>();
            foreach (string filename in myFiles)
            {
                list.Add("include \"" + filename + "\"");
            }
            String[] listArray = list.ToArray();
            File.WriteAllLines(ScanDetails.scannerDir + "\\index.yar", listArray);
        }

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
// Scanner directory contains index.yar. Either remove it manually or run the application as admin