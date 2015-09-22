using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.DataStructures.DMA
{
    public class DMATableDMAFile : DMAFile
    {
        public List<DMATableEntry> DMATableEntries;

        public DMATableDMAFile(byte[] data)
            : base(data)
        {
        }

        public override bool  LoadFromBytes(byte[] bytes)
        {
 	        base.LoadFromBytes(bytes);

            if (DMATableEntries == null)
                DMATableEntries = new List<DMATableEntry>();
            else
                DMATableEntries.Clear();

            byte[] tempData;

            //pull out the DMA info
            for (int i = 0; i < bytes.Length; i += 0x10)
            {
                if (_dmaData.TakeMemory(i, DMATableEntry.Size, out tempData))
                {
                    DMATableEntries.Add(new DMATableEntry(i, tempData));
                }
                else
                {
                    break;
                }
            }

            return true;
        }

        public override byte[] GetAsBytes()
        {
            byte[] bytes =  base.GetAsBytes();

            if (DMATableEntries != null && DMATableEntries.Count > 0)
            {
                //Add in the DMA info
                foreach (DMATableEntry dma in DMATableEntries)
                {
                    Array.Copy(dma.GetAsBytes(), 0, bytes, dma.Offset, 0x10);
                }
            }

            return bytes;
        }
    }
}
