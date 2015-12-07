using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class DMATableDMAFileInfo : DMAFileInfo
    {
        private DMATableDMAFile _dma;

        public DMATableDMAFileInfo(DMATableDMAFile dma)
            : base(dma)
        {
            _dma = dma;
        }
    }
}
