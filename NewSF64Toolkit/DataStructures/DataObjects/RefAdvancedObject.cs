using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class RefAdvancedLevelObject : IGameDataStructure
    {
        public uint DListOffset;
        public uint Unk1;
        public float Unk2;
        public float Unk3;
        public float Unk4;
        public uint Unk5;
        public float Unk6;
        public uint Unk7;

        public int Offset;

        public int[] GLDisplayListOffset; //1 is normal, 2 is selected, 3 is wireframe

        public RefAdvancedLevelObject(int offset, byte[] bytes)
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
            ByteHelper.WriteFloat(Unk2, bytes, 0x08);
            ByteHelper.WriteFloat(Unk3, bytes, 0x0C);
            ByteHelper.WriteFloat(Unk4, bytes, 0x10);
            ByteHelper.WriteUInt(Unk5, bytes, 0x14);
            ByteHelper.WriteFloat(Unk6, bytes, 0x18);
            ByteHelper.WriteUInt(Unk7, bytes, 0x1C);

            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            DListOffset = ByteHelper.ReadUInt(bytes, 0x0);
            Unk1 = ByteHelper.ReadUInt(bytes, 0x04);
            Unk2 = ByteHelper.ReadFloat(bytes, 0x08);
            Unk3 = ByteHelper.ReadFloat(bytes, 0x0C);
            Unk4 = ByteHelper.ReadFloat(bytes, 0x10);
            Unk5 = ByteHelper.ReadUInt(bytes, 0x14);
            Unk6 = ByteHelper.ReadFloat(bytes, 0x18);
            Unk7 = ByteHelper.ReadUInt(bytes, 0x1C);

            return true;
        }

        public static int Size { get { return 0x20; } }

    }
}
