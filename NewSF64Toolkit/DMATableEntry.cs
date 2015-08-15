using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit
{
    public struct DMATableEntry
    {
        public uint VStart;
        public uint PStart;
        public uint PEnd;
        public uint CompFlag;

        public byte[] DMAData;

        public DMATableEntry(uint vstart, uint pstart, uint pend, uint compFlag)
        {
            VStart = vstart;
            PStart = pstart;
            PEnd = pend;
            CompFlag = compFlag;

            DMAData = null;
        }

        public DMATableEntry(uint vstart, uint pstart, uint pend, uint compFlag, byte[] data)
            : this(vstart, pstart, pend, compFlag)
        {
            DMAData = data;
        }
    }
}
