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
using NewSF64Toolkit.DataStructures.DMA;

namespace NewSF64Toolkit.Tools.Controls
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
                _byteViewer.SetBytes(SF64ROM.Instance.DMATable[dgvDMA.SelectedCells[0].RowIndex].GetAsBytes());
            }
            else
            {
                _byteViewer.SetBytes(new byte[0]);
            }
        }

        public void ResetDMATable()
        {
            dgvDMA.Rows.Clear();

            if (!SF64ROM.Instance.IsROMLoaded)
                return;

            for (int i = 0; i < SF64ROM.Instance.DMATable.Count; i++)
            {
                dgvDMA.Rows.Add();
            }

            RefreshDMATable();
        }

        public void RefreshDMATable()
        {
            if (!SF64ROM.Instance.IsROMLoaded)
                return;

            for (int i = 0; i < SF64ROM.Instance.DMATable.Count; i++)
            {
                DMAFile entry = SF64ROM.Instance.DMATable[i];

                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[0].Value = i + 1;
                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[1].Value = ByteHelper.DisplayValue(entry.DMAInfo.VStart);
                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[2].Value = ByteHelper.DisplayValue(entry.DMAInfo.PStart);
                dgvDMA.Rows[dgvDMA.Rows.Count - 1].Cells[3].Value = ByteHelper.DisplayValue(entry.DMAInfo.PEnd);
            }
        }

    }
}
