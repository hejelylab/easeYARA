
namespace easeYARA.Forms
{
    partial class FormScanTarget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScanTarget));
            this.btnPrepareRemoteScan = new System.Windows.Forms.Button();
            this.btnLocalScan = new System.Windows.Forms.Button();
            this.lblChooseScannerPnl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPrepareRemoteScan
            // 
            this.btnPrepareRemoteScan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrepareRemoteScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnPrepareRemoteScan.FlatAppearance.BorderSize = 0;
            this.btnPrepareRemoteScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrepareRemoteScan.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPrepareRemoteScan.ForeColor = System.Drawing.Color.White;
            this.btnPrepareRemoteScan.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrepareRemoteScan.Location = new System.Drawing.Point(675, 309);
            this.btnPrepareRemoteScan.Name = "btnPrepareRemoteScan";
            this.btnPrepareRemoteScan.Size = new System.Drawing.Size(343, 60);
            this.btnPrepareRemoteScan.TabIndex = 5;
            this.btnPrepareRemoteScan.Text = "Prepare for Remote Scan";
            this.btnPrepareRemoteScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrepareRemoteScan.UseVisualStyleBackColor = false;
            this.btnPrepareRemoteScan.Click += new System.EventHandler(this.btnPrepareRemoteScan_Click);
            // 
            // btnLocalScan
            // 
            this.btnLocalScan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLocalScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnLocalScan.FlatAppearance.BorderSize = 0;
            this.btnLocalScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalScan.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLocalScan.ForeColor = System.Drawing.Color.White;
            this.btnLocalScan.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLocalScan.Location = new System.Drawing.Point(313, 309);
            this.btnLocalScan.Name = "btnLocalScan";
            this.btnLocalScan.Size = new System.Drawing.Size(343, 60);
            this.btnLocalScan.TabIndex = 4;
            this.btnLocalScan.Text = "Local Scan";
            this.btnLocalScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLocalScan.UseVisualStyleBackColor = false;
            this.btnLocalScan.Click += new System.EventHandler(this.btnLocalScan_Click);
            // 
            // lblChooseScannerPnl
            // 
            this.lblChooseScannerPnl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblChooseScannerPnl.AutoSize = true;
            this.lblChooseScannerPnl.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblChooseScannerPnl.ForeColor = System.Drawing.Color.White;
            this.lblChooseScannerPnl.Location = new System.Drawing.Point(313, 240);
            this.lblChooseScannerPnl.Name = "lblChooseScannerPnl";
            this.lblChooseScannerPnl.Size = new System.Drawing.Size(315, 32);
            this.lblChooseScannerPnl.TabIndex = 3;
            this.lblChooseScannerPnl.Text = "Select the target of the scan";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 45);
            this.label2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 45);
            this.label1.TabIndex = 14;
            this.label1.Text = "Scan Target";
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
            this.btnBack.TabIndex = 16;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // FormScanTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1332, 750);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrepareRemoteScan);
            this.Controls.Add(this.btnLocalScan);
            this.Controls.Add(this.lblChooseScannerPnl);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormScanTarget";
            this.Text = "FormScanTarget";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button btnPrepareRemoteScan;
        private System.Windows.Forms.Button btnLocalScan;
        private System.Windows.Forms.Label lblChooseScannerPnl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
    }
}