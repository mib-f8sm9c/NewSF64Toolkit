using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class RAMTableEntry : IGameDataStructure
    {
        public uint OverlayStart;
        public uint Loc1Start;
        public uint Loc2Start;
        public uint Loc3Start;
        public uint Seg1Start;
        public uint Seg2Start;
        public uint Seg3Start;
        public uint Seg4Start;
        public uint Seg5Start;
        public uint Seg6Start;
        public uint Seg7Start;
        public uint Seg8Start;
        public uint Seg9Start;
        public uint SegAStart;
        public uint SegBStart;
        public uint SegCStart;
        public uint SegDStart;
        public uint SegEStart;
        public uint SegFStart;

        public uint OverlayEnd;
        public uint Loc1End;
        public uint Loc2End;
        public uint Loc3End;
        public uint Seg1End;
        public uint Seg2End;
        public uint Seg3End;
        public uint Seg4End;
        public uint Seg5End;
        public uint Seg6End;
        public uint Seg7End;
        public uint Seg8End;
        public uint Seg9End;
        public uint SegAEnd;
        public uint SegBEnd;
        public uint SegCEnd;
        public uint SegDEnd;
        public uint SegEEnd;
        public uint SegFEnd;

        public int Offset;

        public RAMTableEntry(int offset, byte[] bytes)
        {
            Offset = offset;

            LoadFromBytes(bytes);
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            ByteHelper.WriteUInt(OverlayStart, bytes, 0x0);
            ByteHelper.WriteUInt(OverlayEnd, bytes, 0x4);
            ByteHelper.WriteUInt(Loc1Start, bytes, 0x8);
            ByteHelper.WriteUInt(Loc1End, bytes, 0xC);
            ByteHelper.WriteUInt(Loc2Start, bytes, 0x10);
            ByteHelper.WriteUInt(Loc2End, bytes, 0x14);
            ByteHelper.WriteUInt(Loc3Start, bytes, 0x18);
            ByteHelper.WriteUInt(Loc3End, bytes, 0x1C);
            ByteHelper.WriteUInt(Seg1Start, bytes, 0x20);
            ByteHelper.WriteUInt(Seg1End, bytes, 0x24);
            ByteHelper.WriteUInt(Seg2Start, bytes, 0x28);
            ByteHelper.WriteUInt(Seg2End, bytes, 0x2C);
            ByteHelper.WriteUInt(Seg3Start, bytes, 0x30);
            ByteHelper.WriteUInt(Seg3End, bytes, 0x34);
            ByteHelper.WriteUInt(Seg4Start, bytes, 0x38);
            ByteHelper.WriteUInt(Seg4End, bytes, 0x3C);
            ByteHelper.WriteUInt(Seg5Start, bytes, 0x40);
            ByteHelper.WriteUInt(Seg5End, bytes, 0x44);
            ByteHelper.WriteUInt(Seg6Start, bytes, 0x48);
            ByteHelper.WriteUInt(Seg6End, bytes, 0x4C);
            ByteHelper.WriteUInt(Seg7Start, bytes, 0x50);
            ByteHelper.WriteUInt(Seg7End, bytes, 0x54);
            ByteHelper.WriteUInt(Seg8Start, bytes, 0x58);
            ByteHelper.WriteUInt(Seg8End, bytes, 0x5C);
            ByteHelper.WriteUInt(Seg9Start, bytes, 0x60);
            ByteHelper.WriteUInt(Seg9End, bytes, 0x64);
            ByteHelper.WriteUInt(SegAStart, bytes, 0x68);
            ByteHelper.WriteUInt(SegAEnd, bytes, 0x6C);
            ByteHelper.WriteUInt(SegBStart, bytes, 0x70);
            ByteHelper.WriteUInt(SegBEnd, bytes, 0x74);
            ByteHelper.WriteUInt(SegCStart, bytes, 0x78);
            ByteHelper.WriteUInt(SegCEnd, bytes, 0x7C);
            ByteHelper.WriteUInt(SegDStart, bytes, 0x80);
            ByteHelper.WriteUInt(SegDEnd, bytes, 0x84);
            ByteHelper.WriteUInt(SegEStart, bytes, 0x88);
            ByteHelper.WriteUInt(SegEEnd, bytes, 0x8C);
            ByteHelper.WriteUInt(SegFStart, bytes, 0x90);
            ByteHelper.WriteUInt(SegFEnd, bytes, 0x94);

            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            OverlayStart = ByteHelper.ReadUInt(bytes, 0x0);
            OverlayEnd = ByteHelper.ReadUInt(bytes, 0x4);
            Loc1Start = ByteHelper.ReadUInt(bytes, 0x8);
            Loc1End = ByteHelper.ReadUInt(bytes, 0xC);
            Loc2Start = ByteHelper.ReadUInt(bytes, 0x10);
            Loc2End = ByteHelper.ReadUInt(bytes, 0x14);
            Loc3Start = ByteHelper.ReadUInt(bytes, 0x18);
            Loc3End = ByteHelper.ReadUInt(bytes, 0x1C);
            Seg1Start = ByteHelper.ReadUInt(bytes, 0x20);
            Seg1End = ByteHelper.ReadUInt(bytes, 0x24);
            Seg2Start = ByteHelper.ReadUInt(bytes, 0x28);
            Seg2End = ByteHelper.ReadUInt(bytes, 0x2C);
            Seg3Start = ByteHelper.ReadUInt(bytes, 0x30);
            Seg3End = ByteHelper.ReadUInt(bytes, 0x34);
            Seg4Start = ByteHelper.ReadUInt(bytes, 0x38);
            Seg4End = ByteHelper.ReadUInt(bytes, 0x3C);
            Seg5Start = ByteHelper.ReadUInt(bytes, 0x40);
            Seg5End = ByteHelper.ReadUInt(bytes, 0x44);
            Seg6Start = ByteHelper.ReadUInt(bytes, 0x48);
            Seg6End = ByteHelper.ReadUInt(bytes, 0x4C);
            Seg7Start = ByteHelper.ReadUInt(bytes, 0x50);
            Seg7End = ByteHelper.ReadUInt(bytes, 0x54);
            Seg8Start = ByteHelper.ReadUInt(bytes, 0x58);
            Seg8End = ByteHelper.ReadUInt(bytes, 0x5C);
            Seg9Start = ByteHelper.ReadUInt(bytes, 0x60);
            Seg9End = ByteHelper.ReadUInt(bytes, 0x64);
            SegAStart = ByteHelper.ReadUInt(bytes, 0x68);
            SegAEnd = ByteHelper.ReadUInt(bytes, 0x6C);
            SegBStart = ByteHelper.ReadUInt(bytes, 0x70);
            SegBEnd = ByteHelper.ReadUInt(bytes, 0x74);
            SegCStart = ByteHelper.ReadUInt(bytes, 0x78);
            SegCEnd = ByteHelper.ReadUInt(bytes, 0x7C);
            SegDStart = ByteHelper.ReadUInt(bytes, 0x80);
            SegDEnd = ByteHelper.ReadUInt(bytes, 0x84);
            SegEStart = ByteHelper.ReadUInt(bytes, 0x88);
            SegEEnd = ByteHelper.ReadUInt(bytes, 0x8C);
            SegFStart = ByteHelper.ReadUInt(bytes, 0x90);
            SegFEnd = ByteHelper.ReadUInt(bytes, 0x94);
            
            return true;
        }

        public static int Size { get { return 0x98; } }

    }
}
