using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DataObjects;
using NewSF64Toolkit.DataStructures.DMA;

namespace NewSF64Toolkit.Tools.Controls.ResourceSubControls
{
    public partial class RamTableResourceControl : ResourceControl
    {
        public RamTableResourceControl()
        {
            InitializeComponent();
        }

        public override void SetObject(object obj)
        {
            if (!(obj is RAMTableEntry))
            {
                txtOverlayStart.Text = string.Empty;
                txtOverlayEnd.Text = string.Empty;
                txtOverlayDma.Text = string.Empty;
                
                txtLoc1Start.Text = string.Empty;
                txtLoc1End.Text = string.Empty;
                txtLoc1Dma.Text = string.Empty;

                txtLoc2Start.Text = string.Empty;
                txtLoc2End.Text = string.Empty;
                txtLoc2Dma.Text = string.Empty;

                txtLoc3Start.Text = string.Empty;
                txtLoc3End.Text = string.Empty;
                txtLoc3Dma.Text = string.Empty;
                
                txtSeg1Start.Text = string.Empty;
                txtSeg1End.Text = string.Empty;
                txtSeg1Dma.Text = string.Empty;

                txtSeg2Start.Text = string.Empty;
                txtSeg2End.Text = string.Empty;
                txtSeg2Dma.Text = string.Empty;

                txtSeg3Start.Text = string.Empty;
                txtSeg3End.Text = string.Empty;
                txtSeg3Dma.Text = string.Empty;

                txtSeg4Start.Text = string.Empty;
                txtSeg4End.Text = string.Empty;
                txtSeg4Dma.Text = string.Empty;
                
                txtSeg5Start.Text = string.Empty;
                txtSeg5End.Text = string.Empty;
                txtSeg5Dma.Text = string.Empty;

                txtSeg6Start.Text = string.Empty;
                txtSeg6End.Text = string.Empty;
                txtSeg6Dma.Text = string.Empty;

                txtSeg7Start.Text = string.Empty;
                txtSeg7End.Text = string.Empty;
                txtSeg7Dma.Text = string.Empty;

                txtSeg8Start.Text = string.Empty;
                txtSeg8End.Text = string.Empty;
                txtSeg8Dma.Text = string.Empty;
                
                txtSeg9Start.Text = string.Empty;
                txtSeg9End.Text = string.Empty;
                txtSeg9Dma.Text = string.Empty;

                txtSegAStart.Text = string.Empty;
                txtSegAEnd.Text = string.Empty;
                txtSegADma.Text = string.Empty;

                txtSegBStart.Text = string.Empty;
                txtSegBEnd.Text = string.Empty;
                txtSegBDma.Text = string.Empty;

                txtSegCStart.Text = string.Empty;
                txtSegCEnd.Text = string.Empty;
                txtSegCDma.Text = string.Empty;
                
                txtSegDStart.Text = string.Empty;
                txtSegDEnd.Text = string.Empty;
                txtSegDDma.Text = string.Empty;

                txtSegEStart.Text = string.Empty;
                txtSegEEnd.Text = string.Empty;
                txtSegEDma.Text = string.Empty;

                txtSegFStart.Text = string.Empty;
                txtSegFEnd.Text = string.Empty;
                txtSegFDma.Text = string.Empty;

                return;
            }

            RAMTableEntry ram = (RAMTableEntry)obj;
            DMAFile matchingDma;

            txtOverlayStart.Text = ByteHelper.DisplayValue(ram.OverlayStart);
            txtOverlayEnd.Text = ByteHelper.DisplayValue(ram.OverlayEnd);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.OverlayStart && d.DMAInfo.PEnd == ram.OverlayEnd);
            if (matchingDma != null)
                txtOverlayDma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtOverlayDma.Text = string.Empty;

            txtLoc1Start.Text = ByteHelper.DisplayValue(ram.Loc1Start);
            txtLoc1End.Text = ByteHelper.DisplayValue(ram.Loc1End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Loc1Start && d.DMAInfo.PEnd == ram.Loc1End);
            if (matchingDma != null)
                txtLoc1Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtLoc1Dma.Text = string.Empty;

            txtLoc2Start.Text = ByteHelper.DisplayValue(ram.Loc2Start);
            txtLoc2End.Text = ByteHelper.DisplayValue(ram.Loc2End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Loc2Start && d.DMAInfo.PEnd == ram.Loc2End);
            if (matchingDma != null)
                txtLoc2Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtLoc2Dma.Text = string.Empty;

            txtLoc3Start.Text = ByteHelper.DisplayValue(ram.Loc3Start);
            txtLoc3End.Text = ByteHelper.DisplayValue(ram.Loc3End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Loc3Start && d.DMAInfo.PEnd == ram.Loc3End);
            if (matchingDma != null)
                txtLoc3Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtLoc3Dma.Text = string.Empty;

            txtSeg1Start.Text = ByteHelper.DisplayValue(ram.Seg1Start);
            txtSeg1End.Text = ByteHelper.DisplayValue(ram.Seg1End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg1Start && d.DMAInfo.PEnd == ram.Seg1End);
            if (matchingDma != null)
                txtSeg1Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg1Dma.Text = string.Empty;

