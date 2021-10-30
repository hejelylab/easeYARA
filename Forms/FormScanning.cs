using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormScanning : Form
    {
        BackgroundWorker bw;
        public FormScanning()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
            //btnNext.Visible = true;
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ScanDetails.scanner == "loki")
            {
                if (ScanDetails.isAddRules)
                {
                    GeneralFunctions.AddExternalRules();
                }
                runLokiScan();
            }
            else
            {
                if (ScanDetails.isAddRules)
                {
                    GeneralFunctions.AddExternalRules();
                }
                if (!GeneralFunctions.IsAdministrator() && File.Exists(ScanDetails.scannerDir + "\\" + "index.yar"))
                {
                    MessageBox.Show("Scanner directory contains index.yar. Either remove it manually or run the application as administrator ");
                    return;
                }
                GeneralFunctions.copyYARARulesFilenames();
                runYaraScan();
            }
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnNext.Visible = true;
        }
        private void runLokiScan()
        {
            string dateOfScan = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            string output = null;
            string path = null;
            if (ScanDetails.isScanAllDrives)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                
                if (ScanDetails.isCPULessThan50)
                {
                    startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\procgov.exe" + "\"";
                    if (ScanDetails.isScanMemory)
                    {
                        startInfo.Arguments = " -e 40 -r  " + "\"\"" + ScanDetails.scannerDir + "\\loki.exe\" --allreasons --csv --nolog " + "--allhds" + "\"";
                    }
                    else
                    {
                        
                        startInfo.Arguments = " -e 40 -r  " + "\"\"" + ScanDetails.scannerDir + "\\loki.exe\" --allreasons --noprocscan --csv --nolog " + "--allhds" + "\"";
                    }
                }
                else
                {
                    startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\loki.exe" + "\"";
                    if (ScanDetails.isScanMemory)
                    {
                        startInfo.Arguments = " --allreasons --csv --nolog " + "--allhds";
                    }
                    else
                    {
                        startInfo.Arguments = " --allreasons --noprocscan --csv --nolog " + "--allhds";
                    }
                }
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo = startInfo;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                if (System.Environment.OSVersion.Version.Major >= 6)
                {
                    process.StartInfo.Verb = "runas";
                }
                process.Start();
                path = String.Format(ScanDetails.scannerDir + "\\{0}_{1}_{2}.csv", ScanDetails.computerName, "allhds", dateOfScan);
                ScanDetails.scanResultFilesDirs.Add(path);
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    while ((output = process.StandardOutput.ReadLine()) != null)
                    {
                        output = ReplaceBackspace(output);
                        streamWriter.WriteLine(output);
                        SetText(output);
                    }
                }
                process.WaitForExit();
            }
            else if (ScanDetails.isScanDrive)
            {
                foreach (object itemChecked in ScanDetails.drivesList)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    string scannedDrive = itemChecked.ToString() + ":\\";
                    if (ScanDetails.isCPULessThan50)
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\procgov.exe\"";
                        if (ScanDetails.isScanMemory)
                        {
                            startInfo.Arguments = " -e 40 -r  " + "\"\"" + ScanDetails.scannerDir + "\\loki.exe\" --allreasons --csv --nolog -p " + scannedDrive + "\\\"";
                        }
                        else
                        {
                            startInfo.Arguments = " -e 40 -r  " + "\"\"" + ScanDetails.scannerDir + "\\loki.exe\" --allreasons --noprocscan --csv --nolog -p " + scannedDrive + "\\\"";
                        }
                    }
                    else
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\loki.exe" + "\"";
                        if (ScanDetails.isScanMemory)
                        {
                            startInfo.Arguments = " --allreasons --csv --nolog -p " + scannedDrive;
                        }
                        else
                        {
                            startInfo.Arguments = " --allreasons --noprocscan --csv --nolog -p " + scannedDrive;
                        }
                    }
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo = startInfo;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                    if (System.Environment.OSVersion.Version.Major >= 6)
                    {
                        process.StartInfo.Verb = "runas";
                    }
                    process.Start();
                    path = String.Format(ScanDetails.scannerDir + "\\{0}_{1}_{2}.csv", ScanDetails.computerName, itemChecked.ToString(), dateOfScan);
                    ScanDetails.scanResultFilesDirs.Add(path);
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        while ((output = process.StandardOutput.ReadLine()) != null)
                        {
                            output = ReplaceBackspace(output);
                            streamWriter.WriteLine(output);
                            SetText(output);
                        }
                    }
                    process.WaitForExit();
                }
            }
            else
            {
                foreach (var item in ScanDetails.susDirectories)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    if (ScanDetails.isCPULessThan50)
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\procgov.exe" + "\"";
                        if (ScanDetails.isScanMemory)
                        {
                            startInfo.Arguments = " -e 40 -r  " + "\"\"" + ScanDetails.scannerDir + "\\loki.exe\" --allreasons --csv --nolog -p \"" + item + "\"\"";
                        }
                        else
                        {
                            startInfo.Arguments = " -e 40 -r  " + "\"\"" + ScanDetails.scannerDir + "\\loki.exe\" --allreasons --noprocscan --csv --nolog -p \"" + item + "\"\"";
                        }
                    }
                    else
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\loki.exe" + "\"";
                        if (ScanDetails.isScanMemory)
                        {
                            startInfo.Arguments = " --allreasons --csv --nolog -p " + item + "\"\"";
                        }
                        else
                        {
                            startInfo.Arguments = " --allreasons --noprocscan --csv --nolog -p " + item + "\"\"";
                        }
                    }
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo = startInfo;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                    if (System.Environment.OSVersion.Version.Major >= 6)
                    {
                        process.StartInfo.Verb = "runas";
                    }
                    process.Start();
                    path = String.Format(ScanDetails.scannerDir + "\\{0}_{1}_{2}.csv", ScanDetails.computerName, Path.GetFileName(item), dateOfScan);
                    ScanDetails.scanResultFilesDirs.Add(path);
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        while ((output = process.StandardOutput.ReadLine()) != null)
                        {
                            output = ReplaceBackspace(output);
                            streamWriter.WriteLine(output);
                            SetText(output);
                        }
                        process.WaitForExit();
                    }
                }
            }
        }
        private string ReplaceBackspace(string hasBackspace)
        {
            if (string.IsNullOrEmpty(hasBackspace))
                return hasBackspace;
            //StringBuilder result = new StringBuilder(hasBackspace.Length);
            String result = hasBackspace.TrimStart('\b', '\\', '|', '/', '\r', '\n', '-');
            return result;
        }
        delegate void SetTextCallback(string text);
        delegate void SetVisibilityCallback();
        private void SetText(string text)
        {
            if (this.richtxtbxOutput.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.richtxtbxOutput.SelectionColor = colorForLine(text);
                this.richtxtbxOutput.AppendText("\r\n" + text);
            }
        }
        private Color colorForLine(string line)
        {
            if (line.Contains("Result", StringComparison.InvariantCultureIgnoreCase) &&
                line.Contains("Alerts", StringComparison.InvariantCultureIgnoreCase) &&
                line.Contains("Warnings", StringComparison.InvariantCultureIgnoreCase) &&
                line.Contains("Notices", StringComparison.InvariantCultureIgnoreCase))
                return Color.DarkSeaGreen;
            if (line.Contains("Alert", StringComparison.InvariantCultureIgnoreCase))
                return Color.Tomato;
            if (line.Contains("Warning", StringComparison.InvariantCultureIgnoreCase))
                return Color.Khaki;
            if (line.Contains("NOTICE", StringComparison.InvariantCultureIgnoreCase))
                return Color.LightSkyBlue;
            if (line.Contains("Error", StringComparison.InvariantCultureIgnoreCase))
                return Color.Plum;
            if (line.Contains("Result", StringComparison.InvariantCultureIgnoreCase) &&
                line.Contains("Alerts", StringComparison.InvariantCultureIgnoreCase) &&
                line.Contains("Warnings", StringComparison.InvariantCultureIgnoreCase) &&
                line.Contains("Notices", StringComparison.InvariantCultureIgnoreCase))
                return Color.DarkSeaGreen;
            return Color.White;
        }
        private void runYaraScan()
        {
            string dateOfScan = DateTime.Now.ToString("yyyyMMdd_hhmmss");
            string output = "";
            string error = "";
            string path = null;
            if (ScanDetails.isScanAllDrives)
            {
                List<string> localDrives = ScanDetails.getLocalDrives();
                foreach (var item in localDrives)
                {
                    string scannedDrive = item.ToString();
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    if (ScanDetails.isCPULessThan50)
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\procgov64.exe" + "\"";
                        startInfo.Arguments = " -e 40 -r  " + "\"\"\"" + ScanDetails.scannerDir + "\\yara64.exe\"\" " + "\"\"" + ScanDetails.scannerDir + "\\" + "index.yar" + "\"\"" + " -r " + scannedDrive + "\"";
                    }
                    else
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\yara64.exe\" ";
                        startInfo.Arguments = "\"" + ScanDetails.scannerDir + "\\" + "index.yar" + "\"" + " -r " + scannedDrive + "";
                    }
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo = startInfo;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.StandardOutputEncoding = Encoding.ASCII;
                    process.StartInfo.StandardErrorEncoding = Encoding.ASCII;
                    if (System.Environment.OSVersion.Version.Major >= 6)
                    {
                        process.StartInfo.Verb = "runas";
                    }
                    path = String.Format(ScanDetails.scannerDir + "\\{0}_{1}_{2}.log", ScanDetails.computerName, scannedDrive.TrimEnd('\\',':'), dateOfScan);
                    ScanDetails.scanResultFilesDirs.Add(path);
                    StreamWriter streamWriter = new StreamWriter(path);

                    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                    {
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                streamWriter.WriteLine(e.Data);
                                SetText(e.Data);
                            }
                        };
                        process.ErrorDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                streamWriter.WriteLine(e.Data);
                                SetText(e.Data);
                            }
                        };

                        process.Start();

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        streamWriter.Flush();

                        process.WaitForExit();
                    }
                    streamWriter.Close();

                }
            }
            else if (ScanDetails.isScanDrive)
            {
                foreach (object itemChecked in ScanDetails.drivesList)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    string scannedDrive = itemChecked.ToString() + ":\\";
                    if (ScanDetails.isCPULessThan50)
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\procgov64.exe" + "\"";
                        startInfo.Arguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDir + "\\yara64.exe " + "\"\"" + ScanDetails.scannerDir + "\\" + "index.yar" + "\"\"" + " -r " + scannedDrive + "\"";
                    }
                    else
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\yara64.exe" + "\" ";
                        startInfo.Arguments = "\"" + ScanDetails.scannerDir + "\\" + "index.yar" + "\"" + " -r " + scannedDrive;
                    }
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo = startInfo;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.StandardOutputEncoding = Encoding.ASCII;
                    process.StartInfo.StandardErrorEncoding = Encoding.ASCII;
                    if (System.Environment.OSVersion.Version.Major >= 6)
                    {
                        process.StartInfo.Verb = "runas";
                    }
                    path = String.Format(ScanDetails.scannerDir + "\\{0}_{1}_{2}.log", ScanDetails.computerName, itemChecked.ToString(), dateOfScan);
                    ScanDetails.scanResultFilesDirs.Add(path);
                    StreamWriter streamWriter = new StreamWriter(path);

                    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                    {
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                streamWriter.WriteLine(e.Data);
                                SetText(e.Data);
                            }
                        };
                        process.ErrorDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                streamWriter.WriteLine(e.Data);
                                SetText(e.Data);
                            }
                        };

                        process.Start();

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        streamWriter.Flush();

                        process.WaitForExit();
                    }
                    streamWriter.Close();
                }
            }
            else
            {
                foreach (var item in ScanDetails.susDirectories)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    if (ScanDetails.isCPULessThan50)
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\procgov64.exe" + "\"";
                        startInfo.Arguments = " -e 40 -r  " + "\"\"\"" + ScanDetails.scannerDir + "\\yara64.exe\"\" " + "\"\"" + ScanDetails.scannerDir + "\\" + "index.yar" + "\"\"" + " -r " + "\"\"" + item + "\"\"\"";
                    }
                    else
                    {
                        startInfo.FileName = "\"" + ScanDetails.scannerDir + "\\yara64.exe" + "\" ";
                        startInfo.Arguments = "\"" + ScanDetails.scannerDir + "\\" + "index.yar" + "\"" + " -r " + "\"" + item + "\"";
                        
                    }
                    //MessageBox.Show(startInfo.FileName + startInfo.Arguments);
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo = startInfo;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.StandardOutputEncoding = Encoding.ASCII;
                    process.StartInfo.StandardErrorEncoding = Encoding.ASCII;
                    if (System.Environment.OSVersion.Version.Major >= 6)
                    {
                        process.StartInfo.Verb = "runas";
                    }
                    path = String.Format(ScanDetails.scannerDir + "\\{0}_{1}_{2}.log", ScanDetails.computerName, Path.GetFileName(item), dateOfScan);
                    ScanDetails.scanResultFilesDirs.Add(path);
                    StreamWriter streamWriter = new StreamWriter(path);

                    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                    {
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                streamWriter.WriteLine(e.Data);
                                SetText(e.Data);
                            }
                        };
                        process.ErrorDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                streamWriter.WriteLine(e.Data);
                                SetText(e.Data);
                            }
                        };

                        process.Start();

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        streamWriter.Flush();

                        process.WaitForExit();
                    }
                    streamWriter.Close();
                }
            }
        }
        public static string[] ShowDialog()
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen
            };
            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 50, Top = 10, Text = "Enter Password" };
            TextBox textBox2 = new TextBox() { Left = 50, Top = 30, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 250, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox2);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? new string[] { textBox2.Text } : null;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            OpenNextForm(new Forms.FormScanCompleted());
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
