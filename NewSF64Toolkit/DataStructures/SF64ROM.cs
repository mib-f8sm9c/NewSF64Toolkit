using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NewSF64Toolkit.Settings;
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.DataStructures.DataObjects;
using NewSF64Toolkit.OpenGL.F3DEX;
using System.Windows.Forms;

namespace NewSF64Toolkit.DataStructures
{
    public class SF64ROM : IGameDataStructure, IResource
    {
        #region Properties & Variables

        private HeaderDMAFile _headerDMA;
        public HeaderDMAFile HeaderInfo { get { return _headerDMA; } }

        private ReferenceDMAFile _referenceDMA;
        public ReferenceDMAFile ReferenceDMA { get { return _referenceDMA; } }

        private DMATableDMAFile _dmaTableDMA;

        private DialogueDMAFile _dialogueDMA;

        private List<LevelDMAFile> _levelDMAs;

        public string Filename { get; private set; }

        public uint Size { get; private set; }

        public uint DMATableOffset { get; private set; }

        public bool IsCompressed { get; private set; }

        public bool IsROMLoaded { get; private set; }

        public bool IsValidRom { get; private set; }

        private bool _hasGoodChecksum;
        public bool HasGoodChecksum { get { _hasGoodChecksum = CheckCRC(); return _hasGoodChecksum; } private set { _hasGoodChecksum = value; } }

        public List<DMAFile> DMATable { get; private set; }

        #endregion

        #region Enums, Structs & Events

        public enum RomUpdateType
        {
            RomLoaded,
            RomUnloaded,
            RomSaved,
            RomEdited,
            CRCFixed,
            Decompressed
        }

        public delegate void RomUpdatedEvent(RomUpdateType type);

        public static event RomUpdatedEvent RomUpdated = delegate { };

        #endregion

        #region SF64ROM Static Functions

        //This might be bad design, but I love Singletons too much not to use one here.
        private static SF64ROM _instance;

        public static SF64ROM Instance { get { if (_instance == null) ResetRom(); return _instance; } }

        public static bool LoadFromROM(string romFile)
        {
            string fileName = Path.GetFileName(romFile);
            try
            {
                _instance = new SF64ROM(fileName, File.ReadAllBytes(romFile));
            }
            catch
            {
                return false;
            }

            RomUpdated(RomUpdateType.RomLoaded);

            return true;
        }

        //Needs to be fixed, now that the system has been changed up
        public static void LoadFromDMATables(string fileName, List<byte[]> DMAData)
        {
            _instance = new SF64ROM(fileName, DMAData);

            RomUpdated(RomUpdateType.RomLoaded);
        }

        public static bool SaveRomTo(string filePath)
        {
            if (!_instance.IsROMLoaded)
                return false;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    byte[] bytes = _instance.GetAsBytes();
                    writer.BaseStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch
            {
                return false;
            }

            RomUpdated(RomUpdateType.RomSaved);

            return true;
        }

        public static void ResetRom()
        {
            _instance = new SF64ROM();

            RomUpdated(RomUpdateType.RomUnloaded);
        }

        #endregion

        //Empty constructor
        private SF64ROM()
        {
            IsROMLoaded = false;
            IsValidRom = false;
            HasGoodChecksum = false;
        }

        private SF64ROM(string fileName, byte[] data)
        {
            Filename = fileName;

            LoadFromBytes(data);
        }

        public bool LoadFromBytes(byte[] data)
        {
            if (DMATable == null)
                DMATable = new List<DMAFile>();

            if (_levelDMAs == null)
                _levelDMAs = new List<LevelDMAFile>();

            string gameID = System.Text.Encoding.UTF8.GetString(data, 59, 4);
            byte version = data[63];
            DMATableOffset = StarFoxRomInfo.GetDMATableOffset(gameID, version);
            Endianness endianness = StarFoxRomInfo.GetEndianness(ByteHelper.ReadUInt(data, 0));

            IsROMLoaded = BytesToDMATable(data, endianness);

            CheckValidity();

            return true;
        }

