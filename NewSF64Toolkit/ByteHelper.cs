using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.Settings;

namespace NewSF64Toolkit
{
    public enum Endianness
    {
        BigEndian,
        LittleEndian,
        ByteSwap,
        WordSwap
    }

    public static class ByteHelper
    {
        public static string DisplayValue(uint val)
        {
            if (ToolSettings.Instance.DisplayInHex)
            {
                return string.Format("0x{0:X}", val);
            }

            return val.ToString();
        }

        #region Byte[] to Byte/Char/Short/UShort/Int/UInt/Float

        public static uint ReadUInt(byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            return ReadUInt(data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static uint ReadUInt(byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length < position + 4)
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
            if (data == null || data.Length < position + 4)
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
            if (data == null || data.Length < position + 2)
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
            if (data == null || data.Length < position + 2)
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
            if (data == null || data.Length < position)
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
            if (data == null || data.Length < position + 4)
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
            if (data == null || data.Length < position + 4)
                throw new Exception();


            data[position] = (byte)(value >> 24 & 0xFF);
            data[position + 1] = (byte)(value >> 16 & 0xFF);
            data[position + 2] = (byte)(value >> 8 & 0xFF);
            data[position + 3] = (byte)(value & 0xFF);

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

        public static void WriteInt(int value, byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            WriteInt(value, data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static void WriteInt(int value, byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length < position + 4)
                throw new Exception();


            data[position] = (byte)(value >> 24 & 0xFF);
            data[position + 1] = (byte)(value >> 16 & 0xFF);
            data[position + 2] = (byte)(value >> 8 & 0xFF);
            data[position + 3] = (byte)(value & 0xFF);

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

        public static void WriteUShort(ushort value, byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            WriteUShort(value, data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static void WriteUShort(ushort value, byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length < position + 2)
                throw new Exception();


            data[position] = (byte)(value >> 8 & 0xFF);
            data[position + 1] = (byte)(value & 0xFF);

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

        public static void WriteShort(short value, byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            WriteShort(value, data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static void WriteShort(short value, byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length < position + 2)
                throw new Exception();


            data[position] = (byte)(value >> 8 & 0xFF);
            data[position + 1] = (byte)(value & 0xFF);

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

        public static void WriteByte(byte value, byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            WriteByte(value, data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static void WriteByte(byte value, byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length < position)
                throw new Exception();


            data[position] = (byte)(value & 0xFF);

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

        public static void WriteFloat(float value, byte[] data, uint position, Endianness endian = Endianness.BigEndian)
        {
            WriteFloat(value, data, (int)position, endian);
        }

        //THIS IS UNFINISHED BECAUSE I CAN'T TEST IT. TRY IT LATER
        public static void WriteFloat(float value, byte[] data, int position, Endianness endian = Endianness.BigEndian)
        {
            if (data == null || data.Length < position + 4)
                throw new Exception();

            byte[] bytes = BitConverter.GetBytes(value);
            bytes = bytes.Reverse().ToArray();

            data[position] = bytes[0];
            data[position + 1] = bytes[1];
            data[position + 2] = bytes[2];
            data[position + 3] = bytes[3];

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
