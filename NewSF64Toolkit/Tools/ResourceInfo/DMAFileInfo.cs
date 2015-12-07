using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using System.ComponentModel;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class DMAFileInfo : IResourceInfo
    {
        private DMAFile _dma;

        public DMAFileInfo(DMAFile dma)
        {
            _dma = dma;
        }

        [CategoryAttribute("DMA Info"), DescriptionAttribute("Index of the DMA file")]
        public int Index
        {
            get { return _dma.Index; }
        }

        [CategoryAttribute("DMA Info"), DescriptionAttribute("Details whether the DMA file is currently compressed")]
        public bool IsCompressed
        {
            get { return _dma.IsCompressed; }
        }

        [CategoryAttribute("DMA Info"), DescriptionAttribute("Size in bytes of the DMA file")]
        public int Size
        {
            get { return _dma.Size; }
        }

        [CategoryAttribute("DMA Info"), DescriptionAttribute("Physical start location of the DMA file")]
        public uint PStart
        {
            get { return _dma.DMAInfo.PStart; }
        }

        [CategoryAttribute("DMA Info"), DescriptionAttribute("Physical end location of the DMA file")]
        public uint PEnd
        {
            get { return _dma.DMAInfo.PEnd; }
        }

        [CategoryAttribute("DMA Info"), DescriptionAttribute("Virtual start location of the DMA file")]
        public uint VStart
        {
            get { return _dma.DMAInfo.VStart; }
        }
    }
}