        //Needs to be fixed, now that the system has been changed up
        private SF64ROM(string fileName, List<byte[]> DMAData)
        {
            throw new NotImplementedException();

            ////Convert byte[] to dma
            //List<DMAFile> dmaEntries = new List<DMAFile>();

            //uint start = 0;

            //for (int i = 0; i < DMAData.Count; i++)
            //{
            //    byte[] data = DMAData[i];
            //    uint end = start + (uint)data.Length;
            //    DMAFile dma;
            //    switch(i)
            //    {
            //        case 0: //Header DMA
            //            dma = new HeaderDMAFile(start, start, end, 0x0, data);
            //            _headerDMA = (HeaderDMAFile)dma;
            //            break;
            //        case 2: //DMA Table DMA
            //            dma = new DMATableDMAFile(start, start, end, 0x0, data);
            //            _dmaTableDMA = (DMATableDMAFile)dma;
            //            break;
            //        default: //Others
            //            dma = new DMAFile(start, start, end, 0x0, data);
            //            break;
            //    }
            //    dmaEntries.Add(dma);
            //    start = end;
            //}

            //DMATable = dmaEntries;

            ////Verify that it's correct?

            //IsROMLoaded = true;

            //this.Filename = fileName;
            //Size = DMATable.Last().DMAInfo.PEnd;

            ////DmaToRom
            //SetRomInfo();

            ////When loading in as dma tables, just adjust the table a bit
            //FixDMATable();

            ////Recheck the CRC
            //HasGoodChecksum = CheckCRC();
        }

        private void CheckValidity()
        {
            if (!IsROMLoaded)
                return;
            
            IsValidRom = StarFoxRomInfo.IsValidVersion(_headerDMA.GameID, _headerDMA.Version);
            HasGoodChecksum = CheckCRC();
        }

