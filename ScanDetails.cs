using System;
using System.Collections.Generic;
using System.IO;
namespace easeYARA
{
    public static class ScanDetails
    {
        public static String scanTarget { get; set; }
        public static String scanner { get; set; }
        public static String scannerDir { get; set; }
        public static String scannerDirUNCPath { get; set; }
        public static bool isScannerLocal { get; set; }
        public static bool isScanAllDrives { get; set; }
        public static bool isScanDrive { get; set; }
        public static List<String> drivesList = new List<string>();
        public static bool isScanSusDirectories { get; set; }
        public static List<String> susDirectories = new List<string>();
        public static List<String> susDirectories2 = new List<string>();
        public static String scanDirectory { get; set; }
        public static bool isAddRules { get; set; }
        public static String addRulesFileDir { get; set; }
        public static bool isScanMemory { get; set; }
        public static bool isCPULessThan50 { get; set; }
        public static String yaraResultsDirectory { get; set; }
        public static String yaraResultsDirectoryUNCPath { get; set; }
        public static String computerName { get; set; }
        public static String generatedCommand { get; set; }
        public static String generatedCommandArguments { get; set; }
        public static List<String> statisticsFilesDirs = new List<string>();
        public static List<String> scanResultFilesDirs = new List<string>();

        public static bool isFromChooseDir = false;
        public static bool isFromScanOptions = false;

        public static List<string> getLocalDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<string> list = new List<string>();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true && d.DriveType == DriveType.Fixed)
                {
                    list.Add(d.ToString());
                }
            }
            return list;
        }
    }
}
