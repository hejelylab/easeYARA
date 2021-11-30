using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace easeYARA.Forms
{
    public partial class FormGenerateScript : Form
    {
        public FormGenerateScript()
        {
            InitializeComponent();
            richTextBox1.AppendText(ScanDetails.generatedCommand + " " + ScanDetails.generatedCommandArguments);

            if (richTextBox1.Text.Contains("Please click on Generate Batch Script"))
            {
                lblCopyToClipboard.Visible = false;
            }

        }
        private void btnBack_Click(object sender, EventArgs e)
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
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ScanDetails.scanTarget = "";
            ScanDetails.scanner = "";
            ScanDetails.scanDirectory = "";
            ScanDetails.isScannerLocal = false;
            ScanDetails.isScanAllDrives = false;
            ScanDetails.isScanDrive = false;
            ScanDetails.drivesList.Clear();
            ScanDetails.isScanSusDirectories = false;
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
            ScanDetails.scannerDirUNCPath = "";
            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);
            foreach (Form f in openForms)
            {
                if (f.Name != "MainForm")
                    f.Close();
            }
        }
        private void btnGenerateBatchScript_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(ScanDetails.scannerDir + "\\RunYARAScanner.bat");
            if (!ScanDetails.scanTarget.Equals("local"))
            {

                if (ScanDetails.scanner == "loki" && ((ScanDetails.isScanDrive && ScanDetails.drivesList.Count > 1) || (ScanDetails.isScanSusDirectories && ScanDetails.susDirectories.Count > 1)))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("@PUSHD %~dp0");
                    if (ScanDetails.isScanDrive && ScanDetails.drivesList.Count > 1)
                    {
                        sw.Write("set list=");
                        foreach (var item in ScanDetails.drivesList)
                        {
                            sw.Write(item + " ");
                        }
                        sw.WriteLine();
                        sw.WriteLine("(FOR %%C in (%list%) DO FOR /F  \"delims=|\" %%D IN (\"%%C\") DO (");
                        if (ScanDetails.isCPULessThan50)
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov.exe";
                            if (ScanDetails.isScanMemory)
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons -p %%" + "D" + ":\\ --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                            else
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons --noprocscan -p %%" + "D" + ":\\ --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                        }
                        else
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\loki.exe";
                            if (ScanDetails.isScanMemory)
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " --allreasons -p %%" + "D" + ":\\ --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                            else
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " --noprocscan --allreasons -p %%" + "D" + ":\\ --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                        }
                    }
                    else
                    {
                        sw.Write("set list=");
                        foreach (var item in ScanDetails.susDirectories)
                        {
                            sw.Write("\"" + item + "\"" + " ");
                        }
                        sw.WriteLine();
                        sw.WriteLine("(FOR %%C in (%list%) DO FOR /F \"delims=|\" %%D IN (\"%%C\") DO (");
                        if (ScanDetails.isCPULessThan50)
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov.exe";
                            if (ScanDetails.isScanMemory)
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons -p \"%%" + "D" + "\" --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                            else
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\loki.exe --allreasons --noprocscan -p \"%%" + "D" + "\" --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                        }
                        else
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\loki.exe";
                            if (ScanDetails.isScanMemory)
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " --allreasons -p %%" + "D" + " --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                            else
                            {
                                sw.WriteLine(ScanDetails.generatedCommand + " --noprocscan --allreasons -p %%" + "D" + " --dontwait --csv --logfolder " + ScanDetails.yaraResultsDirectoryUNCPath + "\"");
                            }
                        }
                    }
                    sw.WriteLine("))");
                }

                else if (ScanDetails.scanner == "yara" && ((ScanDetails.isScanDrive && ScanDetails.drivesList.Count == 1) || (ScanDetails.isScanSusDirectories && ScanDetails.susDirectories.Count == 1)))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("@PUSHD %~dp0");
                    if (ScanDetails.isScanDrive && ScanDetails.drivesList.Count == 1)
                    {
                        string scannedDrive = ScanDetails.drivesList[0] + ":\\";
                        if (ScanDetails.isCPULessThan50)
                        {

                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov64.exe";
                            ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\yara64.exe " + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "\"" + scannedDrive + "\\\"\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%_" + ScanDetails.drivesList[0] + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            sw.WriteLine(ScanDetails.generatedCommand + ScanDetails.generatedCommandArguments);
                        }
                        else
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\yara64.exe";
                            ScanDetails.generatedCommandArguments = " " + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "\"" + scannedDrive + "\\\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%_" + ScanDetails.drivesList[0] + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            sw.WriteLine(ScanDetails.generatedCommand + ScanDetails.generatedCommandArguments);
                        }
                    }
                    else
                    {
                        if (ScanDetails.isCPULessThan50)
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov64.exe";
                            ScanDetails.generatedCommandArguments = " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\yara64.exe " + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "\"\"" + ScanDetails.susDirectories[0] + "\"\"\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            sw.WriteLine(ScanDetails.generatedCommand + ScanDetails.generatedCommandArguments);
                        }
                        else
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\yara64.exe";
                            ScanDetails.generatedCommandArguments = " " + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "\"" + ScanDetails.susDirectories[0] + "\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%%TIME:~1,1%%TIME:~3,2%%TIME:~6,2%.log";
                            sw.WriteLine(ScanDetails.generatedCommand + ScanDetails.generatedCommandArguments);
                        }
                    }
                }
                else if (ScanDetails.scanner == "yara" && ((ScanDetails.isScanDrive && ScanDetails.drivesList.Count > 1) || (ScanDetails.isScanSusDirectories && ScanDetails.susDirectories.Count > 1)))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("@PUSHD %~dp0");

                    if (ScanDetails.isScanDrive && ScanDetails.drivesList.Count > 1)
                    {
                        sw.WriteLine("set t=%TIME: =0%");
                        sw.Write("set list=");
                        foreach (var item in ScanDetails.drivesList)
                        {
                            sw.Write(item + " ");
                        }
                        sw.WriteLine();
                        sw.WriteLine("(FOR %%C in (%list%) DO (");
                        sw.WriteLine("FOR /F \"delims=|\" %%D IN (\"%%C\") DO (");

                        if (ScanDetails.isCPULessThan50)
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov64.exe";
                            sw.WriteLine(ScanDetails.generatedCommand + " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\yara64.exe " + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "\"%%" + "D" + ":\\\\\"\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%_" + "%%D" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%_%t:~0,2%%t:~3,2%%t:~6,2%.log");
                        }
                        else
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\yara64.exe ";
                            sw.WriteLine(ScanDetails.generatedCommand + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "%%" + "D" + ":\\" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%_" + "%%D" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%_%t:~0,2%%t:~3,2%%t:~6,2%.log");
                        }
                    }
                    else
                    {
                        sw.WriteLine("set t=%TIME: =0%");
                        sw.Write("set list=");
                        foreach (var item in ScanDetails.susDirectories)
                        {
                            sw.Write("\"" + item + "\"" + " ");
                        }
                        sw.WriteLine();
                        sw.WriteLine("(FOR %%C in (%list%) DO (");
                        sw.WriteLine("FOR /F \"delims=|\" %%D IN (\"%%C\") DO (");
                        if (ScanDetails.isCPULessThan50)
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov64.exe";
                            sw.WriteLine(ScanDetails.generatedCommand + " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\yara64.exe " + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "\"%%" + "D" + "\"\"" + " >> " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%_%t:~0,2%%t:~3,2%%t:~6,2%.log");
                        }
                        else
                        {
                            ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\yara64.exe ";
                            sw.WriteLine(ScanDetails.generatedCommand + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "%%" + "D" + " >> " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%" + "_%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%_%t:~0,2%%t:~3,2%%t:~6,2%.log");
                        }
                    }
                    sw.WriteLine(")))");
                }
                else if (ScanDetails.scanner == "yara" && ScanDetails.isScanAllDrives)
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("@PUSHD %~dp0");
                    sw.WriteLine("SETLOCAL ENABLEDELAYEDEXPANSION");
                    sw.WriteLine("FOR /F \"skip=1\" %%C in ('WMIC LOGICALDISK WHERE \"DRIVETYPE=3\" GET CAPTION') DO FOR /F %%D IN (\"%%C\") DO (");
                    sw.WriteLine("SET DRIVELETTER2=%%D");
                    sw.WriteLine("SET DRIVELETTER=!DRIVELETTER2:~0,1!");
                    sw.WriteLine("SET CURRENTDATE=%DATE:~10,4%%DATE:~4,2%%DATE:~7,2%");

                    if (ScanDetails.isCPULessThan50)
                    {
                        ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\procgov64.exe";
                        sw.WriteLine(ScanDetails.generatedCommand + " -e 40 -r  " + "\"" + ScanDetails.scannerDirUNCPath + "\\yara64.exe " + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "\"%%" + "D" + "\\\\\"\"" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%_" + "!DRIVELETTER!" + "_!CURRENTDATE!.log");
                    }
                    else
                    {
                        ScanDetails.generatedCommand = ScanDetails.scannerDirUNCPath + "\\yara64.exe ";
                        sw.WriteLine(ScanDetails.generatedCommand + ScanDetails.scannerDirUNCPath + "\\" + "index.yar" + " -r " + "%%" + "D" + "\\" + " > " + ScanDetails.yaraResultsDirectoryUNCPath + "\\%COMPUTERNAME%_" + "!DRIVELETTER!" + "_!CURRENTDATE!.log");
                    }
                    sw.WriteLine(")");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string line in richTextBox1.Lines)
                    {
                        sb.AppendLine(line);
                    }
                    sw.WriteLine("@echo off");
                    sw.WriteLine("@PUSHD %~dp0");
                    sw.Write(sb.ToString());
                }


            }
            sw.WriteLine("@POPD");
            sw.Close();

            MessageBox.Show("The batch script file is generated to: \n" + ScanDetails.scannerDir);

        }



        private void lblCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
            ToolTip toolTip1 = new ToolTip();
            toolTip1.UseFading = true;
            toolTip1.UseAnimation = true;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(lblCopyToClipboard, "Command copied to clipboard");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
