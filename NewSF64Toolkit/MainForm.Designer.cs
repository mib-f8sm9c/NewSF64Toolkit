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
            this.menuStripFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripROM = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripROMFixCRCs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripROMDecompress = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripViewHex = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.viewerPanel = new System.Windows.Forms.Panel();
            this.glPanel = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.romInfoPage = new System.Windows.Forms.TabPage();
            this.txtCRC2 = new System.Windows.Forms.TextBox();
            this.txtCRC1 = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtGameID = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.lblCRC2 = new System.Windows.Forms.Label();
            this.lblCRC1 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblGameID = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblFilename = new System.Windows.Forms.Label();
            this.dmaTablesPage = new System.Windows.Forms.TabPage();
            this.pnlDMATables = new System.Windows.Forms.Panel();
            this.dgvDMA = new System.Windows.Forms.DataGridView();
            this.colNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelViewerPage = new System.Windows.Forms.TabPage();
            this.pnlListing = new System.Windows.Forms.Panel();
            this.tvLevelInfo = new System.Windows.Forms.TreeView();
            this.pnlObjectInfo = new System.Windows.Forms.Panel();
            this.txtModID = new System.Windows.Forms.TextBox();
            this.btnModSnapTo = new System.Windows.Forms.Button();
            this.lblModID = new System.Windows.Forms.Label();
            this.txtModDList = new System.Windows.Forms.TextBox();
            this.lblModPos = new System.Windows.Forms.Label();
            this.lblModDList = new System.Windows.Forms.Label();
            this.cbLevelSelect = new System.Windows.Forms.ComboBox();
            this.txtModZRot = new System.Windows.Forms.TextBox();
            this.btnLoadLevel = new System.Windows.Forms.Button();
            this.txtModPos = new System.Windows.Forms.TextBox();
            this.lblModZRot = new System.Windows.Forms.Label();
            this.txtModUnk = new System.Windows.Forms.TextBox();
            this.txtModZ = new System.Windows.Forms.TextBox();
            this.lblModUnk = new System.Windows.Forms.Label();
            this.lblModZ = new System.Windows.Forms.Label();
            this.txtModX = new System.Windows.Forms.TextBox();
            this.txtModYRot = new System.Windows.Forms.TextBox();
            this.lblModX = new System.Windows.Forms.Label();
            this.lblModYRot = new System.Windows.Forms.Label();
            this.lblModXRot = new System.Windows.Forms.Label();
            this.txtModY = new System.Windows.Forms.TextBox();
            this.txtModXRot = new System.Windows.Forms.TextBox();
            this.lblModY = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStripViewWireframe = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutControl = new NewSF64Toolkit.AboutControl();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.viewerPanel.SuspendLayout();
            this.glPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.romInfoPage.SuspendLayout();
            this.dmaTablesPage.SuspendLayout();
            this.pnlDMATables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMA)).BeginInit();
            this.levelViewerPage.SuspendLayout();
            this.pnlListing.SuspendLayout();
            this.pnlObjectInfo.SuspendLayout();
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
            this.menuStripView});
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
            this.menuStripFileExit});
            this.menuStripFile.Name = "menuStripFile";
            this.menuStripFile.Size = new System.Drawing.Size(37, 20);
            this.menuStripFile.Text = "File";
            // 
            // menuStripFileLoad
            // 
            this.menuStripFileLoad.Name = "menuStripFileLoad";
            this.menuStripFileLoad.Size = new System.Drawing.Size(139, 22);
            this.menuStripFileLoad.Text = "Load ROM...";
            this.menuStripFileLoad.Click += new System.EventHandler(this.menuStripFileLoad_Click);
            // 
            // menuStripFileSave
            // 
            this.menuStripFileSave.Name = "menuStripFileSave";
            this.menuStripFileSave.Size = new System.Drawing.Size(139, 22);
            this.menuStripFileSave.Text = "Save ROM...";
            this.menuStripFileSave.Click += new System.EventHandler(this.menuStripFileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // menuStripFileLoadDMA
            // 
            this.menuStripFileLoadDMA.Name = "menuStripFileLoadDMA";
            this.menuStripFileLoadDMA.Size = new System.Drawing.Size(139, 22);
            this.menuStripFileLoadDMA.Text = "Load DMA...";
            this.menuStripFileLoadDMA.Click += new System.EventHandler(this.menuStripFileLoadDMA_Click);
            // 
            // menuStripFileSaveDMA
            // 
            this.menuStripFileSaveDMA.Name = "menuStripFileSaveDMA";
            this.menuStripFileSaveDMA.Size = new System.Drawing.Size(139, 22);
            this.menuStripFileSaveDMA.Text = "Save DMA...";
            this.menuStripFileSaveDMA.Click += new System.EventHandler(this.menuStripFileSaveDMA_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            // 
            // menuStripFileExit
            // 
            this.menuStripFileExit.Name = "menuStripFileExit";
            this.menuStripFileExit.Size = new System.Drawing.Size(139, 22);
            this.menuStripFileExit.Text = "Exit";
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
            this.menuStripROMFixCRCs.Size = new System.Drawing.Size(152, 22);
            this.menuStripROMFixCRCs.Text = "Fix CRCs";
            this.menuStripROMFixCRCs.Click += new System.EventHandler(this.menuStripROMFixCRCs_Click);
            // 
            // menuStripROMDecompress
            // 
            this.menuStripROMDecompress.Name = "menuStripROMDecompress";
            this.menuStripROMDecompress.Size = new System.Drawing.Size(152, 22);
            this.menuStripROMDecompress.Text = "Decompress";
            this.menuStripROMDecompress.Click += new System.EventHandler(this.menuStripROMDecompress_Click);
            // 
            // menuStripView
            // 
            this.menuStripView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripViewHex,
            this.menuStripViewWireframe});
            this.menuStripView.Name = "menuStripView";
            this.menuStripView.Size = new System.Drawing.Size(44, 20);
            this.menuStripView.Text = "View";
            // 
            // menuStripViewHex
            // 
            this.menuStripViewHex.Checked = true;
            this.menuStripViewHex.CheckOnClick = true;
            this.menuStripViewHex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuStripViewHex.Name = "menuStripViewHex";
            this.menuStripViewHex.Size = new System.Drawing.Size(163, 22);
            this.menuStripViewHex.Text = "Display in Hex";
            this.menuStripViewHex.Click += new System.EventHandler(this.menuStripViewHex_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "sf64.z64";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.viewerPanel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.infoPanel);
            this.splitContainer.Size = new System.Drawing.Size(1121, 582);
            this.splitContainer.SplitterDistance = 810;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 3;
            // 
            // viewerPanel
            // 
            this.viewerPanel.Controls.Add(this.glPanel);
            this.viewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerPanel.Location = new System.Drawing.Point(0, 0);
            this.viewerPanel.Name = "viewerPanel";
            this.viewerPanel.Size = new System.Drawing.Size(810, 582);
            this.viewerPanel.TabIndex = 0;
            // 
            // glPanel
            // 
            this.glPanel.Controls.Add(this._aboutControl);
            this.glPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glPanel.Location = new System.Drawing.Point(0, 0);
            this.glPanel.Name = "glPanel";
            this.glPanel.Padding = new System.Windows.Forms.Padding(6);
            this.glPanel.Size = new System.Drawing.Size(810, 582);
            this.glPanel.TabIndex = 0;
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.tabControl);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Padding = new System.Windows.Forms.Padding(5);
            this.infoPanel.Size = new System.Drawing.Size(309, 582);
            this.infoPanel.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.romInfoPage);
            this.tabControl.Controls.Add(this.dmaTablesPage);
            this.tabControl.Controls.Add(this.levelViewerPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(5, 5);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(299, 572);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // romInfoPage
            // 
            this.romInfoPage.BackColor = System.Drawing.SystemColors.Control;
            this.romInfoPage.Controls.Add(this.txtCRC2);
            this.romInfoPage.Controls.Add(this.txtCRC1);
            this.romInfoPage.Controls.Add(this.txtVersion);
            this.romInfoPage.Controls.Add(this.txtGameID);
            this.romInfoPage.Controls.Add(this.txtSize);
            this.romInfoPage.Controls.Add(this.txtTitle);
            this.romInfoPage.Controls.Add(this.txtFilename);
            this.romInfoPage.Controls.Add(this.lblCRC2);
            this.romInfoPage.Controls.Add(this.lblCRC1);
            this.romInfoPage.Controls.Add(this.lblVersion);
            this.romInfoPage.Controls.Add(this.lblGameID);
            this.romInfoPage.Controls.Add(this.lblTitle);
            this.romInfoPage.Controls.Add(this.lblSize);
            this.romInfoPage.Controls.Add(this.lblFilename);
            this.romInfoPage.Location = new System.Drawing.Point(4, 22);
            this.romInfoPage.Name = "romInfoPage";
            this.romInfoPage.Padding = new System.Windows.Forms.Padding(3);
            this.romInfoPage.Size = new System.Drawing.Size(291, 546);
            this.romInfoPage.TabIndex = 0;
            this.romInfoPage.Text = "ROM Info";
            // 
            // txtCRC2
            // 
            this.txtCRC2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCRC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCRC2.Location = new System.Drawing.Point(90, 238);
            this.txtCRC2.Name = "txtCRC2";
            this.txtCRC2.ReadOnly = true;
            this.txtCRC2.Size = new System.Drawing.Size(195, 23);
            this.txtCRC2.TabIndex = 13;
            // 
            // txtCRC1
            // 
            this.txtCRC1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCRC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCRC1.Location = new System.Drawing.Point(90, 204);
            this.txtCRC1.Name = "txtCRC1";
            this.txtCRC1.ReadOnly = true;
            this.txtCRC1.Size = new System.Drawing.Size(195, 23);
            this.txtCRC1.TabIndex = 12;
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtVersion.Location = new System.Drawing.Point(90, 171);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(195, 23);
            this.txtVersion.TabIndex = 11;
            // 
            // txtGameID
            // 
            this.txtGameID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGameID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtGameID.Location = new System.Drawing.Point(90, 139);
            this.txtGameID.Name = "txtGameID";
            this.txtGameID.ReadOnly = true;
            this.txtGameID.Size = new System.Drawing.Size(195, 23);
            this.txtGameID.TabIndex = 10;
            // 
            // txtSize
            // 
            this.txtSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSize.Location = new System.Drawing.Point(90, 76);
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(195, 23);
            this.txtSize.TabIndex = 9;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtTitle.Location = new System.Drawing.Point(90, 104);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(195, 23);
            this.txtTitle.TabIndex = 8;
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFilename.Location = new System.Drawing.Point(90, 44);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(195, 23);
            this.txtFilename.TabIndex = 7;
            // 
            // lblCRC2
            // 
            this.lblCRC2.AutoSize = true;
            this.lblCRC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCRC2.Location = new System.Drawing.Point(15, 239);
            this.lblCRC2.Name = "lblCRC2";
            this.lblCRC2.Size = new System.Drawing.Size(48, 17);
            this.lblCRC2.TabIndex = 6;
            this.lblCRC2.Text = "CRC2:";
            // 
            // lblCRC1
            // 
            this.lblCRC1.AutoSize = true;
            this.lblCRC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCRC1.Location = new System.Drawing.Point(15, 205);
            this.lblCRC1.Name = "lblCRC1";
            this.lblCRC1.Size = new System.Drawing.Size(48, 17);
            this.lblCRC1.TabIndex = 5;
            this.lblCRC1.Text = "CRC1:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblVersion.Location = new System.Drawing.Point(15, 172);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(60, 17);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version:";
            // 
            // lblGameID
            // 
            this.lblGameID.AutoSize = true;
            this.lblGameID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblGameID.Location = new System.Drawing.Point(15, 140);
            this.lblGameID.Name = "lblGameID";
            this.lblGameID.Size = new System.Drawing.Size(67, 17);
            this.lblGameID.TabIndex = 3;
            this.lblGameID.Text = "Game ID:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTitle.Location = new System.Drawing.Point(15, 107);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(39, 17);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Title:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSize.Location = new System.Drawing.Point(15, 77);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(39, 17);
            this.lblSize.TabIndex = 1;
            this.lblSize.Text = "Size:";
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFilename.Location = new System.Drawing.Point(15, 44);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(69, 17);
            this.lblFilename.TabIndex = 0;
            this.lblFilename.Text = "Filename:";
            // 
            // dmaTablesPage
            // 
            this.dmaTablesPage.BackColor = System.Drawing.SystemColors.Control;
            this.dmaTablesPage.Controls.Add(this.pnlDMATables);
            this.dmaTablesPage.Location = new System.Drawing.Point(4, 22);
            this.dmaTablesPage.Name = "dmaTablesPage";
            this.dmaTablesPage.Padding = new System.Windows.Forms.Padding(3);
            this.dmaTablesPage.Size = new System.Drawing.Size(291, 546);
            this.dmaTablesPage.TabIndex = 1;
            this.dmaTablesPage.Text = "DMA Tables";
            // 
            // pnlDMATables
            // 
            this.pnlDMATables.Controls.Add(this.dgvDMA);
            this.pnlDMATables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDMATables.Location = new System.Drawing.Point(3, 3);
            this.pnlDMATables.Name = "pnlDMATables";
            this.pnlDMATables.Size = new System.Drawing.Size(285, 540);
            this.pnlDMATables.TabIndex = 0;
            // 
            // dgvDMA
            // 
            this.dgvDMA.AllowUserToAddRows = false;
            this.dgvDMA.AllowUserToDeleteRows = false;
            this.dgvDMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDMA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNum,
            this.colVStart,
            this.colPStart,
            this.colPEnd});
            this.dgvDMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDMA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDMA.Location = new System.Drawing.Point(0, 0);
            this.dgvDMA.Name = "dgvDMA";
            this.dgvDMA.ReadOnly = true;
            this.dgvDMA.Size = new System.Drawing.Size(285, 540);
            this.dgvDMA.TabIndex = 0;
            this.dgvDMA.SelectionChanged += new System.EventHandler(this.dgvDMA_SelectionChanged);
            // 
            // colNum
            // 
            this.colNum.HeaderText = "#";
            this.colNum.Name = "colNum";
            this.colNum.ReadOnly = true;
            // 
            // colVStart
            // 
            this.colVStart.HeaderText = "VStart";
            this.colVStart.Name = "colVStart";
            this.colVStart.ReadOnly = true;
            // 
            // colPStart
            // 
            this.colPStart.HeaderText = "PStart";
            this.colPStart.Name = "colPStart";
            this.colPStart.ReadOnly = true;
            // 
            // colPEnd
            // 
            this.colPEnd.HeaderText = "PEnd";
            this.colPEnd.Name = "colPEnd";
            this.colPEnd.ReadOnly = true;
            // 
            // levelViewerPage
            // 
            this.levelViewerPage.BackColor = System.Drawing.SystemColors.Control;
            this.levelViewerPage.Controls.Add(this.pnlListing);
            this.levelViewerPage.Controls.Add(this.pnlObjectInfo);
            this.levelViewerPage.Location = new System.Drawing.Point(4, 22);
            this.levelViewerPage.Name = "levelViewerPage";
            this.levelViewerPage.Padding = new System.Windows.Forms.Padding(3);
            this.levelViewerPage.Size = new System.Drawing.Size(291, 546);
            this.levelViewerPage.TabIndex = 2;
            this.levelViewerPage.Text = "Level Viewer";
            // 
            // pnlListing
            // 
            this.pnlListing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListing.Controls.Add(this.tvLevelInfo);
            this.pnlListing.Location = new System.Drawing.Point(0, 0);
            this.pnlListing.Name = "pnlListing";
            this.pnlListing.Size = new System.Drawing.Size(293, 261);
            this.pnlListing.TabIndex = 33;
            // 
            // tvLevelInfo
            // 
            this.tvLevelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLevelInfo.Location = new System.Drawing.Point(0, 0);
            this.tvLevelInfo.Name = "tvLevelInfo";
            this.tvLevelInfo.Size = new System.Drawing.Size(293, 261);
            this.tvLevelInfo.TabIndex = 0;
            this.tvLevelInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLevelInfo_AfterSelect);
            this.tvLevelInfo.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvLevelInfo_NodeMouseDoubleClick);
            // 
            // pnlObjectInfo
            // 
            this.pnlObjectInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlObjectInfo.Controls.Add(this.txtModID);
            this.pnlObjectInfo.Controls.Add(this.btnModSnapTo);
            this.pnlObjectInfo.Controls.Add(this.lblModID);
            this.pnlObjectInfo.Controls.Add(this.txtModDList);
            this.pnlObjectInfo.Controls.Add(this.lblModPos);
            this.pnlObjectInfo.Controls.Add(this.lblModDList);
            this.pnlObjectInfo.Controls.Add(this.cbLevelSelect);
            this.pnlObjectInfo.Controls.Add(this.txtModZRot);
            this.pnlObjectInfo.Controls.Add(this.btnLoadLevel);
            this.pnlObjectInfo.Controls.Add(this.txtModPos);
            this.pnlObjectInfo.Controls.Add(this.lblModZRot);
            this.pnlObjectInfo.Controls.Add(this.txtModUnk);
            this.pnlObjectInfo.Controls.Add(this.txtModZ);
            this.pnlObjectInfo.Controls.Add(this.lblModUnk);
            this.pnlObjectInfo.Controls.Add(this.lblModZ);
            this.pnlObjectInfo.Controls.Add(this.txtModX);
            this.pnlObjectInfo.Controls.Add(this.txtModYRot);
            this.pnlObjectInfo.Controls.Add(this.lblModX);
            this.pnlObjectInfo.Controls.Add(this.lblModYRot);
            this.pnlObjectInfo.Controls.Add(this.lblModXRot);
            this.pnlObjectInfo.Controls.Add(this.txtModY);
            this.pnlObjectInfo.Controls.Add(this.txtModXRot);
            this.pnlObjectInfo.Controls.Add(this.lblModY);
            this.pnlObjectInfo.Location = new System.Drawing.Point(0, 267);
            this.pnlObjectInfo.Name = "pnlObjectInfo";
            this.pnlObjectInfo.Size = new System.Drawing.Size(295, 279);
            this.pnlObjectInfo.TabIndex = 32;
            // 
            // txtModID
            // 
            this.txtModID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModID.Location = new System.Drawing.Point(135, 112);
            this.txtModID.Name = "txtModID";
            this.txtModID.ReadOnly = true;
            this.txtModID.Size = new System.Drawing.Size(121, 23);
            this.txtModID.TabIndex = 24;
            // 
            // btnModSnapTo
            // 
            this.btnModSnapTo.Enabled = false;
            this.btnModSnapTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnModSnapTo.Location = new System.Drawing.Point(25, 110);
            this.btnModSnapTo.Name = "btnModSnapTo";
            this.btnModSnapTo.Size = new System.Drawing.Size(37, 26);
            this.btnModSnapTo.TabIndex = 31;
            this.btnModSnapTo.UseVisualStyleBackColor = true;
            this.btnModSnapTo.Click += new System.EventHandler(this.btnModSnapTo_Click);
            // 
            // lblModID
            // 
            this.lblModID.AutoSize = true;
            this.lblModID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModID.Location = new System.Drawing.Point(104, 115);
            this.lblModID.Name = "lblModID";
            this.lblModID.Size = new System.Drawing.Size(25, 17);
            this.lblModID.TabIndex = 23;
            this.lblModID.Text = "ID:";
            // 
            // txtModDList
            // 
            this.txtModDList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModDList.Location = new System.Drawing.Point(135, 199);
            this.txtModDList.Name = "txtModDList";
            this.txtModDList.ReadOnly = true;
            this.txtModDList.Size = new System.Drawing.Size(121, 23);
            this.txtModDList.TabIndex = 30;
            // 
            // lblModPos
            // 
            this.lblModPos.AutoSize = true;
            this.lblModPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModPos.Location = new System.Drawing.Point(55, 144);
            this.lblModPos.Name = "lblModPos";
            this.lblModPos.Size = new System.Drawing.Size(74, 17);
            this.lblModPos.TabIndex = 25;
            this.lblModPos.Text = "Level Pos:";
            // 
            // lblModDList
            // 
            this.lblModDList.AutoSize = true;
            this.lblModDList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModDList.Location = new System.Drawing.Point(43, 202);
            this.lblModDList.Name = "lblModDList";
            this.lblModDList.Size = new System.Drawing.Size(86, 17);
            this.lblModDList.TabIndex = 29;
            this.lblModDList.Text = "DList Offset:";
            // 
            // cbLevelSelect
            // 
            this.cbLevelSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevelSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbLevelSelect.FormattingEnabled = true;
            this.cbLevelSelect.Items.AddRange(new object[] {
            "1: Corneria",
            "2: Meteo",
            "3: Sector X",
            "4: Area 6",
            "5: N/A",
            "6: Sector Y",
            "7: Venom 1",
            "8: Solar",
            "9: Zoness",
            "10: Venom 2",
            "11: Training Mode",
            "12: Macbeth",
            "13: Titania",
            "14: Aquas",
            "15: Fortuna",
            "16: N/A",
            "17: Katina",
            "18: Bolse",
            "19: Sector Z",
            "20: Venom (Star Wolf)",
            "21: Corneria (Multi)"});
            this.cbLevelSelect.Location = new System.Drawing.Point(6, 241);
            this.cbLevelSelect.Name = "cbLevelSelect";
            this.cbLevelSelect.Size = new System.Drawing.Size(183, 24);
            this.cbLevelSelect.TabIndex = 1;
            this.cbLevelSelect.SelectedIndexChanged += new System.EventHandler(this.cbLevelSelect_SelectedIndexChanged);
            // 
            // txtModZRot
            // 
            this.txtModZRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModZRot.Location = new System.Drawing.Point(195, 72);
            this.txtModZRot.Name = "txtModZRot";
            this.txtModZRot.Size = new System.Drawing.Size(63, 23);
            this.txtModZRot.TabIndex = 19;
            this.txtModZRot.TextChanged += new System.EventHandler(this.txtMod_TextChanged);
            // 
            // btnLoadLevel
            // 
            this.btnLoadLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnLoadLevel.Location = new System.Drawing.Point(195, 234);
            this.btnLoadLevel.Name = "btnLoadLevel";
            this.btnLoadLevel.Size = new System.Drawing.Size(95, 36);
            this.btnLoadLevel.TabIndex = 0;
            this.btnLoadLevel.Text = "Load";
            this.btnLoadLevel.UseVisualStyleBackColor = true;
            this.btnLoadLevel.Click += new System.EventHandler(this.btnLoadLevel_Click);
            // 
            // txtModPos
            // 
            this.txtModPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModPos.Location = new System.Drawing.Point(135, 141);
            this.txtModPos.Name = "txtModPos";
            this.txtModPos.ReadOnly = true;
            this.txtModPos.Size = new System.Drawing.Size(121, 23);
            this.txtModPos.TabIndex = 26;
            // 
            // lblModZRot
            // 
            this.lblModZRot.AutoSize = true;
            this.lblModZRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModZRot.Location = new System.Drawing.Point(146, 75);
            this.lblModZRot.Name = "lblModZRot";
            this.lblModZRot.Size = new System.Drawing.Size(43, 17);
            this.lblModZRot.TabIndex = 18;
            this.lblModZRot.Text = "ZRot:";
            // 
            // txtModUnk
            // 
            this.txtModUnk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModUnk.Location = new System.Drawing.Point(135, 170);
            this.txtModUnk.Name = "txtModUnk";
            this.txtModUnk.ReadOnly = true;
            this.txtModUnk.Size = new System.Drawing.Size(121, 23);
            this.txtModUnk.TabIndex = 28;
            // 
            // txtModZ
            // 
            this.txtModZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModZ.Location = new System.Drawing.Point(66, 72);
            this.txtModZ.Name = "txtModZ";
            this.txtModZ.Size = new System.Drawing.Size(63, 23);
            this.txtModZ.TabIndex = 17;
            this.txtModZ.TextChanged += new System.EventHandler(this.txtMod_TextChanged);
            // 
            // lblModUnk
            // 
            this.lblModUnk.AutoSize = true;
            this.lblModUnk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModUnk.Location = new System.Drawing.Point(59, 173);
            this.lblModUnk.Name = "lblModUnk";
            this.lblModUnk.Size = new System.Drawing.Size(70, 17);
            this.lblModUnk.TabIndex = 27;
            this.lblModUnk.Text = "Unknown:";
            // 
            // lblModZ
            // 
            this.lblModZ.AutoSize = true;
            this.lblModZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModZ.Location = new System.Drawing.Point(41, 75);
            this.lblModZ.Name = "lblModZ";
            this.lblModZ.Size = new System.Drawing.Size(21, 17);
            this.lblModZ.TabIndex = 16;
            this.lblModZ.Text = "Z:";
            // 
            // txtModX
            // 
            this.txtModX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModX.Location = new System.Drawing.Point(66, 14);
            this.txtModX.Name = "txtModX";
            this.txtModX.Size = new System.Drawing.Size(63, 23);
            this.txtModX.TabIndex = 9;
            this.txtModX.TextChanged += new System.EventHandler(this.txtMod_TextChanged);
            // 
            // txtModYRot
            // 
            this.txtModYRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModYRot.Location = new System.Drawing.Point(195, 43);
            this.txtModYRot.Name = "txtModYRot";
            this.txtModYRot.Size = new System.Drawing.Size(63, 23);
            this.txtModYRot.TabIndex = 15;
            this.txtModYRot.TextChanged += new System.EventHandler(this.txtMod_TextChanged);
            // 
            // lblModX
            // 
            this.lblModX.AutoSize = true;
            this.lblModX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModX.Location = new System.Drawing.Point(41, 17);
            this.lblModX.Name = "lblModX";
            this.lblModX.Size = new System.Drawing.Size(21, 17);
            this.lblModX.TabIndex = 8;
            this.lblModX.Text = "X:";
            // 
            // lblModYRot
            // 
            this.lblModYRot.AutoSize = true;
            this.lblModYRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModYRot.Location = new System.Drawing.Point(146, 46);
            this.lblModYRot.Name = "lblModYRot";
            this.lblModYRot.Size = new System.Drawing.Size(43, 17);
            this.lblModYRot.TabIndex = 14;
            this.lblModYRot.Text = "YRot:";
            // 
            // lblModXRot
            // 
            this.lblModXRot.AutoSize = true;
            this.lblModXRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModXRot.Location = new System.Drawing.Point(146, 17);
            this.lblModXRot.Name = "lblModXRot";
            this.lblModXRot.Size = new System.Drawing.Size(43, 17);
            this.lblModXRot.TabIndex = 10;
            this.lblModXRot.Text = "XRot:";
            // 
            // txtModY
            // 
            this.txtModY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModY.Location = new System.Drawing.Point(66, 43);
            this.txtModY.Name = "txtModY";
            this.txtModY.Size = new System.Drawing.Size(63, 23);
            this.txtModY.TabIndex = 13;
            this.txtModY.TextChanged += new System.EventHandler(this.txtMod_TextChanged);
            // 
            // txtModXRot
            // 
            this.txtModXRot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtModXRot.Location = new System.Drawing.Point(195, 14);
            this.txtModXRot.Name = "txtModXRot";
            this.txtModXRot.Size = new System.Drawing.Size(63, 23);
            this.txtModXRot.TabIndex = 11;
            this.txtModXRot.TextChanged += new System.EventHandler(this.txtMod_TextChanged);
            // 
            // lblModY
            // 
            this.lblModY.AutoSize = true;
            this.lblModY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblModY.Location = new System.Drawing.Point(41, 46);
            this.lblModY.Name = "lblModY";
            this.lblModY.Size = new System.Drawing.Size(21, 17);
            this.lblModY.TabIndex = 12;
            this.lblModY.Text = "Y:";
            // 
            // menuStripViewWireframe
            // 
            this.menuStripViewWireframe.CheckOnClick = true;
            this.menuStripViewWireframe.Name = "menuStripViewWireframe";
            this.menuStripViewWireframe.Size = new System.Drawing.Size(163, 22);
            this.menuStripViewWireframe.Text = "Wireframe Mode";
            this.menuStripViewWireframe.Click += new System.EventHandler(this.menuStripViewWireframe_Click);
            // 
            // _aboutControl
            // 
            this._aboutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._aboutControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._aboutControl.Location = new System.Drawing.Point(6, 6);
            this._aboutControl.Margin = new System.Windows.Forms.Padding(4);
            this._aboutControl.Name = "_aboutControl";
            this._aboutControl.Size = new System.Drawing.Size(798, 570);
            this._aboutControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 628);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Name = "MainForm";
            this.Text = "NewSF64Toolkit";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.viewerPanel.ResumeLayout(false);
            this.glPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.romInfoPage.ResumeLayout(false);
            this.romInfoPage.PerformLayout();
            this.dmaTablesPage.ResumeLayout(false);
            this.pnlDMATables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMA)).EndInit();
            this.levelViewerPage.ResumeLayout(false);
            this.pnlListing.ResumeLayout(false);
            this.pnlObjectInfo.ResumeLayout(false);
            this.pnlObjectInfo.PerformLayout();
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
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel viewerPanel;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage romInfoPage;
        private System.Windows.Forms.TabPage dmaTablesPage;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label lblGameID;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCRC2;
        private System.Windows.Forms.Label lblCRC1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtCRC2;
        private System.Windows.Forms.TextBox txtCRC1;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtGameID;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Panel pnlDMATables;
        private System.Windows.Forms.DataGridView dgvDMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPEnd;
        private System.Windows.Forms.ToolStripMenuItem menuStripView;
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
        private System.Windows.Forms.Panel glPanel;
        private System.Windows.Forms.TabPage levelViewerPage;
        private System.Windows.Forms.Button btnLoadLevel;
        private System.Windows.Forms.ComboBox cbLevelSelect;
        private AboutControl _aboutControl;
        private System.Windows.Forms.TextBox txtModX;
        private System.Windows.Forms.Label lblModX;
        private System.Windows.Forms.TextBox txtModZRot;
        private System.Windows.Forms.Label lblModZRot;
        private System.Windows.Forms.TextBox txtModZ;
        private System.Windows.Forms.Label lblModZ;
        private System.Windows.Forms.TextBox txtModYRot;
        private System.Windows.Forms.Label lblModYRot;
        private System.Windows.Forms.TextBox txtModY;
        private System.Windows.Forms.Label lblModY;
        private System.Windows.Forms.TextBox txtModXRot;
        private System.Windows.Forms.Label lblModXRot;
        private System.Windows.Forms.TextBox txtModID;
        private System.Windows.Forms.Label lblModID;
        private System.Windows.Forms.TextBox txtModDList;
        private System.Windows.Forms.Label lblModDList;
        private System.Windows.Forms.TextBox txtModUnk;
        private System.Windows.Forms.Label lblModUnk;
        private System.Windows.Forms.TextBox txtModPos;
        private System.Windows.Forms.Label lblModPos;
        private System.Windows.Forms.Button btnModSnapTo;
        private System.Windows.Forms.Panel pnlObjectInfo;
        private System.Windows.Forms.Panel pnlListing;
        private System.Windows.Forms.TreeView tvLevelInfo;
        private System.Windows.Forms.ToolStripMenuItem menuStripViewWireframe;
    }
}

