using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class HeaderDMAFileInfo : DMAFileInfo
    {
        private HeaderDMAFile _dma;

        public HeaderDMAFileInfo(HeaderDMAFile dma)
            : base(dma)
        {
            _dma = dma;
        }
    }
}