        private bool BytesToDMATable(byte[] data, Endianness RomEndianness)
        {
            //Using the DMA Table offset, break up the file into its constituent DMA blocks.
            if (DMATableOffset == 0x0)
            {
                return false;
            }

            IsCompressed = false;

            DMATable.Clear();
            _levelDMAs.Clear();
            _headerDMA = null;
            _dmaTableDMA = null;
            _referenceDMA = null;

            int CurrentPos = (int)DMATableOffset;

            try
            {
                while (CurrentPos < data.Length - 16)
                {
                    uint VStart = ByteHelper.ReadUInt(data, CurrentPos, RomEndianness);
                    uint PStart = ByteHelper.ReadUInt(data, CurrentPos + 4, RomEndianness);
                    uint PEnd = ByteHelper.ReadUInt(data, CurrentPos + 8, RomEndianness);
                    uint CompFlag = ByteHelper.ReadUInt(data, CurrentPos + 12, RomEndianness);

                    //End of the DMA Header
                    if ((PStart == 0x00) && (PEnd == 0x00)) break;

                    //Create the actual data
                    byte[] dmaBytes = new byte[PEnd - PStart];
                    Array.Copy(data, PStart, dmaBytes, 0, dmaBytes.Length);

                    DMAFile entry;
                    switch(DMATable.Count) //Should act as an index for the current dma file
                    {
                        case 0: //Header DMA
                            entry = new HeaderDMAFile(dmaBytes, DMATable.Count);
                            _headerDMA = (HeaderDMAFile)entry;
                            break;
                        case 1: //Reference DMA
                            entry = new ReferenceDMAFile(dmaBytes, DMATable.Count);
                            _referenceDMA = (ReferenceDMAFile)entry;
                            break;
                        case 2: //DMA Table DMA
                            entry = new DMATableDMAFile(dmaBytes, DMATable.Count);
                            _dmaTableDMA = (DMATableDMAFile)entry;
                            break;
                        case 54: //Dialogue DMA
                            entry = new DialogueDMAFile(dmaBytes, DMATable.Count);
                            _dialogueDMA = (DialogueDMAFile)entry;
                            break;
                        //case 12: Appears broken right now, the Game Object table is set up differently than the other files. Possibly a prototype setup?
                        case 18:
                        case 19:
                        case 20:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                        case 38:
                        case 39:
                        case 47:
                        case 53:
                            entry = new LevelDMAFile(dmaBytes, DMATable.Count, (int)_referenceDMA.LevelHeaderOffsets[StarFoxRomInfo.DMATableToLevelIndex(DMATable.Count)], 
                                (int)_referenceDMA.LevelInfoOffsets[StarFoxRomInfo.DMATableToLevelIndex(DMATable.Count)]);
                            _levelDMAs.Add(entry as LevelDMAFile);
                            break;
                        default: //Others
                            entry = new DMAFile(dmaBytes, DMATable.Count);
                            break;
                    }

                    DMATable.Add(entry);

                    CurrentPos += 16;
                }

                //Now set the DMA Headers
                if (_dmaTableDMA != null)
                {
                    for (int i = 0; i < DMATable.Count; i++)
                    {
                        DMATable[i].DMAInfo = _dmaTableDMA.DMATableEntries[i];

                        if (DMATable[i].DMAInfo.CFlag == 1)
                            IsCompressed = true;
                    }
                }

                if (_referenceDMA != null && _levelDMAs.Count > 0)
                {
                    //Parse out the F3DEX info
                    //LoadROMResources();
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
            bool success = DecompressDMA();

            if(success)
                RomUpdated(RomUpdateType.Decompressed);

            return success;
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
                if(dma.DMAInfo.CFlag == 0x1)
                {
                    //compressed

                    //Decompress here
                    byte[] newDMAData;
                    if(StarFoxRomInfo.DecompressMIO0(dma.GetAsBytes(), out newDMAData))
                    {
                        dma.LoadFromBytes(newDMAData);

                        dma.DMAInfo.PEnd = dma.DMAInfo.VStart + (uint)newDMAData.Length;
                        dma.DMAInfo.PStart = dma.DMAInfo.VStart;
                        dma.DMAInfo.CFlag = 0x0;

                        _dmaTableDMA.DMATableEntries[i].CFlag = dma.DMAInfo.CFlag;
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
                    dma.DMAInfo.PEnd = dma.DMAInfo.VStart + (dma.DMAInfo.PEnd - dma.DMAInfo.PStart);
                    dma.DMAInfo.PStart = dma.DMAInfo.VStart;
                }
            }

            Size = DMATable.Last().DMAInfo.PEnd;
            FixDMATable();

            return true;
        }

        public bool CheckCRC()
        {
            uint[] crcs = new uint[2];
            if (N64Sums.GetChecksum(crcs, GetAsBytes()))
                return (crcs[0] == _headerDMA.CRC1) && (crcs[1] == _headerDMA.CRC2);

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
                _headerDMA.CRC1 = crcs[0];
                _headerDMA.CRC2 = crcs[1];

                HasGoodChecksum = true;

                RomUpdated(RomUpdateType.CRCFixed);

                return true;
            }
            return false;
        }

        private bool FixDMATable()
        {
            if (!IsROMLoaded)
                return false;

            for (int i = 0; i < DMATable.Count; i++)
            {
                _dmaTableDMA.DMATableEntries[i].VStart = DMATable[i].DMAInfo.VStart;
                _dmaTableDMA.DMATableEntries[i].PStart = DMATable[i].DMAInfo.PStart;
                _dmaTableDMA.DMATableEntries[i].PEnd = DMATable[i].DMAInfo.PEnd;
                _dmaTableDMA.DMATableEntries[i].CFlag = DMATable[i].DMAInfo.CFlag;
            }

            return true;
        }

        public byte[] GetAsBytes()
        {
            if (!IsROMLoaded)
                return null;

            uint fullLength = DMATable.Last().DMAInfo.PEnd;

            //Rom length must be multiple of 0x400000
            if(fullLength % 0x400000 != 0)
                fullLength = (uint)Math.Ceiling((double)fullLength / 0x400000) * 0x400000;

            //Size = fullLength;

            byte[] newRomData = new byte[fullLength];

            foreach (DMAFile dma in DMATable)
            {
                Array.Copy(dma.GetAsBytes(), 0, newRomData, dma.DMAInfo.PStart, dma.DMAInfo.PEnd - dma.DMAInfo.PStart);
            }

            return newRomData;
        }

        private bool _resourcesLoaded = false;

        public void LoadROMResources()
        {
            if (_resourcesLoaded)
                return;

            //Here we'll go through the level files/reference file, and then create the F3DEX objects and link them to the
            // simple object list in the reference file
            F3DEXParser _f3dex = new F3DEXParser();

            //_f3dex.InitInvalidModels();

            //Init the simple objects to use the invalid box dlist indices
            foreach (RefSimpleLevelObject obj in _referenceDMA.SimpleObjects)
            {
                obj.GLDisplayListOffset = F3DEXParser.InvalidBox;
            }

            //Init the simple objects to use the invalid box dlist indices
            foreach (RefAdvancedLevelObject obj in _referenceDMA.AdvancedObjects)
            {
                obj.GLDisplayListOffset = F3DEXParser.InvalidBox;
            }

            //Make a list of the level indices, just for optimization sake later on
            List<int> levelIndices = StarFoxRomInfo.LevelIndexAndDMAs.ToList();

            foreach (LevelDMAFile level in _levelDMAs)
            {
                byte[] levelBytes = level.GetAsBytes();

                Dictionary<int, byte[]> ramBytes = new Dictionary<int, byte[]>();
                int count = _referenceDMA.RAMTable.Count(r => r.Seg6Start == level.DMAInfo.PStart);
                int xs = count;
                RAMTableEntry ramInfo = _referenceDMA.RAMTable.FirstOrDefault(r => r.Seg6Start == level.DMAInfo.PStart);
                if (ramInfo != null)
                {
                    //Load it all in!!
                    DMAFile dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg1Start);
                    if(dma == null)
                        ramBytes.Add(1, null);
                    else
                        ramBytes.Add(1, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg2Start);
                    if (dma == null)
                        ramBytes.Add(2, null);
                    else
                        ramBytes.Add(2, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg3Start);
                    if (dma == null)
                        ramBytes.Add(3, null);
                    else
                        ramBytes.Add(3, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg4Start);
                    if (dma == null)
                        ramBytes.Add(4, null);
                    else
                        ramBytes.Add(4, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg5Start);
                    if (dma == null)
                        ramBytes.Add(5, null);
                    else
                        ramBytes.Add(5, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg6Start);
                    if (dma == null)
                        ramBytes.Add(6, null);
                    else
                        ramBytes.Add(6, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg7Start);
                    if (dma == null)
                        ramBytes.Add(7, null);
                    else
                        ramBytes.Add(7, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg8Start);
                    if (dma == null)
                        ramBytes.Add(8, null);
                    else
                        ramBytes.Add(8, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.Seg9Start);
                    if (dma == null)
                        ramBytes.Add(9, null);
                    else
                        ramBytes.Add(9, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.SegAStart);
                    if (dma == null)
                        ramBytes.Add(0xA, null);
                    else
                        ramBytes.Add(0xA, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.SegBStart);
                    if (dma == null)
                        ramBytes.Add(0xB, null);
                    else
                        ramBytes.Add(0xB, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.SegCStart);
                    if (dma == null)
                        ramBytes.Add(0xC, null);
                    else
                        ramBytes.Add(0xC, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.SegDStart);
                    if (dma == null)
                        ramBytes.Add(0xD, null);
                    else
                        ramBytes.Add(0xD, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.SegEStart);
                    if (dma == null)
                        ramBytes.Add(0xE, null);
                    else
                        ramBytes.Add(0xE, dma.GetAsBytes());

                    dma = SF64ROM.Instance.DMATable.SingleOrDefault(d => d.DMAInfo.PStart == ramInfo.SegFStart);
                    if (dma == null)
                        ramBytes.Add(0xF, null);
                    else
                        ramBytes.Add(0xF, dma.GetAsBytes());
                }

                foreach(SFLevelObject obj in level.LevelObjects)
                {
                    //We know that the 0x190 is the limit for the simple objects
                    if (obj.ID < 0x190)
                    {
                        obj.DListOffset = _referenceDMA.SimpleObjects[obj.ID].DListOffset;

                        // dlist offset sanity checks
                        if (((obj.DListOffset & 4) != 0x0) ||							// dlist offset not 4 byte aligned
                            ((obj.DListOffset & 0xFF000000) == 0x80000000) ||	// dlist offset lies in ram, leave it alone for now
                            ((obj.DListOffset & 0xFF000000) == 0x00000000))	// invalid dlist
                            obj.DListOffset = 0x00;

                        else
                        {
                            byte[] bytes = ramBytes[(int)((obj.DListOffset & 0xFF000000) >> 24)];

                            if (obj.DListOffset != 0x00 && bytes != null) //Need segment 6
                            {
                                //Load through the F3DEX parser, assign the DisplayListIndex to the Simple Object in the Reference DMA
                                //In the future we'll serialize the F3DEX commands/textures/vertices too, but for functional purposes
                                // we'll just use the blank binary
                                if (_referenceDMA.SimpleObjects[obj.ID].GLDisplayListOffset == F3DEXParser.InvalidBox)
                                    _referenceDMA.SimpleObjects[obj.ID].GLDisplayListOffset = _f3dex.ReadGameObject(level, bytes, obj.DListOffset); //KEEP AN EYE OUT FOR ANY CROSS-DMA REFERENCES
                            }
                        }
                    }
                    //We know that the 0x3E8 is the start of advanced objects
                    else if (obj.ID >= 0x3E8)
                    {
                        int scriptID = obj.ID - 0x3E8;

                        //Have to go digging around and creating the object scripts
                        int scriptStartOffset = StarFoxRomInfo.LevelIndexAndAdvItemIndex[levelIndices.IndexOf(DMATable.IndexOf(level))];

                        if (scriptStartOffset == 0)
                            continue;

                        SFAdvancedObjectScript script = level.LevelScripts.SingleOrDefault(s => s.ScriptIndex == scriptID);

                        if (script == null)
                        {
                            int scriptOffset = ByteHelper.ReadInt(levelBytes, scriptStartOffset + scriptID * 0x4);

                            int scriptOffsetShort = scriptOffset & 0x00FFFFFF;

                            int size = 0;
                            uint nextLine = 0x0;

                            do
                            {
                                nextLine = ByteHelper.ReadUInt(levelBytes, scriptOffsetShort + size);
                                size += 4;
                            } while (nextLine != 0xFE000000);

                            byte[] scriptBytes = new byte[size];
                            Array.Copy(levelBytes, scriptOffsetShort, scriptBytes, 0, size);

                            script = new SFAdvancedObjectScript(scriptOffset, scriptID, scriptBytes);

                            level.LevelScripts.Add(script);

                        }

                        obj.AdvancedScript = script;

                        if (script.AdvancedObjectIndex != -1 && script.AdvancedObjectIndex < _referenceDMA.AdvancedObjects.Count)
                        {
                            obj.DListOffset = _referenceDMA.AdvancedObjects[script.AdvancedObjectIndex].DListOffset;

                            // dlist offset sanity checks
                            if (((obj.DListOffset & 4) != 0x0) ||							// dlist offset not 4 byte aligned
                                ((obj.DListOffset & 0xFF000000) == 0x80000000) ||	// dlist offset lies in ram, leave it alone for now
                                ((obj.DListOffset & 0xFF000000) == 0x00000000))	// invalid dlist
                                obj.DListOffset = 0x00;

                            else
                            {
                                byte[] bytes = ramBytes[(int)((obj.DListOffset & 0xFF000000) >> 24)];

                                if (obj.DListOffset != 0x00 && bytes != null) //Need segment 6
                                {
                                    //Load through the F3DEX parser, assign the DisplayListIndex to the Simple Object in the Reference DMA
                                    //In the future we'll serialize the F3DEX commands/textures/vertices too, but for functional purposes
                                    // we'll just use the blank binary
                                    if (_referenceDMA.AdvancedObjects[script.AdvancedObjectIndex].GLDisplayListOffset == F3DEXParser.InvalidBox)
                                        _referenceDMA.AdvancedObjects[script.AdvancedObjectIndex].GLDisplayListOffset = _f3dex.ReadGameObject(level, bytes, obj.DListOffset); //KEEP AN EYE OUT FOR ANY CROSS-DMA REFERENCES
                                }
                            }
                        }
                    }
                }
            }

            _resourcesLoaded = true;
        }

        public TreeNode GetTreeNode()
        {
            TreeNode node = new TreeNode();

            node.Text = "SF64Rom";

            node.Tag = this;

            foreach (DMAFile dma in DMATable)
            {
                node.Nodes.Add(dma.GetTreeNode());
            }

            return node;
        }

    }
}
