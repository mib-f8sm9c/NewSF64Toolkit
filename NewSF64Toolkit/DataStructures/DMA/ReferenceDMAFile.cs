using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;
using System.Windows.Forms;

namespace NewSF64Toolkit.DataStructures.DMA
{
    /// <summary>
    /// Until we know better what this file is/does, ReferenceDMAFile will be an okay name
    /// </summary>
    public class ReferenceDMAFile : DMAFile
    {
        public List<uint> LevelHeaderOffsets;
        public List<uint> LevelInfoOffsets;
        private const int LEVEL_HEADER_OFFSETS_LOCATION = 0xCE158;
        private const int LEVEL_INFO_OFFSETS_LOCATION = 0xCAF60;
        private const int LEVEL_COUNT = 21;

        public RefSimpleLevelObject InvalidSimpObject;
        public List<RefSimpleLevelObject> SimpleObjects;
        private const int SIMPLE_OBJECTS_LOCATION = 0xC72E4;
        private const int SIMPLE_OBJECTS_COUNT = 0x190;

        public RefAdvancedLevelObject InvalidAdvObject;
        public List<RefAdvancedLevelObject> AdvancedObjects;
        private const int ADVANCED_OBJECTS_LOCATION = 0xCB1FC;
        private const int ADVANCED_OBJECTS_COUNT = 0x6C;

        public List<RAMTableEntry> RAMTable;
        private const int RAM_TABLE_LOCATION = 0xC5BFC;
        private const int RAM_TABLE_COUNT = 33;


        public ReferenceDMAFile(byte[] data, int dmaIndex)
            : base(data, dmaIndex)
        {

        }

        public override bool LoadFromBytes(byte[] bytes)
        {
            base.LoadFromBytes(bytes);

            if (LevelInfoOffsets == null)
                LevelInfoOffsets = new List<uint>();

            if (LevelHeaderOffsets == null)
                LevelHeaderOffsets = new List<uint>();

            LevelInfoOffsets.Clear();
            LevelHeaderOffsets.Clear();

            //Level Info Offsets - I wish I didn't have to hardcode these values' locations, but oh well
            byte[] offsetData;

            _dmaData.TakeMemory(LEVEL_INFO_OFFSETS_LOCATION, LEVEL_COUNT * sizeof(uint), out offsetData);

            for (int i = 0; i < LEVEL_COUNT; i++)
            {
                LevelInfoOffsets.Add(ByteHelper.ReadUInt(offsetData, i * sizeof(uint)));
            }

            _dmaData.TakeMemory(LEVEL_HEADER_OFFSETS_LOCATION, LEVEL_COUNT * sizeof(uint), out offsetData);

            for (int i = 0; i < LEVEL_COUNT; i++)
            {
                LevelHeaderOffsets.Add(ByteHelper.ReadUInt(offsetData, i * sizeof(uint)));
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

            //Advanced objects

            if (AdvancedObjects == null)
                AdvancedObjects = new List<RefAdvancedLevelObject>();

            AdvancedObjects.Clear();

            for (int i = 0; i < ADVANCED_OBJECTS_COUNT; i++)
            {
                int index = ADVANCED_OBJECTS_LOCATION + i * RefAdvancedLevelObject.Size;

                _dmaData.TakeMemory(index, RefAdvancedLevelObject.Size, out offsetData);

                RefAdvancedLevelObject newObj = new RefAdvancedLevelObject(index, offsetData);

                AdvancedObjects.Add(newObj);
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

            for (int i = 0; i < LEVEL_COUNT; i++)
            {
                ByteHelper.WriteUInt(LevelInfoOffsets[i], bytes, LEVEL_INFO_OFFSETS_LOCATION + i * sizeof(uint));
                ByteHelper.WriteUInt(LevelHeaderOffsets[i], bytes, LEVEL_HEADER_OFFSETS_LOCATION + i * sizeof(uint));
            }

            for (int i = 0; i < SIMPLE_OBJECTS_COUNT; i++)
            {
                Array.Copy(SimpleObjects[i].GetAsBytes(), 0, bytes, SIMPLE_OBJECTS_LOCATION + i * RefSimpleLevelObject.Size,
                    RefSimpleLevelObject.Size);
            }

            for (int i = 0; i < ADVANCED_OBJECTS_COUNT; i++)
            {
                Array.Copy(AdvancedObjects[i].GetAsBytes(), 0, bytes, ADVANCED_OBJECTS_LOCATION + i * RefAdvancedLevelObject.Size,
                    RefAdvancedLevelObject.Size);
            }

            for (int i = 0; i < RAM_TABLE_COUNT; i++)
            {
                Array.Copy(RAMTable[i].GetAsBytes(), 0, bytes, RAM_TABLE_LOCATION + i * RAMTableEntry.Size,
                    RAMTableEntry.Size);
            }

            return bytes;
        }

        public override TreeNode GetTreeNode()
        {
            TreeNode node = new TreeNode();

            node.Text = "DMA " + Index + " - Reference File";

            node.Tag = this;

            TreeNode RamTable = new TreeNode();
            RamTable.Text = "Ram Table";
            node.Tag = RAMTable;
            int ramCount = 0;
            foreach (RAMTableEntry ram in RAMTable)
            {
                TreeNode newN = new TreeNode();
                newN.Tag = ram;
                newN.Text = "Entry " + ramCount;
                ramCount++;
                RamTable.Nodes.Add(newN);
            }

            node.Nodes.Add(RamTable);


            TreeNode SimpleTable = new TreeNode();
            SimpleTable.Text = "Simple Objects Table";
            node.Tag = SimpleObjects;
            int simpleCount = 0;
            foreach (RefSimpleLevelObject simp in SimpleObjects)
            {
                TreeNode newN = new TreeNode();
                newN.Tag = simp;
                newN.Text = "Entry " + simpleCount;
                simpleCount++;
                SimpleTable.Nodes.Add(newN);
            }

            node.Nodes.Add(SimpleTable);


            TreeNode AdvancedTable = new TreeNode();
            AdvancedTable.Text = "Advanced Objects Table";
            node.Tag = AdvancedObjects;
            int advCount = 0;
            foreach (RefAdvancedLevelObject simp in AdvancedObjects)
            {
                TreeNode newN = new TreeNode();
                newN.Tag = simp;
                newN.Text = "Entry " + advCount;
                advCount++;
                AdvancedTable.Nodes.Add(newN);
            }

            node.Nodes.Add(AdvancedTable);

            //foreach (DMAFile dma in DMATable)
            //{
            //    node.Nodes.Add(dma.GetTreeNode());
            //}

            return node;
        }

    }
}
