/* snesrc - SNES Recompiler
 *
 * Copyright notice for this file:
 *  Copyright (C) 2005 Parasyte
 *
 * Modified March/April 2010 by xdaniel for SF64Toolkit
 *          July 2015 by mib_f8sm9c for NewSF64Toolkit
 *
 * Based on uCON64's N64 checksum algorithm by Andreas Sterbenz
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace NewSF64Toolkit
{
    static public class N64Sums
    {
        static private uint ROL(uint i, int b)
        {
            return (((i) << (b)) | ((i) >> (32 - (b))));
        }

        static private uint BYTES2LONG(byte[] b, int index)
        {
            return (uint)((b)[index] << 24 |
                    (b)[index + 1] << 16 |
                    (b)[index + 2] << 8 |
                    (b)[index + 3]);
        }

        private static ushort N64_HEADER_SIZE = 0x40;
        private static ushort N64_BC_SIZE = (ushort)(0x1000 - N64_HEADER_SIZE);

        private static ushort N64_CRC1 = 0x10;
        private static ushort N64_CRC2 = 0x14;

        private static uint CHECKSUM_START = 0x00001000;
        private static uint CHECKSUM_LENGTH = 0x00100000;
        private static uint CHECKSUM_CIC6102 = 0xF8CA4DDC;
        private static uint CHECKSUM_CIC6103 = 0xA3886759;
        private static uint CHECKSUM_CIC6105 = 0xDF26F436;
        private static uint CHECKSUM_CIC6106 = 0x1FEA617A;

        private static uint[] crc_table = null;

        static private void gen_table()
        {
            if(crc_table != null)
                return;

            crc_table = new uint[256];

	        uint crc, poly;
	        int	i, j;

	        poly = 0xEDB88320;
	        for (i = 0; i < 256; i++) {
		        crc = (uint)i;
		        for (j = 8; j > 0; j--) {
			        if ((crc & 1) != 0) crc = (crc >> 1) ^ poly;
			        else crc >>= 1;
		        }
		        crc_table[i] = crc;
	        }
        }

        static private uint crc32(byte[] data, int offset, int len)
        {
	        uint crc = ~(uint)0;
	        int i;

            for (i = offset; i < offset + len; i++)
            {
		        crc = (crc >> 8) ^ crc_table[(crc ^ data[i]) & 0xFF];
	        }

	        return ~crc;
        }

        static private int N64GetCIC(byte[] data)
        {
	        switch (crc32(data, N64_HEADER_SIZE, N64_BC_SIZE)) {
		        case 0x6170A4A1: return 6101;
		        case 0x90BB6CB5: return 6102;
		        case 0x0B050EE0: return 6103;
		        case 0x98BC2C86: return 6105;
		        case 0xACC8580A: return 6106;
		        default: return 6102;			// works for lylat wars pal
	        }

	        return 0;
        }

        static private bool N64CalcCRC(uint[] crc, byte[] data)
        {
	        int bootcode, i;
	        uint seed;

	        uint t1, t2, t3;
	        uint t4, t5, t6;
	        uint r, d;

	        switch ((bootcode = N64GetCIC(data))) {
		        case 6101:
		        case 6102:
			        seed = CHECKSUM_CIC6102;
			        break;
		        case 6103:
			        seed = CHECKSUM_CIC6103;
			        break;
		        case 6105:
			        seed = CHECKSUM_CIC6105;
			        break;
		        case 6106:
			        seed = CHECKSUM_CIC6106;
			        break;
		        default:
			        return false;
	        }

	        t1 = t2 = t3 = t4 = t5 = t6 = seed;

	        i = (int)CHECKSUM_START;
	        while (i < (CHECKSUM_START + CHECKSUM_LENGTH)) {
		        d = BYTES2LONG(data, i);
		        if ((t6 + d) < t6) t4++;
		        t6 += d;
		        t3 ^= d;
		        r = ROL(d, (int)(d & 0x1F));
		        t5 += r;
		        if (t2 > d) t2 ^= r;
		        else t2 ^= t6 ^ d;

                if (bootcode == 6105) t1 += BYTES2LONG(data, N64_HEADER_SIZE + 0x0710 + (i & 0xFF)) ^ d;
		        else t1 += t5 ^ d;

		        i += 4;
	        }
	        if (bootcode == 6103) {
		        crc[0] = (t6 ^ t4) + t3;
		        crc[1] = (t5 ^ t2) + t1;
	        }
	        else if (bootcode == 6106) {
		        crc[0] = (t6 * t4) + t3;
		        crc[1] = (t5 * t2) + t1;
	        }
	        else {
		        crc[0] = t6 ^ t4 ^ t3;
		        crc[1] = t5 ^ t2 ^ t1;
	        }

	        return true;
        }

        static public bool FixChecksum(byte[] ROMBuffer)
        {
	        int cic;
	        uint[] crc = new uint[2];
	        byte[] buffer;

	        //Init CRC algorithm
	        gen_table();

	        //Allocate memory
            buffer = new byte[CHECKSUM_START + CHECKSUM_LENGTH];
	        
	        //Read data
            Array.Copy(ROMBuffer, buffer, CHECKSUM_START + CHECKSUM_LENGTH);

	        //Check CIC BootChip
	        cic = N64GetCIC(buffer);
            string CICText = string.Format("CIC-NUS-{0}", cic);

	        //Calculate CRC
	        if(!N64CalcCRC(crc, buffer)) {
		        //MSK_ConsolePrint(MSK_COLORTYPE_ERROR, "- Error: Unable to calculate CRC\n");
                return false;
	        } else {
		        //MSK_ConsolePrint(MSK_COLORTYPE_INFO, " - CRC 1: 0x%08X, calculated: 0x%08X...", BYTES2LONG(&buffer[N64_CRC1]), crc[0]);
                if (crc[0] == BYTES2LONG(buffer, N64_CRC1))
                    ;//MSK_ConsolePrint(MSK_COLORTYPE_OKAY, "  - CRC is good, no need to fix.\n");
                else
                {
                    //MSK_ConsolePrint(MSK_COLORTYPE_WARNING, "  - CRC is bad, fixing...\n");
                    Write32(ROMBuffer, N64_CRC1, crc[0]);
                }

		        //MSK_ConsolePrint(MSK_COLORTYPE_INFO, " - CRC 2: 0x%08X, calculated: 0x%08X...", BYTES2LONG(&buffer[N64_CRC2]), crc[1]);
		        if (crc[1] == BYTES2LONG(buffer, N64_CRC2))
			        ;//MSK_ConsolePrint(MSK_COLORTYPE_OKAY, "  - CRC is good, no need to fix.\n");
		        else {
			        //MSK_ConsolePrint(MSK_COLORTYPE_WARNING, "  - CRC is bad, fixing...\n");
			        Write32(ROMBuffer, N64_CRC2, crc[1]);
		        }
	        }

	        return true;
        }


        static private void Write32(byte[] data, int index, uint value)
        {
            //Need endianness to be calculated too later
            data[index] = (byte)((value >> 24) & 0xFF);
            data[index + 1] = (byte)((value >> 16) & 0xFF);
            data[index + 2] = (byte)((value >> 8) & 0xFF);
            data[index + 3] = (byte)(value & 0xFF);
        }
    }
}
