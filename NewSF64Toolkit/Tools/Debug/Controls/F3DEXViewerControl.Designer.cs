namespace NewSF64Toolkit.Tools.Debug.Controls
{
    partial class F3DEXViewerControl
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
            this.pnlF3DEXControl = new System.Windows.Forms.Panel();
            this.btnLoadF3DEX = new System.Windows.Forms.Button();
            this.txtDMA = new System.Windows.Forms.TextBox();
            this.lblDMA = new System.Windows.Forms.Label();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.lblOffset = new System.Windows.Forms.Label();
            this.pnlF3DEXControl.SuspendLayout();
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
            // pnlF3DEXControl
            // 
            this.pnlF3DEXControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlF3DEXControl.Controls.Add(this.txtOffset);
            this.pnlF3DEXControl.Controls.Add(this.lblOffset);
            this.pnlF3DEXControl.Controls.Add(this.lblDMA);
            this.pnlF3DEXControl.Controls.Add(this.txtDMA);
            this.pnlF3DEXControl.Controls.Add(this.btnLoadF3DEX);
            this.pnlF3DEXControl.Location = new System.Drawing.Point(563, 3);
            this.pnlF3DEXControl.Name = "pnlF3DEXControl";
            this.pnlF3DEXControl.Size = new System.Drawing.Size(295, 393);
            this.pnlF3DEXControl.TabIndex = 35;
            // 
            // btnLoadF3DEX
            // 
            this.btnLoadF3DEX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnLoadF3DEX.Location = new System.Drawing.Point(106, 178);
            this.btnLoadF3DEX.Name = "btnLoadF3DEX";
            this.btnLoadF3DEX.Size = new System.Drawing.Size(91, 36);
            this.btnLoadF3DEX.TabIndex = 0;
            this.btnLoadF3DEX.Text = "Load";
            this.btnLoadF3DEX.UseVisualStyleBackColor = true;
            this.btnLoadF3DEX.Click += new System.EventHandler(this.btnLoadLevel_Click);
            // 
            // txtDMA
            // 
            this.txtDMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDMA.Location = new System.Drawing.Point(128, 118);
            this.txtDMA.Name = "txtDMA";
            this.txtDMA.Size = new System.Drawing.Size(63, 23);
            this.txtDMA.TabIndex = 9;
            // 
            // lblDMA
            // 
            this.lblDMA.AutoSize = true;
            this.lblDMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDMA.Location = new System.Drawing.Point(80, 121);
            this.lblDMA.Name = "lblDMA";
            this.lblDMA.Size = new System.Drawing.Size(42, 17);
            this.lblDMA.TabIndex = 8;
            this.lblDMA.Text = "DMA:";
            // 
            // txtOffset
            // 
            this.txtOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtOffset.Location = new System.Drawing.Point(128, 147);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(63, 23);
            this.txtOffset.TabIndex = 13;
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblOffset.Location = new System.Drawing.Point(72, 147);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(50, 17);
            this.lblOffset.TabIndex = 12;
            this.lblOffset.Text = "Offset:";
            // 
            // F3DEXViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlF3DEXControl);
            this.Controls.Add(this.glPanel);
            this.Name = "F3DEXViewerControl";
            this.Size = new System.Drawing.Size(861, 399);
            this.pnlF3DEXControl.ResumeLayout(false);
            this.pnlF3DEXControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel glPanel;
        private System.Windows.Forms.Panel pnlF3DEXControl;
        private System.Windows.Forms.Button btnLoadF3DEX;
        private System.Windows.Forms.TextBox txtDMA;
        private System.Windows.Forms.Label lblDMA;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Label lblOffset;
    }
}
