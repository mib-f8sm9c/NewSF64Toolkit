﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures
{
    public class HeaderDMAFile : DMAFile
    {
        public string Title;
        public string GameID;
        public byte Version;
        public Endianness RomEndianness;
        public uint CRC1;
        public uint CRC2;

        public HeaderDMAFile(byte[] data)
            : base(data)
        {

        }

        public override void LoadFromBytes(byte[] bytes)
        {
            base.LoadFromBytes(bytes);

            Title = System.Text.Encoding.UTF8.GetString(bytes, 32, 20);
            GameID = System.Text.Encoding.UTF8.GetString(bytes, 59, 4);
            Version = bytes[63];
            RomEndianness = StarFoxRomInfo.GetEndianness(ByteHelper.ReadUInt(bytes, 0));

            CRC1 = ByteHelper.ReadUInt(bytes, 16);
            CRC2 = ByteHelper.ReadUInt(bytes, 20);
        }

        public override byte[] GetAsBytes()
        {
            byte[] bytes = base.GetAsBytes();


            Array.Copy(System.Text.Encoding.UTF8.GetBytes(Title), 0, bytes, 32, 20);//32, 20);
            Array.Copy(System.Text.Encoding.UTF8.GetBytes(GameID), 0, bytes, 59, 4);//59, 4);
            bytes[63] = Version;
            
            ByteHelper.WriteUInt(StarFoxRomInfo.GetEndianness(RomEndianness), bytes, 0);
            ByteHelper.WriteUInt(CRC1, bytes, 16);
            ByteHelper.WriteUInt(CRC2, bytes, 20);

            return bytes;
        }

    }
}