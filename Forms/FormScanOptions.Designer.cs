
namespace easeYARA.Forms
{
    partial class FormScanOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScanOptions));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.rbScanDrive = new System.Windows.Forms.RadioButton();
            this.rbScanSusDirectories = new System.Windows.Forms.RadioButton();
            this.chklstDrives = new System.Windows.Forms.CheckedListBox();
            this.chkbxAddExternalRules = new System.Windows.Forms.CheckBox();
            this.txtbxAddExternalRules = new System.Windows.Forms.TextBox();
            this.chkbxScanMemory = new System.Windows.Forms.CheckBox();
            this.chkbxUseLessCPU = new System.Windows.Forms.CheckBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseAddExternalRules = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.rbScanAllDrives = new System.Windows.Forms.RadioButton();
            this.lblComment1 = new System.Windows.Forms.Label();
            this.txtbxDrivesLetters = new System.Windows.Forms.TextBox();
            this.rchtxbxSusDirectories = new System.Windows.Forms.RichTextBox();
            this.lblComment2 = new System.Windows.Forms.Label();
            this.lblEdit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 45);
            this.label4.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 45);
            this.label3.TabIndex = 15;
            this.label3.Text = "Scan Options";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(12, 701);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(91, 37);
            this.btnBack.TabIndex = 14;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // rbScanDrive
            // 
            this.rbScanDrive.AutoSize = true;
            this.rbScanDrive.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbScanDrive.Location = new System.Drawing.Point(155, 221);
            this.rbScanDrive.Name = "rbScanDrive";
            this.rbScanDrive.Size = new System.Drawing.Size(129, 34);
            this.rbScanDrive.TabIndex = 17;
            this.rbScanDrive.TabStop = true;
            this.rbScanDrive.Text = "Scan Drive";
            this.rbScanDrive.UseVisualStyleBackColor = true;
            this.rbScanDrive.CheckedChanged += new System.EventHandler(this.rbScanDrive_CheckedChanged);
            // 
            // rbScanSusDirectories
            // 
            this.rbScanSusDirectories.AutoSize = true;
            this.rbScanSusDirectories.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbScanSusDirectories.Location = new System.Drawing.Point(155, 326);
            this.rbScanSusDirectories.Name = "rbScanSusDirectories";
            this.rbScanSusDirectories.Size = new System.Drawing.Size(283, 34);
            this.rbScanSusDirectories.TabIndex = 18;
            this.rbScanSusDirectories.TabStop = true;
            this.rbScanSusDirectories.Text = "Scan Suspicious Directories";
            this.rbScanSusDirectories.UseVisualStyleBackColor = true;
            this.rbScanSusDirectories.CheckedChanged += new System.EventHandler(this.rbScanSusDirectories_CheckedChanged);
            // 
            // chklstDrives
            // 
            this.chklstDrives.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.chklstDrives.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chklstDrives.ColumnWidth = 50;
            this.chklstDrives.Enabled = false;
            this.chklstDrives.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chklstDrives.ForeColor = System.Drawing.SystemColors.Control;
            this.chklstDrives.FormattingEnabled = true;
            this.chklstDrives.Location = new System.Drawing.Point(176, 261);
            this.chklstDrives.MultiColumn = true;
            this.chklstDrives.Name = "chklstDrives";
            this.chklstDrives.Size = new System.Drawing.Size(637, 28);
            this.chklstDrives.TabIndex = 19;
            this.chklstDrives.Visible = false;
            // 
            // chkbxAddExternalRules
            // 
            this.chkbxAddExternalRules.AutoSize = true;
            this.chkbxAddExternalRules.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkbxAddExternalRules.Location = new System.Drawing.Point(155, 522);
            this.chkbxAddExternalRules.Name = "chkbxAddExternalRules";
            this.chkbxAddExternalRules.Size = new System.Drawing.Size(188, 29);
            this.chkbxAddExternalRules.TabIndex = 23;
            this.chkbxAddExternalRules.Text = "Add External Rules";
            this.chkbxAddExternalRules.UseVisualStyleBackColor = true;
            this.chkbxAddExternalRules.CheckedChanged += new System.EventHandler(this.chkbxAddExternalRules_CheckedChanged);
            // 
            // txtbxAddExternalRules
            // 
            this.txtbxAddExternalRules.Enabled = false;
            this.txtbxAddExternalRules.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbxAddExternalRules.Location = new System.Drawing.Point(176, 557);
            this.txtbxAddExternalRules.Name = "txtbxAddExternalRules";
            this.txtbxAddExternalRules.Size = new System.Drawing.Size(509, 27);
            this.txtbxAddExternalRules.TabIndex = 24;
            // 
            // chkbxScanMemory
            // 
            this.chkbxScanMemory.AutoSize = true;
            this.chkbxScanMemory.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkbxScanMemory.Location = new System.Drawing.Point(155, 601);
            this.chkbxScanMemory.Name = "chkbxScanMemory";
            this.chkbxScanMemory.Size = new System.Drawing.Size(146, 29);
            this.chkbxScanMemory.TabIndex = 26;
            this.chkbxScanMemory.Text = "Scan Memory";
            this.chkbxScanMemory.UseVisualStyleBackColor = true;
            this.chkbxScanMemory.CheckedChanged += new System.EventHandler(this.chkbxScanMemory_CheckedChanged);
            // 
            // chkbxUseLessCPU
            // 
            this.chkbxUseLessCPU.AutoSize = true;
            this.chkbxUseLessCPU.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkbxUseLessCPU.Location = new System.Drawing.Point(155, 636);
            this.chkbxUseLessCPU.Name = "chkbxUseLessCPU";
            this.chkbxUseLessCPU.Size = new System.Drawing.Size(253, 29);
            this.chkbxUseLessCPU.TabIndex = 27;
            this.chkbxUseLessCPU.Text = "Use Less Than 50% of CPU";
            this.chkbxUseLessCPU.UseVisualStyleBackColor = true;
            this.chkbxUseLessCPU.CheckedChanged += new System.EventHandler(this.chkbxUseLessCPU_CheckedChanged);
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnScan.FlatAppearance.BorderSize = 0;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnScan.Location = new System.Drawing.Point(1058, 682);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(242, 39);
            this.btnScan.TabIndex = 28;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(104, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 37);
            this.label1.TabIndex = 29;
            this.label1.Text = "Required";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(104, 473);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 37);
            this.label2.TabIndex = 30;
            this.label2.Text = "Optional";
            // 
            // btnBrowseAddExternalRules
            // 
            this.btnBrowseAddExternalRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(66)))));
            this.btnBrowseAddExternalRules.Enabled = false;
            this.btnBrowseAddExternalRules.FlatAppearance.BorderSize = 0;
            this.btnBrowseAddExternalRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseAddExternalRules.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseAddExternalRules.ForeColor = System.Drawing.Color.Gray;
            this.btnBrowseAddExternalRules.Location = new System.Drawing.Point(691, 557);
            this.btnBrowseAddExternalRules.Name = "btnBrowseAddExternalRules";
            this.btnBrowseAddExternalRules.Size = new System.Drawing.Size(122, 27);
            this.btnBrowseAddExternalRules.TabIndex = 31;
            this.btnBrowseAddExternalRules.Text = "Browse";
            this.btnBrowseAddExternalRules.UseVisualStyleBackColor = false;
            this.btnBrowseAddExternalRules.EnabledChanged += new System.EventHandler(this.btnBrowseAddRules_EnabledChanged);
            this.btnBrowseAddExternalRules.Click += new System.EventHandler(this.btnBrowseAddExternalRules_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rbScanAllDrives
            // 
            this.rbScanAllDrives.AutoSize = true;
            this.rbScanAllDrives.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbScanAllDrives.Location = new System.Drawing.Point(155, 181);
            this.rbScanAllDrives.Name = "rbScanAllDrives";
            this.rbScanAllDrives.Size = new System.Drawing.Size(168, 34);
            this.rbScanAllDrives.TabIndex = 36;
            this.rbScanAllDrives.TabStop = true;
            this.rbScanAllDrives.Text = "Scan All Drives";
            this.rbScanAllDrives.UseVisualStyleBackColor = true;
            this.rbScanAllDrives.CheckedChanged += new System.EventHandler(this.rbScanAllDrives_CheckedChanged);
            // 
            // lblComment1
            // 
            this.lblComment1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblComment1.Location = new System.Drawing.Point(176, 292);
            this.lblComment1.Name = "lblComment1";
            this.lblComment1.Size = new System.Drawing.Size(339, 37);
            this.lblComment1.TabIndex = 38;
            this.lblComment1.Text = "Write the letters of the drives to scan, seperated by comma \',\' EX: C, D";
            this.lblComment1.Visible = false;
            // 
            // txtbxDrivesLetters
            // 
            this.txtbxDrivesLetters.Enabled = false;
            this.txtbxDrivesLetters.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbxDrivesLetters.Location = new System.Drawing.Point(176, 262);
            this.txtbxDrivesLetters.Name = "txtbxDrivesLetters";
            this.txtbxDrivesLetters.Size = new System.Drawing.Size(637, 27);
            this.txtbxDrivesLetters.TabIndex = 37;
            this.txtbxDrivesLetters.Visible = false;
            // 
            // rchtxbxSusDirectories
            // 
            this.rchtxbxSusDirectories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rchtxbxSusDirectories.Enabled = false;
            this.rchtxbxSusDirectories.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rchtxbxSusDirectories.Location = new System.Drawing.Point(176, 366);
            this.rchtxbxSusDirectories.Name = "rchtxbxSusDirectories";
            this.rchtxbxSusDirectories.Size = new System.Drawing.Size(637, 78);
            this.rchtxbxSusDirectories.TabIndex = 39;
            this.rchtxbxSusDirectories.Text = "";
            // 
            // lblComment2
            // 
            this.lblComment2.AutoSize = true;
            this.lblComment2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblComment2.Location = new System.Drawing.Point(176, 447);
            this.lblComment2.Name = "lblComment2";
            this.lblComment2.Size = new System.Drawing.Size(441, 15);
            this.lblComment2.TabIndex = 40;
            this.lblComment2.Text = "You can edit the directories you want to scan by writing thier full path, one per" +
    " line";
            // 
            // lblEdit
            // 
            this.lblEdit.AutoSize = true;
            this.lblEdit.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.lblEdit.Location = new System.Drawing.Point(775, 340);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(35, 20);
            this.lblEdit.TabIndex = 41;
            this.lblEdit.Text = "Edit";
            this.lblEdit.Visible = false;
            this.lblEdit.Click += new System.EventHandler(this.lblEdit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(176, 661);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "This will download process-governor application";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(176, 587);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 15);
            this.label6.TabIndex = 43;
            this.label6.Text = "Accepts password protected ZIP files (.zip)";
            // 
            // FormScanOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1332, 750);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblEdit);
            this.Controls.Add(this.lblComment2);
            this.Controls.Add(this.lblComment1);
            this.Controls.Add(this.rbScanAllDrives);
            this.Controls.Add(this.btnBrowseAddExternalRules);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.chkbxUseLessCPU);
            this.Controls.Add(this.chkbxScanMemory);
            this.Controls.Add(this.txtbxAddExternalRules);
            this.Controls.Add(this.chkbxAddExternalRules);
            this.Controls.Add(this.rbScanSusDirectories);
            this.Controls.Add(this.rbScanDrive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.rchtxbxSusDirectories);
            this.Controls.Add(this.txtbxDrivesLetters);
            this.Controls.Add(this.chklstDrives);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormScanOptions";
            this.Text = "FormScanOptions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.RadioButton rbScanDrive;
        private System.Windows.Forms.RadioButton rbScanSusDirectories;
        private System.Windows.Forms.CheckedListBox chklstDrives;
        private System.Windows.Forms.CheckBox chkbxAddExternalRules;
        private System.Windows.Forms.TextBox txtbxAddExternalRules;
        private System.Windows.Forms.CheckBox chkbxScanMemory;
        private System.Windows.Forms.CheckBox chkbxUseLessCPU;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseAddExternalRules;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton rbScanAllDrives;
        private System.Windows.Forms.Label lblComment1;
        private System.Windows.Forms.TextBox txtbxDrivesLetters;
        private System.Windows.Forms.RichTextBox rchtxbxSusDirectories;
        private System.Windows.Forms.Label lblComment2;
        private System.Windows.Forms.Label lblEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}