# easeYARA

C# Desktop GUI application that either performs YARA scan locally or prepares the scan in a domain environment with a few clicks.
The application utilizes a combination of the following applications to perform the scanning process and limit the CPU rate during the scan.
1. YARA Scanner (VirusTotal/yara)
	Link: https://github.com/VirusTotal/yara
2. LOKI Scanner (Neo23x0/Loki)
	LINK: https://github.com/Neo23x0/Loki
3. PE-sieve process scanner (hasherezade/pe-sieve)
	LINK: https://github.com/hasherezade/pe-sieve
3. process-governor (lowleveldesign/process-governor)
	LINK: https://github.com/lowleveldesign/process-governor

## How does it work?
Welcome Screen <br/>
What would you like to do?<br/> 
1. Local scan\prepare for remote scan<br/>
2. View statistics for previous scan results<br/>

![alt Welcome Screen](/screenshots/welcome_1.png "Welcome Screen")<br/>
<br/>
  
### How to Scan?
Would you like to scan a local system or prepare for a remote scan?<br/>
<br/>
![alt Scan Target](/screenshots/scantarget_1.png  "Scan Target")<br/>
<br/>
Choose your preferred scanner:
- YARA: If you already have YARA rules and want to hunt simply for similar files/patterns, choose this option.
- LOKI: if you don't already have YARA rules to start your CA/IR/Threat Hunting activity with, choose this option.
LOKI isn't only about YARA, it is considered an IOC scanner as well. For example, aspnet_client directory in Microsoft Exchange doesn't have aspx pages, LOKI will warn you if it sees this aside from YARA rules matching.

![alt Choose Scanner](/screenshots/localselectscanner_1.png "Choose Scanner")<br/>
<br/>
Download Scanner or point to the application in your system
- YARA will be downloaded, and unzipped.
- LOKI: will be downloaded, unzipped, and updated.
		In case it's available, latest update will be pulled based on your confirmation.

**Scan Options**
+ Required Options (Choose one option)
  - Scan all local drives in system
  - Scan specific drive(s)
  - Scan specific/suspicious directories (you can either go with the predefined suspicious directories, or modify them to any list of directories you prefer)
  
 Great option to scan the most "maliciously" used directories in the a complete AD domain environment every now and then :)
+ Optional
  - If you already have YARA rules in (zip) format, you can point to it.
  - If you have already chosen LOKI scanner, there is an option to scan memory using (PE-sieve).
  - In case you wish not to exceed around 50% of CPU limit at any time during the scan, pick this option. This will download, unzip and use process-governor with CPU rate limit of around 40% to accept a few percents more.
  
![alt Local Scan Options](/screenshots/localscanoptionsloki_1.png  "Local Scan Options")<br/>
<br/>
In case you chose Prepare remote scan, the following will be applied once clicking NEXT button
+ Scanner parent directory will be shared with the following restrictions
  - Full Control for Administrators group
  - Read and Execute only for Authenticated Users
	  * This will prevent writing to this directory from non-admin users.
+ New scan results directory will be created (YARAScanResults) in the same directory as the parent directory resides with the following restrictions
	- Full Control for Administrators group
	- Write only for Authenticated Users
			* To have results get populated to this directory (from YARA scan target systems).

![alt Remote Scan Options](/screenshots/remotescanoptions_1.png  "Remote Scan Options")<br/>
<br/>
**Local Scan/ Prepare Remote Scan**
- In case you chose to scan the local system, the picked options will be applied during the scan.
	* Output will be provided on the application
	* Output log files containing the output will be created in the same scanner directory.
    
![alt Scanning](/screenshots/scanning_1.png  "Scanning")<br/>
<br/>
![alt Scan Completed](/screenshots/scancompleted_1.png  "Scan Completed")<br/>
<br/>
- In case you chose prepare remote scan
	* If the picked options can be generated in a single command, it will be shown in the RichTextBox.
	* Whether the picked options generated a single or multiple commands, you can click on button "Generate batch script" which will create a BATCH script to the same scanner directory.
  
![alt Generate Script](/screenshots/remotegeneratescript_2.png  "Generate Script")<br/>
<br/>
### How to view statistics?
Would you like to have a glimpse about the scan results?
- If you have already performed scanning in the same session the application is running, click on "view results"
- If you only want to view results without scanning, click on "Statistics"
	- You can choose multiple files at the same time. Applying filters is simple based on columns.
	- Pick any severity level to apply it as filter on the table.
  
![alt Statistics](/screenshots/statistics_1.png  "Statistics")<br/>
<br/>
![alt Statistics Filter](/screenshots/scancompleted_2.png "Statistics Filter")<br/>
<br/>
## How to sweep target systems using the generated commands/batch scripts?
- Create GPO > Schedule task > write the generated commands directlry or point to the UNC path of the batch scripts
- Copy the generated command and run it from any system you wish to scan.
- Windows+R > point to the UNC path of the batch script

All of the above aproaches will return results to YARAScanResults share.

