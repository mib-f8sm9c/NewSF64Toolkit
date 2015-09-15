using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.DataStructures.DMA
{
    //This default DMAFile class will hold all data as a byte array, but inheriting classes
    // may choose to represent the game data with objects rather than simple byte data. For
    // those classes,  make sure to override GetAsBytes().
    public class DMAFile : IGameDataStructure
    {
        public DMATableEntry DMAInfo;

        protected DynamicMemoryMapping _dmaData;

        public int Size { get { return _dmaData.Size; } }

        public DMAFile(byte[] data)
        {
            _dmaData = new DynamicMemoryMapping();

            LoadFromBytes(data);
        }

        public virtual byte[] GetAsBytes()
        {
            return _dmaData.GetAsBytes();
        }

        public virtual bool LoadFromBytes(byte[] bytes)
        {
            _dmaData.ClearMaps();

            _dmaData.AddMemory(0, bytes);

            return true;
        }
    }
}
