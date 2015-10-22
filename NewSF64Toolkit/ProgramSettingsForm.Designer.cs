namespace NewSF64Toolkit
{
    partial class ProgramSettingsForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.cbAutoCRC = new System.Windows.Forms.CheckBox();
            this.cbAutoDecompress = new System.Windows.Forms.CheckBox();
            this.cbShowDebug = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(263, 141);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbAutoCRC
            // 
            this.cbAutoCRC.AutoSize = true;
            this.cbAutoCRC.Location = new System.Drawing.Point(13, 27);
            this.cbAutoCRC.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoCRC.Name = "cbAutoCRC";
            this.cbAutoCRC.Size = new System.Drawing.Size(163, 21);
            this.cbAutoCRC.TabIndex = 1;
            this.cbAutoCRC.Text = "Automatically Fix CRC";
            this.cbAutoCRC.UseVisualStyleBackColor = true;
            // 
            // cbAutoDecompress
            // 
            this.cbAutoDecompress.AutoSize = true;
            this.cbAutoDecompress.Location = new System.Drawing.Point(13, 56);
            this.cbAutoDecompress.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoDecompress.Name = "cbAutoDecompress";
            this.cbAutoDecompress.Size = new System.Drawing.Size(193, 21);
            this.cbAutoDecompress.TabIndex = 2;
            this.cbAutoDecompress.Text = "Automatically Decompress";
            this.cbAutoDecompress.UseVisualStyleBackColor = true;
            // 
            // cbShowDebug
            // 
            this.cbShowDebug.AutoSize = true;
            this.cbShowDebug.Location = new System.Drawing.Point(13, 85);
            this.cbShowDebug.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowDebug.Name = "cbShowDebug";
            this.cbShowDebug.Size = new System.Drawing.Size(146, 21);
            this.cbShowDebug.TabIndex = 3;
            this.cbShowDebug.Text = "Show Debug Tools";
            this.cbShowDebug.UseVisualStyleBackColor = true;
            // 
            // ProgramSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 184);
            this.Controls.Add(this.cbShowDebug);
            this.Controls.Add(this.cbAutoDecompress);
            this.Controls.Add(this.cbAutoCRC);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProgramSettingsForm";
            this.Text = "ProgramSettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgramSettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox cbAutoCRC;
        private System.Windows.Forms.CheckBox cbAutoDecompress;
        private System.Windows.Forms.CheckBox cbShowDebug;
    }
}