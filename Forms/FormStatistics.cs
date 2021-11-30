using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace easeYARA.Forms
{
    public partial class FormStatistics : Form
    {
        BackgroundWorker bw = new BackgroundWorker();

        bool filtered = false;
        bool clear1 = false;
        bool clear2 = false;
        bool clear3 = false;
        bool clear4 = false;
        int alerts = 0;
        int warning = 0;
        int notice = 0;
        int info = 0;
        DataTable table;
        bool oneColumn = false;
        string quickFilter = "";

        public FormStatistics()
        {
            InitializeComponent();
            if (ScanDetails.statisticsFilesDirs.Count == 0)
            {
                lblComment.Text = "No files were selected";
            }
            else
            {
                bw.DoWork += bw_DoWork;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                bw.RunWorkerAsync();
            }

        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BindDataCSV();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            updateView();
        }

        private void BindDataCSV()
        {
            alerts = 0;
            warning = 0;
            notice = 0;
            info = 0;
            table = new DataTable();
            List<int> machines = new List<int>();
            String[] headers = new string[] { "Time", "Computer Name", "Severity", "Message" };
            bool firstLine = true;

            foreach (String file in ScanDetails.statisticsFilesDirs)
            {

                String[] lines = System.IO.File.ReadAllLines(file);

                for (int r = 0; r < lines.Length; r++)
                {
                    if (lines[r] != "")
                    {
                        if (char.IsDigit(lines[r][0]))
                        {
                            if (firstLine)
                            {
                                String[] dw = lines[r].Split(',');
                                if (dw.Length == 1)
                                {
                                    table.Columns.Add("Message");
                                    oneColumn = true;

                                }
                                else
                                {
                                    if (table.Columns.Count == 0)
                                    {
                                        foreach (String headerWord in headers)
                                        {
                                            table.Columns.Add(headerWord);
                                        }
                                    }
                                }
                                firstLine = false;
                            }
                            DataRow row = table.NewRow();
                            if (oneColumn)
                            {
                                row["Message"] += " " + lines[r];
                            }
                            else
                            {
                                String[] dataWords = lines[r].Split(',');

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
                                            if (headerWord == "Severity")
                                            {
                                                switch (dataWords[columnIndex].ToLower())
                                                {
                                                    case "alert":
                                                        alerts++;
                                                        break;
                                                    case "warning":
                                                        warning++;
                                                        break;
                                                    case "notice":
                                                        notice++;
                                                        break;
                                                    case "info":
                                                        info++;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            row[headerWord] = dataWords[columnIndex++];
                                        }
                                    }
                                }
                            }
                            table.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private void updateView()
        {
            lblAlertsNum.Text = "" + alerts;
            lblWarningNum.Text = "" + warning;
            lblNoticeNum.Text = "" + notice;
            lblInfoNum.Text = "" + info;

            if (table.Rows.Count > 0)
            {
                if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
                {
                    Type dgvType = dataGridView1.GetType();
                    PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                      BindingFlags.Instance | BindingFlags.NonPublic);
                    pi.SetValue(dataGridView1, true, null);
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dataGridView1.DataSource = table;

                if (oneColumn)
                {
                    dataGridView1.Columns[0].Width = dataGridView1.Width;
                    cmbbxIn1.Items.Clear();
                    cmbbxIn1.Items.Add("Message");
                    cmbbxIn2.Items.Clear();
                    cmbbxIn2.Items.Add("Message");
                    cmbbxIn3.Items.Clear();
                    cmbbxIn3.Items.Add("Message");
                    cmbbxIn4.Items.Clear();
                    cmbbxIn4.Items.Add("Message");
                }
                else
                {
                    dataGridView1.Columns[0].Width = 200;
                    dataGridView1.Columns[1].Width = 200;
                    dataGridView1.Columns[2].Width = 100;
                    if (dataGridView1.Width == 1200)
                    {
                        dataGridView1.Columns[3].Width = 680;
                    }
                    else
                    {
                        dataGridView1.Columns[3].Width = (int)dataGridView1.Width - 520;
                    }
                }
            }
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            lblNumOfRow.Text = "Number of rows: " + dataGridView1.Rows.Count;
            lblComment.Visible = false;
        }
        private void update()
        {
            BindDataCSV();
            updateView();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            ScanDetails.statisticsFilesDirs.Clear();
            this.Close();
        }

        private void AddNewFilter(object sender, EventArgs e)
        {
            bool increase = false;
            Label x = sender as Label;
            switch (x.Tag)
            {
                case "1":
                    if (tableLayoutPanel2.Visible == false)
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
                update();
                btnFilter_Click(sender, e);
                clear1 = false;
                clear2 = false;
                clear3 = false;
                clear4 = false;

            }

        }

        private void DeleteFilter(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                MessageBox.Show("Please wait until the previous process ends");
            }
            else
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

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                MessageBox.Show("Please wait until the previous process ends");
            }
            else
            {
                btnRefresh.Enabled = false;
                lblComment.Visible = true;
                bw.RunWorkerAsync();
                btnRefresh.Enabled = true;
                lblColorRed.BackColor = Color.FromArgb(255, 128, 128);
                lblColorYellow.BackColor = Color.FromArgb(255, 255, 182);
                lblColorBlue.BackColor = Color.FromArgb(142, 255, 255);
                lblColorGreen.BackColor = Color.FromArgb(192, 255, 182);
                quickFilter = "";
            }
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
            btnFilter.Enabled = false;
            string rowFilter = "";
            string c = "";

            if (sender is Button || (quickFilter != "" && txtbxKeyword1.Text != "" && cmbbxIn1.SelectedIndex != -1 && (rbInclude1.Checked ^ rbExclude1.Checked)))
            {
                rowFilter = RowFilterString(1);
            }

            if (tableLayoutPanel2.Visible || (quickFilter != "" && txtbxKeyword2.Text != "" && cmbbxIn2.SelectedIndex != -1 && (rbInclude2.Checked ^ rbExclude2.Checked)))
            {
                c = RowFilterString(2);
                if (rowFilter != "" && !clear2 && c != "")
                {
                    rowFilter += " AND ";
                }
                rowFilter += c;
                c = "";
            }


            if (tableLayoutPanel3.Visible || (quickFilter != "" && txtbxKeyword3.Text != "" && cmbbxIn3.SelectedIndex != -1 && (rbInclude3.Checked ^ rbExclude3.Checked)))
            {
                c = RowFilterString(3);
                if (rowFilter != "" && !clear3 && c != "")
                {
                    rowFilter += " AND ";
                }
                rowFilter += c;
                c = "";
            }


            if (tableLayoutPanel4.Visible || (quickFilter != "" && txtbxKeyword4.Text != "" && cmbbxIn4.SelectedIndex != -1 && (rbInclude4.Checked ^ rbExclude4.Checked)))
            {
                c = RowFilterString(4);
                if (rowFilter != "" && !clear4 && c != "")
                {
                    rowFilter += " AND ";
                }
                rowFilter += c;
                c = "";
            }


            if (quickFilter != "")
            {
                if (rowFilter != "")
                {
                    rowFilter += " AND ";
                }

                switch (quickFilter)
                {
                    case "alert":
                        if (dataGridView1.Columns.Count >= 4)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Severity", "alert");
                        }
                        else if (dataGridView1.Columns.Count == 1)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Message", "alert");
                        }
                        break;
                    case "warning":
                        if (dataGridView1.Columns.Count >= 4)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Severity", "warning");
                        }
                        else if (dataGridView1.Columns.Count == 1)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Message", "warning");
                        }
                        break;
                    case "notice":
                        if (dataGridView1.Columns.Count >= 4)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Severity", "notice");
                        }
                        else if (dataGridView1.Columns.Count == 1)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Message", "notice");
                        }
                        break;
                    case "info":
                        if (dataGridView1.Columns.Count >= 4)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Severity", "info");
                        }
                        else if (dataGridView1.Columns.Count == 1)
                        {
                            rowFilter += string.Format("[{0}] LIKE '{1}'", "Message", "info");
                        }
                        break;
                }
            }

            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = rowFilter;

            filtered = true;
            lblNumOfRow.Text = "Number of rows: " + dataGridView1.Rows.Count;
            btnFilter.Enabled = true;
        }

        private void QuickFilter(object sender, EventArgs e)
        {
            Label x = sender as Label;
            switch (x.Tag)
            {
                case "alert":
                    quickFilter = "alert";
                    lblColorRed.BackColor = Color.FromArgb(255, 128, 128);
                    lblColorYellow.BackColor = Color.Silver;
                    lblColorBlue.BackColor = Color.Silver;
                    lblColorGreen.BackColor = Color.Silver;
                    break;
                case "warning":
                    quickFilter = "warning";
                    lblColorRed.BackColor = Color.Silver;
                    lblColorYellow.BackColor = Color.FromArgb(255, 255, 182);
                    lblColorBlue.BackColor = Color.Silver;
                    lblColorGreen.BackColor = Color.Silver;
                    break;
                case "notice":
                    quickFilter = "notice";
                    lblColorRed.BackColor = Color.Silver;
                    lblColorYellow.BackColor = Color.Silver;
                    lblColorBlue.BackColor = Color.FromArgb(142, 255, 255);
                    lblColorGreen.BackColor = Color.Silver;
                    break;
                case "info":
                    quickFilter = "info";
                    lblColorRed.BackColor = Color.Silver;
                    lblColorYellow.BackColor = Color.Silver;
                    lblColorBlue.BackColor = Color.Silver;
                    lblColorGreen.BackColor = Color.FromArgb(192, 255, 182);
                    break;
            }
            btnFilter_Click(sender, e);
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count >= 4)
            {
                if (dataGridView1.Width == 1200)
                {
                    dataGridView1.Columns[3].Width = 680;
                }

                else
                {
                    dataGridView1.Columns[3].Width = (int)dataGridView1.Width - 520;
                }
            }
            else if (dataGridView1.Columns.Count == 1)
            {
                dataGridView1.Columns[0].Width = dataGridView1.Width;
            }

        }

    }
}
