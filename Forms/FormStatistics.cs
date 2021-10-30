using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace easeYARA.Forms
{
    public partial class FormStatistics : Form
    {

        bool filtered = false;
        bool clear1 = false;
        bool clear2 = false;
        bool clear3 = false;
        bool clear4 = false;

        public FormStatistics()
        {
            InitializeComponent();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            BindDataCSV();

        }



        private void BindDataCSV()
        {
            int alerts = 0;
            int warning = 0;
            int notice = 0;
            int info = 0;
            List<int> machines = new List<int>();

            DataTable table = new DataTable();
            String[] headers = new string[] { "Time", "Computer Name", "Severity", "Message" };

            foreach (String headerWord in headers)
            {
                table.Columns.Add(headerWord);
            }

            foreach (String file in ScanDetails.statisticsFilesDirs)
            {

                String[] lines = System.IO.File.ReadAllLines(file);

                for (int r = 0; r < lines.Length; r++)
                {
                    if (lines[r] != "")
                    {
                        if (char.IsDigit(lines[r][0]))
                        {
                            if (!(lines[r].Contains("Result", StringComparison.InvariantCultureIgnoreCase) &&
                                lines[r].Contains("Alerts", StringComparison.InvariantCultureIgnoreCase) &&
                                lines[r].Contains("Warnings", StringComparison.InvariantCultureIgnoreCase) &&
                                lines[r].Contains("Notices", StringComparison.InvariantCultureIgnoreCase)))
                            {
                                if (lines[r].Contains("Alert", StringComparison.InvariantCultureIgnoreCase))
                                    alerts++;
                                if (lines[r].Contains("Warning", StringComparison.InvariantCultureIgnoreCase))
                                    warning++;
                                if (lines[r].Contains("NOTICE", StringComparison.InvariantCultureIgnoreCase))
                                    notice++;
                                if (lines[r].Contains("INFO", StringComparison.InvariantCultureIgnoreCase))
                                    info++;
                            }
                            if ((lines[r].Contains("Result", StringComparison.InvariantCultureIgnoreCase) &&
                                lines[r].Contains("Alerts", StringComparison.InvariantCultureIgnoreCase) &&
                                lines[r].Contains("Warnings", StringComparison.InvariantCultureIgnoreCase) &&
                                lines[r].Contains("Notices", StringComparison.InvariantCultureIgnoreCase)))
                            {
                                notice++;
                            }


                            String[] dataWords = lines[r].Split(',');
                            DataRow row = table.NewRow();

                            if (dataWords.Length < 4)
                            {
                                for (int i = 0; i < dataWords.Length; i++)
                                {
                                    row["Message"] += " " + dataWords[i];
                                }
                            }
                            else
                            {
                                int columnIndex = 0;
                                foreach (String headerWord in headers)
                                {

                                    if (columnIndex == 3)
                                    {
                                        for (int i = 3; i < dataWords.Length; i++)
                                        {
                                            row[headerWord] += " " + dataWords[i];
                                        }
                                    }
                                    else
                                    {
                                        row[headerWord] = dataWords[columnIndex++];
                                    }
                                }
                            }
                            
                            table.Rows.Add(row);
                        }
                    }
                    

                }
            }

            lblAlertsNum.Text = "" + alerts;
            lblWarningNum.Text = "" + warning;
            lblNoticeNum.Text = "" + notice;
            lblInfoNum.Text = "" + info;


            if (table.Rows.Count > 0)
            {
                // Double buffering can make DGV slow in remote desktop
                if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
                {
                    Type dgvType = dataGridView1.GetType();
                    PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                      BindingFlags.Instance | BindingFlags.NonPublic);
                    pi.SetValue(dataGridView1, true, null);
                }

                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Width = 200;
                dataGridView1.Columns[1].Width = 200;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            List<string> list = dataGridView1.Rows
                             .OfType<DataGridViewRow>()
                             .Select(r => r.Cells[1].Value.ToString())
                             .ToList();
            IEnumerable<String> uniqueItems = list.Distinct<String>();
            lblMachineNum.Text = "" + uniqueItems.ToList().Count;

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void AddNewFilter(object sender, EventArgs e)
        {
            bool increase = false;
            Label x = sender as Label;
            switch (x.Tag)
            {
                case "1":
                    if(tableLayoutPanel2.Visible == false)
                    {
                        lblAdd1.ForeColor = Color.Gray;
                        tableLayoutPanel2.Visible = true;
                        increase = true;
                    }
                    break;
                case "2":
                    if (tableLayoutPanel3.Visible == false)
                    {
                        lblAdd2.ForeColor = Color.Gray;
                        lblDelete2.ForeColor = Color.Gray;
                        tableLayoutPanel3.Visible = true;
                        increase = true;
                    }
                    break;
                case "3":
                    if (tableLayoutPanel4.Visible == false)
                    {
                        lblAdd3.ForeColor = Color.Gray;
                        lblDelete3.ForeColor = Color.Gray;
                        tableLayoutPanel4.Visible = true;
                        increase = true;
                    }
                    break;
            }
            if (increase)
            {
                dataGridView1.Location = new System.Drawing.Point(dataGridView1.Location.X, dataGridView1.Location.Y + 35);
                dataGridView1.Size = new System.Drawing.Size(dataGridView1.Size.Width, dataGridView1.Size.Height - 35);
            }
        }

        private void ClearFilter(object sender, EventArgs e)
        {
            Label x = sender as Label;
            switch (x.Tag)
            {
                case "1":
                    txtbxKeyword1.Text = "";
                    cmbbxIn1.SelectedIndex = -1;
                    rbExclude1.Checked = false;
                    rbInclude1.Checked = false;
                    clear1 = true;
                    break;
                case "2":
                    txtbxKeyword2.Text = "";
                    cmbbxIn2.SelectedIndex = -1;
                    rbExclude2.Checked = false;
                    rbInclude2.Checked = false;
                    clear2 = true;
                    break;
                case "3":
                    txtbxKeyword3.Text = "";
                    cmbbxIn3.SelectedIndex = -1;
                    rbExclude3.Checked = false;
                    rbInclude3.Checked = false;
                    clear3 = true;
                    break;
                case "4":
                    txtbxKeyword4.Text = "";
                    cmbbxIn4.SelectedIndex = -1;
                    rbExclude4.Checked = false;
                    rbInclude4.Checked = false;
                    clear4 = true;
                    break;
            }

            if (filtered)
            {
                BindDataCSV();
                btnFilter_Click(sender, e);
                clear1 = false;
                clear2 = false;
                clear3 = false;
                clear4 = false;
            }
        }

        private void DeleteFilter(object sender, EventArgs e)
        {
            bool decrease = false;
            Label x = sender as Label;
            switch (x.Tag)
            {
                case "2":
                    if (lblDelete2.ForeColor != Color.Gray)
                    {
                        
                        tableLayoutPanel2.Visible = false;
                        lblAdd1.ForeColor = Color.FromArgb(192, 255, 192);
                        decrease = true;
                    }
                    break;
                case "3":
                    if (lblDelete3.ForeColor != Color.Gray)
                    {
                        tableLayoutPanel3.Visible = false;
                        lblAdd2.ForeColor = Color.FromArgb(192, 255, 192);
                        lblDelete2.ForeColor = Color.FromArgb(255, 128, 128);
                        decrease = true;
                    }
                    break;
                case "4":
                    if (lblDelete4.ForeColor != Color.Gray)
                    {
                        tableLayoutPanel4.Visible = false;
                        lblAdd3.ForeColor = Color.FromArgb(192, 255, 192);
                        lblDelete3.ForeColor = Color.FromArgb(255, 128, 128);
                        decrease = true;
                    }
                    break;
            }

            if (decrease)
            {
                ClearFilter(sender, e);
                dataGridView1.Location = new System.Drawing.Point(dataGridView1.Location.X, dataGridView1.Location.Y - 35);
                dataGridView1.Size = new System.Drawing.Size(dataGridView1.Size.Width, dataGridView1.Size.Height + 35);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindDataCSV();
        }

        private String RowFilterString(int index)
        {
            TextBox textbox = new TextBox();
            ComboBox combobox = new ComboBox();
            RadioButton include = new RadioButton();
            RadioButton exclude = new RadioButton();
            bool clear = false;

            switch (index)
            {
                case 1:
                    textbox = txtbxKeyword1;
                    combobox = cmbbxIn1;
                    include = rbInclude1;
                    exclude = rbExclude1;
                    clear = clear1;
                    break;
                case 2:
                    textbox = txtbxKeyword2;
                    combobox = cmbbxIn2;
                    include = rbInclude2;
                    exclude = rbExclude2;
                    clear = clear2;

                    break;
                case 3:
                    textbox = txtbxKeyword3;
                    combobox = cmbbxIn3;
                    include = rbInclude3;
                    exclude = rbExclude3;
                    clear = clear3;
                    break;
                case 4:
                    textbox = txtbxKeyword4;
                    combobox = cmbbxIn4;
                    include = rbInclude4;
                    exclude = rbExclude4;
                    clear = clear4;
                    break;
            }

            string rowFilter = "";
            if (index != 1 && !clear)
            {
                rowFilter += " AND ";
            }

            if (!clear && (textbox.Text == "" || combobox.SelectedIndex == -1 || !(include.Checked ^ exclude.Checked)))
            {
                MessageBox.Show("Please fill all filter options to apply the filter");
                return "";
            }
            if (include.Checked && !clear)
            {
                if (combobox.SelectedItem.ToString() == "All")
                {
                    foreach (String col in combobox.Items)
                    {
                        if (col == "All")
                        {

                        }
                        else if (col == "Time")
                        {
                            rowFilter += string.Format("([{0}] LIKE '%{1}%'", col, textbox.Text);
                        }
                        else
                        {
                            rowFilter += string.Format(" OR [{0}] LIKE '%{1}%'", col, textbox.Text);
                        }
                    }
                    rowFilter += ")";
                }
                else
                {
                    rowFilter += string.Format("[{0}] LIKE '%{1}%'", combobox.SelectedItem.ToString(), textbox.Text);

                }
            }
            else if (!clear)
            {
                if (combobox.SelectedItem.ToString() == "All")
                {
                    foreach (String col in combobox.Items)
                    {
                        if (col == "All")
                        {

                        }
                        else if (col == "Time")
                        {
                            rowFilter += string.Format("([{0}] NOT LIKE '%{1}%'", col, textbox.Text);
                        }
                        else
                        {
                            rowFilter += string.Format(" AND [{0}] NOT LIKE '%{1}%'", col, textbox.Text);
                        }
                    }
                    rowFilter += ")";
                }
                else
                {
                    rowFilter += string.Format("[{0}] NOT LIKE '%{1}%'", combobox.SelectedItem.ToString(), textbox.Text);
                }
            }
            return rowFilter;
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            string rowFilter = "";

            rowFilter = RowFilterString(1);

            if (tableLayoutPanel2.Visible)
                rowFilter += RowFilterString(2);

            if (tableLayoutPanel3.Visible)
                rowFilter += RowFilterString(3);

            if (tableLayoutPanel4.Visible)
                rowFilter += RowFilterString(4);

            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            filtered = true;
        }

        private void QuickFilter(object sender, EventArgs e)
        {
            Label x = sender as Label;
            switch (x.Tag)
            {
                case "alert":
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", "Severity", "alert");
                    break;
                case "warning":
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", "Severity", "warning");
                    break;
                case "notice":
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", "Severity", "notice");
                    break;
                case "info":
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", "Severity", "info");
                    break;
            }
        }
    }
        
}
