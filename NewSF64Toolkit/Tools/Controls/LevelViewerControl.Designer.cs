namespace NewSF64Toolkit.Tools.Controls
{
    partial class LevelViewerControl
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
            this.tvLevelInfo = new System.Windows.Forms.TreeView();
            this.pnlLevelControl = new System.Windows.Forms.Panel();
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
            // tvLevelInfo
            // 
            this.tvLevelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvLevelInfo.Location = new System.Drawing.Point(3, 3);
            this.tvLevelInfo.Name = "tvLevelInfo";
            this.tvLevelInfo.Size = new System.Drawing.Size(289, 105);
            this.tvLevelInfo.TabIndex = 34;
            this.tvLevelInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLevelInfo_AfterSelect);
            this.tvLevelInfo.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvLevelInfo_NodeMouseDoubleClick);
            // 
            // pnlLevelControl
            // 
            this.pnlLevelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLevelControl.Controls.Add(this.pnlObjectInfo);
            this.pnlLevelControl.Controls.Add(this.tvLevelInfo);
            this.pnlLevelControl.Location = new System.Drawing.Point(563, 3);
            this.pnlLevelControl.Name = "pnlLevelControl";
            this.pnlLevelControl.Size = new System.Drawing.Size(295, 393);
            this.pnlLevelControl.TabIndex = 35;
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
            this.pnlObjectInfo.Location = new System.Drawing.Point(3, 114);
            this.pnlObjectInfo.Name = "pnlObjectInfo";
            this.pnlObjectInfo.Size = new System.Drawing.Size(289, 276);
            this.pnlObjectInfo.TabIndex = 33;
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
            this.btnLoadLevel.Size = new System.Drawing.Size(91, 36);
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
            // LevelViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlLevelControl);
            this.Controls.Add(this.glPanel);
            this.Name = "LevelViewerControl";
            this.Size = new System.Drawing.Size(861, 399);
            this.pnlLevelControl.ResumeLayout(false);
            this.pnlObjectInfo.ResumeLayout(false);
            this.pnlObjectInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel glPanel;
        private System.Windows.Forms.TreeView tvLevelInfo;
        private System.Windows.Forms.Panel pnlLevelControl;
        private System.Windows.Forms.Panel pnlObjectInfo;
        private System.Windows.Forms.TextBox txtModID;
        private System.Windows.Forms.Button btnModSnapTo;
        private System.Windows.Forms.Label lblModID;
        private System.Windows.Forms.TextBox txtModDList;
        private System.Windows.Forms.Label lblModPos;
        private System.Windows.Forms.Label lblModDList;
        private System.Windows.Forms.ComboBox cbLevelSelect;
        private System.Windows.Forms.TextBox txtModZRot;
        private System.Windows.Forms.Button btnLoadLevel;
        private System.Windows.Forms.TextBox txtModPos;
        private System.Windows.Forms.Label lblModZRot;
        private System.Windows.Forms.TextBox txtModUnk;
        private System.Windows.Forms.TextBox txtModZ;
        private System.Windows.Forms.Label lblModUnk;
        private System.Windows.Forms.Label lblModZ;
        private System.Windows.Forms.TextBox txtModX;
        private System.Windows.Forms.TextBox txtModYRot;
        private System.Windows.Forms.Label lblModX;
        private System.Windows.Forms.Label lblModYRot;
        private System.Windows.Forms.Label lblModXRot;
        private System.Windows.Forms.TextBox txtModY;
        private System.Windows.Forms.TextBox txtModXRot;
        private System.Windows.Forms.Label lblModY;
    }
}
