namespace NewSF64Toolkit
{
    partial class OpenGLControl
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
            this.glDisplay = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // glDisplay
            // 
            this.glDisplay.BackColor = System.Drawing.Color.Black;
            this.glDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glDisplay.Location = new System.Drawing.Point(0, 0);
            this.glDisplay.Name = "glDisplay";
            this.glDisplay.Size = new System.Drawing.Size(444, 279);
            this.glDisplay.TabIndex = 1;
            this.glDisplay.VSync = true;
            this.glDisplay.Load += new System.EventHandler(this.glDisplay_Load);
            this.glDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.glDisplay_Paint);
            this.glDisplay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glDisplay_KeyDown);
            this.glDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glDisplay_MouseDown);
            this.glDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glDisplay_MouseMove);
            this.glDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glDisplay_MouseUp);
            // 
            // OpenGLControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.glDisplay);
            this.Name = "OpenGLControl";
            this.Size = new System.Drawing.Size(444, 279);
            this.Resize += new System.EventHandler(this.OpenGLControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glDisplay;
    }
}
