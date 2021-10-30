/*
 *  Author: hejelylab.github.io 
 */
using System;
using System.Windows.Forms;
namespace easeYARA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            ScanDetails.susDirectories.Add("C:\\Users");
            ScanDetails.susDirectories.Add("C:\\Users\\Public");
            ScanDetails.susDirectories.Add("C:\\Users\\All Users");
            ScanDetails.susDirectories.Add("C:\\ProgramData");
            ScanDetails.susDirectories.Add("C:\\inetpub\\wwwroot\\aspnet_client");
            ScanDetails.susDirectories.Add("C:\\Program Files\\Microsoft\\Exchange Server");
            ScanDetails.susDirectories.Add("C:\\Windows\\Temp");
            ScanDetails.susDirectories.Add("C:\\Temp");
            ScanDetails.susDirectories.Add("C:\\Program Files");
            ScanDetails.susDirectories.Add("C:\\Program Files (x86)");
            ScanDetails.susDirectories.Add("C:\\Windows\\System32");
            ScanDetails.susDirectories.Add("C:\\Users\\Administrator");

            
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void pnlTop_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void OpenNextForm(Form nextForm)
        {
            nextForm.TopLevel = false;
            nextForm.Dock = DockStyle.Fill;
            this.pnlChildForm.Controls.Add(nextForm);
            nextForm.BringToFront();
            nextForm.Show();
        }
        private void btnScan_Click(object sender, EventArgs e)
        {
            ScanDetails.computerName = System.Environment.MachineName;
            OpenNextForm(new Forms.FormScanTarget());
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            OpenNextForm(new Forms.FormChooseStatisticsDirectory());
        }

        //private void MainForm_Load(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Some of the application features needs Admin privileges to run correctly");
        //}
    }
}
