using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit
{
    public enum Endianness
    {
        BigEndian,
        LittleEndian,
        ByteSwap,
        WordSwap
    }

    public class ToolSettings
    {
        public static bool DisplayInHex = true;

        public static string DisplayValue(uint val)
        {
            if (DisplayInHex)
            {
                return string.Format("0x{0:X}", val);
            }

            return val.ToString();
        }

        public static string DisplayValue(int val)
        {
            if (DisplayInHex)
            {
                return string.Format("0x{0:X}", val);
            }

            return val.ToString();
        }
        
        private struct MIO0Header
        {
	        public uint ID;			// always "MIO0"
            public uint OutputSize;	// decompressed data size
            public uint CompLoc;		// compressed data loc
            public uint RawLoc;		// uncompressed data loc
        }

        // MIO0 decompression code by HyperHacker (adapted from SF64Toolkit)
        public static bool DecompressMIO0(byte[] data, out byte[] outputData)
        {
            MIO0Header Header;
	        byte MapByte = 0x80, CurMapByte, Length;
	        ushort SData, Dist;
	        uint NumBytesOutput = 0;
            uint MapLoc = 0;	// current compression map position
            uint CompLoc = 0;	// current compressed data position
            uint RawLoc = 0;	// current raw data position
            uint OutLoc = 0;	// current output position

            outputData = null;

	        int i;

	        Header.ID = ReadUInt(data, 0);
	        Header.OutputSize = ReadUInt(data, 4);
	        Header.CompLoc = ReadUInt(data, 8);
	        Header.RawLoc = ReadUInt(data, 12);

	        // "MIO0"
	        if(Header.ID != 0x4D494F30) {
		        return false;
	        }

	        byte[] MIO0Buffer = new byte[Header.OutputSize];

	        MapLoc = 0x10;
	        CompLoc = Header.CompLoc;
	        RawLoc = Header.RawLoc;

	        CurMapByte = data[MapLoc];

	        while(NumBytesOutput < Header.OutputSize) {
		        // raw
		        if((CurMapByte & MapByte) != 0x0) {
			        MIO0Buffer[OutLoc] = data[RawLoc]; // copy a byte to output.
			        OutLoc++;
			        RawLoc++;
			        NumBytesOutput++;
		        }

		        // compressed
		        else {
			        SData = ReadUShort(data, CompLoc); // get compressed data
			        Length = (byte)((SData >> 12) + 3);
			        Dist = (ushort)((SData & 0xFFF) + 1);

			        // sanity check: can't copy from before first byte
			        if(((int)OutLoc - Dist) < 0) {
                        return false;
			        }

			        // copy from output
			        for(i = 0; i < Length; i++) {
				        MIO0Buffer[OutLoc] = MIO0Buffer[OutLoc - Dist];
				        OutLoc++;
				        NumBytesOutput++;
				        if(NumBytesOutput >= Header.OutputSize) break;
			        }
			        CompLoc += 2;
		        }

		        MapByte >>= 1; // next map bit

		        // if we did them all, get the next map byte
		        if(MapByte == 0x0) {
			        MapByte = 0x80;
			        MapLoc++;
			        CurMapByte = data[MapLoc];

			        // sanity check: map pointer should never reach this
			        int Check = (int)CompLoc;
			        if(RawLoc < CompLoc) Check = (int)RawLoc;

			        if(MapLoc > Check) {
                        return false;
			        }
		        }
	        }

            outputData = MIO0Buffer;

	        return true;
        }

        #region Byte[] to Byte/Char/Short/UShort/Int/UInt/Float

        public static uint ReadUInt(byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            return ReadUInt(data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static uint ReadUInt(byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length <= position + 4)
                throw new Exception();

            byte[] bytes = new byte[4];
            Array.Copy(data, position, bytes, 0, 4);
            bytes = bytes.Reverse().ToArray();
            return BitConverter.ToUInt32(bytes, 0);

            switch (endian)
            {
                case Endianness.BigEndian:
                    //Do nothing
                    break;
                case Endianness.LittleEndian:
                    //Reverse
                    break;
                case Endianness.ByteSwap:

                    break;
                case Endianness.WordSwap:

                    break;
            }

            return 0;
        }

        public static int ReadInt(byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            return ReadInt(data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static int ReadInt(byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length <= position + 4)
                throw new Exception();

            byte[] bytes = new byte[4];
            Array.Copy(data, position, bytes, 0, 4);
            bytes = bytes.Reverse().ToArray();
            return BitConverter.ToInt32(bytes, 0);

            switch (endian)
            {
                case Endianness.BigEndian:
                    //Do nothing
                    break;
                case Endianness.LittleEndian:
                    //Reverse
                    break;
                case Endianness.ByteSwap:

                    break;
                case Endianness.WordSwap:

                    break;
            }

            return 0;
        }

        public static ushort ReadUShort(byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            return ReadUShort(data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static ushort ReadUShort(byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length <= position + 2)
                throw new Exception();

            byte[] bytes = new byte[2];
            Array.Copy(data, position, bytes, 0, 2);
            bytes = bytes.Reverse().ToArray();
            return BitConverter.ToUInt16(bytes, 0);

            switch (endian)
            {
                case Endianness.BigEndian:
                    //Do nothing
                    break;
                case Endianness.LittleEndian:
                    //Reverse
                    break;
                case Endianness.ByteSwap:

                    break;
                case Endianness.WordSwap:

                    break;
            }

            return 0;
        }

        public static short ReadShort(byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            return ReadShort(data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static short ReadShort(byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length <= position + 2)
                throw new Exception();

            byte[] bytes = new byte[2];
            Array.Copy(data, position, bytes, 0, 2);
            bytes = bytes.Reverse().ToArray();
            return BitConverter.ToInt16(bytes, 0);

            switch (endian)
            {
                case Endianness.BigEndian:
                    //Do nothing
                    break;
                case Endianness.LittleEndian:
                    //Reverse
                    break;
                case Endianness.ByteSwap:

                    break;
                case Endianness.WordSwap:

                    break;
            }

            return 0;
        }

        public static byte ReadByte(byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            return ReadByte(data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static byte ReadByte(byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length <= position)
                throw new Exception();

            return data[position];
        }

        public static float ReadFloat(byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            return ReadFloat(data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static float ReadFloat(byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length <= position + 4)
                throw new Exception();

            byte[] bytes = new byte[4];
            Array.Copy(data, position, bytes, 0, 4);
            bytes = bytes.Reverse().ToArray();
            return BitConverter.ToSingle(bytes, 0);

            switch (endian)
            {
                case Endianness.BigEndian:
                    //Do nothing
                    break;
                case Endianness.LittleEndian:
                    //Reverse
                    break;
                case Endianness.ByteSwap:

                    break;
                case Endianness.WordSwap:

                    break;
            }

            return 0;
        }

        public static void WriteUInt(uint value, byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            WriteUInt(value, data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static void WriteUInt(uint value, byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length <= position + 4)
                throw new Exception();


            data[position] = (byte)(value >> 24 & 0xFF);
            data[position+1] = (byte)(value >> 16 & 0xFF);
            data[position+2] = (byte)(value >> 8 & 0xFF);
            data[position+3] = (byte)(value & 0xFF);

            return;

            switch (endian)
            {
                case Endianness.BigEndian:
                    //Do nothing
                    break;
                case Endianness.LittleEndian:
                    //Reverse
                    break;
                case Endianness.ByteSwap:

                    break;
                case Endianness.WordSwap:

                    break;
            }

            return;
        }

        #endregion

    }
}
