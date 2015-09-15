using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.DataStructures.DMA
{
    public class LevelDMAFile : DMAFile
    {
        private int _levelInfoOffset = -1;

        public LevelHeader LevelHeader;
        public List<SFLevelObject> LevelObjects;

        public LevelDMAFile(byte[] data, int offset)
            : base(data)
        {
            _levelInfoOffset = offset & 0x00FFFFFF; //Remove the segment number
            LoadFromBytes(data);
        }

        public override bool LoadFromBytes(byte[] bytes)
        {
            //Need the offset before loading
            if (_levelInfoOffset == -1)
                return false;

            base.LoadFromBytes(bytes);

            if (LevelObjects == null)
                LevelObjects = new List<SFLevelObject>();

            LevelObjects.Clear();

            int levelObjectOffset = _levelInfoOffset;
            byte[] data;

            if (!_dmaData.TakeMemory(levelObjectOffset, LevelHeader.Size, out data))
                return false;

            LevelHeader = new DataObjects.LevelHeader(levelObjectOffset, data);

            levelObjectOffset += LevelHeader.Size;

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

                // if object id < 0x190, get offset like this
                if (newObj.ID < 0x190)
                {
                    //NOTE: SET -2 TO DMA 1
                    //newObj.DListOffset = SF64ROM.Instance.ReferenceDMA.SimpleObjects[newObj.ID].DListOffset;//MemoryManager.Instance.ReadUInt((byte)0xFF, (0xC72E4 + ((uint)newObj.ID * 0x24)));
                }

                // dlist offset sanity checks
                if (((newObj.DListOffset & 3) != 0x0) ||							// dlist offset not 4 byte aligned
                  ((newObj.DListOffset & 0xFF000000) == 0x80000000))	// dlist offset lies in ram
                    newObj.DListOffset = 0x00;

                levelObjectOffset += SFLevelObject.Size;
                LevelObjects.Add(newObj);
            }

            return true;
        }

        public override byte[] GetAsBytes()
        {
            byte[] bytes = base.GetAsBytes();

            int levelObjectOffset = _levelInfoOffset;

            Array.Copy(LevelHeader.GetAsBytes(), 0, bytes, levelObjectOffset, LevelHeader.Size);

            levelObjectOffset += LevelHeader.Size;

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

    }
}
