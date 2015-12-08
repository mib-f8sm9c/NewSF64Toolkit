using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class RefSimpleLevelObject : IGameDataStructure
    {
        public uint DListOffset;
        public uint Unk1;
        public uint Unk2;
        public uint Unk3;
        public float Unk4;
        public uint Unk5;
        public uint Unk6;
        public float Unk7;
        public uint Unk8;

        public int Offset;

        public int[] GLDisplayListOffset; //1 is normal, 2 is selected, 3 is wireframe

        public RefSimpleLevelObject(int offset, byte[] bytes)
        {
            Offset = offset;

            GLDisplayListOffset = new int[3];
            LoadFromBytes(bytes);
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            ByteHelper.WriteUInt(DListOffset, bytes, 0x0);
            ByteHelper.WriteUInt(Unk1, bytes, 0x04);
            ByteHelper.WriteUInt(Unk2, bytes, 0x08);
            ByteHelper.WriteUInt(Unk3, bytes, 0x0C);
            ByteHelper.WriteFloat(Unk4, bytes, 0x10);
            ByteHelper.WriteUInt(Unk5, bytes, 0x14);
            ByteHelper.WriteUInt(Unk6, bytes, 0x18);
            ByteHelper.WriteFloat(Unk7, bytes, 0x1C);
            ByteHelper.WriteUInt(Unk8, bytes, 0x20);

            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            DListOffset = ByteHelper.ReadUInt(bytes, 0x0);
            Unk1 = ByteHelper.ReadUInt(bytes, 0x04);
            Unk2 = ByteHelper.ReadUInt(bytes, 0x08);
            Unk3 = ByteHelper.ReadUInt(bytes, 0x0C);
            Unk4 = ByteHelper.ReadFloat(bytes, 0x10);
            Unk5 = ByteHelper.ReadUInt(bytes, 0x14);
            Unk6 = ByteHelper.ReadUInt(bytes, 0x18);
            Unk7 = ByteHelper.ReadFloat(bytes, 0x1C);
            Unk8 = ByteHelper.ReadUInt(bytes, 0x20);

            return true;
        }

        public static int Size { get { return 0x24; } }

    }
}
