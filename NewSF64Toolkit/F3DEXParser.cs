using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace NewSF64Toolkit
{
    public class F3DEXParser
    {
        public struct BankData
        {
            public uint VirtualStart;
            public uint VirtualEnd;
            public byte[] Data;

            public BankData(uint vStart, uint vEnd, byte[] data)
            {
                VirtualStart = vStart;
                VirtualEnd = vEnd;
                Data = data;
            }

            public bool IsValid()
            {
                return Data != null;
            }
        }

        private Dictionary<byte, List<BankData>> _dataBanks;
        private OpenGLControl _viewer;

        public F3DEXParser(OpenGLControl viewer)
        {
            _viewer = viewer;
            _dataBanks = new Dictionary<byte, List<BankData>>();
        }

        #region Data Bank Functions

        public void AddBank(byte bankNo, byte[] bankData, uint startPos)
        {
            //Check that the bank won't interfere with others
            if(_dataBanks.ContainsKey(bankNo))
            {
                foreach (BankData b in _dataBanks[bankNo])
                {
                    if (Overlaps(b.VirtualStart, b.VirtualEnd, startPos, startPos + (uint)bankData.Length))
                    {
                        //Throw error
                        return;
                    }
                }
            }
            else
            {
                //Add in the bank
                _dataBanks.Add(bankNo, new List<BankData>());
            }

            _dataBanks[bankNo].Add(new BankData(startPos, startPos + (uint)bankData.Length, bankData));
        }

        public void RemoveBank(byte bankNo, uint startPos)
        {
            int bankIndex;

            if (_dataBanks.ContainsKey(bankNo) && (bankIndex = _dataBanks[bankNo].FindIndex(b => b.VirtualStart == startPos)) >= 0)
            {
                _dataBanks[bankNo].RemoveAt(bankIndex);
            }
        }

        public void ClearBanks()
        {
            _dataBanks.Clear();
        }

        private bool Overlaps(uint start1, uint end1, uint start2, uint end2) //exclusive end
        {
            if (start2 <= start1 && start1 < end2)
                return true;

            if (start2 < end1 && end1 <= end2)
                return true;

            if (start1 <= start2 && start2 < end1)
                return true;

            if (start1 < end2 && end2 <= end1)
                return true;

            return false;
        }

        public BankData LocateBank(byte bankNo, uint offset)
        {
            if (_dataBanks.ContainsKey(bankNo))
            {
                if (_dataBanks[bankNo].Count(b => b.VirtualStart <= offset && offset < b.VirtualEnd) > 0)
                {
                    return _dataBanks[bankNo].First(b => b.VirtualStart <= offset && offset < b.VirtualEnd);
                }
            }

            return new BankData();
        }

        public uint ReadUInt(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ToolSettings.ReadUInt(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return uint.MaxValue;
        }

        public int ReadInt(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ToolSettings.ReadInt(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return int.MaxValue;
        }

        public ushort ReadUShort(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ToolSettings.ReadUShort(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return ushort.MaxValue;
        }

        public short ReadShort(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ToolSettings.ReadShort(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return short.MaxValue;
        }

        public byte ReadByte(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ToolSettings.ReadByte(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return byte.MaxValue;
        }

        public float ReadFloat(byte bankNo, uint offset)
        {
            BankData bank = LocateBank(bankNo, offset);

            if (bank.IsValid())
            {
                return ToolSettings.ReadFloat(bank.Data, offset - bank.VirtualStart);
            }

            //Bad value
            return float.MaxValue;
        }

        public bool HasBank(byte bankNo)
        {
            return _dataBanks.ContainsKey(bankNo);
        }

#endregion

        public void ReadGameObject(StarFoxLevelLoader.GameObject gameObject)
        {
            byte bankNo = (byte)((gameObject.DListOffset & 0xFF000000) >> 24);
            uint offset = gameObject.DListOffset & 0x00FFFFFF;

            if (offset == 0 || !HasBank(bankNo) || !LocateBank(bankNo, offset).IsValid())
            {
                //Draw the invalid model
                GL.Disable(EnableCap.Lighting);
                GL.Disable(EnableCap.Texture2D);

                GL.Begin(PrimitiveType.Quads);
                GL.Color3(1.0f, 0.0f, 0.0f);

                GL.Vertex3(15.0f, 15.0f, 15.0f);   //V2
                GL.Vertex3(15.0f, -15.0f, 15.0f);   //V1
                GL.Vertex3(15.0f, -15.0f, -15.0f);   //V3
                GL.Vertex3(15.0f, 15.0f, -15.0f);   //V4

                GL.Vertex3(15.0f, 15.0f, -15.0f);   //V4
                GL.Vertex3(15.0f, -15.0f, -15.0f);   //V3
                GL.Vertex3(-15.0f, -15.0f, -15.0f);   //V5
                GL.Vertex3(-15.0f, 15.0f, -15.0f);   //V6

                GL.Vertex3(-15.0f, 15.0f, -15.0f);   //V6
                GL.Vertex3(-15.0f, -15.0f, -15.0f);   //V5
                GL.Vertex3(-15.0f, -15.0f, 15.0f);   //V7
                GL.Vertex3(-15.0f, 15.0f, 15.0f);   //V8

                GL.Vertex3(-15.0f, 15.0f, -15.0f);   //V6
                GL.Vertex3(-15.0f, 15.0f, 15.0f);   //V8
                GL.Vertex3(15.0f, 15.0f, 15.0f);   //V2
                GL.Vertex3(15.0f, 15.0f, -15.0f);   //V4

                GL.Vertex3(-15.0f, -15.0f, 15.0f);   //V7
                GL.Vertex3(-15.0f, -15.0f, -15.0f);   //V5
                GL.Vertex3(15.0f, -15.0f, -15.0f);   //V3
                GL.Vertex3(15.0f, -15.0f, 15.0f);   //V1

                //front
                GL.Color3(1.0f, 1.0f, 1.0f);

                GL.Vertex3(-15.0f, 15.0f, 15.0f);   //V8
                GL.Vertex3(-15.0f, -15.0f, 15.0f);   //V7
                GL.Vertex3(15.0f, -15.0f, 15.0f);   //V1
                GL.Vertex3(15.0f, 15.0f, 15.0f);   //V2
                GL.End();

                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Lighting);
            }
            else
            {
                SFGfx.DLStackPos = 0;
                ParseDisplayList(gameObject.DListOffset);
            }
        }

        public void UcodeCmd(byte line)
        {
            switch (line)
            {
                case 0x01: //dl_F3DEX_MTX
                    F3DEX_MTX();
                    break;
                case 0x03: //dl_F3DEX_MOVEMEM
                    F3DEX_MOVEMEM();
                    break;
                case 0x04: //dl_F3DEX_VTX
                    F3DEX_VTX();
                    break;
                case 0x06: //dl_F3DEX_DL
                    F3DEX_DL();
                    break;
                case 0xB0: //dl_F3DEX_BRANCH_Z
                    F3DEX_BRANCH_Z();
                    break;
                case 0xB1: //dl_F3DEX_TRI2
                    F3DEX_TRI2();
                    break;
                case 0xB2: //dl_F3DEX_MODIFYVTX
                    F3DEX_MODIFYVTX();
                    break;
                case 0xB3: //dl_F3DEX_RDPHALF_2
                    F3DEX_RDPHALF_2();
                    break;
                case 0xB4: //dl_F3DEX_RDPHALF_1
                    F3DEX_RDPHALF_1();
                    break;
                case 0xB6: //dl_F3DEX_CLEARGEOMETRYMODE
                    F3DEX_CLEARGEOMETRYMODE();
                    break;
                case 0xB7: //dl_F3DEX_SETGEOMETRYMODE
                    F3DEX_SETGEOMETRYMODE();
                    break;
                case 0xB8: //dl_F3DEX_ENDDL
                    F3DEX_ENDDL();
                    break;
                case 0xB9: //dl_F3DEX_SETOTHERMODE_L
                    F3DEX_SETOTHERMODE_L();
                    break;
                case 0xBA: //dl_F3DEX_SETOTHERMODE_H
                    F3DEX_SETOTHERMODE_H();
                    break;
                case 0xBB: //dl_F3DEX_TEXTURE
                    F3DEX_TEXTURE();
                    break;
                case 0xBC: //dl_F3DEX_MOVEWORD
                    F3DEX_MOVEWORD();
                    break;
                case 0xBD: //dl_F3DEX_POPMTX
                    F3DEX_POPMTX();
                    break;
                case 0xBE: //dl_F3DEX_CULLDL
                    F3DEX_CULLDL();
                    break;
                case 0xBF: //dl_F3DEX_TRI1
                    F3DEX_TRI1();
                    break;
                case 0xE4: //dl_G_TEXRECT
                    G_TEXRECT();
                    break;
                case 0xE5: //dl_G_TEXRECTFLIP
                    G_TEXRECTFLIP();
                    break;
                case 0xE6: //dl_G_RDPLOADSYNC
                    G_RDPLOADSYNC();
                    break;
                case 0xE7: //dl_G_RDPPIPESYNC
                    G_RDPPIPESYNC();
                    break;
                case 0xE8: //dl_G_RDPTILESYNC
                    G_RDPTILESYNC();
                    break;
                case 0xE9: //dl_G_RDPFULLSYNC
                    G_RDPFULLSYNC();
                    break;
                case 0xEA: //dl_G_SETKEYGB
                    G_SETKEYGB();
                    break;
                case 0xEB: //dl_G_SETKEYR
                    G_SETKEYR();
                    break;
                case 0xEC: //dl_G_SETCONVERT
                    G_SETCONVERT();
                    break;
                case 0xED: //dl_G_SETSCISSOR
                    G_SETSCISSOR();
                    break;
                case 0xEE: //dl_G_SETPRIMDEPTH
                    G_SETPRIMDEPTH();
                    break;
                case 0xEF: //dl_G_RDPSETOTHERMODE
                    G_RDPSETOTHERMODE();
                    break;
                case 0xF0: //dl_G_LOADTLUT
                    G_LOADTLUT();
                    break;
                case 0xF2: //dl_G_SETTILESIZE
                    G_SETTILESIZE();
                    break;
                case 0xF3: //dl_G_LOADBLOCK
                    G_LOADBLOCK();
                    break;
                case 0xF4: //dl_G_LOADTILE
                    G_LOADTILE();
                    break;
                case 0xF5: //dl_G_SETTILE
                    G_SETTILE();
                    break;
                case 0xF6: //dl_G_FILLRECT
                    G_FILLRECT();
                    break;
                case 0xF7: //dl_G_SETFILLCOLOR
                    G_SETFILLCOLOR();
                    break;
                case 0xF8: //dl_G_SETFOGCOLOR
                    G_SETFOGCOLOR();
                    break;
                case 0xF9: //dl_G_SETBLENDCOLOR
                    G_SETBLENDCOLOR();
                    break;
                case 0xFA: //dl_G_SETPRIMCOLOR
                    G_SETPRIMCOLOR();
                    break;
                case 0xFB: //dl_G_SETENVCOLOR
                    G_SETENVCOLOR();
                    break;
                case 0xFC: //dl_G_SETCOMBINE
                    G_SETCOMBINE();
                    break;
                case 0xFD: //dl_G_SETTIMG
                    G_SETTIMG();
                    break;
                case 0xFE: //dl_G_SETZIMG
                    G_SETZIMG();
                    break;
                case 0xFF: //dl_G_SETCIMG
                    G_SETCIMG();
                    break;
                default: //dl_UnemulatedCmd
                    UnemulatedCmd();
                    break;
            }
        }
        
        #region F3DEXCommands

        uint DListAddress;
        char Segment;
        uint Offset;

        uint w0, w1;
        uint wp0, wp1;
        uint wn0, wn1;

        /* Currently avoiding this like the plague
        void InitCombiner()
        {
            CreateCombinerProgram(0x0011FFFF, 0xFFFFFC38);
            CreateCombinerProgram(0x00127E03, 0xFFFFFDF8);
            CreateCombinerProgram(0x00127E03, 0xFFFFF3F8);
            CreateCombinerProgram(0x00127E03, 0xFFFFF7F8);
            CreateCombinerProgram(0x00121603, 0xFF5BFFF8);
            CreateCombinerProgram(0x00267E04, 0x1F0CFDFF);
            CreateCombinerProgram(0x0041FFFF, 0xFFFFFC38);
            CreateCombinerProgram(0x00127E0C, 0xFFFFFDF8);
            CreateCombinerProgram(0x00267E04, 0x1FFCFDF8);
            CreateCombinerProgram(0x00262A04, 0x1F0C93FF);
            CreateCombinerProgram(0x00121803, 0xFF5BFFF8);
            CreateCombinerProgram(0x00121803, 0xFF0FFFFF);
            CreateCombinerProgram(0x0041FFFF, 0xFFFFF638);
            CreateCombinerProgram(0x0011FFFF, 0xFFFFF238);
            CreateCombinerProgram(0x0041C7FF, 0xFFFFFE38);
            CreateCombinerProgram(0x0041FFFF, 0xFFFFF838);

            CreateCombinerProgram(0x00127E60, 0xFFFFF3F8);
            CreateCombinerProgram(0x00272C04, 0x1F0C93FF);
            CreateCombinerProgram(0x0020AC04, 0xFF0F93FF);
            CreateCombinerProgram(0x0026A004, 0x1FFC93F8);
            CreateCombinerProgram(0x00277E04, 0x1F0CF7FF);
            CreateCombinerProgram(0x0020FE04, 0xFF0FF7FF);
            CreateCombinerProgram(0x00272E04, 0x1F0C93FF);
            CreateCombinerProgram(0x00272C04, 0x1F1093FF);
            CreateCombinerProgram(0x0020A203, 0xFF13FFFF);
            CreateCombinerProgram(0x0011FE04, 0xFFFFF7F8);
            CreateCombinerProgram(0x0020AC03, 0xFF0F93FF);
            CreateCombinerProgram(0x00272C03, 0x1F0C93FF);
            CreateCombinerProgram(0x0011FE04, 0xFF0FF3FF);
            CreateCombinerProgram(0x00119C04, 0xFFFFFFF8);
            CreateCombinerProgram(0x00271204, 0x1F0CFFFF);
            CreateCombinerProgram(0x0011FE04, 0xFFFFF3F8);
            CreateCombinerProgram(0x00272C80, 0x350CF37F);
        }
        */

        void ParseDisplayList(uint Address)
        {
            char TempSegment;
            uint TempOffset;

            SplitAddress(Address, out TempSegment, out TempOffset);

            if (!LocateBank((byte)TempSegment, TempOffset).IsValid()) return;

            DListAddress = Address;

            //Decode the rest here
            SetRenderMode(0, 0);

            SFGfx.GeometryMode = SFGfx.Constants.G_LIGHTING | SFGfx.Constants.F3DEX_SHADING_SMOOTH;
            SFGfx.ChangedModes |= SFGfx.Constants.CHANGED_GEOMETRYMODE;

            while (SFGfx.DLStackPos >= 0)
            {
                SplitAddress(DListAddress, out Segment, out Offset);

                w0 = ReadUInt((byte)Segment, Offset);//Read32(RAM[Segment].Data, Offset);
                w1 = ReadUInt((byte)Segment, Offset + 4);//RRead32(RAM[Segment].Data, Offset + 4);

                wp0 = ReadUInt((byte)Segment, Offset - 8);//Read32(RAM[Segment].Data, Offset - 8);
                wp1 = ReadUInt((byte)Segment, Offset - 4);//Read32(RAM[Segment].Data, Offset - 4);

                wn0 = ReadUInt((byte)Segment, Offset + 8);//Read32(RAM[Segment].Data, Offset + 8);
                wn1 = ReadUInt((byte)Segment, Offset + 12);//Read32(RAM[Segment].Data, Offset + 12);

                UcodeCmd((byte)(w0 >> 24));//UcodeCmd[(byte)(w0 >> 24)]();

                DListAddress += 8;
            }
        }

        void UnemulatedCmd()
        {
            //Add to the error logs!
            //MSK_ConsolePrint(MSK_COLORTYPE_WARNING, "Illegal Ucode command 0x%02X!", w0 >> 24);
        }

        void F3DEX_MTX()
        {
	        int i = 0, j = 0;
	        long MtxTemp1 = 0, MtxTemp2 = 0;

	        char TempSegment;
            uint TempOffset;
            SplitAddress(w1, out TempSegment, out TempOffset);

            float[] Matrix = new float[16];
	        float fRecip = 1.0f / 65536.0f;

            switch (TempSegment)
            {
		        case (char)0x01:
		        case (char)0x0C:
		        case (char)0x0D:
			        return;
		        case (char)0x80:
                    GL.PopMatrix();
			        return;
	        }

	        for(i = 0; i < 4; i++) {
		        for(j = 0; j < 4; j++) {
                    MtxTemp1 = ReadShort((byte)TempSegment, TempOffset);//((RAM[Segment].Data[Offset		] * 0x100) + RAM[Segment].Data[Offset + 1		]);
                    MtxTemp2 = ReadShort((byte)TempSegment, TempOffset + 32);//((RAM[Segment].Data[Offset + 32	] * 0x100) + RAM[Segment].Data[Offset + 33	]);
			        Matrix[i * 4 + j] = ((MtxTemp1 << 16) | MtxTemp2) * fRecip;

			        Offset += 2;
		        }
	        }

	        GL.PushMatrix();
	        GL.MultMatrix(Matrix); //POSSIBLE BUG, IF ROW-MAJOR AND NOT COLUMN-MAJOR
        }
        
        void F3DEX_MOVEMEM()
        {
	        //Unimplemented
        }

        private uint _SHIFTL(uint a, int b, int c)
        {
            return (((uint)a & (((uint)0x01 << c) - 1)) << b);
        }

        private uint _SHIFTR(uint a, int b, int c)
        {
            return (((uint)a >> b) & (((uint)0x01 << c) - 1));
        }

        private void SplitAddress(uint address, out char segment, out uint offset)
        {
            segment = (char)((address & 0xFF000000) >> 24);
            offset = (address & 0x00FFFFFF);
        }

        void F3DEX_VTX()
        {
            char TempSegment;
            uint TempOffset;

            SplitAddress(w1, out TempSegment, out TempOffset);

	        if(!LocateBank((byte)TempSegment, TempOffset).IsValid()) return;

	        uint V = _SHIFTR( w0, 17, 7 );
	        uint N = _SHIFTR( w0, 10, 6 );

	        if((N > 32) || (V > 32)) return;

	        int i = 0;
	        for(i = 0; i < (N << 4); i += 16) {
                SFGfx.Vertices[V].X = ReadShort((byte)TempSegment, TempOffset + (uint)i);//((RAM[TempSegment].Data[TempOffset + i] << 8) | RAM[TempSegment].Data[TempOffset + i + 1]);
                SFGfx.Vertices[V].Y = ReadShort((byte)TempSegment, TempOffset + (uint)i + 2);//((RAM[TempSegment].Data[TempOffset + i + 2] << 8) | RAM[TempSegment].Data[TempOffset + i + 3]);
                SFGfx.Vertices[V].Z = ReadShort((byte)TempSegment, TempOffset + (uint)i + 4);//((RAM[TempSegment].Data[TempOffset + i + 4] << 8) | RAM[TempSegment].Data[TempOffset + i + 5]);
                SFGfx.Vertices[V].S = ReadShort((byte)TempSegment, TempOffset + (uint)i + 8);//((RAM[TempSegment].Data[TempOffset + i + 8] << 8) | RAM[TempSegment].Data[TempOffset + i + 9]);
                SFGfx.Vertices[V].T = ReadShort((byte)TempSegment, TempOffset + (uint)i + 10);//((RAM[TempSegment].Data[TempOffset + i + 10] << 8) | RAM[TempSegment].Data[TempOffset + i + 11]);
                SFGfx.Vertices[V].R = (char)ReadByte((byte)TempSegment, TempOffset + (uint)i + 12);//RAM[TempSegment].Data[TempOffset + i + 12];
                SFGfx.Vertices[V].G = (char)ReadByte((byte)TempSegment, TempOffset + (uint)i + 13);//RAM[TempSegment].Data[TempOffset + i + 13];
                SFGfx.Vertices[V].B = (char)ReadByte((byte)TempSegment, TempOffset + (uint)i + 14);//RAM[TempSegment].Data[TempOffset + i + 14];
                SFGfx.Vertices[V].A = (char)ReadByte((byte)TempSegment, TempOffset + (uint)i + 15);//RAM[TempSegment].Data[TempOffset + i + 15];

		        V++;
	        }

            //Bring back in after textures are working
	        InitLoadTexture();
        }
        
        void F3DEX_DL()
        {
            char TempSegment;
            uint TempOffset;

            SplitAddress(w1, out TempSegment, out TempOffset);

            if (!LocateBank((byte)TempSegment, TempOffset).IsValid()) return;

	        SFGfx.DLStack[SFGfx.DLStackPos] = DListAddress;
	        SFGfx.DLStackPos++;

	        ParseDisplayList(w1);
        }

        void F3DEX_LOAD_UCODE()
        {
	        //unimplemented
        }

        void F3DEX_BRANCH_Z()
        {
            //NOTE TO SELF: Make this into its own check (CheckAddressValidity())?
            char TempSegment;
            uint TempOffset;

            SplitAddress(SFGfx.Store_RDPHalf1, out TempSegment, out TempOffset);

            if (!LocateBank((byte)TempSegment, TempOffset).IsValid()) return;



	        int Vtx = (int)_SHIFTR(w0, 1, 11);
	        int ZVal = (int)w1;

	        if(SFGfx.Vertices[Vtx].Z < ZVal) {
		        SFGfx.DLStack[SFGfx.DLStackPos] = DListAddress;
		        SFGfx.DLStackPos++;

		        ParseDisplayList(SFGfx.Store_RDPHalf1);
	        }
        }

        void F3DEX_TRI2()
        {
            if (SFGfx.ChangedModes != 0x00) this._viewer.UpdateStates();

	        int[] Vtxs1 = new int[] { (int)_SHIFTR( w0, 17, 7 ), (int)_SHIFTR( w0, 9, 7 ), (int)_SHIFTR( w0, 1, 7 ) };

	        DrawTriangle(Vtxs1);

            if (SFGfx.ChangedModes != 0x00) this._viewer.UpdateStates();

            int[] Vtxs2 = new int[] { (int)_SHIFTR(w1, 17, 7), (int)_SHIFTR(w1, 9, 7), (int)_SHIFTR(w1, 1, 7) };
	        DrawTriangle(Vtxs2);
        }
        
        void F3DEX_MODIFYVTX()
        {
	        //Unimplemented
        }

        void F3DEX_RDPHALF_2()
        {
	        SFGfx.Store_RDPHalf2 = w1;
        }

        void F3DEX_RDPHALF_1()
        {
	        SFGfx.Store_RDPHalf1 = w1;
        }

        void F3DEX_CLEARGEOMETRYMODE()
        {
	        SFGfx.GeometryMode &= ~w1;

	        SFGfx.ChangedModes |= SFGfx.Constants.CHANGED_GEOMETRYMODE;
        }
        
        void F3DEX_SETGEOMETRYMODE()
        {
	        SFGfx.GeometryMode |= w1;

	        SFGfx.ChangedModes |= SFGfx.Constants.CHANGED_GEOMETRYMODE;
        }

        void F3DEX_ENDDL()
        {
	        SFGfx.DLStackPos--;
            if (SFGfx.DLStackPos >= 0)
                DListAddress = SFGfx.DLStack[SFGfx.DLStackPos];
            else
                DListAddress = 0x0;
        }
        
        void F3DEX_SETOTHERMODE_L()
        {
	        switch(_SHIFTR( w0, 8, 8 )) {
                case SFGfx.Constants.G_MDSFT_RENDERMODE:
			        SetRenderMode(w1 & 0xCCCCFFFF, w1 & 0x3333FFFF);
			        break;
		        default: {
			        uint shift = _SHIFTR( w0, 8, 8 );
			        uint length = _SHIFTR( w0, 0, 8 );
                    uint mask = (uint)((1 << (int)length) - 1) << (int)shift;

			        SFGfx.OtherModeL &= ~mask;
			        SFGfx.OtherModeL |= w1 & mask;

                    SFGfx.ChangedModes |= SFGfx.Constants.CHANGED_RENDERMODE | SFGfx.Constants.CHANGED_ALPHACOMPARE;
			        break;
		        }
	        }
        }
        
        void F3DEX_SETOTHERMODE_H()
        {
	        switch(_SHIFTR(w0, 8, 8)) {
		        default: {
			        uint shift = _SHIFTR(w0, 8, 8);
			        uint length = _SHIFTR(w0, 0, 8);
			        uint mask = (uint)((1 << (int)length) - 1) << (int)shift;

			        SFGfx.OtherModeH &= ~mask;
			        SFGfx.OtherModeH |= w1 & mask;
			        break;
		        }
	        }
        }

        void F3DEX_TEXTURE()
        {
	        SFGfx.Textures[0].ScaleS = FIXED2FLOAT(_SHIFTR(w1, 16, 16), 16);
	        SFGfx.Textures[0].ScaleT = FIXED2FLOAT(_SHIFTR(w1, 0, 16), 16);

            if (SFGfx.Textures[0].ScaleS == 0.0f) SFGfx.Textures[0].ScaleS = 1.0f;
            if (SFGfx.Textures[0].ScaleT == 0.0f) SFGfx.Textures[0].ScaleT = 1.0f;

	        SFGfx.Textures[1].ScaleS = SFGfx.Textures[0].ScaleS;
	        SFGfx.Textures[1].ScaleT = SFGfx.Textures[0].ScaleT;
        }
        
        void F3DEX_MOVEWORD()
        {
	        //Unimplemented
        }

        void F3DEX_POPMTX()
        {
	        GL.PopMatrix();
        }

        void F3DEX_CULLDL()
        {
	        //Unimplemented
        }

        void F3DEX_TRI1()
        {
	        if(SFGfx.ChangedModes != 0x0) this._viewer.UpdateStates();

            int[] Vtxs = new int[] { (int)_SHIFTR(w1, 17, 7), (int)_SHIFTR(w1, 9, 7), (int)_SHIFTR(w1, 1, 7) };
	        DrawTriangle(Vtxs);
        }
        
        void G_TEXRECT()
        {
	        //Unimplemented
        }

        void G_TEXRECTFLIP()
        {
	        //Unimplemented
        }

        void G_RDPLOADSYNC()
        {
	        //Unimplemented
        }

        void G_RDPPIPESYNC()
        {
	        //Unimplemented
        }

        void G_RDPTILESYNC()
        {
	        //Unimplemented
        }

        void G_RDPFULLSYNC()
        {
	        //Unimplemented
        }

        void G_SETKEYGB()
        {
	        //Unimplemented
        }

        void G_SETKEYR()
        {
	        //Unimplemented
        }

        void G_SETCONVERT()
        {
	        //Unimplemented
        }

        void G_SETSCISSOR()
        {
	        //Unimplemented
        }

        void G_SETPRIMDEPTH()
        {
	        //Unimplemented
        }

        void G_RDPSETOTHERMODE()
        {
	        //Unimplemented
        }
        
        void G_LOADTLUT()
        {
            char PalSegment;
            uint PalOffset;

            SplitAddress(SFGfx.Textures[SFGfx.CurrentTexture].PalOffset, out PalSegment, out PalOffset);

            if (!LocateBank((byte)PalSegment, PalOffset).IsValid()) return;
            
	        uint PalSize = ((w1 & 0x00FFF000) >> 14) + 1;

	        ushort Raw;
	        char R, G, B, A;

	        uint PalLoop;

	        for(PalLoop = 0; PalLoop < PalSize; PalLoop++) {
                Raw = ReadUShort((byte)PalSegment, PalOffset);//(RAM[PalSegment].Data[PalOffset] << 8) | RAM[PalSegment].Data[PalOffset + 1];

		        R = (char)((Raw & 0xF800) >> 8);
                G = (char)(((Raw & 0x07C0) << 5) >> 8);
                B = (char)(((Raw & 0x003E) << 18) >> 16);

                if (((Raw & 0x0001) != 0x0000)) { A = (char)0xFF; } else { A = (char)0x00; }

		        SFGfx.Palettes[PalLoop].R = R;
                SFGfx.Palettes[PalLoop].G = G;
                SFGfx.Palettes[PalLoop].B = B;
                SFGfx.Palettes[PalLoop].A = A;

		        PalOffset += 2;

                if (!LocateBank((byte)PalSegment, PalOffset).IsValid()) break;
	        }
        }

        void G_SETTILESIZE()
        {
	        ChangeTileSize(_SHIFTR(w1, 24, 3), _SHIFTR(w0, 12, 12), _SHIFTR(w0, 0, 12), _SHIFTR(w1, 12, 12), _SHIFTR(w1, 0, 12));
        }

        void G_LOADBLOCK()
        {
	        ChangeTileSize(_SHIFTR(w1, 24, 3), _SHIFTR(w0, 12, 12), _SHIFTR(w0, 0, 12), _SHIFTR(w1, 12, 12), _SHIFTR(w1, 0, 12));
        }

        void G_LOADTILE()
        {
	        //Unimplemented
        }

        void G_SETTILE()
        {
	        if(w1 == 0x07000000) return;

            SFGfx.Textures[SFGfx.CurrentTexture].Format = (w0 & 0x00FF0000) >> 16;
            SFGfx.Textures[SFGfx.CurrentTexture].CMT = _SHIFTR(w1, 18, 2);
            SFGfx.Textures[SFGfx.CurrentTexture].CMS = _SHIFTR(w1, 8, 2);
            SFGfx.Textures[SFGfx.CurrentTexture].LineSize = _SHIFTR(w0, 9, 9);
            SFGfx.Textures[SFGfx.CurrentTexture].Palette = _SHIFTR(w1, 20, 4);
            SFGfx.Textures[SFGfx.CurrentTexture].ShiftT = _SHIFTR(w1, 10, 4);
            SFGfx.Textures[SFGfx.CurrentTexture].ShiftS = _SHIFTR(w1, 0, 4);
            SFGfx.Textures[SFGfx.CurrentTexture].MaskT = _SHIFTR(w1, 14, 4);
            SFGfx.Textures[SFGfx.CurrentTexture].MaskS = _SHIFTR(w1, 4, 4);
        }

        void G_FILLRECT()
        {
	        //Unimplemented
        }

        void G_SETFILLCOLOR()
        {
	        SFGfx.FillColor.R = _SHIFTR(w1, 11, 5) * 0.032258064f;
	        SFGfx.FillColor.G = _SHIFTR(w1, 6, 5) * 0.032258064f;
	        SFGfx.FillColor.B = _SHIFTR(w1, 1, 5) * 0.032258064f;
	        SFGfx.FillColor.A = _SHIFTR(w1, 0, 1);

	        SFGfx.FillColor.Z = _SHIFTR(w1, 2, 14);
	        SFGfx.FillColor.DZ = _SHIFTR(w1, 0, 2);
        }

        void G_SETFOGCOLOR()
        {
	        SFGfx.FogColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        SFGfx.FogColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        SFGfx.FogColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        SFGfx.FogColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;
        }
        
        void G_SETBLENDCOLOR()
        {
	        SFGfx.BlendColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        SFGfx.BlendColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        SFGfx.BlendColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        SFGfx.BlendColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;

            //Bring in with textures
            //if(OpenGL.Ext_FragmentProgram) {
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 2, Gfx.BlendColor.R, Gfx.BlendColor.G, Gfx.BlendColor.B, Gfx.BlendColor.A);
            //}
        }

        void G_SETPRIMCOLOR()
        {
	        SFGfx.PrimColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        SFGfx.PrimColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        SFGfx.PrimColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        SFGfx.PrimColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;

	        SFGfx.PrimColor.M = (ushort)_SHIFTL(w0, 8, 8);
	        SFGfx.PrimColor.L = _SHIFTL(w0, 0, 8) * 0.0039215689f;

            //Bring in with other textures
            //if(OpenGL.Ext_FragmentProgram) {
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 1, Gfx.PrimColor.R, Gfx.PrimColor.G, Gfx.PrimColor.B, Gfx.PrimColor.A);
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 3, Gfx.PrimColor.L, Gfx.PrimColor.L, Gfx.PrimColor.L, Gfx.PrimColor.L);
            //}
        }

        void G_SETENVCOLOR()
        {
	        SFGfx.EnvColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        SFGfx.EnvColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        SFGfx.EnvColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        SFGfx.EnvColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;

            //Bring in with texture code
            //if(OpenGL.Ext_FragmentProgram) {
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 0, Gfx.EnvColor.R, Gfx.EnvColor.G, Gfx.EnvColor.B, Gfx.EnvColor.A);
            //}
        }

        void G_SETCOMBINE()
        {
	        SFGfx.Combiner0 = (w0 & 0x00FFFFFF);
	        SFGfx.Combiner1 = w1;

            //Bring in with texture code
            //if(OpenGL.Ext_FragmentProgram) dl_CheckFragmentCache();
        }

        void G_SETTIMG()
        {
	        SFGfx.CurrentTexture = 0;
	        SFGfx.IsMultiTexture = false;

	        SFGfx.Textures[SFGfx.CurrentTexture].Offset = w1;
        }

        void G_SETZIMG()
        {
	        //Unimplemented
        }

        void G_SETCIMG()
        {
	        //Unimplemented
        }

        internal float FIXED2FLOAT(uint v, byte b)
        {
            float recip;
            switch (b)
            {
                case 1: recip = 0.5f;
                    break;
                case 2: recip = 0.25f;
                    break;
                case 3: recip = 0.125f;
                    break;
                case 4: recip = 0.0625f;
                    break;
                case 5: recip = 0.03125f;
                    break;
                case 6: recip = 0.015625f;
                    break;
                case 7: recip = 0.0078125f;
                    break;
                case 8: recip = 0.00390625f;
                    break;
                case 9: recip = 0.001953125f;
                    break;
                case 10: recip = 0.0009765625f;
                    break;
                case 11: recip = 0.00048828125f;
                    break;
                case 12: recip = 0.00024414063f;
                    break;
                case 13: recip = 0.00012207031f;
                    break;
                case 14: recip = 6.1035156e-05f;
                    break;
                case 15: recip = 3.0517578e-05f;
                    break;
                case 16: recip = 1.5258789e-05f;
                    break;
                default: recip = 1.0f;
                    break;
            }

            return (float)v * recip;
        }

        internal float FIXED2FLOAT(short v, byte b)
        {
            float recip;
            switch (b)
            {
                case 1: recip = 0.5f;
                    break;
                case 2: recip = 0.25f;
                    break;
                case 3: recip = 0.125f;
                    break;
                case 4: recip = 0.0625f;
                    break;
                case 5: recip = 0.03125f;
                    break;
                case 6: recip = 0.015625f;
                    break;
                case 7: recip = 0.0078125f;
                    break;
                case 8: recip = 0.00390625f;
                    break;
                case 9: recip = 0.001953125f;
                    break;
                case 10: recip = 0.0009765625f;
                    break;
                case 11: recip = 0.00048828125f;
                    break;
                case 12: recip = 0.00024414063f;
                    break;
                case 13: recip = 0.00012207031f;
                    break;
                case 14: recip = 6.1035156e-05f;
                    break;
                case 15: recip = 3.0517578e-05f;
                    break;
                case 16: recip = 1.5258789e-05f;
                    break;
                default: recip = 1.0f;
                    break;
            }

            return (float)v * recip;
        }

        void DrawTriangle(int[] Vtxs)
        {
            GL.Begin(PrimitiveType.Triangles);

	        int i = 0;
	        for(i = 0; i < 3; i++) {
		        float TempS0 = FIXED2FLOAT(SFGfx.Vertices[Vtxs[i]].S, 16) * (SFGfx.Textures[0].ScaleS * SFGfx.Textures[0].ShiftScaleS) / 32.0f / FIXED2FLOAT(SFGfx.Textures[0].RealWidth, 16);
                float TempT0 = FIXED2FLOAT(SFGfx.Vertices[Vtxs[i]].T, 16) * (SFGfx.Textures[0].ScaleT * SFGfx.Textures[0].ShiftScaleT) / 32.0f / FIXED2FLOAT(SFGfx.Textures[0].RealHeight, 16);

                //Bring in with texture code
		        /*if(OpenGL.Ext_MultiTexture) {
			        GL.MultiTexCoord2(TextureUnit.Texture0, TempS0, TempT0);
			        if(SFGfx.IsMultiTexture) {
				        float TempS1 = _FIXED2FLOAT(SFGfx.Vertices[Vtxs[i]].S, 16) * (SFGfx.Textures[1].ScaleS * SFGfx.Textures[1].ShiftScaleS) / 32.0f / _FIXED2FLOAT(SFGfx.Textures[1].RealWidth, 16);
				        float TempT1 = _FIXED2FLOAT(SFGfx.Vertices[Vtxs[i]].T, 16) * (SFGfx.Textures[1].ScaleT * SFGfx.Textures[1].ShiftScaleT) / 32.0f / _FIXED2FLOAT(SFGfx.Textures[1].RealHeight, 16);
				        GL.MultiTexCoord2(TextureUnit.Texture1, TempS1, TempT1);
			        }
		        } else*/ {
			        GL.TexCoord2(TempS0, TempT0);
		        }

        ///*		static bool check = true;
        //        if(check) {
        //            MSK_ConsolePrint(MSK_COLORTYPE_ERROR, "%4.2f, %4.2f, %4.2f, %4.2f -> %4.2f",
        //                _FIXED2FLOAT(Vertex[Vtxs[i]].S, 16), (Texture[0].ScaleS), (Texture[0].ShiftScaleS), _FIXED2FLOAT(Texture[0].RealWidth, 16), TempS0);
        //            check = false;
        //        }
        //* /
		        GL.Normal3(SFGfx.Vertices[Vtxs[i]].R, SFGfx.Vertices[Vtxs[i]].G, SFGfx.Vertices[Vtxs[i]].B);
		        if((SFGfx.GeometryMode & SFGfx.Constants.G_LIGHTING) == 0x0) GL.Color4(SFGfx.Vertices[Vtxs[i]].R, SFGfx.Vertices[Vtxs[i]].G, SFGfx.Vertices[Vtxs[i]].B, SFGfx.Vertices[Vtxs[i]].A);

		        GL.Vertex3(SFGfx.Vertices[Vtxs[i]].X, SFGfx.Vertices[Vtxs[i]].Y, SFGfx.Vertices[Vtxs[i]].Z);
	        }

	        GL.End();
        }

        void SetRenderMode(uint Mode1, uint Mode2)
        {
	        SFGfx.OtherModeL &= 0x00000007;
	        SFGfx.OtherModeL |= Mode1 | Mode2;
            
	        SFGfx.ChangedModes |= SFGfx.Constants.CHANGED_RENDERMODE;
        }
        /* What is this
        void CheckFragmentCache()
        {
	        int CacheCheck = 0; bool SearchingCache = true; bool NewProg = false;
	        while(SearchingCache) {
		        if((FragmentCache[CacheCheck].Combiner0 == SFGfx.Combiner0) && (FragmentCache[CacheCheck].Combiner1 == SFGfx.Combiner1)) {
			        SearchingCache = false;
			        NewProg = false;
		        } else {
			        if(CacheCheck != 256) {
				        CacheCheck++;
			        } else {
				        SearchingCache = false;
				        NewProg = true;
			        }
		        }
	        }

	        GL.Enable(EnableCap.GL_FRAGMENT_PROGRAM_ARB);

	        if(NewProg) {
		        dl_CreateCombinerProgram(Gfx.Combiner0, Gfx.Combiner1);
	        } else {
		        glBindProgramARB(GL_FRAGMENT_PROGRAM_ARB, FragmentCache[CacheCheck].ProgramID);
	        }

	        if(Program.FragCachePosition > 256) {
		        Program.FragCachePosition = 0;
		        memset(FragmentCache, 0x00, sizeof(FragmentCache));
	        }
        }


        // No idea what the hell this is, avoid if at all possible
        void CreateCombinerProgram(unsigned int Cmb0, unsigned int Cmb1)
        {
	        if(!OpenGL.Ext_FragmentProgram) return;

	        int cA[2], cB[2], cC[2], cD[2], aA[2], aB[2], aC[2], aD[2];

	        cA[0] = ((Cmb0 >> 20) & 0x0F);
	        cB[0] = ((Cmb1 >> 28) & 0x0F);
	        cC[0] = ((Cmb0 >> 15) & 0x1F);
	        cD[0] = ((Cmb1 >> 15) & 0x07);

	        aA[0] = ((Cmb0 >> 12) & 0x07);
	        aB[0] = ((Cmb1 >> 12) & 0x07);
	        aC[0] = ((Cmb0 >>  9) & 0x07);
	        aD[0] = ((Cmb1 >>  9) & 0x07);

	        cA[1] = ((Cmb0 >>  5) & 0x0F);
	        cB[1] = ((Cmb1 >> 24) & 0x0F);
	        cC[1] = ((Cmb0 >>  0) & 0x1F);
	        cD[1] = ((Cmb1 >>  6) & 0x07);

	        aA[1] = ((Cmb1 >> 21) & 0x07);
	        aB[1] = ((Cmb1 >>  3) & 0x07);
	        aC[1] = ((Cmb1 >> 18) & 0x07);
	        aD[1] = ((Cmb1 >>  0) & 0x07);

	        char ProgramString[16384];
	        memset(ProgramString, 0x00, sizeof(ProgramString));

	        char * LeadIn =
		        "!!ARBfp1.0\n"
		        "\n"
		        "TEMP Tex0; TEMP Tex1;\n"
		        "TEMP R0; TEMP R1;\n"
		        "TEMP aR0; TEMP aR1;\n"
		        "TEMP Comb; TEMP aComb;\n"
		        "\n"
		        "PARAM EnvColor = program.env[0];\n"
		        "PARAM PrimColor = program.env[1];\n"
		        "PARAM BlendColor = program.env[2];\n"
		        "PARAM PrimColorLOD = program.env[3];\n"
		        "ATTRIB Shade = fragment.color.primary;\n"
		        "\n"
		        "OUTPUT Out = result.color;\n"
		        "\n"
		        "TEX Tex0, fragment.texcoord[0], texture[0], 2D;\n"
		        "TEX Tex1, fragment.texcoord[1], texture[1], 2D;\n"
		        "\n";

	        strcpy(ProgramString, LeadIn);

	        int Cycle = 0, NumCycles = 2;
	        for(Cycle = 0; Cycle < NumCycles; Cycle++) {
		        sprintf(ProgramString, "%s# Color %d\n", ProgramString, Cycle);
		        switch(cA[Cycle]) {
			        case G_CCMUX_COMBINED:
				        strcat(ProgramString, "MOV R0.rgb, Comb;\n");
				        break;
			        case G_CCMUX_TEXEL0:
				        strcat(ProgramString, "MOV R0.rgb, Tex0;\n");
				        break;
			        case G_CCMUX_TEXEL1:
				        strcat(ProgramString, "MOV R0.rgb, Tex1;\n");
				        break;
			        case G_CCMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV R0.rgb, PrimColor;\n");
				        break;
			        case G_CCMUX_SHADE:
				        strcat(ProgramString, "MOV R0.rgb, Shade;\n");
				        break;
			        case G_CCMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV R0.rgb, EnvColor;\n");
				        break;
			        case G_CCMUX_1:
				        strcat(ProgramString, "MOV R0.rgb, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case G_CCMUX_COMBINED_ALPHA:
				        strcat(ProgramString, "MOV R0.rgb, Comb.a;\n");
				        break;
			        case G_CCMUX_TEXEL0_ALPHA:
				        strcat(ProgramString, "MOV R0.rgb, Tex0.a;\n");
				        break;
			        case G_CCMUX_TEXEL1_ALPHA:
				        strcat(ProgramString, "MOV R0.rgb, Tex1.a;\n");
				        break;
			        case G_CCMUX_PRIMITIVE_ALPHA:
				        strcat(ProgramString, "MOV R0.rgb, PrimColor.a;\n");
				        break;
			        case G_CCMUX_SHADE_ALPHA:
				        strcat(ProgramString, "MOV R0.rgb, Shade.a;\n");
				        break;
			        case G_CCMUX_ENV_ALPHA:
				        strcat(ProgramString, "MOV R0.rgb, EnvColor.a;\n");
				        break;
			        case G_CCMUX_LOD_FRACTION:
				        strcat(ProgramString, "MOV R0.rgb, {0.0, 0.0, 0.0, 0.0};\n");	// unemulated
				        break;
			        case G_CCMUX_PRIM_LOD_FRAC:
				        strcat(ProgramString, "MOV R0.rgb, PrimColorLOD;\n");
				        break;
			        case 15:	// 0
				        strcat(ProgramString, "MOV R0.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV R0.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, cA[Cycle]);
				        break;
		        }

		        switch(cB[Cycle]) {
			        case G_CCMUX_COMBINED:
				        strcat(ProgramString, "MOV R1.rgb, Comb;\n");
				        break;
			        case G_CCMUX_TEXEL0:
				        strcat(ProgramString, "MOV R1.rgb, Tex0;\n");
				        break;
			        case G_CCMUX_TEXEL1:
				        strcat(ProgramString, "MOV R1.rgb, Tex1;\n");
				        break;
			        case G_CCMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV R1.rgb, PrimColor;\n");
				        break;
			        case G_CCMUX_SHADE:
				        strcat(ProgramString, "MOV R1.rgb, Shade;\n");
				        break;
			        case G_CCMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV R1.rgb, EnvColor;\n");
				        break;
			        case G_CCMUX_1:
				        strcat(ProgramString, "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case G_CCMUX_COMBINED_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Comb.a;\n");
				        break;
			        case G_CCMUX_TEXEL0_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Tex0.a;\n");
				        break;
			        case G_CCMUX_TEXEL1_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Tex1.a;\n");
				        break;
			        case G_CCMUX_PRIMITIVE_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, PrimColor.a;\n");
				        break;
			        case G_CCMUX_SHADE_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Shade.a;\n");
				        break;
			        case G_CCMUX_ENV_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, EnvColor.a;\n");
				        break;
			        case G_CCMUX_LOD_FRACTION:
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");	// unemulated
				        break;
			        case G_CCMUX_PRIM_LOD_FRAC:
				        strcat(ProgramString, "MOV R1.rgb, PrimColorLOD;\n");
				        break;
			        case 15:	// 0
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, cB[Cycle]);
				        break;
		        }
		        strcat(ProgramString, "SUB R0, R0, R1;\n\n");

		        switch(cC[Cycle]) {
			        case G_CCMUX_COMBINED:
				        strcat(ProgramString, "MOV R1.rgb, Comb;\n");
				        break;
			        case G_CCMUX_TEXEL0:
				        strcat(ProgramString, "MOV R1.rgb, Tex0;\n");
				        break;
			        case G_CCMUX_TEXEL1:
				        strcat(ProgramString, "MOV R1.rgb, Tex1;\n");
				        break;
			        case G_CCMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV R1.rgb, PrimColor;\n");
				        break;
			        case G_CCMUX_SHADE:
				        strcat(ProgramString, "MOV R1.rgb, Shade;\n");
				        break;
			        case G_CCMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV R1.rgb, EnvColor;\n");
				        break;
			        case G_CCMUX_1:
				        strcat(ProgramString, "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case G_CCMUX_COMBINED_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Comb.a;\n");
				        break;
			        case G_CCMUX_TEXEL0_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Tex0.a;\n");
				        break;
			        case G_CCMUX_TEXEL1_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Tex1.a;\n");
				        break;
			        case G_CCMUX_PRIMITIVE_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, PrimColor.a;\n");
				        break;
			        case G_CCMUX_SHADE_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, Shade.a;\n");
				        break;
			        case G_CCMUX_ENV_ALPHA:
				        strcat(ProgramString, "MOV R1.rgb, EnvColor.a;\n");
				        break;
			        case G_CCMUX_LOD_FRACTION:
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");	// unemulated
				        break;
			        case G_CCMUX_PRIM_LOD_FRAC:
				        strcat(ProgramString, "MOV R1.rgb, PrimColorLOD;\n");
				        break;
			        case G_CCMUX_K5:
				        strcat(ProgramString, "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n");	// unemulated
				        break;
			        case G_CCMUX_0:
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, cC[Cycle]);
				        break;
		        }
		        strcat(ProgramString, "MUL R0, R0, R1;\n\n");

		        switch(cD[Cycle]) {
			        case G_CCMUX_COMBINED:
				        strcat(ProgramString, "MOV R1.rgb, Comb;\n");
				        break;
			        case G_CCMUX_TEXEL0:
				        strcat(ProgramString, "MOV R1.rgb, Tex0;\n");
				        break;
			        case G_CCMUX_TEXEL1:
				        strcat(ProgramString, "MOV R1.rgb, Tex1;\n");
				        break;
			        case G_CCMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV R1.rgb, PrimColor;\n");
				        break;
			        case G_CCMUX_SHADE:
				        strcat(ProgramString, "MOV R1.rgb, Shade;\n");
				        break;
			        case G_CCMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV R1.rgb, EnvColor;\n");
				        break;
			        case G_CCMUX_1:
				        strcat(ProgramString, "MOV R1.rgb, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case 7:		// 0
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV R1.rgb, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, cD[Cycle]);
				        break;
		        }
		        strcat(ProgramString, "ADD R0, R0, R1;\n\n");

		        sprintf(ProgramString, "%s# Alpha %d\n", ProgramString, Cycle);

		        switch(aA[Cycle]) {
			        case G_ACMUX_COMBINED:
				        strcat(ProgramString, "MOV aR0.a, aComb;\n");
				        break;
			        case G_ACMUX_TEXEL0:
				        strcat(ProgramString, "MOV aR0.a, Tex0;\n");
				        break;
			        case G_ACMUX_TEXEL1:
				        strcat(ProgramString, "MOV aR0.a, Tex1;\n");
				        break;
			        case G_ACMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV aR0.a, PrimColor;\n");
				        break;
			        case G_ACMUX_SHADE:
				        strcat(ProgramString, "MOV aR0.a, Shade;\n");
				        break;
			        case G_ACMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV aR0.a, EnvColor;\n");
				        break;
			        case G_ACMUX_1:
				        strcat(ProgramString, "MOV aR0.a, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case G_ACMUX_0:
				        strcat(ProgramString, "MOV aR0.a, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV aR0.a, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, aA[Cycle]);
				        break;
		        }

		        switch(aB[Cycle]) {
			        case G_ACMUX_COMBINED:
				        strcat(ProgramString, "MOV aR1.a, aComb.a;\n");
				        break;
			        case G_ACMUX_TEXEL0:
				        strcat(ProgramString, "MOV aR1.a, Tex0.a;\n");
				        break;
			        case G_ACMUX_TEXEL1:
				        strcat(ProgramString, "MOV aR1.a, Tex1.a;\n");
				        break;
			        case G_ACMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV aR1.a, PrimColor.a;\n");
				        break;
			        case G_ACMUX_SHADE:
				        strcat(ProgramString, "MOV aR1.a, Shade.a;\n");
				        break;
			        case G_ACMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV aR1.a, EnvColor.a;\n");
				        break;
			        case G_ACMUX_1:
				        strcat(ProgramString, "MOV aR1.a, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case G_ACMUX_0:
				        strcat(ProgramString, "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, aB[Cycle]);
				        break;
		        }
		        strcat(ProgramString, "SUB aR0.a, aR0.a, aR1.a;\n\n");

		        switch(aC[Cycle]) {
			        case G_ACMUX_COMBINED:
				        strcat(ProgramString, "MOV aR1.a, aComb.a;\n");
				        break;
			        case G_ACMUX_TEXEL0:
				        strcat(ProgramString, "MOV aR1.a, Tex0.a;\n");
				        break;
			        case G_ACMUX_TEXEL1:
				        strcat(ProgramString, "MOV aR1.a, Tex1.a;\n");
				        break;
			        case G_ACMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV aR1.a, PrimColor.a;\n");
				        break;
			        case G_ACMUX_SHADE:
				        strcat(ProgramString, "MOV aR1.a, Shade.a;\n");
				        break;
			        case G_ACMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV aR1.a, EnvColor.a;\n");
				        break;
			        case G_ACMUX_1:
				        strcat(ProgramString, "MOV aR1.a, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case G_ACMUX_0:
				        strcat(ProgramString, "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, aC[Cycle]);
				        break;
		        }
		        strcat(ProgramString, "MUL aR0.a, aR0.a, aR1.a;\n\n");

		        switch(aD[Cycle]) {
			        case G_ACMUX_COMBINED:
				        strcat(ProgramString, "MOV aR1.a, aComb.a;\n");
				        break;
			        case G_ACMUX_TEXEL0:
				        strcat(ProgramString, "MOV aR1.a, Tex0.a;\n");
				        break;
			        case G_ACMUX_TEXEL1:
				        strcat(ProgramString, "MOV aR1.a, Tex1.a;\n");
				        break;
			        case G_ACMUX_PRIMITIVE:
				        strcat(ProgramString, "MOV aR1.a, PrimColor.a;\n");
				        break;
			        case G_ACMUX_SHADE:
				        strcat(ProgramString, "MOV aR1.a, Shade.a;\n");
				        break;
			        case G_ACMUX_ENVIRONMENT:
				        strcat(ProgramString, "MOV aR1.a, EnvColor.a;\n");
				        break;
			        case G_ACMUX_1:
				        strcat(ProgramString, "MOV aR1.a, {1.0, 1.0, 1.0, 1.0};\n");
				        break;
			        case G_ACMUX_0:
				        strcat(ProgramString, "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n");
				        break;
			        default:
				        strcat(ProgramString, "MOV aR1.a, {0.0, 0.0, 0.0, 0.0};\n");
				        sprintf(ProgramString, "%s# -%d\n", ProgramString, aD[Cycle]);
				        break;
		        }
		        strcat(ProgramString, "ADD aR0.a, aR0.a, aR1.a;\n\n");

		        strcat(ProgramString, "MOV Comb.rgb, R0;\n");
		        strcat(ProgramString, "MOV aComb.a, aR0.a;\n\n");
	        }

	        strcat(ProgramString, "# Finish\n");
	        strcat(ProgramString,
			        "MOV Comb.a, aComb.a;\n"
			        "MOV Out, Comb;\n"
			        "END\n");

	        glGenProgramsARB(1, &FragmentCache[Program.FragCachePosition].ProgramID);
	        glBindProgramARB(GL_FRAGMENT_PROGRAM_ARB, FragmentCache[Program.FragCachePosition].ProgramID);
	        glProgramStringARB(GL_FRAGMENT_PROGRAM_ARB, GL_PROGRAM_FORMAT_ASCII_ARB, strlen(ProgramString), ProgramString);

	        FragmentCache[Program.FragCachePosition].Combiner0 = Cmb0;
	        FragmentCache[Program.FragCachePosition].Combiner1 = Cmb1;
	        Program.FragCachePosition++;
        }
*/

        void ChangeTileSize(uint Tile, uint ULS, uint ULT, uint LRS, uint LRT)
        {
	        SFGfx.Textures[SFGfx.CurrentTexture].Tile = Tile;
	        SFGfx.Textures[SFGfx.CurrentTexture].ULS = ULS;
	        SFGfx.Textures[SFGfx.CurrentTexture].ULT = ULT;
	        SFGfx.Textures[SFGfx.CurrentTexture].LRS = LRS;
	        SFGfx.Textures[SFGfx.CurrentTexture].LRT = LRT;
        }

        void CalcTextureSize(int TextureID)
        {
	        uint MaxTexel = 0, Line_Shift = 0;

	        switch(SFGfx.Textures[TextureID].Format) {
		        /* 4-bit */
		        case 0x00: { MaxTexel = 4096; Line_Shift = 4; break; }	// RGBA
		        case 0x40: { MaxTexel = 4096; Line_Shift = 4; break; }	// CI
		        case 0x60: { MaxTexel = 8192; Line_Shift = 4; break; }	// IA
		        case 0x80: { MaxTexel = 8192; Line_Shift = 4; break; }	// I

		        /* 8-bit */
		        case 0x08: { MaxTexel = 2048; Line_Shift = 3; break; }	// RGBA
		        case 0x48: { MaxTexel = 2048; Line_Shift = 3; break; }	// CI
		        case 0x68: { MaxTexel = 4096; Line_Shift = 3; break; }	// IA
		        case 0x88: { MaxTexel = 4096; Line_Shift = 3; break; }	// I

		        /* 16-bit */
		        case 0x10: { MaxTexel = 2048; Line_Shift = 2; break; }	// RGBA
		        case 0x50: { MaxTexel = 2048; Line_Shift = 0; break; }	// CI
		        case 0x70: { MaxTexel = 2048; Line_Shift = 2; break; }	// IA
		        case 0x90: { MaxTexel = 2048; Line_Shift = 0; break; }	// I

		        /* 32-bit */
		        case 0x18: { MaxTexel = 1024; Line_Shift = 2; break; }	// RGBA
	        }

	        uint Line_Width = SFGfx.Textures[TextureID].LineSize << (int)Line_Shift;

	        uint Tile_Width = SFGfx.Textures[TextureID].LRS - SFGfx.Textures[TextureID].ULS + 1;
	        uint Tile_Height = SFGfx.Textures[TextureID].LRT - SFGfx.Textures[TextureID].ULT + 1;

            uint Mask_Width = (uint)1 << (int)SFGfx.Textures[TextureID].MaskS;
            uint Mask_Height = (uint)1 << (int)SFGfx.Textures[TextureID].MaskT;

	        uint Line_Height = 0;
	        if(Line_Width > 0) Line_Height = Math.Min(MaxTexel / Line_Width, Tile_Height);

	        if((SFGfx.Textures[TextureID].MaskS > 0) && ((Mask_Width * Mask_Height) <= MaxTexel)) {
		        SFGfx.Textures[TextureID].Width = Mask_Width;
	        } else if((Tile_Width * Tile_Height) <= MaxTexel) {
		        SFGfx.Textures[TextureID].Width = Tile_Width;
	        } else {
		        SFGfx.Textures[TextureID].Width = Line_Width;
	        }

	        if((SFGfx.Textures[TextureID].MaskT > 0) && ((Mask_Width * Mask_Height) <= MaxTexel)) {
		        SFGfx.Textures[TextureID].Height = Mask_Height;
	        } else if((Tile_Width * Tile_Height) <= MaxTexel) {
		        SFGfx.Textures[TextureID].Height = Tile_Height;
	        } else {
		        SFGfx.Textures[TextureID].Height = Line_Height;
	        }

	        uint Clamp_Width = 0;
	        uint Clamp_Height = 0;

            //if ((SFGfx.Textures[TextureID].CMS & SFGfx.Constants.G_TX_CLAMP) && (!SFGfx.Textures[TextureID].CMS & SFGfx.Constants.G_TX_MIRROR))
            if (false) //NOTE: THIS LINE IS IMPOSSIBLE TO GET THROUGH. The ! WILL CONVERT 0x0 TO 0x1, AND ANY OTHER VALUE TO 0x0.
            {
		        Clamp_Width = Tile_Width;
	        } else {
		        Clamp_Width = SFGfx.Textures[TextureID].Width;
	        }
            //if ((SFGfx.Textures[TextureID].CMT & SFGfx.Constants.G_TX_CLAMP) && (!SFGfx.Textures[TextureID].CMT & SFGfx.Constants.G_TX_MIRROR))
            if (false) //NOTE: THIS LINE IS IMPOSSIBLE TO GET THROUGH. The ! WILL CONVERT 0x0 TO 0x1, AND ANY OTHER VALUE TO 0x0.
            {
		        Clamp_Height = Tile_Height;
	        } else {
		        Clamp_Height = SFGfx.Textures[TextureID].Height;
	        }

	        if(Mask_Width > SFGfx.Textures[TextureID].Width) {
		        SFGfx.Textures[TextureID].MaskS = PowOf(SFGfx.Textures[TextureID].Width);
                Mask_Width = (uint)1 << (int)SFGfx.Textures[TextureID].MaskS;
	        }
	        if(Mask_Height > SFGfx.Textures[TextureID].Height) {
		        SFGfx.Textures[TextureID].MaskT = PowOf(SFGfx.Textures[TextureID].Height);
                Mask_Height = (uint)1 << (int)SFGfx.Textures[TextureID].MaskT;
	        }

            if ((SFGfx.Textures[TextureID].CMS & SFGfx.Constants.G_TX_CLAMP) != 0x0)
            {
		        SFGfx.Textures[TextureID].RealWidth = Pow2(Clamp_Width);
            }
            else if ((SFGfx.Textures[TextureID].CMS & SFGfx.Constants.G_TX_MIRROR) != 0x0)
            {
		        SFGfx.Textures[TextureID].RealWidth = Pow2(Mask_Width);
	        } else {
		        SFGfx.Textures[TextureID].RealWidth = Pow2(SFGfx.Textures[TextureID].Width);
	        }

            if ((SFGfx.Textures[TextureID].CMT & SFGfx.Constants.G_TX_CLAMP) != 0x0)
            {
		        SFGfx.Textures[TextureID].RealHeight = Pow2(Clamp_Height);
	        } else if((SFGfx.Textures[TextureID].CMT & SFGfx.Constants.G_TX_MIRROR) != 0x0) {
		        SFGfx.Textures[TextureID].RealHeight = Pow2(Mask_Height);
	        } else {
		        SFGfx.Textures[TextureID].RealHeight = Pow2(SFGfx.Textures[TextureID].Height);
	        }

	        SFGfx.Textures[TextureID].ShiftScaleS = 1.0f;
	        SFGfx.Textures[TextureID].ShiftScaleT = 1.0f;

	        if(SFGfx.Textures[TextureID].ShiftS > 10) {
                SFGfx.Textures[TextureID].ShiftScaleS = (1 << (int)(16 - SFGfx.Textures[TextureID].ShiftS));
	        } else if(SFGfx.Textures[TextureID].ShiftS > 0) {
                SFGfx.Textures[TextureID].ShiftScaleS /= (1 << (int)SFGfx.Textures[TextureID].ShiftS);
	        }

	        if(SFGfx.Textures[TextureID].ShiftT > 10) {
                SFGfx.Textures[TextureID].ShiftScaleT = (1 << (int)(16 - SFGfx.Textures[TextureID].ShiftT));
	        } else if(SFGfx.Textures[TextureID].ShiftT > 0) {
                SFGfx.Textures[TextureID].ShiftScaleT /= (1 << (int)SFGfx.Textures[TextureID].ShiftT);
	        }
        }

        internal uint Pow2(uint dim) {
	        uint i = 1;

	        while (i < dim) i <<= 1;

	        return i;
        }

        internal uint PowOf(uint dim) {
	        uint num = 1;
	        uint i = 0;

	        while (num < dim) {
		        num <<= 1;
		        i++;
	        }

	        return i;
        }

        //Handle later
        void InitLoadTexture()
        {
            if (false)
            {//OpenGL.Ext_MultiTexture) {
                if (SFGfx.Textures[0].Offset != 0x00)
                {
                    GL.Enable(EnableCap.Texture2D);
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(0));
                }

                if (SFGfx.IsMultiTexture && (SFGfx.Textures[1].Offset != 0x00))
                {
                    GL.Enable(EnableCap.Texture2D);
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(1));
                }

                GL.ActiveTexture(TextureUnit.Texture1);
                GL.Disable(EnableCap.Texture2D);
                GL.ActiveTexture(TextureUnit.Texture0);
            }
            else
            {
                if (SFGfx.Textures[0].Offset != 0x00)
                {
                    GL.Enable(EnableCap.Texture2D);
                    GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(0));
                }
            }
        }

        uint CheckTextureCache(uint TexID)
        {
            uint GLID = 0;

            CalcTextureSize((int)TexID);

            int CacheCheck = 0; bool SearchingCache = true; bool NewTexture = false;
            while (SearchingCache)
            {
                if ((SFGfx.TextureCache[CacheCheck].Offset == SFGfx.Textures[TexID].Offset) &&
                    (SFGfx.TextureCache[CacheCheck].RealWidth == SFGfx.Textures[TexID].RealWidth) &&
                    (SFGfx.TextureCache[CacheCheck].RealHeight == SFGfx.Textures[TexID].RealHeight))
                {
                    SearchingCache = false;
                    NewTexture = false;
                }
                else
                {
                    if (CacheCheck != 2047)
                    {
                        CacheCheck++;
                    }
                    else
                    {
                        SearchingCache = false;
                        NewTexture = true;
                    }
                }
            }

            if (NewTexture)
            {
                CalcTextureSize((int)TexID);

                GLID = LoadTexture((int)TexID);
                SFGfx.TextureCache[SFGfx.TextureCachePosition].Offset = SFGfx.Textures[TexID].Offset;
                SFGfx.TextureCache[SFGfx.TextureCachePosition].RealWidth = SFGfx.Textures[TexID].RealWidth;
                SFGfx.TextureCache[SFGfx.TextureCachePosition].RealHeight = SFGfx.Textures[TexID].RealHeight;
                SFGfx.TextureCache[SFGfx.TextureCachePosition].TextureID = GLID;
                SFGfx.TextureCachePosition++;
            }
            else
            {
                GLID = SFGfx.TextureCache[CacheCheck].TextureID;
            }

            if (SFGfx.TextureCachePosition >= 2048)
            {
                for(int i = 0; i < 2048; i++) SFGfx.TextureCache[i] = SFGfx.BlankTextureCache;
                SFGfx.TextureCachePosition = 0;
            }

            return GLID;
        }

        private void Write32(byte[] array, uint position, uint value)
        {
            array[position + 3] = (byte)(value & 0xff);
            array[position + 2] = (byte)((value >> 8) & 0xff);
            array[position + 1] = (byte)((value >> 16) & 0xff);
            array[position] = (byte)((value >> 24) & 0xff);
        }

        //Handle later
        uint LoadTexture(int TextureID)
        {
            char TexSegment = (char)((SFGfx.Textures[TextureID].Offset & 0xFF000000) >> 24);
            uint TexOffset = (SFGfx.Textures[TextureID].Offset & 0x00FFFFFF);

            if (SFGfx.GLTextureCount == 23)
            {
                SFGfx.Textures[TextureID].RealHeight = SFGfx.Textures[TextureID].Height;
                SFGfx.Textures[TextureID].RealWidth = SFGfx.Textures[TextureID].Width;
            }

            // CalcTextureSize(TextureID);

            int i = 0, j = 0;

            int BytesPerPixel = 0x08;
            switch(SFGfx.Textures[TextureID].Format) {
                /* 4bit, 8bit */
                case 0x00: case 0x40: case 0x60: case 0x80:
                case 0x08: case 0x48: case 0x68: case 0x88:
                    BytesPerPixel = 0x04;
                    break;
                /* 16bit */
                case 0x10: case 0x50: case 0x70: case 0x90:
                    BytesPerPixel = 0x08;
                    break;
                /* 32bit */
                case 0x18:
                    BytesPerPixel = 0x10;
                    break;
            }

            uint BufferSize = (SFGfx.Textures[TextureID].RealHeight * SFGfx.Textures[TextureID].RealWidth) * (uint)BytesPerPixel;
            byte[] TextureData = new byte[BufferSize];

            for (int lj = 0; lj < TextureData.Length; lj++)
            {
                TextureData[lj] = (byte)0xFF;
            }

            //memset(TextureData, 0xFF, BufferSize);

            uint GLTexPosition = 0;
            
            
            char TempSegment;
            uint TempOffset;

            SplitAddress(SFGfx.Textures[TextureID].Offset, out TempSegment, out TempOffset);

            if (!LocateBank((byte)TempSegment, TempOffset).IsValid())
            {
                //memset(TextureData, 0xFF, BufferSize);
            }
            else
            {
                switch (SFGfx.Textures[TextureID].Format)
                {
                    case 0x00:
                    case 0x08:
                    case 0x10:
                        {
                            ushort Raw;
                            uint RGBA = 0;

                            for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                            {
                                for (i = 0; i < SFGfx.Textures[TextureID].Width; i++)
                                {
                                    Raw = ReadUShort((byte)TexSegment, TexOffset);//(RAM[TexSegment].Data[TexOffset] << 8) | RAM[TexSegment].Data[TexOffset + 1];

                                    RGBA = (((uint)Raw & 0xF800) >> 8) << 24;
                                    RGBA |= ((((uint)Raw & 0x07C0) << 5) >> 8) << 16;
                                    RGBA |= ((((uint)Raw & 0x003E) << 18) >> 16) << 8;
                                    if (((uint)Raw & 0x0001) != 0x0) RGBA |= 0xFF;
                                    Write32(TextureData, GLTexPosition, RGBA);

                                    TexOffset += 2;
                                    GLTexPosition += 4;

                                    if (!LocateBank((byte)TexSegment, TexOffset).IsValid()) break;//(TexOffset > RAM[TexSegment].Size) break;
                                }
                                TexOffset += SFGfx.Textures[TextureID].LineSize * 4 - SFGfx.Textures[TextureID].Width;
                            }
                            break;
                        }

                    case 0x18:
                        {
                            uint totalSize = (SFGfx.Textures[TextureID].Height * SFGfx.Textures[TextureID].Width);
                            for(uint k = 0; k < totalSize; k++)
                                Write32(TextureData, k * 4, ReadUInt((byte)TexSegment, TexOffset + 4 * k));
                            //memcpy(TextureData, &RAM[TexSegment].Data[TexOffset], (SFGfx.Textures[TextureID].Height * SFGfx.Textures[TextureID].Width * 4));
                            break;
                        }

                    //case 0x40:
                    //case 0x50:
                    //    {
                    //        uint CI1, CI2;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < SFGfx.Textures[TextureID].Width / 2; i++)
                    //            {
                    //                CI1 = (RAM[TexSegment].Data[TexOffset] & 0xF0) >> 4;
                    //                CI2 = (RAM[TexSegment].Data[TexOffset] & 0x0F);

                    //                RGBA = ((uint)SFGfx.Palettes[CI1].R << 24);
                    //                RGBA |= ((uint)SFGfx.Palettes[CI1].G << 16);
                    //                RGBA |= ((uint)SFGfx.Palettes[CI1].B << 8);
                    //                RGBA |= (uint)SFGfx.Palettes[CI1].A;
                    //                Write32(TextureData, GLTexPosition, RGBA);

                    //                RGBA = ((uint)SFGfx.Palettes[CI2].R << 24);
                    //                RGBA |= ((uint)SFGfx.Palettes[CI2].G << 16);
                    //                RGBA |= ((uint)SFGfx.Palettes[CI2].B << 8);
                    //                RGBA |= (uint)SFGfx.Palettes[CI2].A;
                    //                Write32(TextureData, GLTexPosition + 4, RGBA);

                    //                TexOffset += 1;
                    //                GLTexPosition += 8;
                    //            }
                    //            TexOffset += SFGfx.Textures[TextureID].LineSize * 8 - (SFGfx.Textures[TextureID].Width / 2);
                    //        }
                    //        break;
                    //    }

                    //case 0x48:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < SFGfx.Textures[TextureID].Width; i++)
                    //            {
                    //                Raw = RAM[TexSegment].Data[TexOffset];

                    //                RGBA = ((uint)SFGfx.Palettes[Raw].R << 24);
                    //                RGBA |= ((uint)SFGfx.Palettes[Raw].G << 16);
                    //                RGBA |= ((uint)SFGfx.Palettes[Raw].B << 8);
                    //                RGBA |= (uint)SFGfx.Palettes[Raw].A;
                    //                Write32(TextureData, GLTexPosition, RGBA);

                    //                TexOffset += 1;
                    //                GLTexPosition += 4;
                    //            }
                    //            TexOffset += SFGfx.Textures[TextureID].LineSize * 8 - SFGfx.Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    //case 0x60:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < SFGfx.Textures[TextureID].Width / 2; i++)
                    //            {
                    //                Raw = (RAM[TexSegment].Data[TexOffset] & 0xF0) >> 4;
                    //                RGBA = (((Raw & 0x0E) << 4) << 24);
                    //                RGBA |= (((Raw & 0x0E) << 4) << 16);
                    //                RGBA |= (((Raw & 0x0E) << 4) << 8);
                    //                if ((Raw & 0x01)) RGBA |= 0xFF;
                    //                Write32(TextureData, GLTexPosition, RGBA);

                    //                Raw = (RAM[TexSegment].Data[TexOffset] & 0x0F);
                    //                RGBA = (((Raw & 0x0E) << 4) << 24);
                    //                RGBA |= (((Raw & 0x0E) << 4) << 16);
                    //                RGBA |= (((Raw & 0x0E) << 4) << 8);
                    //                if ((Raw & 0x01)) RGBA |= 0xFF;
                    //                Write32(TextureData, GLTexPosition + 4, RGBA);

                    //                TexOffset += 1;
                    //                GLTexPosition += 8;
                    //            }
                    //            TexOffset += SFGfx.Textures[TextureID].LineSize * 8 - (SFGfx.Textures[TextureID].Width / 2);
                    //        }
                    //        break;
                    //    }

                    //case 0x68:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < SFGfx.Textures[TextureID].Width; i++)
                    //            {
                    //                Raw = RAM[TexSegment].Data[TexOffset];
                    //                RGBA = (((Raw & 0xF0) + 0x0F) << 24);
                    //                RGBA |= (((Raw & 0xF0) + 0x0F) << 16);
                    //                RGBA |= (((Raw & 0xF0) + 0x0F) << 8);
                    //                RGBA |= ((Raw & 0x0F) << 4);
                    //                Write32(TextureData, GLTexPosition, RGBA);

                    //                TexOffset += 1;
                    //                GLTexPosition += 4;
                    //            }
                    //            TexOffset += SFGfx.Textures[TextureID].LineSize * 8 - SFGfx.Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    //case 0x70:
                    //    {
                    //        for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < SFGfx.Textures[TextureID].Width; i++)
                    //            {
                    //                TextureData[GLTexPosition] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 1] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 2] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 3] = RAM[TexSegment].Data[TexOffset + 1];

                    //                TexOffset += 2;
                    //                GLTexPosition += 4;
                    //            }
                    //            TexOffset += SFGfx.Textures[TextureID].LineSize * 4 - SFGfx.Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    //case 0x80:
                    //case 0x90:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < SFGfx.Textures[TextureID].Width / 2; i++)
                    //            {
                    //                Raw = (RAM[TexSegment].Data[TexOffset] & 0xF0) >> 4;
                    //                RGBA = (((Raw & 0x0F) << 4) << 24);
                    //                RGBA |= (((Raw & 0x0F) << 4) << 16);
                    //                RGBA |= (((Raw & 0x0F) << 4) << 8);
                    //                RGBA |= 0xFF;
                    //                Write32(TextureData, GLTexPosition, RGBA);

                    //                Raw = (RAM[TexSegment].Data[TexOffset] & 0x0F);
                    //                RGBA = (((Raw & 0x0F) << 4) << 24);
                    //                RGBA |= (((Raw & 0x0F) << 4) << 16);
                    //                RGBA |= (((Raw & 0x0F) << 4) << 8);
                    //                RGBA |= 0xFF;
                    //                Write32(TextureData, GLTexPosition + 4, RGBA);

                    //                TexOffset += 1;
                    //                GLTexPosition += 8;
                    //            }
                    //            TexOffset += SFGfx.Textures[TextureID].LineSize * 8 - (SFGfx.Textures[TextureID].Width / 2);
                    //        }
                    //        break;
                    //    }

                    //case 0x88:
                    //    {
                    //        for (j = 0; j < SFGfx.Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < SFGfx.Textures[TextureID].Width; i++)
                    //            {
                    //                TextureData[GLTexPosition] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 1] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 2] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 3] = 0xFF;

                    //                TexOffset += 1;
                    //                GLTexPosition += 4;
                    //            }
                    //            TexOffset += SFGfx.Textures[TextureID].LineSize * 8 - SFGfx.Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    default:
                        //memset(TextureData, 0xFF, BufferSize);
                        break;
                }
            }

            GL.BindTexture(TextureTarget.Texture2D, SFGfx.GLTextureID[SFGfx.GLTextureCount]);

            if ((SFGfx.Textures[TextureID].CMT & SFGfx.Constants.G_TX_CLAMP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge); }
            if ((SFGfx.Textures[TextureID].CMT & SFGfx.Constants.G_TX_WRAP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat); }
            if ((SFGfx.Textures[TextureID].CMT & SFGfx.Constants.G_TX_MIRROR) != 0) { if (SFGfx.OpenGlSettings.Ext_TexMirroredRepeat) GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat); }

            if ((SFGfx.Textures[TextureID].CMS & SFGfx.Constants.G_TX_CLAMP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge); }
            if ((SFGfx.Textures[TextureID].CMS & SFGfx.Constants.G_TX_WRAP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); }
            if ((SFGfx.Textures[TextureID].CMS & SFGfx.Constants.G_TX_MIRROR) != 0) { if (SFGfx.OpenGlSettings.Ext_TexMirroredRepeat) GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat); }

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)SFGfx.Textures[TextureID].RealWidth, (int)SFGfx.Textures[TextureID].RealHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, TextureData); //last param needs ref??
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //Debug outputting texture data
            //DrawTextureRGBA(TextureData, (int)SFGfx.Textures[TextureID].RealWidth, (int)SFGfx.Textures[TextureID].RealHeight, string.Format("Texture{0}.bmp", SFGfx.GLTextureCount));

            SFGfx.GLTextureCount++;

            return SFGfx.GLTextureID[SFGfx.GLTextureCount-1];
            
        }


        
        #endregion

        private void DrawTextureRGBA(byte[] textureData, int width, int height, string fileName)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height);
            int index;
            for (int h = 0; h < width; h++)
            {
                for (int k = 0; k < height; k++)
                {
                    index = h * 4 + k * width * 4;
                    bmp.SetPixel(h, k, Color.FromArgb(textureData[index + 3], textureData[index], textureData[index + 1], textureData[index + 2]));
                }
            }
            bmp.Save(fileName);
        }

    }
}