            txtSeg2Start.Text = ByteHelper.DisplayValue(ram.Seg2Start);
            txtSeg2End.Text = ByteHelper.DisplayValue(ram.Seg2End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg2Start && d.DMAInfo.PEnd == ram.Seg2End);
            if (matchingDma != null)
                txtSeg2Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg2Dma.Text = string.Empty;

            txtSeg3Start.Text = ByteHelper.DisplayValue(ram.Seg3Start);
            txtSeg3End.Text = ByteHelper.DisplayValue(ram.Seg3End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg3Start && d.DMAInfo.PEnd == ram.Seg3End);
            if (matchingDma != null)
                txtSeg3Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg3Dma.Text = string.Empty;

            txtSeg4Start.Text = ByteHelper.DisplayValue(ram.Seg4Start);
            txtSeg4End.Text = ByteHelper.DisplayValue(ram.Seg4End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg4Start && d.DMAInfo.PEnd == ram.Seg4End);
            if (matchingDma != null)
                txtSeg4Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg4Dma.Text = string.Empty;

            txtSeg5Start.Text = ByteHelper.DisplayValue(ram.Seg5Start);
            txtSeg5End.Text = ByteHelper.DisplayValue(ram.Seg5End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg5Start && d.DMAInfo.PEnd == ram.Seg5End);
            if (matchingDma != null)
                txtSeg5Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg5Dma.Text = string.Empty;

            txtSeg6Start.Text = ByteHelper.DisplayValue(ram.Seg6Start);
            txtSeg6End.Text = ByteHelper.DisplayValue(ram.Seg6End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg6Start && d.DMAInfo.PEnd == ram.Seg6End);
            if (matchingDma != null)
                txtSeg6Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg6Dma.Text = string.Empty;

            txtSeg7Start.Text = ByteHelper.DisplayValue(ram.Seg7Start);
            txtSeg7End.Text = ByteHelper.DisplayValue(ram.Seg7End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg7Start && d.DMAInfo.PEnd == ram.Seg7End);
            if (matchingDma != null)
                txtSeg7Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg7Dma.Text = string.Empty;

            txtSeg8Start.Text = ByteHelper.DisplayValue(ram.Seg8Start);
            txtSeg8End.Text = ByteHelper.DisplayValue(ram.Seg8End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg8Start && d.DMAInfo.PEnd == ram.Seg8End);
            if (matchingDma != null)
                txtSeg8Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg8Dma.Text = string.Empty;

            txtSeg9Start.Text = ByteHelper.DisplayValue(ram.Seg9Start);
            txtSeg9End.Text = ByteHelper.DisplayValue(ram.Seg9End);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.Seg9Start && d.DMAInfo.PEnd == ram.Seg9End);
            if (matchingDma != null)
                txtSeg9Dma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSeg9Dma.Text = string.Empty;

            txtSegAStart.Text = ByteHelper.DisplayValue(ram.SegAStart);
            txtSegAEnd.Text = ByteHelper.DisplayValue(ram.SegAEnd);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.SegAStart && d.DMAInfo.PEnd == ram.SegAEnd);
            if (matchingDma != null)
                txtSegADma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSegADma.Text = string.Empty;

            txtSegBStart.Text = ByteHelper.DisplayValue(ram.SegBStart);
            txtSegBEnd.Text = ByteHelper.DisplayValue(ram.SegBEnd);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.SegBStart && d.DMAInfo.PEnd == ram.SegBEnd);
            if (matchingDma != null)
                txtSegBDma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSegBDma.Text = string.Empty;

            txtSegCStart.Text = ByteHelper.DisplayValue(ram.SegCStart);
            txtSegCEnd.Text = ByteHelper.DisplayValue(ram.SegCEnd);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.SegCStart && d.DMAInfo.PEnd == ram.SegCEnd);
            if (matchingDma != null)
                txtSegCDma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSegCDma.Text = string.Empty;

            txtSegDStart.Text = ByteHelper.DisplayValue(ram.SegDStart);
            txtSegDEnd.Text = ByteHelper.DisplayValue(ram.SegDEnd);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.SegDStart && d.DMAInfo.PEnd == ram.SegDEnd);
            if (matchingDma != null)
                txtSegDDma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSegDDma.Text = string.Empty;

            txtSegEStart.Text = ByteHelper.DisplayValue(ram.SegEStart);
            txtSegEEnd.Text = ByteHelper.DisplayValue(ram.SegEEnd);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.SegEStart && d.DMAInfo.PEnd == ram.SegEEnd);
            if (matchingDma != null)
                txtSegEDma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSegEDma.Text = string.Empty;

            txtSegFStart.Text = ByteHelper.DisplayValue(ram.SegFStart);
            txtSegFEnd.Text = ByteHelper.DisplayValue(ram.SegFEnd);
            matchingDma = SF64ROM.Instance.DMATable.SingleOrDefault(
                d => d.DMAInfo.PStart == ram.SegFStart && d.DMAInfo.PEnd == ram.SegFEnd);
            if (matchingDma != null)
                txtSegFDma.Text = SF64ROM.Instance.DMATable.IndexOf(matchingDma).ToString();
            else
                txtSegFDma.Text = string.Empty;

        }

    }
}
