using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class SFAdvancedObjectScript : IGameDataStructure
    {
        public int Offset;

        public uint[] Script;

        public int ScriptIndex;
        public int AdvancedObjectIndex;

        public SFAdvancedObjectScript(int offset, int scriptIndex, byte[] bytes)
        {
            Offset = offset;
            ScriptIndex = scriptIndex;

            AdvancedObjectIndex = -1;
            LoadFromBytes(bytes);
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            for (int i = 0; i < Script.Length; i++)
            {
                ByteHelper.WriteUInt(Script[i], bytes, i * 0x4);
            }
            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            int count = bytes.Length / 4;

            Script = new uint[count];

            for (int i = 0; i < count; i++)
            {
                Script[i] = ByteHelper.ReadUInt(bytes, i * 4);

                if (AdvancedObjectIndex == -1 && (Script[i] & 0xFF000000) == 0xD0000000)
                {
                    AdvancedObjectIndex = (int)(Script[i] & 0x000000FF);
                }
            }

            return true;
        }

        public int Size { get { return Script.Length * sizeof(uint); } }

    }
}
