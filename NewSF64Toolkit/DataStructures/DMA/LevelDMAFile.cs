using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;
using System.Windows.Forms;

namespace NewSF64Toolkit.DataStructures.DMA
{
    public class LevelDMAFile : DMAFile
    {
        private int _levelInfoOffset = -1;
        private int _levelHeaderOffset = -1;

        public LevelHeader LevelHeader;
        public List<SFLevelObject> LevelObjects;
        public List<SFAdvancedObjectScript> LevelScripts;

        public LevelDMAFile(byte[] data, int dmaIndex, int headerOffset, int objectOffset)
            : base(data, dmaIndex)
        {
            _levelHeaderOffset = headerOffset & 0x00FFFFFF; //Remove the segment number
            _levelInfoOffset = objectOffset & 0x00FFFFFF; //Remove the segment number
            LoadFromBytes(data);
        }

        public override bool LoadFromBytes(byte[] bytes)
        {
            //Need the offset before loading
            if (_levelInfoOffset == -1 || _levelHeaderOffset == -1)
                return false;

            base.LoadFromBytes(bytes);

            if (LevelObjects == null)
                LevelObjects = new List<SFLevelObject>();

            LevelObjects.Clear();

            if (LevelScripts == null)
                LevelScripts = new List<SFAdvancedObjectScript>();

            LevelScripts.Clear();

            byte[] data;

            if (!_dmaData.TakeMemory(_levelHeaderOffset, LevelHeader.Size, out data))
                return false;

            LevelHeader = new DataObjects.LevelHeader(_levelHeaderOffset, data);

            int levelObjectOffset = _levelInfoOffset;

            bool one = true;
            while (one)
            {
                if (!_dmaData.TakeMemory(levelObjectOffset, SFLevelObject.Size, out data))
                    break;

                SFLevelObject newObj = new SFLevelObject(levelObjectOffset, data);

                // default dlist offset to 0
                newObj.DListOffset = 0x00;

                // if object id == 0xffff, break out because this marks end of data!
                if (newObj.ID == 0xFFFF) break;

                levelObjectOffset += SFLevelObject.Size;
                LevelObjects.Add(newObj);
            }

            return true;
        }

        public override byte[] GetAsBytes()
        {
            byte[] bytes = base.GetAsBytes();

            if (IsCompressed)
                return bytes;

            Array.Copy(LevelHeader.GetAsBytes(), 0, bytes, _levelHeaderOffset, LevelHeader.Size);

            int levelObjectOffset = _levelInfoOffset;

            for(int i = 0; i < LevelObjects.Count; i++)
            {
                ByteHelper.WriteFloat(LevelObjects[i].LvlPos, bytes, levelObjectOffset);
                ByteHelper.WriteShort(LevelObjects[i].Z, bytes, levelObjectOffset + 0x4);
                ByteHelper.WriteShort(LevelObjects[i].X, bytes, levelObjectOffset + 0x6);
                ByteHelper.WriteShort(LevelObjects[i].Y, bytes, levelObjectOffset + 0x8);
                ByteHelper.WriteShort(LevelObjects[i].XRot, bytes, levelObjectOffset + 0xA);
                ByteHelper.WriteShort(LevelObjects[i].YRot, bytes, levelObjectOffset + 0xC);
                ByteHelper.WriteShort(LevelObjects[i].ZRot, bytes, levelObjectOffset + 0xE);
                ByteHelper.WriteUShort(LevelObjects[i].ID, bytes, levelObjectOffset + 0x10);
                ByteHelper.WriteUShort(LevelObjects[i].Unk, bytes, levelObjectOffset + 0x12);

                levelObjectOffset += SFLevelObject.Size;
            }

            //Make the final level ending entry
            ByteHelper.WriteUShort(0xFFFF, bytes, levelObjectOffset + 0x10);

            return bytes;
        }

        public override TreeNode GetTreeNode()
        {
            TreeNode node = new TreeNode();

            node.Text = string.Format("DMA {0} - Level File ({1})", Index, 
                StarFoxRomInfo.LevelNamesByIndex[StarFoxRomInfo.DMATableToLevelIndex(Index)]);

            node.Tag = this;


            TreeNode LevelObjectTable = new TreeNode();
            LevelObjectTable.Text = "Level Objects Table";
            LevelObjectTable.Tag = LevelObjects;
            int objCount = 0;
            foreach (SFLevelObject obj in LevelObjects)
            {
                TreeNode newN = new TreeNode();
                newN.Tag = obj;
                newN.Text = "Entry " + objCount;
                objCount++;
                LevelObjectTable.Nodes.Add(newN);
            }

            node.Nodes.Add(LevelObjectTable);

            //foreach (DMAFile dma in DMATable)
            //{
            //    node.Nodes.Add(dma.GetTreeNode());
            //}

            return node;
        }

    }
}
