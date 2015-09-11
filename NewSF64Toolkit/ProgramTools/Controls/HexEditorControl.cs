using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using NewSF64Toolkit.DataStructures;

namespace NewSF64Toolkit.ProgramTools.Controls
{
    public partial class HexEditorControl : UserControl
    {
        ByteViewer _byteViewer;

        public HexEditorControl()
        {
            InitializeComponent();

            _byteViewer = new ByteViewer();
            //_byteViewer.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            //_byteViewer.Location = new Point(0, 0);
            //_byteViewer.Size = glPanel.Size;
            _byteViewer.Dock = DockStyle.Fill;
            pnlHexViewer.Controls.Add(_byteViewer);
        }

        private void dgvDMA_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDMA.SelectedCells.Count == 1)
            {
                _byteViewer.SetBytes(SF64ROM.Instance.DMATable[dgvDMA.SelectedCells[0].RowIndex].DMAData);
            }
        }
        
        private void RefreshDMATable()
        {
            dgvDMA.Rows.Clear();

            for (int i = 0; i < SF64ROM.Instance.DMATable.Count; i++)
            {
                DMAFile entry = SF64ROM.Instance.DMATable[i];

                dgvDMA.Rows.Add();
                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[0].Value = i + 1;
                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[1].Value = ToolSettings.DisplayValue(entry.VStart);
                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[2].Value = ToolSettings.DisplayValue(entry.PStart);
                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[3].Value = ToolSettings.DisplayValue(entry.PEnd);
            }
        }

    }
}
