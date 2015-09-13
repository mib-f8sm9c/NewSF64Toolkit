using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class DMATableEntry : IGameDataStructure
    {
        public uint VStart; //virtual start
        public uint PStart; //physical start
        public uint PEnd; //physical end
        public uint CFlag; //compressed flag

        public int Offset;

        public DMATableEntry(int offset, uint vstart, uint pstart, uint pend, uint cflag)
        {
            Offset = offset;

            VStart = vstart;
            PStart = pstart;
            PEnd = pend;
            CFlag = cflag;
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[16];

            ByteHelper.WriteUInt(VStart, bytes, 0);
            ByteHelper.WriteUInt(PStart, bytes, 4);
            ByteHelper.WriteUInt(PEnd, bytes, 8);
            ByteHelper.WriteUInt(CFlag, bytes, 12);

            return bytes;
        }

    }
}
