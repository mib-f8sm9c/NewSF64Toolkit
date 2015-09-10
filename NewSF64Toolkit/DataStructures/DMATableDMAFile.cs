using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures
{
    public class DMATableDMAFile : DMAFile
    {
        public DMATableDMAFile(uint vstart, uint pstart, uint pend, uint compFlag)
            : base(vstart, pstart, pend, compFlag)
        {
        }

        public DMATableDMAFile(uint vstart, uint pstart, uint pend, uint compFlag, byte[] data)
            : base(vstart, pstart, pend, compFlag, data)
        {
        }

        public void SetDMAEntry(int dmaIndex, uint vstart, uint pstart, uint pend, uint compFlag, Endianness romEndianness)
        {
            int CurrentPos = 16 * dmaIndex;

            ToolSettings.WriteUInt(vstart, DMAData, CurrentPos, romEndianness);
            ToolSettings.WriteUInt(pstart, DMAData, CurrentPos + 4, romEndianness);
            ToolSettings.WriteUInt(pend, DMAData, CurrentPos + 8, romEndianness);
            ToolSettings.WriteUInt(compFlag, DMAData, CurrentPos + 12, romEndianness);
        }
    }
}
