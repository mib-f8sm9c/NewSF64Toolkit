using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.OpenGL;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class SFLevelObject : IGameDataStructure, IGLRenderable
    {
        public float LvlPos;
        public short X;
        public short Y;
        public short Z;
        public short XRot;
        public short YRot;
        public short ZRot;
        public ushort ID;
        public ushort Unk;

        public uint DListOffset;

        public int Offset;

        public SFLevelObject(int offset, byte[] bytes)
        {
            Offset = offset;

            LoadFromBytes(bytes);
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            ByteHelper.WriteFloat(LvlPos, bytes, 0);
            ByteHelper.WriteShort(Z, bytes, 0x4);
            ByteHelper.WriteShort(X, bytes, 0x6);
            ByteHelper.WriteShort(Y, bytes, 0x8);
            ByteHelper.WriteShort(XRot, bytes, 0xA);
            ByteHelper.WriteShort(YRot, bytes, 0xC);
            ByteHelper.WriteShort(ZRot, bytes, 0xE);
            ByteHelper.WriteUShort(ID, bytes, 0x10);
            ByteHelper.WriteUShort(Unk, bytes, 0x12);

            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            LvlPos = ByteHelper.ReadFloat(bytes, 0);
            Z = ByteHelper.ReadShort(bytes, 0x4);
            X = ByteHelper.ReadShort(bytes, 0x6);
            Y = ByteHelper.ReadShort(bytes, 0x8);
            XRot = ByteHelper.ReadShort(bytes, 0xA);
            YRot = ByteHelper.ReadShort(bytes, 0xC);
            ZRot = ByteHelper.ReadShort(bytes, 0xE);
            ID = ByteHelper.ReadUShort(bytes, 0x10);
            Unk = ByteHelper.ReadUShort(bytes, 0x12);

            return true;
        }

        public static int Size { get { return 0x14; } }

        #region IGLRenderable

        public float GL_X { get { return (float)X; } }
        public float GL_Y { get { return (float)Y; } }
        public float GL_Z { get { return (float)Z; } }
        public float GL_XRot { get { return (float)XRot; } }
        public float GL_YRot { get { return (float)YRot; } }
        public float GL_ZRot { get { return (float)ZRot; } }
        public float GL_XScale { get { return 1.0f; } }
        public float GL_YScale { get { return 1.0f; } }
        public float GL_ZScale { get { return 1.0f; } }

        public int GL_DisplayListIndex { 
            get
            {
                if (ID < 0x190)
                    return (int)SF64ROM.Instance.ReferenceDMA.SimpleObjects[ID].DListOffset;
                //NOTE THIS IS WRONG!!
                return 0;
            } 
        }

        #endregion

    }
}
