using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using NewSF64Toolkit.OpenGL;
using NewSF64Toolkit.OpenGL.F3DEX;

namespace NewSF64Toolkit
{
    public class StarFoxLevelLoader
    {
        public List<string> ErrorLog;

        F3DEXParser _parser;

        public StarFoxLevelLoader(F3DEXParser parser)
        {
            _parser = parser;

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
            SFGfx.GameObjects.Clear();
            SFGfx.GameObjCount = 0;

            index += 0x44; //Skip over header

            bool one = true;
            while (one)
            {
                if (!CheckAddressValidity(bankNo, index)) break;

                SFGfx.GameObject newObj = new SFGfx.GameObject();


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
                SFGfx.GameObjects.Add(newObj);

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
                GL.DeleteLists(SFGfx.GLListBase, SFGfx.GameObjCount * 3);

                SFGfx.GLListBase = (uint)GL.GenLists(SFGfx.GameObjects.Select(x => x.DListOffset).Distinct().Count() * 3);
                GL.ListBase(SFGfx.GLListBase);
                SFGfx.GameObjectDListIndices.Clear();
                SFGfx.SelectedGameObjectDListIndices.Clear();
                SFGfx.WireframeGameObjectDListIndices.Clear();
            }

            uint glListCount = 0;

            for (int ObjectNo = 0; ObjectNo < SFGfx.GameObjects.Count; ObjectNo++)
            {
                if (index != -1 && ObjectNo != index)
                    continue;

                SFGfx.GameObject gameObject = SFGfx.GameObjects[ObjectNo];

                if (SFGfx.GameObjectDListIndices.ContainsKey(gameObject.DListOffset))
                    continue;

                GL.NewList(SFGfx.GLListBase + glListCount, ListMode.Compile);
                
                GL.PushMatrix();

                _parser.DrawingMode = F3DEXParser.DrawingModeType.Texture;

                _parser.ReadGameObject(gameObject);

                GL.PopMatrix();
                
                GL.EndList();

                SFGfx.GameObjectDListIndices.Add(gameObject.DListOffset, SFGfx.GLListBase + glListCount);

                glListCount++;

                //Highlighted
                GL.NewList(SFGfx.GLListBase + glListCount, ListMode.Compile);

                GL.PushMatrix();

                _parser.DrawingMode = F3DEXParser.DrawingModeType.TextureSelected;

                _parser.ReadGameObject(gameObject);

                GL.PopMatrix();

                GL.EndList();

                SFGfx.SelectedGameObjectDListIndices.Add(gameObject.DListOffset, SFGfx.GLListBase + glListCount);

                glListCount++;

                //Wireframe
                GL.NewList(SFGfx.GLListBase + glListCount, ListMode.Compile);

                GL.PushMatrix();

                _parser.DrawingMode = F3DEXParser.DrawingModeType.Wireframe;

                _parser.ReadGameObject(gameObject);

                GL.PopMatrix();

                GL.EndList();

                SFGfx.WireframeGameObjectDListIndices.Add(gameObject.DListOffset, SFGfx.GLListBase + glListCount);

                glListCount++;
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

            MemoryManager.Instance.WriteFloat(segment, offset, SFGfx.GameObjects[gameObjectIndex].LvlPos);
            MemoryManager.Instance.WriteShort(segment, offset + 0x4, SFGfx.GameObjects[gameObjectIndex].Z);
            MemoryManager.Instance.WriteShort(segment, offset + 0x6, SFGfx.GameObjects[gameObjectIndex].X);
            MemoryManager.Instance.WriteShort(segment, offset + 0x8, SFGfx.GameObjects[gameObjectIndex].Y);
            MemoryManager.Instance.WriteShort(segment, offset + 0xA, SFGfx.GameObjects[gameObjectIndex].XRot);
            MemoryManager.Instance.WriteShort(segment, offset + 0xC, SFGfx.GameObjects[gameObjectIndex].YRot);
            MemoryManager.Instance.WriteShort(segment, offset + 0xE, SFGfx.GameObjects[gameObjectIndex].ZRot);
            MemoryManager.Instance.WriteUShort(segment, offset + 0x10, SFGfx.GameObjects[gameObjectIndex].ID);
            MemoryManager.Instance.WriteUShort(segment, offset + 0x12, SFGfx.GameObjects[gameObjectIndex].Unk);

        }
    }
}
