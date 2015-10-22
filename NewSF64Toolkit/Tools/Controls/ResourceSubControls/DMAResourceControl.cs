using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;

namespace NewSF64Toolkit.Tools.Controls.ResourceSubControls
{
    public partial class DMAResourceControl : ResourceControl
    {
        public DMAResourceControl()
        {
            InitializeComponent();
        }

        public override void SetObject(object obj)
        {
            if (!(obj is DMAFile))
            {
                txtIndex.Text = string.Empty;
                txtVS.Text = string.Empty;
                txtPS.Text = string.Empty;
                txtPE.Text = string.Empty;
                txtSize.Text = string.Empty;
                txtCFlag.Text = string.Empty;

                return;
            }

            DMAFile dma = (DMAFile)obj;

            txtIndex.Text = dma.Index.ToString();
            txtVS.Text = ByteHelper.DisplayValue(dma.DMAInfo.VStart);
            txtPS.Text = ByteHelper.DisplayValue(dma.DMAInfo.PStart);
            txtPE.Text = ByteHelper.DisplayValue(dma.DMAInfo.PEnd);
            txtSize.Text = ByteHelper.DisplayValue(dma.Size);
            txtCFlag.Text = dma.DMAInfo.CFlag.ToString();
            
        }
    }
}
