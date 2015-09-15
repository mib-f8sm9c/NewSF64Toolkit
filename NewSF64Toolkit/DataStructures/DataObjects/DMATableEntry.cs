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

        public DMATableEntry(int offset, byte[] bytes)
        {
            Offset = offset;

            LoadFromBytes(bytes);
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            ByteHelper.WriteUInt(VStart, bytes, 0x0);
            ByteHelper.WriteUInt(PStart, bytes, 0x4);
            ByteHelper.WriteUInt(PEnd, bytes, 0x8);
            ByteHelper.WriteUInt(CFlag, bytes, 0xC);

            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            VStart = ByteHelper.ReadUInt(bytes, 0x0);
            PStart = ByteHelper.ReadUInt(bytes, 0x4);
            PEnd = ByteHelper.ReadUInt(bytes, 0x8);
            CFlag = ByteHelper.ReadUInt(bytes, 0xC);

            return true;
        }

        public static int Size { get { return 0x10; } }
    }
}
