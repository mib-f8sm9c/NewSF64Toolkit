using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects.F3DEX
{
    public class Texture : IGameDataStructure
    {
        public int Width;
        public int Height;

        public int Offset;

        public byte[] TextureData { get; private set; }

        public Texture(int offset, int width, int height, byte[] data)
        {
            Offset = offset;
            Width = width;
            Height = height;
            LoadFromBytes(data);
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            //To do: find out how the texture is encoded
            //if (Width * Height != bytes.Length)
            //    return false;

            TextureData = bytes;
            return true;
        }

        public byte[] GetAsBytes()
        {
            return TextureData;
        }

        public int Size { get { return TextureData.Length; } }
    }
}
