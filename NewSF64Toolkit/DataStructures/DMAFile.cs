using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures
{
    //This default DMAFile class will hold all data as a byte array, but inheriting classes
    // may choose to represent the game data with objects rather than simple byte data. For
    // those classes,  make sure to override GetAsBytes().
    public class DMAFile : IGameDataStructure
    {
        public uint VStart;
        public uint PStart;
        public uint PEnd;
        public uint CompFlag;

        public byte[] DMAData;

        public DMAFile(uint vstart, uint pstart, uint pend, uint compFlag)
        {
            VStart = vstart;
            PStart = pstart;
            PEnd = pend;
            CompFlag = compFlag;

            DMAData = null;
        }

        public DMAFile(uint vstart, uint pstart, uint pend, uint compFlag, byte[] data)
            : this(vstart, pstart, pend, compFlag)
        {
            DMAData = data;
        }

        public virtual byte[] GetAsBytes()
        {
            return DMAData;
        }
    }
}
