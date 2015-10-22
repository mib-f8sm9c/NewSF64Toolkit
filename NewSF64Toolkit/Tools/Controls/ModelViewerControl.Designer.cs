namespace NewSF64Toolkit.Tools.Controls
{
    partial class ModelViewerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glPanel = new System.Windows.Forms.Panel();
            this.pnlLevelControl = new System.Windows.Forms.Panel();
            this.pnlObjectInfo = new System.Windows.Forms.Panel();
            this.btnLoadLevel = new System.Windows.Forms.Button();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.lblModZ = new System.Windows.Forms.Label();
            this.txtDMA = new System.Windows.Forms.TextBox();
            this.lblDMA = new System.Windows.Forms.Label();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.lblBank = new System.Windows.Forms.Label();
            this.pnlLevelControl.SuspendLayout();
            this.pnlObjectInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // glPanel
            // 
            this.glPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glPanel.Location = new System.Drawing.Point(3, 3);
            this.glPanel.Name = "glPanel";
            this.glPanel.Size = new System.Drawing.Size(554, 393);
            this.glPanel.TabIndex = 0;
            // 
            // pnlLevelControl
            // 
            this.pnlLevelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLevelControl.Controls.Add(this.pnlObjectInfo);
            this.pnlLevelControl.Location = new System.Drawing.Point(563, 3);
            this.pnlLevelControl.Name = "pnlLevelControl";
            this.pnlLevelControl.Size = new System.Drawing.Size(295, 393);
            this.pnlLevelControl.TabIndex = 35;
            // 
            // pnlObjectInfo
            // 
            this.pnlObjectInfo.Controls.Add(this.btnLoadLevel);
            this.pnlObjectInfo.Controls.Add(this.txtOffset);
            this.pnlObjectInfo.Controls.Add(this.lblModZ);
            this.pnlObjectInfo.Controls.Add(this.txtDMA);
            this.pnlObjectInfo.Controls.Add(this.lblDMA);
            this.pnlObjectInfo.Controls.Add(this.txtBank);
            this.pnlObjectInfo.Controls.Add(this.lblBank);
            this.pnlObjectInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlObjectInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlObjectInfo.Name = "pnlObjectInfo";
            this.pnlObjectInfo.Size = new System.Drawing.Size(295, 393);
            this.pnlObjectInfo.TabIndex = 33;
            // 
            // btnLoadLevel
            // 
            this.btnLoadLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnLoadLevel.Location = new System.Drawing.Point(44, 113);
            this.btnLoadLevel.Name = "btnLoadLevel";
            this.btnLoadLevel.Size = new System.Drawing.Size(91, 36);
            this.btnLoadLevel.TabIndex = 0;
            this.btnLoadLevel.Text = "Load";
            this.btnLoadLevel.UseVisualStyleBackColor = true;
            this.btnLoadLevel.Click += new System.EventHandler(this.btnLoadLevel_Click);
            // 
            // txtOffset
            // 
            this.txtOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOffset.Location = new System.Drawing.Point(66, 72);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(63, 23);
            this.txtOffset.TabIndex = 17;
            // 
            // lblModZ
            // 
            this.lblModZ.AutoSize = true;
            this.lblModZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModZ.Location = new System.Drawing.Point(10, 75);
            this.lblModZ.Name = "lblModZ";
            this.lblModZ.Size = new System.Drawing.Size(50, 17);
            this.lblModZ.TabIndex = 16;
            this.lblModZ.Text = "Offset:";
            // 
            // txtDMA
            // 
            this.txtDMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDMA.Location = new System.Drawing.Point(66, 14);
            this.txtDMA.Name = "txtDMA";
            this.txtDMA.Size = new System.Drawing.Size(63, 23);
            this.txtDMA.TabIndex = 9;
            // 
            // lblDMA
            // 
            this.lblDMA.AutoSize = true;
            this.lblDMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDMA.Location = new System.Drawing.Point(20, 17);
            this.lblDMA.Name = "lblDMA";
            this.lblDMA.Size = new System.Drawing.Size(42, 17);
            this.lblDMA.TabIndex = 8;
            this.lblDMA.Text = "DMA:";
            // 
            // txtBank
            // 
            this.txtBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtBank.Location = new System.Drawing.Point(66, 43);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(63, 23);
            this.txtBank.TabIndex = 13;
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblBank.Location = new System.Drawing.Point(18, 46);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(44, 17);
            this.lblBank.TabIndex = 12;
            this.lblBank.Text = "Bank:";
            // 
            // ModelViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlLevelControl);
            this.Controls.Add(this.glPanel);
            this.Name = "ModelViewerControl";
            this.Size = new System.Drawing.Size(861, 399);
            this.pnlLevelControl.ResumeLayout(false);
            this.pnlObjectInfo.ResumeLayout(false);
            this.pnlObjectInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel glPanel;
        private System.Windows.Forms.Panel pnlLevelControl;
        private System.Windows.Forms.Panel pnlObjectInfo;
        private System.Windows.Forms.Button btnLoadLevel;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Label lblModZ;
        private System.Windows.Forms.TextBox txtDMA;
        private System.Windows.Forms.Label lblDMA;
        private System.Windows.Forms.TextBox txtBank;
        private System.Windows.Forms.Label lblBank;
    }
}
