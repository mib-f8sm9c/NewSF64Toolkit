﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit
{
    static public class StarFoxRomInfo
    {
        static private ROMInfo[] VALID_GAME_VERSIONS =
        {
            new ROMInfo("A","NFXJ", 0, 0xFFCAA7C1, 0x68858537, 0x0E93C0),
            new ROMInfo("Star Fox 64 (U) [v1.0]",	"NFXE", 0, 0xA7D015F8, 0x2289AA43, 0x0D9A90),
            new ROMInfo("Star Fox 64 (U) [v1.1]",	"NFXE", 1, 0xBA780BA0, 0x0F21DB34, 0x0DE480),
            new ROMInfo("Lylat Wars (E)",			"NFXP", 0, 0xF4CBE92C, 0xB392ED12, 0x0E0570),
            new ROMInfo("Lylat Wars (A)",			"NFXU", 0, 0x2483F22B, 0x136E025E, 0x0E0470)
        };

        public static uint[] EndiannessMarkers = { 0x80371240, 0x40123780, 0x37804012, 0x12408037 };

        static public uint GetDMATableOffset(string gameID, byte version)
        {
            //Do more work here, check against the CRC, etc.
            ROMInfo matchingInfo = VALID_GAME_VERSIONS.SingleOrDefault(v => v.GameID == gameID && v.Version == version);

            if (matchingInfo.GameID == gameID)
                return matchingInfo.DMATableOffset;

            return 0x0;
        }

        static public bool IsValidVersion(ROMInfo info)
        {
            //Do more work here, check against the CRC, etc.
            return VALID_GAME_VERSIONS.Count(v => v.GameID == info.GameID && v.Version == info.Version) > 0;
        }

        static public Endianness GetEndianness(uint endianBytes)
        {
            return EndiannessMarkers.Contains(endianBytes) ? (Endianness)EndiannessMarkers.ToList().IndexOf(endianBytes) : Endianness.BigEndian;
        }
    }
}
