using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.DataStructures.DataObjects;
using System.ComponentModel;

namespace NewSF64Toolkit.Tools.ResourceInfo
{
    public class RAMTableEntryInfo : IResourceInfo
    {
        private RAMTableEntry _ram;

        public RAMTableEntryInfo(RAMTableEntry ram)
        {
            _ram = ram;
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Loc1")]
        public uint Loc1Start
        {
            get { return _ram.Loc1Start; }
            set { _ram.Loc1Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Loc1")]
        public uint Loc1End
        {
            get { return _ram.Loc1End; }
            set { _ram.Loc1End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Loc2")]
        public uint Loc2Start
        {
            get { return _ram.Loc2Start; }
            set { _ram.Loc2Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Loc2")]
        public uint Loc2End
        {
            get { return _ram.Loc2End; }
            set { _ram.Loc2End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Loc3")]
        public uint Loc3Start
        {
            get { return _ram.Loc3Start; }
            set { _ram.Loc3Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Loc3")]
        public uint Loc3End
        {
            get { return _ram.Loc3End; }
            set { _ram.Loc3End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg1")]
        public uint Seg1Start
        {
            get { return _ram.Seg1Start; }
            set { _ram.Seg1Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg1")]
        public uint Seg1End
        {
            get { return _ram.Seg1End; }
            set { _ram.Seg1End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg2")]
        public uint Seg2Start
        {
            get { return _ram.Seg2Start; }
            set { _ram.Seg2Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg2")]
        public uint Seg2End
        {
            get { return _ram.Seg2End; }
            set { _ram.Seg2End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg3")]
        public uint Seg3Start
        {
            get { return _ram.Seg3Start; }
            set { _ram.Seg3Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg3")]
        public uint Seg3End
        {
            get { return _ram.Seg3End; }
            set { _ram.Seg3End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg4")]
        public uint Seg4Start
        {
            get { return _ram.Seg4Start; }
            set { _ram.Seg4Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg4")]
        public uint Seg4End
        {
            get { return _ram.Seg4End; }
            set { _ram.Seg4End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg5")]
        public uint Seg5Start
        {
            get { return _ram.Seg5Start; }
            set { _ram.Seg5Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg5")]
        public uint Seg5End
        {
            get { return _ram.Seg5End; }
            set { _ram.Seg5End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg6")]
        public uint Seg6Start
        {
            get { return _ram.Seg6Start; }
            set { _ram.Seg6Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg6")]
        public uint Seg6End
        {
            get { return _ram.Seg6End; }
            set { _ram.Seg6End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg7")]
        public uint Seg7Start
        {
            get { return _ram.Seg7Start; }
            set { _ram.Seg7Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg7")]
        public uint Seg7End
        {
            get { return _ram.Seg7End; }
            set { _ram.Seg7End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg8")]
        public uint Seg8Start
        {
            get { return _ram.Seg8Start; }
            set { _ram.Seg8Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg8")]
        public uint Seg8End
        {
            get { return _ram.Seg8End; }
            set { _ram.Seg8End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of Seg9")]
        public uint Seg9Start
        {
            get { return _ram.Seg9Start; }
            set { _ram.Seg9Start = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of Seg9")]
        public uint Seg9End
        {
            get { return _ram.Seg9End; }
            set { _ram.Seg9End = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of SegA")]
        public uint SegAStart
        {
            get { return _ram.SegAStart; }
            set { _ram.SegAStart = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of SegA")]
        public uint SegAEnd
        {
            get { return _ram.SegAEnd; }
            set { _ram.SegAEnd = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of SegB")]
        public uint SegBStart
        {
            get { return _ram.SegBStart; }
            set { _ram.SegBStart = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of SegB")]
        public uint SegBEnd
        {
            get { return _ram.SegBEnd; }
            set { _ram.SegBEnd = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of SegC")]
        public uint SegCStart
        {
            get { return _ram.SegCStart; }
            set { _ram.SegCStart = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of SegC")]
        public uint SegCEnd
        {
            get { return _ram.SegCEnd; }
            set { _ram.SegCEnd = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of SegD")]
        public uint SegDStart
        {
            get { return _ram.SegDStart; }
            set { _ram.SegDStart = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of SegD")]
        public uint SegDEnd
        {
            get { return _ram.SegDEnd; }
            set { _ram.SegDEnd = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of SegE")]
        public uint SegEStart
        {
            get { return _ram.SegEStart; }
            set { _ram.SegEStart = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of SegE")]
        public uint SegEEnd
        {
            get { return _ram.SegEEnd; }
            set { _ram.SegEEnd = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of SegF")]
        public uint SegFStart
        {
            get { return _ram.SegFStart; }
            set { _ram.SegFStart = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of SegF")]
        public uint SegFEnd
        {
            get { return _ram.SegFEnd; }
            set { _ram.SegFEnd = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("Start of the overlay")]
        public uint OverlayStart
        {
            get { return _ram.OverlayStart; }
            set { _ram.OverlayStart = value; }
        }

        [CategoryAttribute("RAM Table"), DescriptionAttribute("End of the overlay")]
        public uint OverlayEnd
        {
            get { return _ram.OverlayEnd; }
            set { _ram.OverlayEnd = value; }
        }
    }
}
