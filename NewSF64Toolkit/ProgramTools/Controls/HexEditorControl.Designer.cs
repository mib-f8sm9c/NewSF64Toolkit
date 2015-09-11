﻿namespace NewSF64Toolkit.ProgramTools.Controls
{
    partial class HexEditorControl
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
            this.pnlDMATables = new System.Windows.Forms.Panel();
            this.dgvDMA = new System.Windows.Forms.DataGridView();
            this.colNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewerPanel = new System.Windows.Forms.Panel();
            this.pnlHexViewer = new System.Windows.Forms.Panel();
            this.pnlDMATables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMA)).BeginInit();
            this.viewerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDMATables
            // 
            this.pnlDMATables.Controls.Add(this.dgvDMA);
            this.pnlDMATables.Location = new System.Drawing.Point(506, 18);
            this.pnlDMATables.Name = "pnlDMATables";
            this.pnlDMATables.Size = new System.Drawing.Size(242, 296);
            this.pnlDMATables.TabIndex = 1;
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
            this.dgvDMA.Size = new System.Drawing.Size(242, 296);
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
            // viewerPanel
            // 
            this.viewerPanel.Controls.Add(this.pnlHexViewer);
            this.viewerPanel.Location = new System.Drawing.Point(13, 67);
            this.viewerPanel.Name = "viewerPanel";
            this.viewerPanel.Size = new System.Drawing.Size(510, 303);
            this.viewerPanel.TabIndex = 2;
            // 
            // pnlHexViewer
            // 
            this.pnlHexViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHexViewer.Location = new System.Drawing.Point(0, 0);
            this.pnlHexViewer.Name = "pnlHexViewer";
            this.pnlHexViewer.Padding = new System.Windows.Forms.Padding(6);
            this.pnlHexViewer.Size = new System.Drawing.Size(510, 303);
            this.pnlHexViewer.TabIndex = 0;
            // 
            // HexViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewerPanel);
            this.Controls.Add(this.pnlDMATables);
            this.Name = "HexViewControl";
            this.Size = new System.Drawing.Size(765, 451);
            this.pnlDMATables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDMA)).EndInit();
            this.viewerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDMATables;
        private System.Windows.Forms.DataGridView dgvDMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPEnd;
        private System.Windows.Forms.Panel viewerPanel;
        private System.Windows.Forms.Panel pnlHexViewer;
    }
}
