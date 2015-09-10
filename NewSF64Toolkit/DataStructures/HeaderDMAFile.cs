using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures
{
    public class HeaderDMAFile : DMAFile
    {
        public HeaderDMAFile(uint vstart, uint pstart, uint pend, uint compFlag)
            : base(vstart, pstart, pend, compFlag)
        {
        }

        public HeaderDMAFile(uint vstart, uint pstart, uint pend, uint compFlag, byte[] data)
            : base(vstart, pstart, pend, compFlag, data)
        {
        }

        public bool FixChecksum(byte[] data)
        {
            return N64Sums.FixChecksum(data);
        }

        public uint CRC1
        {
            get
            {
                return ToolSettings.ReadUInt(DMAData, 16);
            }
            set
            {
                ToolSettings.WriteUInt(value, DMAData, 16);
            }
        }

        public uint CRC2
        {
            get
            {
                return ToolSettings.ReadUInt(DMAData, 20);
            }
            set
            {
                ToolSettings.WriteUInt(value, DMAData, 20);
            }
        }

    }
}
