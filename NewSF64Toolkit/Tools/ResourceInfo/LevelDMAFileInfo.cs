using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using System.ComponentModel;
using System.Drawing;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class LevelDMAFileInfo : DMAFileInfo
    {
        private LevelDMAFile _dma;

        public LevelDMAFileInfo(LevelDMAFile dma)
            : base(dma)
        {
            _dma = dma;
        }

        [CategoryAttribute("Level Header Settings"), DescriptionAttribute("ID for the level's BGM")]
        public byte BGMTrack
        {
            get { return _dma.LevelHeader.BGMTrack; }
            set { _dma.LevelHeader.BGMTrack = value; }
        }

        [CategoryAttribute("Level Header Settings"), DescriptionAttribute("Color of the level's fog")]
        public Color FogColor
        {
            get { return Color.FromArgb((int)_dma.LevelHeader.FogRed, (int)_dma.LevelHeader.FogGreen, (int)_dma.LevelHeader.FogBlue); }
            set { _dma.LevelHeader.FogRed = value.R; _dma.LevelHeader.FogGreen = value.G; _dma.LevelHeader.FogBlue = value.B; }
        }

        [CategoryAttribute("Level Header Settings"), DescriptionAttribute("Distance at which fog starts to appear")]
        public uint FogNearDist
        {
            get { return _dma.LevelHeader.FogNearDist; }
            set { _dma.LevelHeader.FogNearDist = value; }
        }

        [CategoryAttribute("Level Header Settings"), DescriptionAttribute("Distance at which fog completely obscures the level")]
        public uint FogFarDist
        {
            get { return _dma.LevelHeader.FogFarDist; }
            set { _dma.LevelHeader.FogFarDist = value; }
        }

        [CategoryAttribute("Level Header Settings"), DescriptionAttribute("Diffuse lighting color for the level")]
        public Color DiffuseLightingColor
        {
            get { return Color.FromArgb((int)_dma.LevelHeader.DiffuseLightingRed, (int)_dma.LevelHeader.DiffuseLightingGreen, (int)_dma.LevelHeader.DiffuseLightingBlue); }
            set { _dma.LevelHeader.DiffuseLightingRed = value.R; _dma.LevelHeader.DiffuseLightingGreen = value.G; _dma.LevelHeader.DiffuseLightingBlue = value.B; }
        }

        [CategoryAttribute("Level Header Settings"), DescriptionAttribute("Ambient lighting color for the level")]
        public Color AmbientLightingColor
        {
            get { return Color.FromArgb((int)_dma.LevelHeader.AmbientLightingRed, (int)_dma.LevelHeader.AmbientLightingGreen, (int)_dma.LevelHeader.AmbientLightingBlue); }
            set { _dma.LevelHeader.AmbientLightingRed = value.R; _dma.LevelHeader.AmbientLightingGreen = value.G; _dma.LevelHeader.AmbientLightingBlue = value.B; }
        }

        [CategoryAttribute("Level Header Settings"), DescriptionAttribute("Offset in the DMA where the level header starts"), TypeConverter(typeof(Int32HexTypeConverter))]
        public int HeaderOffset
        {
            get { return _dma.LevelHeader.Offset; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 1 (Note: value is 1 for space levels, 0 for planet levels)")]
        public uint Unknown1
        {
            get { return _dma.LevelHeader.Unk1; }
            set { _dma.LevelHeader.Unk1 = value; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 2")]
        public uint Unknown2
        {
            get { return _dma.LevelHeader.Unk2; }
            set { _dma.LevelHeader.Unk2 = value; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 3")]
        public byte Unknown3
        {
            get { return _dma.LevelHeader.Unk3; }
            set { _dma.LevelHeader.Unk3 = value; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 4")]
        public byte Unknown4
        {
            get { return _dma.LevelHeader.Unk4; }
            set { _dma.LevelHeader.Unk4 = value; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 5")]
        public byte Unknown5
        {
            get { return _dma.LevelHeader.Unk5; }
            set { _dma.LevelHeader.Unk5 = value; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 6"), TypeConverter(typeof(FloatTypeConverter))]
        public float Unknown6
        {
            get { return _dma.LevelHeader.Unk6; }
            set { _dma.LevelHeader.Unk6 = value; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 7"), TypeConverter(typeof(FloatTypeConverter))]
        public float Unknown7
        {
            get { return _dma.LevelHeader.Unk7; }
            set { _dma.LevelHeader.Unk7 = value; }
        }

        [CategoryAttribute("Level Header Unknowns"), DescriptionAttribute("Unknown 8"), TypeConverter(typeof(FloatTypeConverter))]
        public float Unknown8
        {
            get { return _dma.LevelHeader.Unk8; }
            set { _dma.LevelHeader.Unk8 = value; }
        }
    }
}
