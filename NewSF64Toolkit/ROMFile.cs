using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit
{
    //Notes: Make it so it has 2 different data structures: the combined ROM data and the separated DMA table data.
    //        The two should be interchangeable, though for our purposes we'll use the DMA table.
    //        You should be able to load from both and convert between them.

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

    public class ROMFile
    {
        public Endianness ROMEndianness;

        public byte[] RomData { get { return _romData; } }
        private byte[] _romData;

        public ROMFile(string fileName, List<byte[]> DMAData)
        {
            //Convert byte[] to dma
            List<DMATableEntry> dmaEntries = new List<DMATableEntry>();

            uint start = 0;

            foreach(byte[] data in DMAData)
            {
                uint end = start + (uint)data.Length;
                DMATableEntry dma = new DMATableEntry(start, start, end, 0x0, data);
                dmaEntries.Add(dma);
                start = end;
            }

            DMATable = dmaEntries;

            IsDMALoaded = true;

            this.Filename = fileName;

            //DmaToRom
            DMAToRom();

			//Fix CRC
            FixCRC();
        }

        public ROMFile(string fileName, byte[] data)
            : this(fileName, data, 0x0)
        {
        }

        public ROMFile(string fileName, byte[] data, uint dmaTableOffset)
        {
            _romData = data;

            Filename = fileName;

            IsROMLoaded = true;

            SetRomInfo(dmaTableOffset);

            DMATable = new List<DMATableEntry>();

            RomToDMA();
        }

        private void SetRomInfo(uint dmaTableOffset = 0x0)
        {
            string title = System.Text.Encoding.UTF8.GetString(_romData, 32, 20);
            string gameID = System.Text.Encoding.UTF8.GetString(_romData, 59, 4);
            byte version = _romData[63];

            uint endian = ToolSettings.ReadUInt(_romData, 0);
            ROMEndianness = StarFoxRomInfo.GetEndianness(endian);

            uint crc1 = ToolSettings.ReadUInt(_romData, 16, ROMEndianness);
            uint crc2 = ToolSettings.ReadUInt(_romData, 20, ROMEndianness);

            if (dmaTableOffset == 0x0)
                dmaTableOffset = StarFoxRomInfo.GetDMATableOffset(gameID, version);

            //To be discovered: will this need to be changed to also change the CRC values??? Or can I leave them untouched?
            Info = new ROMInfo(title, gameID, version, crc1, crc2, dmaTableOffset);

            IsValidRom = StarFoxRomInfo.IsValidVersion(Info);
        }

        public bool IsValidRom { get; private set; }

        public int Size { get { return _romData.Length; } }

        public string Filename { get; private set; }

        public ROMInfo Info { get; private set; }

        public bool IsCompressed { get; private set; }

        public bool IsROMLoaded { get; private set; }

        public bool IsDMALoaded { get; private set; }

        public List<DMATableEntry> DMATable { get; private set; }

        public void ClearROMData()
        {
            //Let garbage collector handle it
            _romData = null;
        }

        private void RomToDMA()
        {
            //Transfer data from ROM to DMA Tables
            if (Info.DMATableOffset == 0x0)
            {
                IsDMALoaded = false;
                return;
            }

            IsCompressed = false;

            DMATable.Clear();

            int CurrentPos = (int)Info.DMATableOffset;

            try
            {
                while (CurrentPos < Size - 16)
                {
                    uint VStart = ToolSettings.ReadUInt(_romData, CurrentPos, ROMEndianness);
                    uint PStart = ToolSettings.ReadUInt(_romData, CurrentPos + 4, ROMEndianness);
                    uint PEnd = ToolSettings.ReadUInt(_romData, CurrentPos + 8, ROMEndianness);
                    uint CompFlag = ToolSettings.ReadUInt(_romData, CurrentPos + 12, ROMEndianness);

                    //Create the actual data
                    byte[] dmaBytes = new byte[PEnd - PStart];
                    Array.Copy(_romData, PStart, dmaBytes, 0, dmaBytes.Length);

                    DMATableEntry entry = new DMATableEntry(VStart, PStart, PEnd, CompFlag, dmaBytes);

                    if ((entry.PStart == 0x00) && (entry.PEnd == 0x00)) break;

                    if (entry.CompFlag == 1) IsCompressed = true;

                    DMATable.Add(entry);

                    CurrentPos += 16;
                }
                IsDMALoaded = true;
            }
            catch
            {
                IsDMALoaded = false;
            }

            //if (IsCompressed)
            //{
            //    if (DecompressDMA())
            //    {
            //        DMAToRom();
            //        FixCRC();
            //    }
            //}
        }

        public bool Decompress()
        {
            if (DecompressDMA())
            {
                DMAToRom();
                return true;
            }

            return false;
        }

        private bool DecompressDMA()
        {
            if(!IsDMALoaded)
            {
                return false;
            }

            for (int i = 0; i < DMATable.Count; i++)
            {
                DMATableEntry dma = DMATable[i];
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

                DMATable[i] = dma;
            }

            return true;
        }

        private void DMAToRom()
        {
            if(!IsDMALoaded)
                return;

            //Reconvert the DMA data to a rom
            ClearROMData();

            uint fullLength = DMATable.Last().PEnd;

            //Rom length must be multiple of 0x400000
            fullLength = (fullLength / 0x400000 + 1) * 0x400000;

            byte[] newRomData = new byte[fullLength];

            foreach (DMATableEntry dma in DMATable)
            {
                Array.Copy(dma.DMAData, 0, newRomData, dma.PStart, dma.PEnd - dma.PStart);
            }

            _romData = newRomData;

            SetRomInfo();

            IsROMLoaded = true;

            FixDMATable();
        }

        public bool FixCRC()
        {
            //Only apply crc fix to the full rom data container, not to the separated dma table data container
            if(N64Sums.FixChecksum(_romData))
            {
                IsValidRom = true;
                uint crc1 = ToolSettings.ReadUInt(_romData, 16, ROMEndianness);
                uint crc2 = ToolSettings.ReadUInt(_romData, 20, ROMEndianness);

                Info = new ROMInfo(Info.Title, Info.GameID, Info.Version, crc1, crc2, Info.DMATableOffset);

                return true;
            }
            return false;
        }

        private void FixDMATable()
        {
            for (int i = 0; i < DMATable.Count; i++)
            {
                if (i < 2) //Don't apply to early entries, it's okay
                    continue;

                int CurrentPos = (int)Info.DMATableOffset + 16 * i;

                ToolSettings.WriteUInt(DMATable[i].VStart, _romData, CurrentPos, ROMEndianness);
                ToolSettings.WriteUInt(DMATable[i].PStart, _romData, CurrentPos + 4, ROMEndianness);
                ToolSettings.WriteUInt(DMATable[i].PEnd, _romData, CurrentPos + 8, ROMEndianness);
                ToolSettings.WriteUInt(DMATable[i].CompFlag, _romData, CurrentPos + 12, ROMEndianness);

            }

        }

    }
}
