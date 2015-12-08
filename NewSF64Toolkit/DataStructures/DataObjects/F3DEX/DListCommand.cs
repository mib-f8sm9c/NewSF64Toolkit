using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects.F3DEX
{
    public class DListCommand : IGameDataStructure
    {
        public enum CommandType
        {
            F3DEX_MTX = 0x01,
            F3DEX_MOVEMEM = 0x03,
            F3DEX_VTX = 0x04,
            F3DEX_DL = 0x06,
            F3DEX_BRANCH_Z = 0xB0,
            F3DEX_TRI2 = 0xB1,
            F3DEX_MODIFYVTX = 0xB2,
            F3DEX_RDPHALF_2 = 0xB3,
            F3DEX_RDPHALF_1 = 0xB4,
            F3DEX_CLEARGEOMETRYMODE = 0xB6,
            F3DEX_SETGEOMETRYMODE = 0xB7,
            F3DEX_ENDDL = 0xB8,
            F3DEX_SETOTHERMODE_L = 0xB9,
            F3DEX_SETOTHERMODE_H = 0xBA,
            F3DEX_TEXTURE = 0xBB,
            F3DEX_MOVEWORD = 0xBC,
            F3DEX_POPMTX = 0xBD,
            F3DEX_CULLDL = 0xBE,
            F3DEX_TRI1 = 0xBF,
            G_TEXRECT = 0xE4,
            G_TEXRECTFLIP = 0xE5,
            G_RDPLOADSYNC = 0xE6,
            G_RDPPIPESYNC = 0xE7,
            G_RDPTILESYNC = 0xE8,
            G_RDPFULLSYNC = 0xE9,
            G_SETKEYGB = 0xEA,
            G_SETKEYR = 0xEB,
            G_SETCONVERT = 0xEC,
            G_SETSCISSOR = 0xED,
            G_SETPRIMDEPTH = 0xEE,
            G_RDPSETOTHERMODE = 0xEF,
            G_LOADTLUT = 0xF0,
            G_SETTILESIZE = 0xF2,
            G_LOADBLOCK = 0xF3,
            G_LOADTILE = 0xF4,
            G_SETTILE = 0xF5,
            G_FILLRECT = 0xF6,
            G_SETFILLCOLOR = 0xF7,
            G_SETFOGCOLOR = 0xF8,
            G_SETBLENDCOLOR = 0xF9,
            G_SETPRIMCOLOR = 0xFA,
            G_SETENVCOLOR = 0xFB,
            G_SETCOMBINE = 0xFC,
            G_SETTIMG = 0xFD,
            G_SETZIMG = 0xFE,
            G_SETCIMG = 0xFF,
            UnemulatedCmd = 0x00
        }

        public List<byte[]> CommandData;

        public int Offset;

        public DListCommand(int offset, byte[] data)
        {
            Offset = offset;

            CommandData = new List<byte[]>();

            LoadFromBytes(data);
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length == 0 || bytes.Length % 8 != 0)
                return false;

            byte[] singleLine;

            for (int i = 0; i < bytes.Length; i += 8)
            {
                singleLine = new byte[8];
                Array.Copy(bytes, i, singleLine, 0, 8);
                CommandData.Add(singleLine);
            }

            return true;
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            for (int i = 0; i < CommandData.Count; i++)
            {
                Array.Copy(CommandData[i], 0, bytes, i * 8, 8);
            }

            return bytes;
        }

        public int Size { get { return CommandData.Count * 0x8; } }

        public CommandType GetCommandType()
        {
            if (CommandData.Count == 0)
                return CommandType.UnemulatedCmd;

            return (CommandType)CommandData[0][0];
        }
    }
}
