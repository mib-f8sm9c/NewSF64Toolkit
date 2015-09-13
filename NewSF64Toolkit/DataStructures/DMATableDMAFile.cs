using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.DataStructures
{
    public class DMATableDMAFile : DMAFile
    {
        public List<DMATableEntry> DMATableEntries;

        public DMATableDMAFile(byte[] data)
            : base(data)
        {
        }

        public override void  LoadFromBytes(byte[] bytes)
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
                if (_dmaData.TakeMemory(i, 0x10, out tempData))
                {
                    uint vstart = ByteHelper.ReadUInt(tempData, 0);
                    uint pstart = ByteHelper.ReadUInt(tempData, 4);
                    uint pend = ByteHelper.ReadUInt(tempData, 8);
                    uint cflag = ByteHelper.ReadUInt(tempData, 12);

                    DMATableEntries.Add(new DMATableEntry(i, vstart, pstart, pend, cflag));
                }
                else
                {
                    break;
                }
            }
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
