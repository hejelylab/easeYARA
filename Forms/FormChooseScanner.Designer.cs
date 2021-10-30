
namespace easeYARA.Forms
{
    partial class FormChooseScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChooseScanner));
            this.btnLokiScanner = new System.Windows.Forms.Button();
            this.btnYaraScanner = new System.Windows.Forms.Button();
            this.lblChooseScannerPnl = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnHaveScanner = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLokiScanner
            // 
            this.btnLokiScanner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLokiScanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnLokiScanner.FlatAppearance.BorderSize = 0;
            this.btnLokiScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLokiScanner.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLokiScanner.ForeColor = System.Drawing.Color.White;
            this.btnLokiScanner.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLokiScanner.Location = new System.Drawing.Point(675, 309);
            this.btnLokiScanner.Name = "btnLokiScanner";
            this.btnLokiScanner.Size = new System.Drawing.Size(343, 60);
            this.btnLokiScanner.TabIndex = 5;
            this.btnLokiScanner.Text = "Loki Scanner";
            this.btnLokiScanner.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLokiScanner.UseVisualStyleBackColor = false;
            this.btnLokiScanner.Click += new System.EventHandler(this.btnLokiScanner_Click);
            // 
            // btnYaraScanner
            // 
            this.btnYaraScanner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnYaraScanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnYaraScanner.FlatAppearance.BorderSize = 0;
            this.btnYaraScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYaraScanner.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnYaraScanner.ForeColor = System.Drawing.Color.White;
            this.btnYaraScanner.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnYaraScanner.Location = new System.Drawing.Point(313, 309);
            this.btnYaraScanner.Name = "btnYaraScanner";
            this.btnYaraScanner.Size = new System.Drawing.Size(343, 60);
            this.btnYaraScanner.TabIndex = 4;
            this.btnYaraScanner.Text = "YARA Scanner";
            this.btnYaraScanner.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnYaraScanner.UseVisualStyleBackColor = false;
            this.btnYaraScanner.Click += new System.EventHandler(this.btnYaraScanner_Click);
            // 
            // lblChooseScannerPnl
            // 
            this.lblChooseScannerPnl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblChooseScannerPnl.AutoSize = true;
            this.lblChooseScannerPnl.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblChooseScannerPnl.ForeColor = System.Drawing.Color.White;
            this.lblChooseScannerPnl.Location = new System.Drawing.Point(313, 240);
            this.lblChooseScannerPnl.Name = "lblChooseScannerPnl";
            this.lblChooseScannerPnl.Size = new System.Drawing.Size(445, 32);
            this.lblChooseScannerPnl.TabIndex = 3;
            this.lblChooseScannerPnl.Text = "Choose your preferred Scanner to install";
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
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnHaveScanner
            // 
            this.btnHaveScanner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHaveScanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.btnHaveScanner.FlatAppearance.BorderSize = 0;
            this.btnHaveScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHaveScanner.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.btnHaveScanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnHaveScanner.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHaveScanner.Location = new System.Drawing.Point(798, 418);
            this.btnHaveScanner.Name = "btnHaveScanner";
            this.btnHaveScanner.Size = new System.Drawing.Size(220, 34);
            this.btnHaveScanner.TabIndex = 7;
            this.btnHaveScanner.Text = "Already have a scanner";
            this.btnHaveScanner.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHaveScanner.UseVisualStyleBackColor = false;
            this.btnHaveScanner.Click += new System.EventHandler(this.btnHaveScanner_Click);
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
            this.label1.Size = new System.Drawing.Size(244, 45);
            this.label1.TabIndex = 14;
            this.label1.Text = "Select Scanner ";
            // 
            // FormChooseScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1332, 750);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHaveScanner);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLokiScanner);
            this.Controls.Add(this.btnYaraScanner);
            this.Controls.Add(this.lblChooseScannerPnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChooseScanner";
            this.Text = "FormChooseScanner";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Button btnLokiScanner;
        private System.Windows.Forms.Button btnYaraScanner;
        private System.Windows.Forms.Label lblChooseScannerPnl;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnHaveScanner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}