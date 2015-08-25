using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace NewSF64Toolkit
{
    public class StarFoxLevelLoader
    {
        public struct GameObject
        {
	        public float LvlPos;
            public short X;
            public short Y;
            public short Z;
            public short XRot;
            public short YRot;
            public short ZRot;
            public ushort ID;
            public ushort Unk;

            public uint DListOffset;
        }

        public List<GameObject> GameObjects;
        public List<string> ErrorLog;

        F3DEXParser _parser;

        public StarFoxLevelLoader(F3DEXParser parser)
        {
            _parser = parser;

            GameObjects = new List<GameObject>();
            ErrorLog = new List<string>();
        }

        bool CheckAddressValidity(byte bankNo, uint offset)
        {
            if (offset == 0) return false;

            if (!MemoryManager.Instance.HasBank(bankNo))
            {
		        ErrorLog.Add(string.Format("- Warning: Segment 0x{0:X2} was not initialized, cannot access offset 0x{0:X6}!\n", bankNo, offset));
		        return false;
	        }
            else if (!MemoryManager.Instance.LocateBank(bankNo, offset).IsValid())
            {
                ErrorLog.Add(string.Format("- Warning: Offset 0x{0:X6} is out of bounds for segment 0x{0:X2}!\n", offset, bankNo));
		        return false;
	        }

	        return true;
        }


        public void StartReadingLevelDataAt(byte bankNo, uint index)
        {
            //This will need to parse the commands from the level data and pick off the F3DEX
            SFGfx.sv_ClearStructures(false);
            SFGfx.gl_ClearRenderer(true);
            GameObjects.Clear();
            SFGfx.GameObjCount = 0;

            index += 0x44; //Skip over header

            bool one = true;
            while (one)
            {
                if (!CheckAddressValidity(bankNo, index)) break;

                GameObject newObj = new GameObject();


                newObj.LvlPos = MemoryManager.Instance.ReadFloat(bankNo, index);
                newObj.Z = MemoryManager.Instance.ReadShort(bankNo, index + 0x4);
                newObj.X = MemoryManager.Instance.ReadShort(bankNo, index + 0x6);
                newObj.Y = MemoryManager.Instance.ReadShort(bankNo, index + 0x8);
                newObj.XRot = MemoryManager.Instance.ReadShort(bankNo, index + 0xA);
                newObj.YRot = MemoryManager.Instance.ReadShort(bankNo, index + 0xC);
                newObj.ZRot = MemoryManager.Instance.ReadShort(bankNo, index + 0xE);
                newObj.ID = MemoryManager.Instance.ReadUShort(bankNo, index + 0x10);
                newObj.Unk = MemoryManager.Instance.ReadUShort(bankNo, index + 0x12);

                // default dlist offset to 0
                newObj.DListOffset = 0x00;

                // if object id == 0xffff, break out because this marks end of data!
                if (newObj.ID == 0xFFFF) break;

                // if object id < 0x190, get offset like this
                if (newObj.ID < 0x190)
                {
                    //NOTE: SET -2 TO DMA 1
                    newObj.DListOffset = MemoryManager.Instance.ReadUInt((byte)0xFF, (0xC72E4 + ((uint)newObj.ID * 0x24)));
                }

                // dlist offset sanity checks
                if (((newObj.DListOffset & 3) != 0x0) ||							// dlist offset not 4 byte aligned
                  ((newObj.DListOffset & 0xFF000000) == 0x80000000))	// dlist offset lies in ram
                    newObj.DListOffset = 0x00;

                index += 0x14;
                GameObjects.Add(newObj);

                SFGfx.GameObjCount++;
            }

            ExecuteDisplayLists();

            SFCamera.Reset();
        }

        public void ExecuteDisplayLists(int index = -1)
        {
            SFGfx.DLStackPos = 0;

            if (index == -1)
            {
                GL.DeleteLists(SFGfx.GLListBase, SFGfx.GameObjCount);

                SFGfx.GLListBase = (uint)GL.GenLists(GameObjects.Count);
                GL.ListBase(SFGfx.GLListBase);
            }

            for(int ObjectNo = 0; ObjectNo < GameObjects.Count; ObjectNo++)
            {
                if (index != -1 && ObjectNo != index)
                    continue;

                GameObject gameObject = GameObjects[ObjectNo];

                GL.NewList(SFGfx.GLListBase + (uint)ObjectNo, ListMode.Compile);
                
                GL.PushMatrix();

                GL.Translate((float)gameObject.X, (float)gameObject.Y, ((float)gameObject.Z - gameObject.LvlPos));
                GL.Rotate((float)gameObject.XRot, 1.0f, 0, 0);
                GL.Rotate((float)gameObject.YRot, 0, 1.0f, 0);
                GL.Rotate((float)gameObject.ZRot, 0, 0, 1.0f);

                _parser.ReadGameObject(gameObject);

                GL.PopMatrix();
                
                GL.EndList();
            }
        }

        public void SaveGameObject(int levelIndex, int gameObjectIndex)
        {
            //Need to get better linkage between the rom file info and the loader. Maybe make the ROM container a Singleton?
            if (!CheckAddressValidity((byte)0xFF, (uint)0xCE158 + (uint)levelIndex * 0x04)) return;

            uint offset = MemoryManager.Instance.ReadUInt(0xFF, (uint)0xCE158 + (uint)levelIndex * 0x04);
            byte segment = (byte)((offset & 0xFF000000) >> 24);
            offset &= 0x00FFFFFF;


            offset += 0x44 + 0x14 * (uint)gameObjectIndex;

            if (!CheckAddressValidity(segment, offset)) return;

            MemoryManager.Instance.WriteFloat(segment, offset, GameObjects[gameObjectIndex].LvlPos);
            MemoryManager.Instance.WriteShort(segment, offset + 0x4, GameObjects[gameObjectIndex].Z);
            MemoryManager.Instance.WriteShort(segment, offset + 0x6, GameObjects[gameObjectIndex].X);
            MemoryManager.Instance.WriteShort(segment, offset + 0x8, GameObjects[gameObjectIndex].Y);
            MemoryManager.Instance.WriteShort(segment, offset + 0xA, GameObjects[gameObjectIndex].XRot);
            MemoryManager.Instance.WriteShort(segment, offset + 0xC, GameObjects[gameObjectIndex].YRot);
            MemoryManager.Instance.WriteShort(segment, offset + 0xE, GameObjects[gameObjectIndex].ZRot);
            MemoryManager.Instance.WriteUShort(segment, offset + 0x10, GameObjects[gameObjectIndex].ID);
            MemoryManager.Instance.WriteUShort(segment, offset + 0x12, GameObjects[gameObjectIndex].Unk);

        }
    }
}
