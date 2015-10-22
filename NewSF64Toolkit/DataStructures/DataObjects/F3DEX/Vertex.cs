using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects.F3DEX
{
    public class Vertex : IGameDataStructure
    {
        public short X;
        public short Y;
        public short Z;
        public short Unk;
        public short S;
        public short T;
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public int Offset;

        public Vertex(int offset, byte[] data)
        {
            Offset = offset;

            LoadFromBytes(data);
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            X = ByteHelper.ReadShort(bytes, 0x0);
            Y = ByteHelper.ReadShort(bytes, 0x2);
            Z = ByteHelper.ReadShort(bytes, 0x4);
            Unk = ByteHelper.ReadShort(bytes, 0x6);
            S = ByteHelper.ReadShort(bytes, 0x8);
            T = ByteHelper.ReadShort(bytes, 0xA);
            R = ByteHelper.ReadByte(bytes, 0xC);
            G = ByteHelper.ReadByte(bytes, 0xD);
            B = ByteHelper.ReadByte(bytes, 0xE);
            A = ByteHelper.ReadByte(bytes, 0xF);

            return true;
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            ByteHelper.WriteShort(X, bytes, 0x0);
            ByteHelper.WriteShort(Y, bytes, 0x2);
            ByteHelper.WriteShort(Z, bytes, 0x4);
            ByteHelper.WriteShort(Unk, bytes, 0x6);
            ByteHelper.WriteShort(S, bytes, 0x8);
            ByteHelper.WriteShort(T, bytes, 0xA);
            ByteHelper.WriteByte(R, bytes, 0xC);
            ByteHelper.WriteByte(G, bytes, 0xD);
            ByteHelper.WriteByte(B, bytes, 0xE);
            ByteHelper.WriteByte(A, bytes, 0xF);

            return bytes;
        }

        public static int Size { get { return 0x10; } }
    }
}
