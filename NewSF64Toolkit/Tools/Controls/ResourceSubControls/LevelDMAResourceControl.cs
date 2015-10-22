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
    public partial class LevelDMAResourceControl : ResourceControl
    {
        public LevelDMAResourceControl()
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

                txtBGM.Text = string.Empty;
                txtUnk1.Text = string.Empty;
                txtUnk2.Text = string.Empty;
                txtUnk3.Text = string.Empty;
                txtUnk4.Text = string.Empty;
                txtUnk5.Text = string.Empty;
                txtUnk6.Text = string.Empty;
                txtUnk7.Text = string.Empty;
                txtUnk8.Text = string.Empty;

                txtLevel.Text = string.Empty;

                return;
            }

            LevelDMAFile dma = (LevelDMAFile)obj;

            txtIndex.Text = dma.Index.ToString();
            txtVS.Text = ByteHelper.DisplayValue(dma.DMAInfo.VStart);
            txtPS.Text = ByteHelper.DisplayValue(dma.DMAInfo.PStart);
            txtPE.Text = ByteHelper.DisplayValue(dma.DMAInfo.PEnd);
            txtSize.Text = ByteHelper.DisplayValue(dma.Size);
            txtCFlag.Text = dma.DMAInfo.CFlag.ToString();

            txtBGM.Text = dma.LevelHeader.BGMTrack.ToString();
            txtUnk1.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk1);
            txtUnk2.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk2);
            txtUnk3.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk3);
            txtUnk4.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk4);
            txtUnk5.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk5);
            txtUnk6.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk6);
            txtUnk7.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk7);
            txtUnk8.Text = ByteHelper.DisplayValue(dma.LevelHeader.Unk8);

            int index = StarFoxRomInfo.LevelIndexAndDMAs.ToList().IndexOf(dma.Index);
            if (index < 0 || index >= StarFoxRomInfo.LevelNamesByIndex.Length)
                txtLevel.Text = string.Empty;
            else
                txtLevel.Text = StarFoxRomInfo.LevelNamesByIndex[index];
        }
    }
}
