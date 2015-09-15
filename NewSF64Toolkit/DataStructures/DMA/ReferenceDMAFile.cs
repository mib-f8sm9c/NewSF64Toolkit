using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.DataStructures.DMA
{
    /// <summary>
    /// Until we know better what this file is/does, ReferenceDMAFile will be an okay name
    /// </summary>
    public class ReferenceDMAFile : DMAFile
    {
        public List<uint> LevelInfoOffsets;
        private const int LEVEL_INFO_OFFSETS_LOCATION = 0xCE158;
        private const int LEVEL_INFO_OFFSETS_COUNT = 21;

        public List<RefSimpleLevelObject> SimpleObjects;
        private const int SIMPLE_OBJECTS_LOCATION = 0xC72E4;
        private const int SIMPLE_OBJECTS_COUNT = 0x190;

        public List<RAMTableEntry> RAMTable;
        private const int RAM_TABLE_LOCATION = 0xC5BFC;
        private const int RAM_TABLE_COUNT = 33;


        public ReferenceDMAFile(byte[] data)
            : base(data)
        {

        }

        public override bool LoadFromBytes(byte[] bytes)
        {
            base.LoadFromBytes(bytes);

            if (LevelInfoOffsets == null)
                LevelInfoOffsets = new List<uint>();

            LevelInfoOffsets.Clear();

            //Level Info Offsets - I wish I didn't have to hardcode these values' locations, but oh well
            byte[] offsetData;

            _dmaData.TakeMemory(LEVEL_INFO_OFFSETS_LOCATION, LEVEL_INFO_OFFSETS_COUNT * sizeof(uint), out offsetData);

            for (int i = 0; i < LEVEL_INFO_OFFSETS_COUNT; i++)
            {
                LevelInfoOffsets.Add(ByteHelper.ReadUInt(offsetData, i * sizeof(uint)));
            }

            //Simple objects

            if (SimpleObjects == null)
                SimpleObjects = new List<RefSimpleLevelObject>();

            SimpleObjects.Clear();

            for (int i = 0; i < SIMPLE_OBJECTS_COUNT; i++)
            {
                int index = SIMPLE_OBJECTS_LOCATION + i * RefSimpleLevelObject.Size;

                _dmaData.TakeMemory(index, RefSimpleLevelObject.Size, out offsetData);

                RefSimpleLevelObject newObj = new RefSimpleLevelObject(index, offsetData);
                
                SimpleObjects.Add(newObj);
            }

            //RAM table

            if (RAMTable == null)
                RAMTable = new List<RAMTableEntry>();

            RAMTable.Clear();

            for (int i = 0; i < RAM_TABLE_COUNT; i++)
            {
                int index = RAM_TABLE_LOCATION + i * RAMTableEntry.Size;

                _dmaData.TakeMemory(index, RAMTableEntry.Size, out offsetData);

                RAMTableEntry newObj = new RAMTableEntry(index, offsetData);

                RAMTable.Add(newObj);
            }



            return true;
        }

        public override byte[] GetAsBytes()
        {
            byte[] bytes = base.GetAsBytes();

            for (int i = 0; i < LEVEL_INFO_OFFSETS_COUNT; i++)
            {
                ByteHelper.WriteUInt(LevelInfoOffsets[i], bytes, LEVEL_INFO_OFFSETS_LOCATION + i * sizeof(uint));
            }

            for (int i = 0; i < SIMPLE_OBJECTS_COUNT; i++)
            {
                Array.Copy(SimpleObjects[i].GetAsBytes(), 0, bytes, SIMPLE_OBJECTS_LOCATION + i * RefSimpleLevelObject.Size,
                    RefSimpleLevelObject.Size);
            }

            for (int i = 0; i < RAM_TABLE_COUNT; i++)
            {
                Array.Copy(RAMTable[i].GetAsBytes(), 0, bytes, RAM_TABLE_LOCATION + i * RAMTableEntry.Size,
                    RAMTableEntry.Size);
            }

            return bytes;
        }

    }
}
