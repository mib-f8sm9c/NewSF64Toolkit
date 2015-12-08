using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;
using System.ComponentModel;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class SFLevelObjectInfo : IResourceInfo
    {
        private SFLevelObject _obj;

        public SFLevelObjectInfo(SFLevelObject obj)
        {
            _obj = obj;
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Position in the level where this object is at"), TypeConverter(typeof(FloatTypeConverter))]
        public float LevelPosition
        {
            get { return _obj.LvlPos; }
            set { _obj.LvlPos = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("X offset for the object")]
        public short X
        {
            get { return _obj.X; }
            set { _obj.X = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Y offset for the object")]
        public short Y
        {
            get { return _obj.Y; }
            set { _obj.Y = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Z offset for the object")]
        public short Z
        {
            get { return _obj.Z; }
            set { _obj.Z = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("X rotation for the object")]
        public short XRotation
        {
            get { return _obj.XRot; }
            set { _obj.XRot = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Y rotation for the object")]
        public short YRotation
        {
            get { return _obj.YRot; }
            set { _obj.YRot = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Z rotation for the object")]
        public short ZRotation
        {
            get { return _obj.ZRot; }
            set { _obj.ZRot = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Object ID. Denotes what type of object it is. Note that not all ID types are compatible between levels")]
        public ushort ID
        {
            get { return _obj.ID; }
            set { _obj.ID = value; }
        }

        [CategoryAttribute("Object Info"), DescriptionAttribute("Unknown")]
        public ushort Unknown
        {
            get { return _obj.Unk; }
            set { _obj.Unk = value; }
        }

    }
}
