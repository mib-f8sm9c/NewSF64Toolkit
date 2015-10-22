namespace NewSF64Toolkit
{
    partial class MainForm
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuStripFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripFileLoadDMA = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripFileSaveDMA = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripFileRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripROM = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripROMFixCRCs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripROMDecompress = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripViewHex = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripViewWireframe = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripSettingsSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripToolsInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripToolsHex = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripToolsLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripToolsF3DEX = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlCurrentTool = new System.Windows.Forms.Panel();
            this.panelToolButtons = new System.Windows.Forms.Panel();
            this.btnF3DEX = new System.Windows.Forms.Button();
            this.btnLevel = new System.Windows.Forms.Button();
            this.btnHex = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnResource = new System.Windows.Forms.Button();
            this.menuStripToolsResource = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelToolButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 606);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1121, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // tsStatus
            // 
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripFile,
            this.menuStripROM,
            this.menuStripSettings,
            this.menuStripTools});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1121, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip2";
            // 
            // menuStripFile
            // 
            this.menuStripFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripFileLoad,
            this.menuStripFileSave,
            this.toolStripSeparator1,
            this.menuStripFileLoadDMA,
            this.menuStripFileSaveDMA,
            this.toolStripSeparator2,
            this.menuStripFileRecent,
            this.toolStripSeparator3,
            this.menuStripFileExit});
            this.menuStripFile.Name = "menuStripFile";
            this.menuStripFile.Size = new System.Drawing.Size(37, 20);
            this.menuStripFile.Text = "File";
            // 
            // menuStripFileLoad
            // 
            this.menuStripFileLoad.Name = "menuStripFileLoad";
            this.menuStripFileLoad.Size = new System.Drawing.Size(173, 22);
            this.menuStripFileLoad.Text = "Load ROM...";
            this.menuStripFileLoad.Click += new System.EventHandler(this.menuStripFileLoad_Click);
            // 
            // menuStripFileSave
            // 
            this.menuStripFileSave.Name = "menuStripFileSave";
            this.menuStripFileSave.Size = new System.Drawing.Size(173, 22);
            this.menuStripFileSave.Text = "Save ROM...";
            this.menuStripFileSave.Click += new System.EventHandler(this.menuStripFileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // menuStripFileLoadDMA
            // 
            this.menuStripFileLoadDMA.Enabled = false;
            this.menuStripFileLoadDMA.Name = "menuStripFileLoadDMA";
            this.menuStripFileLoadDMA.Size = new System.Drawing.Size(173, 22);
            this.menuStripFileLoadDMA.Text = "Load DMA...";
            this.menuStripFileLoadDMA.Click += new System.EventHandler(this.menuStripFileLoadDMA_Click);
            // 
            // menuStripFileSaveDMA
            // 
            this.menuStripFileSaveDMA.Enabled = false;
            this.menuStripFileSaveDMA.Name = "menuStripFileSaveDMA";
            this.menuStripFileSaveDMA.Size = new System.Drawing.Size(173, 22);
            this.menuStripFileSaveDMA.Text = "Save DMA...";
            this.menuStripFileSaveDMA.Click += new System.EventHandler(this.menuStripFileSaveDMA_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // menuStripFileRecent
            // 
            this.menuStripFileRecent.Name = "menuStripFileRecent";
            this.menuStripFileRecent.Size = new System.Drawing.Size(173, 22);
            this.menuStripFileRecent.Text = "Recently Opened...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
            // 
            // menuStripFileExit
            // 
            this.menuStripFileExit.Name = "menuStripFileExit";
            this.menuStripFileExit.Size = new System.Drawing.Size(173, 22);
            this.menuStripFileExit.Text = "Exit";
            this.menuStripFileExit.Click += new System.EventHandler(this.menuStripFileExit_Click);
            // 
            // menuStripROM
            // 
            this.menuStripROM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripROMFixCRCs,
            this.menuStripROMDecompress});
            this.menuStripROM.Name = "menuStripROM";
            this.menuStripROM.Size = new System.Drawing.Size(46, 20);
            this.menuStripROM.Text = "ROM";
            // 
            // menuStripROMFixCRCs
            // 
            this.menuStripROMFixCRCs.Name = "menuStripROMFixCRCs";
            this.menuStripROMFixCRCs.Size = new System.Drawing.Size(139, 22);
            this.menuStripROMFixCRCs.Text = "Fix CRCs";
            this.menuStripROMFixCRCs.Click += new System.EventHandler(this.menuStripROMFixCRCs_Click);
            // 
            // menuStripROMDecompress
            // 
            this.menuStripROMDecompress.Name = "menuStripROMDecompress";
            this.menuStripROMDecompress.Size = new System.Drawing.Size(139, 22);
            this.menuStripROMDecompress.Text = "Decompress";
            this.menuStripROMDecompress.Click += new System.EventHandler(this.menuStripROMDecompress_Click);
            // 
            // menuStripSettings
            // 
            this.menuStripSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripViewHex,
            this.menuStripViewWireframe,
            this.toolStripSeparator4,
            this.menuStripSettingsSettings});
            this.menuStripSettings.Name = "menuStripSettings";
            this.menuStripSettings.Size = new System.Drawing.Size(61, 20);
            this.menuStripSettings.Text = "Settings";
            // 
            // menuStripViewHex
            // 
            this.menuStripViewHex.Checked = true;
            this.menuStripViewHex.CheckOnClick = true;
            this.menuStripViewHex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuStripViewHex.Name = "menuStripViewHex";
            this.menuStripViewHex.Size = new System.Drawing.Size(174, 22);
            this.menuStripViewHex.Text = "Display in Hex";
            this.menuStripViewHex.Click += new System.EventHandler(this.menuStripViewHex_Click);
            // 
            // menuStripViewWireframe
            // 
            this.menuStripViewWireframe.CheckOnClick = true;
            this.menuStripViewWireframe.Name = "menuStripViewWireframe";
            this.menuStripViewWireframe.Size = new System.Drawing.Size(174, 22);
            this.menuStripViewWireframe.Text = "Wireframe Mode";
            this.menuStripViewWireframe.Click += new System.EventHandler(this.menuStripViewWireframe_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(171, 6);
            // 
            // menuStripSettingsSettings
            // 
            this.menuStripSettingsSettings.Name = "menuStripSettingsSettings";
            this.menuStripSettingsSettings.Size = new System.Drawing.Size(174, 22);
            this.menuStripSettingsSettings.Text = "Program Settings...";
            this.menuStripSettingsSettings.Click += new System.EventHandler(this.menuStripSettingsSettings_Click);
            // 
            // menuStripTools
            // 
            this.menuStripTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripToolsInfo,
            this.menuStripToolsHex,
            this.menuStripToolsLevel,
            this.menuStripToolsSep1,
            this.menuStripToolsF3DEX,
            this.menuStripToolsResource});
            this.menuStripTools.Name = "menuStripTools";
            this.menuStripTools.Size = new System.Drawing.Size(48, 20);
            this.menuStripTools.Text = "Tools";
            // 
            // menuStripToolsInfo
            // 
            this.menuStripToolsInfo.Name = "menuStripToolsInfo";
            this.menuStripToolsInfo.Size = new System.Drawing.Size(160, 22);
            this.menuStripToolsInfo.Text = "Rom Info";
            this.menuStripToolsInfo.Click += new System.EventHandler(this.menuStripToolsInfo_Click);
            // 
            // menuStripToolsHex
            // 
            this.menuStripToolsHex.Name = "menuStripToolsHex";
            this.menuStripToolsHex.Size = new System.Drawing.Size(160, 22);
            this.menuStripToolsHex.Text = "Hex Editor";
            this.menuStripToolsHex.Click += new System.EventHandler(this.menuStripToolsHex_Click);
            // 
            // menuStripToolsLevel
            // 
            this.menuStripToolsLevel.Name = "menuStripToolsLevel";
            this.menuStripToolsLevel.Size = new System.Drawing.Size(160, 22);
            this.menuStripToolsLevel.Text = "Level Viewer";
            this.menuStripToolsLevel.Click += new System.EventHandler(this.menuStripToolsLevel_Click);
            // 
            // menuStripToolsSep1
            // 
            this.menuStripToolsSep1.Name = "menuStripToolsSep1";
            this.menuStripToolsSep1.Size = new System.Drawing.Size(157, 6);
            this.menuStripToolsSep1.Visible = false;
            // 
            // menuStripToolsF3DEX
            // 
            this.menuStripToolsF3DEX.Name = "menuStripToolsF3DEX";
            this.menuStripToolsF3DEX.Size = new System.Drawing.Size(160, 22);
            this.menuStripToolsF3DEX.Text = "F3DEX Viewer";
            this.menuStripToolsF3DEX.Visible = false;
            this.menuStripToolsF3DEX.Click += new System.EventHandler(this.menuStripToolsF3DEX_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "sf64.z64";
            this.openFileDialog.Filter = "Rom files|*.n64;*.z64;*.rom|All files|*.*";
            // 
            // pnlCurrentTool
            // 
            this.pnlCurrentTool.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCurrentTool.Location = new System.Drawing.Point(0, 83);
            this.pnlCurrentTool.Name = "pnlCurrentTool";
            this.pnlCurrentTool.Size = new System.Drawing.Size(1121, 523);
            this.pnlCurrentTool.TabIndex = 3;
            // 
            // panelToolButtons
            // 
            this.panelToolButtons.Controls.Add(this.btnResource);
            this.panelToolButtons.Controls.Add(this.btnF3DEX);
            this.panelToolButtons.Controls.Add(this.btnLevel);
            this.panelToolButtons.Controls.Add(this.btnHex);
            this.panelToolButtons.Controls.Add(this.btnInfo);
            this.panelToolButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolButtons.Location = new System.Drawing.Point(0, 24);
            this.panelToolButtons.Name = "panelToolButtons";
            this.panelToolButtons.Size = new System.Drawing.Size(1121, 57);
            this.panelToolButtons.TabIndex = 4;
            // 
            // btnF3DEX
            // 
            this.btnF3DEX.Location = new System.Drawing.Point(238, 4);
            this.btnF3DEX.Name = "btnF3DEX";
            this.btnF3DEX.Size = new System.Drawing.Size(56, 50);
            this.btnF3DEX.TabIndex = 3;
            this.btnF3DEX.Text = "F3DEX";
            this.btnF3DEX.UseVisualStyleBackColor = true;
            this.btnF3DEX.Visible = false;
            this.btnF3DEX.Click += new System.EventHandler(this.btnF3DEX_Click);
            // 
            // btnLevel
            // 
            this.btnLevel.Location = new System.Drawing.Point(162, 4);
            this.btnLevel.Name = "btnLevel";
            this.btnLevel.Size = new System.Drawing.Size(56, 50);
            this.btnLevel.TabIndex = 2;
            this.btnLevel.Text = "Level";
            this.btnLevel.UseVisualStyleBackColor = true;
            this.btnLevel.Click += new System.EventHandler(this.btnLevel_Click);
            // 
            // btnHex
            // 
            this.btnHex.Location = new System.Drawing.Point(85, 3);
            this.btnHex.Name = "btnHex";
            this.btnHex.Size = new System.Drawing.Size(56, 50);
            this.btnHex.TabIndex = 1;
            this.btnHex.Text = "Hex";
            this.btnHex.UseVisualStyleBackColor = true;
            this.btnHex.Click += new System.EventHandler(this.btnHex_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(12, 3);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(56, 50);
            this.btnInfo.TabIndex = 0;
            this.btnInfo.Text = "Info";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnResource
            // 
            this.btnResource.Location = new System.Drawing.Point(315, 4);
            this.btnResource.Name = "btnResource";
            this.btnResource.Size = new System.Drawing.Size(61, 50);
            this.btnResource.TabIndex = 4;
            this.btnResource.Text = "Resource";
            this.btnResource.UseVisualStyleBackColor = true;
            this.btnResource.Visible = false;
            this.btnResource.Click += new System.EventHandler(this.btnResource_Click);
            // 
            // menuStripToolsResource
            // 
            this.menuStripToolsResource.Name = "menuStripToolsResource";
            this.menuStripToolsResource.Size = new System.Drawing.Size(160, 22);
            this.menuStripToolsResource.Text = "Resource Viewer";
            this.menuStripToolsResource.Click += new System.EventHandler(this.menuStripToolsResource_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 628);
            this.Controls.Add(this.panelToolButtons);
            this.Controls.Add(this.pnlCurrentTool);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Name = "MainForm";
            this.Text = "NewSF64Toolkit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelToolButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuStripFile;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileLoad;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem menuStripSettings;
        private System.Windows.Forms.ToolStripMenuItem menuStripViewHex;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileLoadDMA;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileSaveDMA;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem menuStripROM;
        private System.Windows.Forms.ToolStripMenuItem menuStripROMFixCRCs;
        private System.Windows.Forms.ToolStripMenuItem menuStripROMDecompress;
        private System.Windows.Forms.ToolStripMenuItem menuStripViewWireframe;
        private System.Windows.Forms.Panel pnlCurrentTool;
        private System.Windows.Forms.ToolStripMenuItem menuStripTools;
        private System.Windows.Forms.ToolStripMenuItem menuStripToolsInfo;
        private System.Windows.Forms.ToolStripMenuItem menuStripToolsHex;
        private System.Windows.Forms.ToolStripMenuItem menuStripToolsLevel;
        private System.Windows.Forms.ToolStripMenuItem menuStripFileRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel panelToolButtons;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnLevel;
        private System.Windows.Forms.Button btnHex;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuStripSettingsSettings;
        private System.Windows.Forms.Button btnF3DEX;
        private System.Windows.Forms.ToolStripMenuItem menuStripToolsF3DEX;
        private System.Windows.Forms.ToolStripSeparator menuStripToolsSep1;
        private System.Windows.Forms.Button btnResource;
        private System.Windows.Forms.ToolStripMenuItem menuStripToolsResource;
    }
}

