using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;
using System.ComponentModel;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class RefAdvancedObjectInfo : IResourceInfo
    {
        private RefAdvancedLevelObject _obj;

        public RefAdvancedObjectInfo(RefAdvancedLevelObject obj)
        {
            _obj = obj;
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Offset of the corresponding DList for the object (the dma file the object belongs to is not specified)"),
            TypeConverter(typeof(UInt32HexTypeConverter))]
        public uint DListOffset
        {
            get { return _obj.DListOffset; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 1"), TypeConverter(typeof(UInt32HexTypeConverter))]
        public uint Unknown1
        {
            get { return _obj.Unk1; }
            set { _obj.Unk1 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 2"), TypeConverter(typeof(FloatTypeConverter))]
        public float Unknown2
        {
            get { return _obj.Unk2; }
            set { _obj.Unk2 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 3"), TypeConverter(typeof(FloatTypeConverter))]
        public float Unknown3
        {
            get { return _obj.Unk3; }
            set { _obj.Unk3 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 4"), TypeConverter(typeof(FloatTypeConverter))]
        public float Unknown4
        {
            get { return _obj.Unk4; }
            set { _obj.Unk4 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 5"), TypeConverter(typeof(UInt32HexTypeConverter))]
        public uint Unknown5
        {
            get { return _obj.Unk5; }
            set { _obj.Unk5 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 6"), TypeConverter(typeof(FloatTypeConverter))]
        public float Unknown6
        {
            get { return _obj.Unk6; }
            set { _obj.Unk6 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 7"), TypeConverter(typeof(UInt32HexTypeConverter))]
        public uint Unknown7
        {
            get { return _obj.Unk7; }
            set { _obj.Unk7 = value; }
        }

    }
}
