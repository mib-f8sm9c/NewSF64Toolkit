using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class RAMTableEntry : IGameDataStructure
    {
        public uint OverlayRAM;
        public uint Loc1RAM;
        public uint Loc2RAM;
        public uint Loc3RAM;
        public uint Seg1RAM;
        public uint Seg2RAM;
        public uint Seg3RAM;
        public uint Seg4RAM;
        public uint Seg5RAM;
        public uint Seg6RAM;
        public uint Seg7RAM;
        public uint Seg8RAM;
        public uint Seg9RAM;
        public uint SegARAM;
        public uint SegBRAM;
        public uint SegCRAM;
        public uint SegDRAM;
        public uint SegERAM;
        public uint SegFRAM;

        public uint OverlayROM;
        public uint Loc1ROM;
        public uint Loc2ROM;
        public uint Loc3ROM;
        public uint Seg1ROM;
        public uint Seg2ROM;
        public uint Seg3ROM;
        public uint Seg4ROM;
        public uint Seg5ROM;
        public uint Seg6ROM;
        public uint Seg7ROM;
        public uint Seg8ROM;
        public uint Seg9ROM;
        public uint SegAROM;
        public uint SegBROM;
        public uint SegCROM;
        public uint SegDROM;
        public uint SegEROM;
        public uint SegFROM;

        public int Offset;

        public RAMTableEntry(int offset, byte[] bytes)
        {
            Offset = offset;

            LoadFromBytes(bytes);
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            ByteHelper.WriteUInt(OverlayRAM, bytes, 0x0);
            ByteHelper.WriteUInt(OverlayROM, bytes, 0x4);
            ByteHelper.WriteUInt(Loc1RAM, bytes, 0x8);
            ByteHelper.WriteUInt(Loc1ROM, bytes, 0xC);
            ByteHelper.WriteUInt(Loc2RAM, bytes, 0x10);
            ByteHelper.WriteUInt(Loc2ROM, bytes, 0x14);
            ByteHelper.WriteUInt(Loc3RAM, bytes, 0x18);
            ByteHelper.WriteUInt(Loc3ROM, bytes, 0x1C);
            ByteHelper.WriteUInt(Seg1RAM, bytes, 0x20);
            ByteHelper.WriteUInt(Seg1ROM, bytes, 0x24);
            ByteHelper.WriteUInt(Seg2RAM, bytes, 0x28);
            ByteHelper.WriteUInt(Seg2ROM, bytes, 0x2C);
            ByteHelper.WriteUInt(Seg3RAM, bytes, 0x30);
            ByteHelper.WriteUInt(Seg3ROM, bytes, 0x34);
            ByteHelper.WriteUInt(Seg4RAM, bytes, 0x38);
            ByteHelper.WriteUInt(Seg4ROM, bytes, 0x3C);
            ByteHelper.WriteUInt(Seg5RAM, bytes, 0x40);
            ByteHelper.WriteUInt(Seg5ROM, bytes, 0x44);
            ByteHelper.WriteUInt(Seg6RAM, bytes, 0x48);
            ByteHelper.WriteUInt(Seg6ROM, bytes, 0x4C);
            ByteHelper.WriteUInt(Seg7RAM, bytes, 0x50);
            ByteHelper.WriteUInt(Seg7ROM, bytes, 0x54);
            ByteHelper.WriteUInt(Seg8RAM, bytes, 0x58);
            ByteHelper.WriteUInt(Seg8ROM, bytes, 0x5C);
            ByteHelper.WriteUInt(Seg9RAM, bytes, 0x60);
            ByteHelper.WriteUInt(Seg9ROM, bytes, 0x64);
            ByteHelper.WriteUInt(SegARAM, bytes, 0x68);
            ByteHelper.WriteUInt(SegAROM, bytes, 0x6C);
            ByteHelper.WriteUInt(SegBRAM, bytes, 0x70);
            ByteHelper.WriteUInt(SegBROM, bytes, 0x74);
            ByteHelper.WriteUInt(SegCRAM, bytes, 0x78);
            ByteHelper.WriteUInt(SegCROM, bytes, 0x7C);
            ByteHelper.WriteUInt(SegDRAM, bytes, 0x80);
            ByteHelper.WriteUInt(SegDROM, bytes, 0x84);
            ByteHelper.WriteUInt(SegERAM, bytes, 0x88);
            ByteHelper.WriteUInt(SegEROM, bytes, 0x8C);
            ByteHelper.WriteUInt(SegFRAM, bytes, 0x90);
            ByteHelper.WriteUInt(SegFROM, bytes, 0x94);

            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            OverlayRAM = ByteHelper.ReadUInt(bytes, 0x0);
            OverlayROM = ByteHelper.ReadUInt(bytes, 0x4);
            Loc1RAM = ByteHelper.ReadUInt(bytes, 0x8);
            Loc1ROM = ByteHelper.ReadUInt(bytes, 0xC);
            Loc2RAM = ByteHelper.ReadUInt(bytes, 0x10);
            Loc2ROM = ByteHelper.ReadUInt(bytes, 0x14);
            Loc3RAM = ByteHelper.ReadUInt(bytes, 0x18);
            Loc3ROM = ByteHelper.ReadUInt(bytes, 0x1C);
            Seg1RAM = ByteHelper.ReadUInt(bytes, 0x20);
            Seg1ROM = ByteHelper.ReadUInt(bytes, 0x24);
            Seg2RAM = ByteHelper.ReadUInt(bytes, 0x28);
            Seg2ROM = ByteHelper.ReadUInt(bytes, 0x2C);
            Seg3RAM = ByteHelper.ReadUInt(bytes, 0x30);
            Seg3ROM = ByteHelper.ReadUInt(bytes, 0x34);
            Seg4RAM = ByteHelper.ReadUInt(bytes, 0x38);
            Seg4ROM = ByteHelper.ReadUInt(bytes, 0x3C);
            Seg5RAM = ByteHelper.ReadUInt(bytes, 0x40);
            Seg5ROM = ByteHelper.ReadUInt(bytes, 0x44);
            Seg6RAM = ByteHelper.ReadUInt(bytes, 0x48);
            Seg6ROM = ByteHelper.ReadUInt(bytes, 0x4C);
            Seg7RAM = ByteHelper.ReadUInt(bytes, 0x50);
            Seg7ROM = ByteHelper.ReadUInt(bytes, 0x54);
            Seg8RAM = ByteHelper.ReadUInt(bytes, 0x58);
            Seg8ROM = ByteHelper.ReadUInt(bytes, 0x5C);
            Seg9RAM = ByteHelper.ReadUInt(bytes, 0x60);
            Seg9ROM = ByteHelper.ReadUInt(bytes, 0x64);
            SegARAM = ByteHelper.ReadUInt(bytes, 0x68);
            SegAROM = ByteHelper.ReadUInt(bytes, 0x6C);
            SegBRAM = ByteHelper.ReadUInt(bytes, 0x70);
            SegBROM = ByteHelper.ReadUInt(bytes, 0x74);
            SegCRAM = ByteHelper.ReadUInt(bytes, 0x78);
            SegCROM = ByteHelper.ReadUInt(bytes, 0x7C);
            SegDRAM = ByteHelper.ReadUInt(bytes, 0x80);
            SegDROM = ByteHelper.ReadUInt(bytes, 0x84);
            SegERAM = ByteHelper.ReadUInt(bytes, 0x88);
            SegEROM = ByteHelper.ReadUInt(bytes, 0x8C);
            SegFRAM = ByteHelper.ReadUInt(bytes, 0x90);
            SegFROM = ByteHelper.ReadUInt(bytes, 0x94);
            
            return true;
        }

        public static int Size { get { return 0x98; } }

    }
}
