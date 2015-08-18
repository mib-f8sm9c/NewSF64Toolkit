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
            this.cbLevelSelect = new System.Windows.Forms.ComboBox();
            this.btnLoadLevel = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
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
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 475);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(766, 22);
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
            this.menuStrip.Size = new System.Drawing.Size(766, 24);
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
            // menuStripView
            // 
            this.menuStripView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStripViewHex});
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
            this.menuStripViewHex.Size = new System.Drawing.Size(148, 22);
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
            this.splitContainer.Size = new System.Drawing.Size(766, 451);
            this.splitContainer.SplitterDistance = 496;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 3;
            // 
            // viewerPanel
            // 
            this.viewerPanel.Controls.Add(this.glPanel);
            this.viewerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerPanel.Location = new System.Drawing.Point(0, 0);
            this.viewerPanel.Name = "viewerPanel";
            this.viewerPanel.Size = new System.Drawing.Size(496, 451);
            this.viewerPanel.TabIndex = 0;
            // 
            // glPanel
            // 
            this.glPanel.Controls.Add(this._aboutControl);
            this.glPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glPanel.Location = new System.Drawing.Point(0, 0);
            this.glPanel.Name = "glPanel";
            this.glPanel.Padding = new System.Windows.Forms.Padding(6);
            this.glPanel.Size = new System.Drawing.Size(496, 451);
            this.glPanel.TabIndex = 0;
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.tabControl);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Padding = new System.Windows.Forms.Padding(5);
            this.infoPanel.Size = new System.Drawing.Size(268, 451);
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
            this.tabControl.Size = new System.Drawing.Size(258, 441);
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
            this.romInfoPage.Size = new System.Drawing.Size(250, 415);
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
            this.txtCRC2.Size = new System.Drawing.Size(154, 23);
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
            this.txtCRC1.Size = new System.Drawing.Size(154, 23);
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
            this.txtVersion.Size = new System.Drawing.Size(154, 23);
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
            this.txtGameID.Size = new System.Drawing.Size(154, 23);
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
            this.txtSize.Size = new System.Drawing.Size(154, 23);
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
            this.txtTitle.Size = new System.Drawing.Size(154, 23);
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
            this.txtFilename.Size = new System.Drawing.Size(154, 23);
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
            this.dmaTablesPage.Size = new System.Drawing.Size(250, 415);
            this.dmaTablesPage.TabIndex = 1;
            this.dmaTablesPage.Text = "DMA Tables";
            // 
            // pnlDMATables
            // 
            this.pnlDMATables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDMATables.Controls.Add(this.dgvDMA);
            this.pnlDMATables.Location = new System.Drawing.Point(0, 0);
            this.pnlDMATables.Name = "pnlDMATables";
            this.pnlDMATables.Size = new System.Drawing.Size(258, 409);
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
            this.dgvDMA.Size = new System.Drawing.Size(258, 409);
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
            this.levelViewerPage.Controls.Add(this.cbLevelSelect);
            this.levelViewerPage.Controls.Add(this.btnLoadLevel);
            this.levelViewerPage.Location = new System.Drawing.Point(4, 22);
            this.levelViewerPage.Name = "levelViewerPage";
            this.levelViewerPage.Padding = new System.Windows.Forms.Padding(3);
            this.levelViewerPage.Size = new System.Drawing.Size(250, 415);
            this.levelViewerPage.TabIndex = 2;
            this.levelViewerPage.Text = "Level Viewer";
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
            this.cbLevelSelect.Location = new System.Drawing.Point(23, 50);
            this.cbLevelSelect.Name = "cbLevelSelect";
            this.cbLevelSelect.Size = new System.Drawing.Size(209, 24);
            this.cbLevelSelect.TabIndex = 1;
            this.cbLevelSelect.SelectedIndexChanged += new System.EventHandler(this.cbLevelSelect_SelectedIndexChanged);
            // 
            // btnLoadLevel
            // 
            this.btnLoadLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnLoadLevel.Location = new System.Drawing.Point(71, 104);
            this.btnLoadLevel.Name = "btnLoadLevel";
            this.btnLoadLevel.Size = new System.Drawing.Size(121, 36);
            this.btnLoadLevel.TabIndex = 0;
            this.btnLoadLevel.Text = "Load";
            this.btnLoadLevel.UseVisualStyleBackColor = true;
            this.btnLoadLevel.Click += new System.EventHandler(this.btnLoadLevel_Click);
            // 
            // _aboutControl
            // 
            this._aboutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._aboutControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this._aboutControl.Location = new System.Drawing.Point(6, 6);
            this._aboutControl.Margin = new System.Windows.Forms.Padding(4);
            this._aboutControl.Name = "_aboutControl";
            this._aboutControl.Size = new System.Drawing.Size(484, 439);
            this._aboutControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 497);
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
    }
}

