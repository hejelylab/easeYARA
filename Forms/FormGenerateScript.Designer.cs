
namespace easeYARA.Forms
{
    partial class FormGenerateScript
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGenerateScript));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnGenerateBatchScript = new System.Windows.Forms.Button();
            this.btnReturnToMainScreen = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblCopyToClipboard = new System.Windows.Forms.Label();
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
            this.label3.Size = new System.Drawing.Size(265, 45);
            this.label3.TabIndex = 15;
            this.label3.Text = "Generate Scripts";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
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
            this.btnBack.TabIndex = 17;
            this.btnBack.Text = "Back";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnGenerateBatchScript
            // 
            this.btnGenerateBatchScript.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGenerateBatchScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnGenerateBatchScript.FlatAppearance.BorderSize = 0;
            this.btnGenerateBatchScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateBatchScript.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGenerateBatchScript.Location = new System.Drawing.Point(868, 278);
            this.btnGenerateBatchScript.Name = "btnGenerateBatchScript";
            this.btnGenerateBatchScript.Size = new System.Drawing.Size(341, 47);
            this.btnGenerateBatchScript.TabIndex = 29;
            this.btnGenerateBatchScript.Text = "Generate Batch Script";
            this.btnGenerateBatchScript.UseVisualStyleBackColor = false;
            this.btnGenerateBatchScript.Click += new System.EventHandler(this.btnGenerateBatchScript_Click);
            // 
            // btnReturnToMainScreen
            // 
            this.btnReturnToMainScreen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReturnToMainScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.btnReturnToMainScreen.FlatAppearance.BorderSize = 0;
            this.btnReturnToMainScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnToMainScreen.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReturnToMainScreen.Location = new System.Drawing.Point(868, 331);
            this.btnReturnToMainScreen.Name = "btnReturnToMainScreen";
            this.btnReturnToMainScreen.Size = new System.Drawing.Size(341, 47);
            this.btnReturnToMainScreen.TabIndex = 31;
            this.btnReturnToMainScreen.Text = "Return to Main Screen";
            this.btnReturnToMainScreen.UseVisualStyleBackColor = false;
            this.btnReturnToMainScreen.Click += new System.EventHandler(this.btnReturnToMainScreen_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.Location = new System.Drawing.Point(118, 278);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(744, 100);
            this.richTextBox1.TabIndex = 43;
            this.richTextBox1.Text = "";
            // 
            // lblCopyToClipboard
            // 
            this.lblCopyToClipboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCopyToClipboard.AutoSize = true;
            this.lblCopyToClipboard.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCopyToClipboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(140)))), ((int)(((byte)(224)))));
            this.lblCopyToClipboard.Location = new System.Drawing.Point(118, 381);
            this.lblCopyToClipboard.Name = "lblCopyToClipboard";
            this.lblCopyToClipboard.Size = new System.Drawing.Size(136, 21);
            this.lblCopyToClipboard.TabIndex = 44;
            this.lblCopyToClipboard.Tag = "1";
            this.lblCopyToClipboard.Text = "Copy to Clipboard";
            this.lblCopyToClipboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCopyToClipboard.Click += new System.EventHandler(this.lblCopyToClipboard_Click);
            // 
            // FormGenerateScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(44)))), ((int)(((byte)(52)))));
            this.ClientSize = new System.Drawing.Size(1332, 750);
            this.Controls.Add(this.lblCopyToClipboard);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnReturnToMainScreen);
            this.Controls.Add(this.btnGenerateBatchScript);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormGenerateScript";
            this.Text = "FormGenerateScript";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnGenerateBatchScript;
        private System.Windows.Forms.Button btnReturnToMainScreen;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblCopyToClipboard;
    }
}