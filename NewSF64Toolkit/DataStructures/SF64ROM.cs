using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.DataStructures
{
    public struct ROMInfo
    {
        public string Title;

        public string GameID;

        public byte Version;

        public uint CRC1;

        public uint CRC2;

        public uint DMATableOffset;

        public ROMInfo(string title, string gameID, byte version, uint crc1, uint crc2, uint dmaTableOffset)
        {
            Title = title;
            GameID = gameID;
            Version = version;
            CRC1 = crc1;
            CRC2 = crc2;
            DMATableOffset = dmaTableOffset;

            //Double check the DMA if it's invalid
            if (dmaTableOffset == 0x0)
                DMATableOffset = StarFoxRomInfo.GetDMATableOffset(GameID, Version);
                
        }
    }

    public class SF64ROM : IGameDataStructure
    {
        public Endianness ROMEndianness;
        private HeaderDMAFile headerDMA;
        private DMATableDMAFile dmaTableDMA;

        public string Filename { get; private set; }

        public uint Size { get; private set; }

        public ROMInfo Info { get; private set; }

        public bool IsCompressed { get; private set; }

        public bool IsROMLoaded { get; private set; }

        public bool IsValidRom { get; private set; }

        public bool HasGoodChecksum { get { HasGoodChecksum = CheckCRC(); return HasGoodChecksum; } private set; }

        public List<DMAFile> DMATable { get; private set; }

        public SF64ROM(string fileName, List<byte[]> DMAData)
        {
            //Convert byte[] to dma
            List<DMAFile> dmaEntries = new List<DMAFile>();

            uint start = 0;

            for (int i = 0; i < DMAData.Count; i++)
            {
                byte[] data = DMAData[i];
                uint end = start + (uint)data.Length;
                DMAFile dma;
                switch(i)
                {
                    case 0: //Header DMA
                        dma = new HeaderDMAFile(start, start, end, 0x0, data);
                        headerDMA = (HeaderDMAFile)dma;
                        break;
                    case 2: //DMA Table DMA
                        dma = new DMATableDMAFile(start, start, end, 0x0, data);
                        dmaTableDMA = (DMATableDMAFile)dma;
                        break;
                    default: //Others
                        dma = new DMAFile(start, start, end, 0x0, data);
                        break;
                }
                dmaEntries.Add(dma);
                start = end;
            }

            DMATable = dmaEntries;

            //Verify that it's correct?

            IsROMLoaded = true;

            this.Filename = fileName;
            Size = DMATable.Last().PEnd;

            //DmaToRom
            SetRomInfo();

            //When loading in as dma tables, just adjust the table a bit
            FixDMATable();

            //Recheck the CRC
            HasGoodChecksum = CheckCRC();
        }

        public SF64ROM(string fileName, byte[] data)
        {
            Filename = fileName;

            DMATable = new List<DMAFile>();

            //For now, we'll load in a dummy ROMInfo file and actually get
            // the rest of it later
            string gameID = System.Text.Encoding.UTF8.GetString(data, 59, 4);
            byte version = data[63];
            Info = new ROMInfo("","",0,0,0,StarFoxRomInfo.GetDMATableOffset(gameID, version));

            IsROMLoaded = BytesToDMATable(data);

            SetRomInfo();
        }

        private void SetRomInfo()
        {
            if (!IsROMLoaded)
                return;
            
            string title = System.Text.Encoding.UTF8.GetString(headerDMA.DMAData, 32, 20);
            string gameID = System.Text.Encoding.UTF8.GetString(headerDMA.DMAData, 59, 4);
            byte version = headerDMA.DMAData[63];

            uint endian = ToolSettings.ReadUInt(headerDMA.DMAData, 0);
            ROMEndianness = StarFoxRomInfo.GetEndianness(endian);

            uint crc1 = ToolSettings.ReadUInt(headerDMA.DMAData, 16, ROMEndianness);
            uint crc2 = ToolSettings.ReadUInt(headerDMA.DMAData, 20, ROMEndianness);

            uint dmaTableOffset = StarFoxRomInfo.GetDMATableOffset(gameID, version);

            //To be discovered: will this need to be changed to also change the CRC values??? Or can I leave them untouched?
            Info = new ROMInfo(title, gameID, version, crc1, crc2, dmaTableOffset);
            Size = (uint)headerDMA.DMAData.Length;

            IsValidRom = StarFoxRomInfo.IsValidVersion(Info);
            HasGoodChecksum = CheckCRC();
        }

        private bool BytesToDMATable(byte[] data)
        {
            //Transfer data from ROM to DMA Tables
            if (Info.DMATableOffset == 0x0)
            {
                return false;
            }

            IsCompressed = false;

            DMATable.Clear();

            int CurrentPos = (int)Info.DMATableOffset;

            try
            {
                while (CurrentPos < data.Length - 16)
                {
                    uint VStart = ToolSettings.ReadUInt(data, CurrentPos, ROMEndianness);
                    uint PStart = ToolSettings.ReadUInt(data, CurrentPos + 4, ROMEndianness);
                    uint PEnd = ToolSettings.ReadUInt(data, CurrentPos + 8, ROMEndianness);
                    uint CompFlag = ToolSettings.ReadUInt(data, CurrentPos + 12, ROMEndianness);

                    //Create the actual data
                    byte[] dmaBytes = new byte[PEnd - PStart];
                    Array.Copy(data, PStart, dmaBytes, 0, dmaBytes.Length);

                    DMAFile entry;
                    switch(DMATable.Count) //Should act as an index for the current dma file
                    {
                        case 0: //Header DMA
                            entry = new HeaderDMAFile(VStart, PStart, PEnd, 0x0, dmaBytes);
                            headerDMA = (HeaderDMAFile)entry;
                            break;
                        case 2: //DMA Table DMA
                            entry = new DMATableDMAFile(VStart, PStart, PEnd, 0x0, dmaBytes);
                            dmaTableDMA = (DMATableDMAFile)entry;
                            break;
                        default: //Others
                            entry = new DMAFile(VStart, PStart, PEnd, 0x0, dmaBytes);
                            break;
                    }

                    if ((entry.PStart == 0x00) && (entry.PEnd == 0x00)) break;

                    if (entry.CompFlag == 1) IsCompressed = true;

                    DMATable.Add(entry);

                    CurrentPos += 16;
                }
            }
            catch
            {
                //Error log entry?
                return false;
            }

            return true;

            ////Automatically decompress on loading?
            //if (IsCompressed)
            //{
            //    if (DecompressDMA())
            //    {
            //        FixCRC();
            //    }
            //}
        }

        public bool Decompress()
        {
            return DecompressDMA();
        }

        private bool DecompressDMA()
        {
            if(!IsROMLoaded)
            {
                return false;
            }

            for (int i = 0; i < DMATable.Count; i++)
            {
                DMAFile dma = DMATable[i];
                if(dma.CompFlag == 0x1)
                {
                    //compressed

                    //Decompress here
                    byte[] newDMAData;
                    if(ToolSettings.DecompressMIO0(dma.DMAData, out newDMAData))
                    {
                        dma.DMAData = newDMAData;

                        dma.PEnd = dma.VStart + (uint)newDMAData.Length;
                        dma.PStart = dma.VStart;
                        dma.CompFlag = 0x0;
                    }
                    else
                    {
                        //Catastrophic failure, just quit out
                        return false;
                    }

                }
                else
                {
                    //uncompressed
                    dma.PEnd = dma.VStart + (dma.PEnd - dma.PStart);
                    dma.PStart = dma.VStart;
                }
            }

            Size = DMATable.Last().PEnd;
            FixDMATable();

            return true;
        }

        public bool CheckCRC()
        {
            uint[] crcs = new uint[2];
            if (N64Sums.GetChecksum(crcs, GetAsBytes()))
                return (crcs[0] == headerDMA.CRC1) && (crcs[1] == headerDMA.CRC2);

            return false;
        }

        public bool FixCRC()
        {
            if (!IsROMLoaded)
                return false;

            //Only apply crc fix to the full rom data container, not to the separated dma table data container
            uint[] crcs = new uint[2];

            if (N64Sums.GetChecksum(crcs, GetAsBytes()))
            {
                headerDMA.CRC1 = crcs[0];
                headerDMA.CRC2 = crcs[1];

                Info = new ROMInfo(Info.Title, Info.GameID, Info.Version, crcs[0], crcs[1], Info.DMATableOffset);

                HasGoodChecksum = true;

                return true;
            }
            return false;
        }

        private void FixDMATable()
        {
            for (int i = 0; i < DMATable.Count; i++)
            {
                dmaTableDMA.SetDMAEntry(i, DMATable[i].VStart, DMATable[i].PStart, DMATable[i].PEnd, DMATable[i].CompFlag, ROMEndianness);
            }

        }

        public byte[] GetAsBytes()
        {
            if (!IsROMLoaded)
                return null;

            uint fullLength = DMATable.Last().PEnd;

            //Rom length must be multiple of 0x400000
            if(fullLength % 0x400000 != 0)
                fullLength = (uint)Math.Ceiling((double)fullLength / 0x400000) * 0x400000;

            Size = fullLength;

            byte[] newRomData = new byte[fullLength];

            foreach (DMAFile dma in DMATable)
            {
                Array.Copy(dma.DMAData, 0, newRomData, dma.PStart, dma.PEnd - dma.PStart);
            }

            return newRomData;
        }

    }
}
