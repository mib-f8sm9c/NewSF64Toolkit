using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;
using System.ComponentModel;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class RefSimpleObjectInfo : IResourceInfo
    {
        private RefSimpleLevelObject _obj;

        public RefSimpleObjectInfo(RefSimpleLevelObject obj)
        {
            _obj = obj;
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Offset of the corresponding DList for the object (the dma file the object belongs to is not specified)")]
        public uint DListOffset
        {
            get { return _obj.DListOffset; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 1")]
        public uint Unknown1
        {
            get { return _obj.Unk1; }
            set { _obj.Unk1 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 2")]
        public uint Unknown2
        {
            get { return _obj.Unk2; }
            set { _obj.Unk2 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 3")]
        public uint Unknown3
        {
            get { return _obj.Unk3; }
            set { _obj.Unk3 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 4")]
        public uint Unknown4
        {
            get { return _obj.Unk4; }
            set { _obj.Unk4 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 5")]
        public uint Unknown5
        {
            get { return _obj.Unk5; }
            set { _obj.Unk5 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 6")]
        public uint Unknown6
        {
            get { return _obj.Unk6; }
            set { _obj.Unk6 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 7")]
        public uint Unknown7
        {
            get { return _obj.Unk7; }
            set { _obj.Unk7 = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown 8")]
        public uint Unknown8
        {
            get { return _obj.Unk8; }
            set { _obj.Unk8 = value; }
        }
    }
}
