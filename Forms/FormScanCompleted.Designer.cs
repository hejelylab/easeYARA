
namespace easeYARA.Forms
{
    partial class FormScanCompleted
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
            this.btnViewResults = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblScanning = new System.Windows.Forms.Label();
            this.btnReturnToMainScreen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnViewResults
            // 
            this.btnViewResults.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnViewResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnViewResults.FlatAppearance.BorderSize = 0;
            this.btnViewResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewResults.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnViewResults.ForeColor = System.Drawing.Color.White;
            this.btnViewResults.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewResults.Location = new System.Drawing.Point(483, 336);
            this.btnViewResults.Name = "btnViewResults";
            this.btnViewResults.Size = new System.Drawing.Size(369, 60);
            this.btnViewResults.TabIndex = 5;
            this.btnViewResults.Text = "View Results";
            this.btnViewResults.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnViewResults.UseVisualStyleBackColor = false;
            this.btnViewResults.Click += new System.EventHandler(this.btnViewResults_Click);
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
            // lblScanning
            // 
            this.lblScanning.AutoSize = true;
            this.lblScanning.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblScanning.ForeColor = System.Drawing.Color.White;
            this.lblScanning.Location = new System.Drawing.Point(17, 73);
            this.lblScanning.Name = "lblScanning";
            this.lblScanning.Size = new System.Drawing.Size(262, 45);
            this.lblScanning.TabIndex = 14;
            this.lblScanning.Text = "Scan Completed";
            // 
            // btnReturnToMainScreen
            // 
            this.btnReturnToMainScreen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReturnToMainScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnReturnToMainScreen.FlatAppearance.BorderSize = 0;
            this.btnReturnToMainScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnToMainScreen.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReturnToMainScreen.ForeColor = System.Drawing.Color.White;
            this.btnReturnToMainScreen.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReturnToMainScreen.Location = new System.Drawing.Point(483, 416);
            this.btnReturnToMainScreen.Name = "btnReturnToMainScreen";
            this.btnReturnToMainScreen.Size = new System.Drawing.Size(369, 60);
            this.btnReturnToMainScreen.TabIndex = 17;
            this.btnReturnToMainScreen.Text = "Return to Main Screen";
            this.btnReturnToMainScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReturnToMainScreen.UseVisualStyleBackColor = false;
            this.btnReturnToMainScreen.Click += new System.EventHandler(this.btnReturnToMainScreen_Click);
            // 
            // FormScanCompleted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1332, 750);
            this.Controls.Add(this.btnReturnToMainScreen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblScanning);
            this.Controls.Add(this.btnViewResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormScanCompleted";
            this.Text = "FormScanCompleted";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button btnViewResults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblScanning;
        private System.Windows.Forms.Button btnReturnToMainScreen;
    }
}