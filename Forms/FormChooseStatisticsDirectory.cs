using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
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
                        ScanDetails.statisticsFilesDirs.Add(file);
                        txtbxStstisticsFilesDir.Text += " \"" + file + "\"";
                    }

                }
            }
        }

        private void btnViewStatistics_Click(object sender, EventArgs e)
        {
            foreach (String dir in ScanDetails.statisticsFilesDirs)
            {
                if (!File.Exists(dir))
                {
                    MessageBox.Show("Please select a valid file");
                    return;
                }
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
