using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures.DataObjects
{
    public class LevelHeader : IGameDataStructure
    {
        public uint Unk1;
        public uint Unk2;
        public byte Unk3;
        public byte Unk4;
        public byte Unk5;
        public byte BGMTrack;
        public uint FogRed;
        public uint FogGreen;
        public uint FogBlue;
        public uint FogNearDist;
        public uint FogFarDist;
        public float Unk6;
        public float Unk7;
        public float Unk8;
        public uint AmbientLightingRed; //Ambient?
        public uint AmbientLightingGreen; //Ambient?
        public uint AmbientLightingBlue; //Ambient?
        public uint DiffuseLightingRed; //Diffuse?
        public uint DiffuseLightingGreen; //Diffuse?
        public uint DiffuseLightingBlue; //Diffuse?

        public int Offset;

        public LevelHeader(int offset, byte[] bytes)
        {
            Offset = offset;

            LoadFromBytes(bytes);
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[Size];

            ByteHelper.WriteUInt(Unk1, bytes, 0x0);
            ByteHelper.WriteUInt(Unk2, bytes, 0x04);
            ByteHelper.WriteByte(Unk3, bytes, 0x08);
            ByteHelper.WriteByte(Unk4, bytes, 0x09);
            ByteHelper.WriteByte(Unk5, bytes, 0x0A);
            ByteHelper.WriteByte(BGMTrack, bytes, 0x0B);
            ByteHelper.WriteUInt(FogRed, bytes, 0x0C);
            ByteHelper.WriteUInt(FogGreen, bytes, 0x10);
            ByteHelper.WriteUInt(FogBlue, bytes, 0x14);
            ByteHelper.WriteUInt(FogNearDist, bytes, 0x18);
            ByteHelper.WriteUInt(FogFarDist, bytes, 0x1C);
            ByteHelper.WriteFloat(Unk6, bytes, 0x20);
            ByteHelper.WriteFloat(Unk7, bytes, 0x24);
            ByteHelper.WriteFloat(Unk8, bytes, 0x28);
            ByteHelper.WriteUInt(AmbientLightingRed, bytes, 0x2C);
            ByteHelper.WriteUInt(AmbientLightingGreen, bytes, 0x30);
            ByteHelper.WriteUInt(AmbientLightingBlue, bytes, 0x34);
            ByteHelper.WriteUInt(DiffuseLightingRed, bytes, 0x38);
            ByteHelper.WriteUInt(DiffuseLightingGreen, bytes, 0x3C);
            ByteHelper.WriteUInt(DiffuseLightingBlue, bytes, 0x40);

            return bytes;
        }

        public bool LoadFromBytes(byte[] bytes)
        {
            if (bytes.Length != Size)
                return false;

            Unk1 = ByteHelper.ReadUInt(bytes, 0x00);
            Unk2 = ByteHelper.ReadUInt(bytes, 0x04);
            Unk3 = ByteHelper.ReadByte(bytes, 0x08);
            Unk4 = ByteHelper.ReadByte(bytes, 0x09);
            Unk5 = ByteHelper.ReadByte(bytes, 0x0A);
            BGMTrack = ByteHelper.ReadByte(bytes, 0x0B);
            FogRed = ByteHelper.ReadUInt(bytes, 0x0C);
            FogGreen = ByteHelper.ReadUInt(bytes, 0x10);
            FogBlue = ByteHelper.ReadUInt(bytes, 0x14);
            FogNearDist = ByteHelper.ReadUInt(bytes, 0x18);
            FogFarDist = ByteHelper.ReadUInt(bytes, 0x1C);
            Unk6 = ByteHelper.ReadFloat(bytes, 0x20);
            Unk7 = ByteHelper.ReadFloat(bytes, 0x24);
            Unk8 = ByteHelper.ReadFloat(bytes, 0x28);
            AmbientLightingRed = ByteHelper.ReadUInt(bytes, 0x2C);
            AmbientLightingGreen = ByteHelper.ReadUInt(bytes, 0x30);
            AmbientLightingBlue = ByteHelper.ReadUInt(bytes, 0x34);
            DiffuseLightingRed = ByteHelper.ReadUInt(bytes, 0x38);
            DiffuseLightingGreen = ByteHelper.ReadUInt(bytes, 0x3C);
            DiffuseLightingBlue = ByteHelper.ReadUInt(bytes, 0x40);

            return true;
        }

        public static int Size { get { return 0x44; } }

    }
}
