using System;
using System.IO;
using System.Windows.Forms;

namespace easeYARA.Forms
{
    public partial class FormChooseStatisticsDirectory : Form
    {
        public FormChooseStatisticsDirectory()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            using (openFileDialog1)
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "log files (*.log, *.csv)|*.log;*.csv";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ScanDetails.statisticsFilesDirs.Clear();
                    txtbxStstisticsFilesDir.Text = "";
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        txtbxStstisticsFilesDir.Text += file + ",";
                    }
                    txtbxStstisticsFilesDir.Text = txtbxStstisticsFilesDir.Text.Remove(txtbxStstisticsFilesDir.Text.Length - 1, 1);
                }
            }
        }

        private void btnViewStatistics_Click(object sender, EventArgs e)
        {
            long filesSize = 0;
            String[] dirs = txtbxStstisticsFilesDir.Text.Split(',');
            foreach (String dir in dirs)
            {
                ScanDetails.statisticsFilesDirs.Add(dir);
                if (!File.Exists(dir))
                {
                    MessageBox.Show("Please select a valid file");
                    ScanDetails.statisticsFilesDirs.Clear();
                    return;
                }

                filesSize += new System.IO.FileInfo(dir).Length;
            }

            if (filesSize > 52428800)
            {
                MessageBox.Show("The sum of all files size must be less than 50MB.");
                ScanDetails.statisticsFilesDirs.Clear();
                return;
            }

            OpenNextForm(new Forms.FormStatistics());
        }

        private void OpenNextForm(Form nextForm)
        {

            nextForm.TopLevel = false;
            nextForm.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(nextForm);
            nextForm.BringToFront();
            nextForm.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ScanDetails.statisticsFilesDirs.Clear();
            this.Close();
        }
    }
}
