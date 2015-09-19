using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using NewSF64Toolkit.OpenGL;

namespace NewSF64Toolkit.OpenGL.F3DEX
{
    public class F3DEXParser
    {
        public enum DrawingModeType
        {
            Texture,
            TextureSelected,
            Wireframe
        }

        public DrawingModeType DrawingMode;

        public static int[] InvalidBox = new int[3] {0, 1, 2};

        public F3DEXParser()
        {
            DrawingMode = DrawingModeType.Texture;

            sv_ClearStructures(false);
            gl_ClearRenderer(true);

            InitInvalidModels();
        }

        public void InitInvalidModels()
        {
            int listBase = GL.GenLists(3);

            //Normal
            GL.NewList(listBase, ListMode.Compile);

            GL.PushMatrix();

            DrawingMode = F3DEXParser.DrawingModeType.Texture;

            DrawInvalidModel();

            GL.PopMatrix();

            GL.EndList();

            //Selected
            GL.NewList(listBase + 1, ListMode.Compile);

            GL.PushMatrix();

            DrawingMode = F3DEXParser.DrawingModeType.TextureSelected;

            DrawInvalidModel();

            GL.PopMatrix();

            GL.EndList();

            //Wireframe
            GL.NewList(listBase + 2, ListMode.Compile);

            GL.PushMatrix();

            DrawingMode = F3DEXParser.DrawingModeType.Wireframe;

            DrawInvalidModel();

            GL.PopMatrix();

            GL.EndList();

            DrawingMode = F3DEXParser.DrawingModeType.Texture;

            InvalidBox = new int[3] { listBase, listBase + 1, listBase + 2 };
        }

        private void DrawInvalidModel()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);

            GL.Begin(PrimitiveType.Quads);
            if (DrawingMode != DrawingModeType.Wireframe)
            {
                if (DrawingMode == DrawingModeType.TextureSelected)
                    GL.Color3(0.0f, 1.0f, 0.0f);
                else
                    GL.Color3(1.0f, 0.0f, 0.0f);
            }

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
            if (DrawingMode != DrawingModeType.Wireframe)
            {
                GL.Color3(1.0f, 1.0f, 1.0f);
            }

            GL.Vertex3(-15.0f, 15.0f, 15.0f);   //V8
            GL.Vertex3(-15.0f, -15.0f, 15.0f);   //V7
            GL.Vertex3(15.0f, -15.0f, 15.0f);   //V1
            GL.Vertex3(15.0f, 15.0f, 15.0f);   //V2
            GL.End();

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Lighting);
        }

        //NOTE: We should make a way to clean up [DELETE] the old GL display lists

        public int[] ReadGameObject(byte[] bytes, uint fullOffset)//F3DEXParser.GameObject gameObject)
        {
            //byte bankNo = (byte)((gameObject.DListOffset & 0xFF000000) >> 24);
            uint offset = fullOffset & 0x00FFFFFF;

            //Is 0 a valid location?
            if (offset == 0)// || !MemoryManager.Instance.HasBank(bankNo) || !MemoryManager.Instance.LocateBank(bankNo, offset).IsValid())
            {
                return InvalidBox;
            }
            else
            {
                int listBase = GL.GenLists(3);
                //GL.ListBase(listBase);

                //Normal
                GL.NewList(listBase, ListMode.Compile);

                GL.PushMatrix();

                DrawingMode = F3DEXParser.DrawingModeType.Texture;

                ReadF3DEX(bytes, offset);

                GL.PopMatrix();

                GL.EndList();

                //Selected
                GL.NewList(listBase + 1, ListMode.Compile);

                GL.PushMatrix();

                DrawingMode = F3DEXParser.DrawingModeType.TextureSelected;

                ReadF3DEX(bytes, offset);

                GL.PopMatrix();

                GL.EndList();

                //Wireframe
                GL.NewList(listBase + 2, ListMode.Compile);

                GL.PushMatrix();

                DrawingMode = F3DEXParser.DrawingModeType.Wireframe;

                ReadF3DEX(bytes, offset);

                GL.PopMatrix();

                GL.EndList();

                return new int[3] { listBase, listBase + 1, listBase + 2 };
            }
        }

        private byte[] currentBytes;

        private void ReadF3DEX(byte[] bytes, uint offset)
        {
            currentBytes = bytes;

            if (DrawingMode == DrawingModeType.TextureSelected)
            {
                GL.PushAttrib(AttribMask.AllAttribBits);
                GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Add);
            }

            if (DrawingMode == DrawingModeType.Wireframe)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            }

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Lighting);

            DLStackPos = 0;
            ParseDisplayList(offset);

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Lighting);

            if (DrawingMode == DrawingModeType.Wireframe)
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            }

            if (DrawingMode == DrawingModeType.TextureSelected)
            {
                GL.PopAttrib();
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
                    if (DrawingMode != DrawingModeType.Wireframe)
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
                    if (DrawingMode != DrawingModeType.Wireframe)
                        G_TEXRECT();
                    break;
                case 0xE5: //dl_G_TEXRECTFLIP
                    if (DrawingMode != DrawingModeType.Wireframe)
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
                    if (DrawingMode != DrawingModeType.Wireframe)
                        G_SETTILESIZE();
                    break;
                case 0xF3: //dl_G_LOADBLOCK
                    G_LOADBLOCK();
                    break;
                case 0xF4: //dl_G_LOADTILE
                    if (DrawingMode != DrawingModeType.Wireframe)
                        G_LOADTILE();
                    break;
                case 0xF5: //dl_G_SETTILE
                    if (DrawingMode != DrawingModeType.Wireframe)
                        G_SETTILE();
                    break;
                case 0xF6: //dl_G_FILLRECT
                    if (DrawingMode != DrawingModeType.Wireframe)
                        G_FILLRECT();
                    break;
                case 0xF7: //dl_G_SETFILLCOLOR
                    if (DrawingMode != DrawingModeType.Wireframe)
                        G_SETFILLCOLOR();
                    break;
                case 0xF8: //dl_G_SETFOGCOLOR
                    G_SETFOGCOLOR();
                    break;
                case 0xF9: //dl_G_SETBLENDCOLOR
                    G_SETBLENDCOLOR();
                    break;
                case 0xFA: //dl_G_SETPRIMCOLOR
                    if (DrawingMode != DrawingModeType.Wireframe)
                        G_SETPRIMCOLOR();
                    break;
                case 0xFB: //dl_G_SETENVCOLOR
                    G_SETENVCOLOR();
                    break;
                case 0xFC: //dl_G_SETCOMBINE
                    G_SETCOMBINE();
                    break;
                case 0xFD: //dl_G_SETTIMG
                    if (DrawingMode != DrawingModeType.Wireframe)
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
            //We'll assume for now that the address is contained
            //if (!MemoryManager.Instance.LocateBank((byte)TempSegment, TempOffset).IsValid()) return;

            DListAddress = Address;

            //Decode the rest here
            SetRenderMode(0, 0);

            GeometryMode = F3DEXParser.Constants.G_LIGHTING | F3DEXParser.Constants.F3DEX_SHADING_SMOOTH;
            ChangedModes |= F3DEXParser.Constants.CHANGED_GEOMETRYMODE;

            while (DLStackPos >= 0)
            {
                SplitAddress(DListAddress, out Segment, out Offset);

                w0 = ByteHelper.ReadUInt(currentBytes, Offset);//Read32(RAM[Segment].Data, Offset);
                w1 = ByteHelper.ReadUInt(currentBytes, Offset + 4);//RRead32(RAM[Segment].Data, Offset + 4);

                wp0 = ByteHelper.ReadUInt(currentBytes, Offset - 8);//Read32(RAM[Segment].Data, Offset - 8);
                wp1 = ByteHelper.ReadUInt(currentBytes, Offset - 4);//Read32(RAM[Segment].Data, Offset - 4);

                wn0 = ByteHelper.ReadUInt(currentBytes, Offset + 8);//Read32(RAM[Segment].Data, Offset + 8);
                wn1 = ByteHelper.ReadUInt(currentBytes, Offset + 12);//Read32(RAM[Segment].Data, Offset + 12);

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
                    MtxTemp1 = ByteHelper.ReadShort(currentBytes, TempOffset);//((RAM[Segment].Data[Offset		] * 0x100) + RAM[Segment].Data[Offset + 1		]);
                    MtxTemp2 = ByteHelper.ReadShort(currentBytes, TempOffset + 32);//((RAM[Segment].Data[Offset + 32	] * 0x100) + RAM[Segment].Data[Offset + 33	]);
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

            //if (!MemoryManager.Instance.LocateBank((byte)TempSegment, TempOffset).IsValid()) return;

	        uint V = _SHIFTR( w0, 17, 7 );
	        uint N = _SHIFTR( w0, 10, 6 );

	        if((N > 32) || (V > 32)) return;

	        int i = 0;
	        for(i = 0; i < (N << 4); i += 16) {
                Vertices[V].X = ByteHelper.ReadShort(currentBytes, TempOffset + (uint)i);//((RAM[TempSegment].Data[TempOffset + i] << 8) | RAM[TempSegment].Data[TempOffset + i + 1]);
                Vertices[V].Y = ByteHelper.ReadShort(currentBytes, TempOffset + (uint)i + 2);//((RAM[TempSegment].Data[TempOffset + i + 2] << 8) | RAM[TempSegment].Data[TempOffset + i + 3]);
                Vertices[V].Z = ByteHelper.ReadShort(currentBytes, TempOffset + (uint)i + 4);//((RAM[TempSegment].Data[TempOffset + i + 4] << 8) | RAM[TempSegment].Data[TempOffset + i + 5]);
                Vertices[V].S = ByteHelper.ReadShort(currentBytes, TempOffset + (uint)i + 8);//((RAM[TempSegment].Data[TempOffset + i + 8] << 8) | RAM[TempSegment].Data[TempOffset + i + 9]);
                Vertices[V].T = ByteHelper.ReadShort(currentBytes, TempOffset + (uint)i + 10);//((RAM[TempSegment].Data[TempOffset + i + 10] << 8) | RAM[TempSegment].Data[TempOffset + i + 11]);
                Vertices[V].R = (char)ByteHelper.ReadByte(currentBytes, TempOffset + (uint)i + 12);//RAM[TempSegment].Data[TempOffset + i + 12];
                Vertices[V].G = (char)ByteHelper.ReadByte(currentBytes, TempOffset + (uint)i + 13);//RAM[TempSegment].Data[TempOffset + i + 13];
                Vertices[V].B = (char)ByteHelper.ReadByte(currentBytes, TempOffset + (uint)i + 14);//RAM[TempSegment].Data[TempOffset + i + 14];
                Vertices[V].A = (char)ByteHelper.ReadByte(currentBytes, TempOffset + (uint)i + 15);//RAM[TempSegment].Data[TempOffset + i + 15];

		        V++;
	        }
            
            if(DrawingMode != DrawingModeType.Wireframe)
	            InitLoadTexture();
        }
        
        void F3DEX_DL()
        {
            char TempSegment;
            uint TempOffset;

            SplitAddress(w1, out TempSegment, out TempOffset);

            //if (!MemoryManager.Instance.LocateBank((byte)TempSegment, TempOffset).IsValid()) return;

	        DLStack[DLStackPos] = DListAddress;
	        DLStackPos++;

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

            SplitAddress(Store_RDPHalf1, out TempSegment, out TempOffset);

            //if (!MemoryManager.Instance.LocateBank((byte)TempSegment, TempOffset).IsValid()) return;



	        int Vtx = (int)_SHIFTR(w0, 1, 11);
	        int ZVal = (int)w1;

	        if(Vertices[Vtx].Z < ZVal) {
		        DLStack[DLStackPos] = DListAddress;
		        DLStackPos++;

		        ParseDisplayList(Store_RDPHalf1);
	        }
        }

        void F3DEX_TRI2()
        {
            if (ChangedModes != 0x00) UpdateStates();

	        int[] Vtxs1 = new int[] { (int)_SHIFTR( w0, 17, 7 ), (int)_SHIFTR( w0, 9, 7 ), (int)_SHIFTR( w0, 1, 7 ) };

	        DrawTriangle(Vtxs1);

            if (ChangedModes != 0x00) UpdateStates();

            int[] Vtxs2 = new int[] { (int)_SHIFTR(w1, 17, 7), (int)_SHIFTR(w1, 9, 7), (int)_SHIFTR(w1, 1, 7) };
	        DrawTriangle(Vtxs2);
        }
        
        void F3DEX_MODIFYVTX()
        {
	        //Unimplemented
        }

        void F3DEX_RDPHALF_2()
        {
	        Store_RDPHalf2 = w1;
        }

        void F3DEX_RDPHALF_1()
        {
	        Store_RDPHalf1 = w1;
        }

        void F3DEX_CLEARGEOMETRYMODE()
        {
	        GeometryMode &= ~w1;

	        ChangedModes |= F3DEXParser.Constants.CHANGED_GEOMETRYMODE;
        }
        
        void F3DEX_SETGEOMETRYMODE()
        {
	        GeometryMode |= w1;

	        ChangedModes |= F3DEXParser.Constants.CHANGED_GEOMETRYMODE;
        }

        void F3DEX_ENDDL()
        {
	        DLStackPos--;
            if (DLStackPos >= 0)
                DListAddress = DLStack[DLStackPos];
            else
                DListAddress = 0x0;
        }
        
        void F3DEX_SETOTHERMODE_L()
        {
	        switch(_SHIFTR( w0, 8, 8 )) {
                case F3DEXParser.Constants.G_MDSFT_RENDERMODE:
			        SetRenderMode(w1 & 0xCCCCFFFF, w1 & 0x3333FFFF);
			        break;
		        default: {
			        uint shift = _SHIFTR( w0, 8, 8 );
			        uint length = _SHIFTR( w0, 0, 8 );
                    uint mask = (uint)((1 << (int)length) - 1) << (int)shift;

			        OtherModeL &= ~mask;
			        OtherModeL |= w1 & mask;

                    ChangedModes |= F3DEXParser.Constants.CHANGED_RENDERMODE | F3DEXParser.Constants.CHANGED_ALPHACOMPARE;
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

			        OtherModeH &= ~mask;
			        OtherModeH |= w1 & mask;
			        break;
		        }
	        }
        }

        void F3DEX_TEXTURE()
        {
	        Textures[0].ScaleS = FIXED2FLOAT(_SHIFTR(w1, 16, 16), 16);
	        Textures[0].ScaleT = FIXED2FLOAT(_SHIFTR(w1, 0, 16), 16);

            if (Textures[0].ScaleS == 0.0f) Textures[0].ScaleS = 1.0f;
            if (Textures[0].ScaleT == 0.0f) Textures[0].ScaleT = 1.0f;

	        Textures[1].ScaleS = Textures[0].ScaleS;
	        Textures[1].ScaleT = Textures[0].ScaleT;
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
	        if(ChangedModes != 0x0) UpdateStates();

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

            SplitAddress(Textures[CurrentTexture].PalOffset, out PalSegment, out PalOffset);

            //if (!MemoryManager.Instance.LocateBank((byte)PalSegment, PalOffset).IsValid()) return;
            
	        uint PalSize = ((w1 & 0x00FFF000) >> 14) + 1;

	        ushort Raw;
	        char R, G, B, A;

	        uint PalLoop;

	        for(PalLoop = 0; PalLoop < PalSize; PalLoop++) {
                Raw = ByteHelper.ReadUShort(currentBytes, PalOffset);//(RAM[PalSegment].Data[PalOffset] << 8) | RAM[PalSegment].Data[PalOffset + 1];

		        R = (char)((Raw & 0xF800) >> 8);
                G = (char)(((Raw & 0x07C0) << 5) >> 8);
                B = (char)(((Raw & 0x003E) << 18) >> 16);

                if (((Raw & 0x0001) != 0x0000)) { A = (char)0xFF; } else { A = (char)0x00; }

		        Palettes[PalLoop].R = R;
                Palettes[PalLoop].G = G;
                Palettes[PalLoop].B = B;
                Palettes[PalLoop].A = A;

		        PalOffset += 2;

                //if (!MemoryManager.Instance.LocateBank((byte)PalSegment, PalOffset).IsValid()) break;
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

            Textures[CurrentTexture].Format = (w0 & 0x00FF0000) >> 16;
            Textures[CurrentTexture].CMT = _SHIFTR(w1, 18, 2);
            Textures[CurrentTexture].CMS = _SHIFTR(w1, 8, 2);
            Textures[CurrentTexture].LineSize = _SHIFTR(w0, 9, 9);
            Textures[CurrentTexture].Palette = _SHIFTR(w1, 20, 4);
            Textures[CurrentTexture].ShiftT = _SHIFTR(w1, 10, 4);
            Textures[CurrentTexture].ShiftS = _SHIFTR(w1, 0, 4);
            Textures[CurrentTexture].MaskT = _SHIFTR(w1, 14, 4);
            Textures[CurrentTexture].MaskS = _SHIFTR(w1, 4, 4);
        }

        void G_FILLRECT()
        {
	        //Unimplemented
        }

        void G_SETFILLCOLOR()
        {
	        FillColor.R = _SHIFTR(w1, 11, 5) * 0.032258064f;
	        FillColor.G = _SHIFTR(w1, 6, 5) * 0.032258064f;
	        FillColor.B = _SHIFTR(w1, 1, 5) * 0.032258064f;
	        FillColor.A = _SHIFTR(w1, 0, 1);

	        FillColor.Z = _SHIFTR(w1, 2, 14);
	        FillColor.DZ = _SHIFTR(w1, 0, 2);
        }

        void G_SETFOGCOLOR()
        {
	        FogColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        FogColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        FogColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        FogColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;
        }
        
        void G_SETBLENDCOLOR()
        {
	        BlendColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        BlendColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        BlendColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        BlendColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;

            //Bring in with textures
            //if(OpenGL.Ext_FragmentProgram) {
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 2, Gfx.BlendColor.R, Gfx.BlendColor.G, Gfx.BlendColor.B, Gfx.BlendColor.A);
            //}
        }

        void G_SETPRIMCOLOR()
        {
	        PrimColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        PrimColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        PrimColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        PrimColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;

	        PrimColor.M = (ushort)_SHIFTL(w0, 8, 8);
	        PrimColor.L = _SHIFTL(w0, 0, 8) * 0.0039215689f;

            //Bring in with other textures
            //if(OpenGL.Ext_FragmentProgram) {
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 1, Gfx.PrimColor.R, Gfx.PrimColor.G, Gfx.PrimColor.B, Gfx.PrimColor.A);
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 3, Gfx.PrimColor.L, Gfx.PrimColor.L, Gfx.PrimColor.L, Gfx.PrimColor.L);
            //}
        }

        void G_SETENVCOLOR()
        {
	        EnvColor.R = _SHIFTR(w1, 24, 8) * 0.0039215689f;
	        EnvColor.G = _SHIFTR(w1, 16, 8) * 0.0039215689f;
	        EnvColor.B = _SHIFTR(w1, 8, 8) * 0.0039215689f;
	        EnvColor.A = _SHIFTR(w1, 0, 8) * 0.0039215689f;

            //Bring in with texture code
            //if(OpenGL.Ext_FragmentProgram) {
            //    glProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 0, Gfx.EnvColor.R, Gfx.EnvColor.G, Gfx.EnvColor.B, Gfx.EnvColor.A);
            //}
        }

        void G_SETCOMBINE()
        {
	        Combiner0 = (w0 & 0x00FFFFFF);
	        Combiner1 = w1;

            //Bring in with texture code
            //if(OpenGL.Ext_FragmentProgram) dl_CheckFragmentCache();
        }

        void G_SETTIMG()
        {
	        CurrentTexture = 0;
	        IsMultiTexture = false;

	        Textures[CurrentTexture].Offset = w1;
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
		        float TempS0 = FIXED2FLOAT(Vertices[Vtxs[i]].S, 16) * (Textures[0].ScaleS * Textures[0].ShiftScaleS) / 32.0f / FIXED2FLOAT(Textures[0].RealWidth, 16);
                float TempT0 = FIXED2FLOAT(Vertices[Vtxs[i]].T, 16) * (Textures[0].ScaleT * Textures[0].ShiftScaleT) / 32.0f / FIXED2FLOAT(Textures[0].RealHeight, 16);

                //Bring in with texture code
		        /*if(OpenGL.Ext_MultiTexture) {
			        GL.MultiTexCoord2(TextureUnit.Texture0, TempS0, TempT0);
			        if(F3DEXParser.IsMultiTexture) {
				        float TempS1 = _FIXED2FLOAT(F3DEXParser.Vertices[Vtxs[i]].S, 16) * (Textures[1].ScaleS * Textures[1].ShiftScaleS) / 32.0f / _FIXED2FLOAT(Textures[1].RealWidth, 16);
				        float TempT1 = _FIXED2FLOAT(F3DEXParser.Vertices[Vtxs[i]].T, 16) * (Textures[1].ScaleT * Textures[1].ShiftScaleT) / 32.0f / _FIXED2FLOAT(Textures[1].RealHeight, 16);
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
		        GL.Normal3(Vertices[Vtxs[i]].R, Vertices[Vtxs[i]].G, Vertices[Vtxs[i]].B);
		        if((GeometryMode & F3DEXParser.Constants.G_LIGHTING) == 0x0) GL.Color4(Vertices[Vtxs[i]].R, Vertices[Vtxs[i]].G, Vertices[Vtxs[i]].B, Vertices[Vtxs[i]].A);

		        GL.Vertex3(Vertices[Vtxs[i]].X, Vertices[Vtxs[i]].Y, Vertices[Vtxs[i]].Z);
	        }

	        GL.End();
        }

        void SetRenderMode(uint Mode1, uint Mode2)
        {
	        OtherModeL &= 0x00000007;
	        OtherModeL |= Mode1 | Mode2;
            
	        ChangedModes |= F3DEXParser.Constants.CHANGED_RENDERMODE;
        }
        /* What is this
        void CheckFragmentCache()
        {
	        int CacheCheck = 0; bool SearchingCache = true; bool NewProg = false;
	        while(SearchingCache) {
		        if((FragmentCache[CacheCheck].Combiner0 == F3DEXParser.Combiner0) && (FragmentCache[CacheCheck].Combiner1 == F3DEXParser.Combiner1)) {
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
	        Textures[CurrentTexture].Tile = Tile;
	        Textures[CurrentTexture].ULS = ULS;
	        Textures[CurrentTexture].ULT = ULT;
	        Textures[CurrentTexture].LRS = LRS;
	        Textures[CurrentTexture].LRT = LRT;
        }

        void CalcTextureSize(int TextureID)
        {
	        uint MaxTexel = 0, Line_Shift = 0;

	        switch(Textures[TextureID].Format) {
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

	        uint Line_Width = Textures[TextureID].LineSize << (int)Line_Shift;

	        uint Tile_Width = Textures[TextureID].LRS - Textures[TextureID].ULS + 1;
	        uint Tile_Height = Textures[TextureID].LRT - Textures[TextureID].ULT + 1;

            uint Mask_Width = (uint)1 << (int)Textures[TextureID].MaskS;
            uint Mask_Height = (uint)1 << (int)Textures[TextureID].MaskT;

	        uint Line_Height = 0;
	        if(Line_Width > 0) Line_Height = Math.Min(MaxTexel / Line_Width, Tile_Height);

	        if((Textures[TextureID].MaskS > 0) && ((Mask_Width * Mask_Height) <= MaxTexel)) {
		        Textures[TextureID].Width = Mask_Width;
	        } else if((Tile_Width * Tile_Height) <= MaxTexel) {
		        Textures[TextureID].Width = Tile_Width;
	        } else {
		        Textures[TextureID].Width = Line_Width;
	        }

	        if((Textures[TextureID].MaskT > 0) && ((Mask_Width * Mask_Height) <= MaxTexel)) {
		        Textures[TextureID].Height = Mask_Height;
	        } else if((Tile_Width * Tile_Height) <= MaxTexel) {
		        Textures[TextureID].Height = Tile_Height;
	        } else {
		        Textures[TextureID].Height = Line_Height;
	        }

	        uint Clamp_Width = 0;
	        uint Clamp_Height = 0;

            //if ((Textures[TextureID].CMS & F3DEXParser.Constants.G_TX_CLAMP) && (!Textures[TextureID].CMS & F3DEXParser.Constants.G_TX_MIRROR))
            if (false) //NOTE: THIS LINE IS IMPOSSIBLE TO GET THROUGH. The ! WILL CONVERT 0x0 TO 0x1, AND ANY OTHER VALUE TO 0x0.
            {
		        Clamp_Width = Tile_Width;
	        } else {
		        Clamp_Width = Textures[TextureID].Width;
	        }
            //if ((Textures[TextureID].CMT & F3DEXParser.Constants.G_TX_CLAMP) && (!Textures[TextureID].CMT & F3DEXParser.Constants.G_TX_MIRROR))
            if (false) //NOTE: THIS LINE IS IMPOSSIBLE TO GET THROUGH. The ! WILL CONVERT 0x0 TO 0x1, AND ANY OTHER VALUE TO 0x0.
            {
		        Clamp_Height = Tile_Height;
	        } else {
		        Clamp_Height = Textures[TextureID].Height;
	        }

	        if(Mask_Width > Textures[TextureID].Width) {
		        Textures[TextureID].MaskS = PowOf(Textures[TextureID].Width);
                Mask_Width = (uint)1 << (int)Textures[TextureID].MaskS;
	        }
	        if(Mask_Height > Textures[TextureID].Height) {
		        Textures[TextureID].MaskT = PowOf(Textures[TextureID].Height);
                Mask_Height = (uint)1 << (int)Textures[TextureID].MaskT;
	        }

            if ((Textures[TextureID].CMS & F3DEXParser.Constants.G_TX_CLAMP) != 0x0)
            {
		        Textures[TextureID].RealWidth = Pow2(Clamp_Width);
            }
            else if ((Textures[TextureID].CMS & F3DEXParser.Constants.G_TX_MIRROR) != 0x0)
            {
		        Textures[TextureID].RealWidth = Pow2(Mask_Width);
	        } else {
		        Textures[TextureID].RealWidth = Pow2(Textures[TextureID].Width);
	        }

            if ((Textures[TextureID].CMT & F3DEXParser.Constants.G_TX_CLAMP) != 0x0)
            {
		        Textures[TextureID].RealHeight = Pow2(Clamp_Height);
	        } else if((Textures[TextureID].CMT & F3DEXParser.Constants.G_TX_MIRROR) != 0x0) {
		        Textures[TextureID].RealHeight = Pow2(Mask_Height);
	        } else {
		        Textures[TextureID].RealHeight = Pow2(Textures[TextureID].Height);
	        }

	        Textures[TextureID].ShiftScaleS = 1.0f;
	        Textures[TextureID].ShiftScaleT = 1.0f;

	        if(Textures[TextureID].ShiftS > 10) {
                Textures[TextureID].ShiftScaleS = (1 << (int)(16 - Textures[TextureID].ShiftS));
	        } else if(Textures[TextureID].ShiftS > 0) {
                Textures[TextureID].ShiftScaleS /= (1 << (int)Textures[TextureID].ShiftS);
	        }

	        if(Textures[TextureID].ShiftT > 10) {
                Textures[TextureID].ShiftScaleT = (1 << (int)(16 - Textures[TextureID].ShiftT));
	        } else if(Textures[TextureID].ShiftT > 0) {
                Textures[TextureID].ShiftScaleT /= (1 << (int)Textures[TextureID].ShiftT);
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
                if (Textures[0].Offset != 0x00)
                {
                    GL.Enable(EnableCap.Texture2D);
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, CheckTextureCache(0));
                }

                if (IsMultiTexture && (Textures[1].Offset != 0x00))
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
                if (Textures[0].Offset != 0x00)
                {
                    GL.Enable(EnableCap.Texture2D);
                    uint cache = CheckTextureCache(0);
                    //GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, cache);
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
                if ((TextureCache[CacheCheck].Offset == Textures[TexID].Offset) &&
                    (TextureCache[CacheCheck].RealWidth == Textures[TexID].RealWidth) &&
                    (TextureCache[CacheCheck].RealHeight == Textures[TexID].RealHeight))
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
                TextureCache[TextureCachePosition].Offset = Textures[TexID].Offset;
                TextureCache[TextureCachePosition].RealWidth = Textures[TexID].RealWidth;
                TextureCache[TextureCachePosition].RealHeight = Textures[TexID].RealHeight;
                TextureCache[TextureCachePosition].TextureID = GLID;
                TextureCachePosition++;
            }
            else
            {
                GLID = TextureCache[CacheCheck].TextureID;
            }

            if (TextureCachePosition >= 2048)
            {
                for(int i = 0; i < 2048; i++) TextureCache[i] = BlankTextureCache;
                TextureCachePosition = 0;
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
            char TexSegment = (char)((Textures[TextureID].Offset & 0xFF000000) >> 24);
            uint TexOffset = (Textures[TextureID].Offset & 0x00FFFFFF);

            if (GLTextureCount == 23)
            {
                Textures[TextureID].RealHeight = Textures[TextureID].Height;
                Textures[TextureID].RealWidth = Textures[TextureID].Width;
            }

            // CalcTextureSize(TextureID);

            int i = 0, j = 0;

            int BytesPerPixel = 0x08;
            switch(Textures[TextureID].Format) {
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

            uint BufferSize = (Textures[TextureID].RealHeight * Textures[TextureID].RealWidth) * (uint)BytesPerPixel;
            byte[] TextureData = new byte[BufferSize];

            for (int lj = 0; lj < TextureData.Length; lj++)
            {
                TextureData[lj] = (byte)0xFF;
            }

            //memset(TextureData, 0xFF, BufferSize);

            uint GLTexPosition = 0;
            
            
            char TempSegment;
            uint TempOffset;

            SplitAddress(Textures[TextureID].Offset, out TempSegment, out TempOffset);

            //if (!MemoryManager.Instance.LocateBank((byte)TempSegment, TempOffset).IsValid())
            {
                //memset(TextureData, 0xFF, BufferSize);
            }
            //else
            {
                switch (Textures[TextureID].Format)
                {
                    case 0x00:
                    case 0x08:
                    case 0x10:
                        {
                            ushort Raw;
                            uint RGBA = 0;

                            for (j = 0; j < Textures[TextureID].Height; j++)
                            {
                                for (i = 0; i < Textures[TextureID].Width; i++)
                                {
                                    Raw = ByteHelper.ReadUShort(currentBytes, TexOffset);//(RAM[TexSegment].Data[TexOffset] << 8) | RAM[TexSegment].Data[TexOffset + 1];

                                    RGBA = (((uint)Raw & 0xF800) >> 8) << 24;
                                    RGBA |= ((((uint)Raw & 0x07C0) << 5) >> 8) << 16;
                                    RGBA |= ((((uint)Raw & 0x003E) << 18) >> 16) << 8;
                                    if (((uint)Raw & 0x0001) != 0x0) RGBA |= 0xFF;
                                    Write32(TextureData, GLTexPosition, RGBA);

                                    TexOffset += 2;
                                    GLTexPosition += 4;

                                    //For now assume it will be valid
                                    //if (!MemoryManager.Instance.LocateBank((byte)TexSegment, TexOffset).IsValid()) break;//(TexOffset > RAM[TexSegment].Size) break;
                                }
                                TexOffset += Textures[TextureID].LineSize * 4 - Textures[TextureID].Width;
                            }
                            break;
                        }

                    case 0x18:
                        {
                            uint totalSize = (Textures[TextureID].Height * Textures[TextureID].Width);
                            for(uint k = 0; k < totalSize; k++)
                                Write32(TextureData, k * 4, ByteHelper.ReadUInt(currentBytes, TexOffset + 4 * k));
                            //memcpy(TextureData, &RAM[TexSegment].Data[TexOffset], (Textures[TextureID].Height * Textures[TextureID].Width * 4));
                            break;
                        }

                    //case 0x40:
                    //case 0x50:
                    //    {
                    //        uint CI1, CI2;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < Textures[TextureID].Width / 2; i++)
                    //            {
                    //                CI1 = (RAM[TexSegment].Data[TexOffset] & 0xF0) >> 4;
                    //                CI2 = (RAM[TexSegment].Data[TexOffset] & 0x0F);

                    //                RGBA = ((uint)F3DEXParser.Palettes[CI1].R << 24);
                    //                RGBA |= ((uint)F3DEXParser.Palettes[CI1].G << 16);
                    //                RGBA |= ((uint)F3DEXParser.Palettes[CI1].B << 8);
                    //                RGBA |= (uint)F3DEXParser.Palettes[CI1].A;
                    //                Write32(TextureData, GLTexPosition, RGBA);

                    //                RGBA = ((uint)F3DEXParser.Palettes[CI2].R << 24);
                    //                RGBA |= ((uint)F3DEXParser.Palettes[CI2].G << 16);
                    //                RGBA |= ((uint)F3DEXParser.Palettes[CI2].B << 8);
                    //                RGBA |= (uint)F3DEXParser.Palettes[CI2].A;
                    //                Write32(TextureData, GLTexPosition + 4, RGBA);

                    //                TexOffset += 1;
                    //                GLTexPosition += 8;
                    //            }
                    //            TexOffset += Textures[TextureID].LineSize * 8 - (Textures[TextureID].Width / 2);
                    //        }
                    //        break;
                    //    }

                    //case 0x48:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < Textures[TextureID].Width; i++)
                    //            {
                    //                Raw = RAM[TexSegment].Data[TexOffset];

                    //                RGBA = ((uint)F3DEXParser.Palettes[Raw].R << 24);
                    //                RGBA |= ((uint)F3DEXParser.Palettes[Raw].G << 16);
                    //                RGBA |= ((uint)F3DEXParser.Palettes[Raw].B << 8);
                    //                RGBA |= (uint)F3DEXParser.Palettes[Raw].A;
                    //                Write32(TextureData, GLTexPosition, RGBA);

                    //                TexOffset += 1;
                    //                GLTexPosition += 4;
                    //            }
                    //            TexOffset += Textures[TextureID].LineSize * 8 - Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    //case 0x60:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < Textures[TextureID].Width / 2; i++)
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
                    //            TexOffset += Textures[TextureID].LineSize * 8 - (Textures[TextureID].Width / 2);
                    //        }
                    //        break;
                    //    }

                    //case 0x68:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < Textures[TextureID].Width; i++)
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
                    //            TexOffset += Textures[TextureID].LineSize * 8 - Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    //case 0x70:
                    //    {
                    //        for (j = 0; j < Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < Textures[TextureID].Width; i++)
                    //            {
                    //                TextureData[GLTexPosition] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 1] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 2] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 3] = RAM[TexSegment].Data[TexOffset + 1];

                    //                TexOffset += 2;
                    //                GLTexPosition += 4;
                    //            }
                    //            TexOffset += Textures[TextureID].LineSize * 4 - Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    //case 0x80:
                    //case 0x90:
                    //    {
                    //        ushort Raw;
                    //        uint RGBA = 0;

                    //        for (j = 0; j < Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < Textures[TextureID].Width / 2; i++)
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
                    //            TexOffset += Textures[TextureID].LineSize * 8 - (Textures[TextureID].Width / 2);
                    //        }
                    //        break;
                    //    }

                    //case 0x88:
                    //    {
                    //        for (j = 0; j < Textures[TextureID].Height; j++)
                    //        {
                    //            for (i = 0; i < Textures[TextureID].Width; i++)
                    //            {
                    //                TextureData[GLTexPosition] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 1] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 2] = RAM[TexSegment].Data[TexOffset];
                    //                TextureData[GLTexPosition + 3] = 0xFF;

                    //                TexOffset += 1;
                    //                GLTexPosition += 4;
                    //            }
                    //            TexOffset += Textures[TextureID].LineSize * 8 - Textures[TextureID].Width;
                    //        }
                    //        break;
                    //    }

                    default:
                        //memset(TextureData, 0xFF, BufferSize);
                        break;
                }
            }

            GL.BindTexture(TextureTarget.Texture2D, GLTextureID[GLTextureCount]);

            if ((Textures[TextureID].CMT & F3DEXParser.Constants.G_TX_CLAMP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge); }
            if ((Textures[TextureID].CMT & F3DEXParser.Constants.G_TX_WRAP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat); }
            if ((Textures[TextureID].CMT & F3DEXParser.Constants.G_TX_MIRROR) != 0) { if (OpenGlSettings.Ext_TexMirroredRepeat) GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat); }

            if ((Textures[TextureID].CMS & F3DEXParser.Constants.G_TX_CLAMP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge); }
            if ((Textures[TextureID].CMS & F3DEXParser.Constants.G_TX_WRAP) != 0) { GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); }
            if ((Textures[TextureID].CMS & F3DEXParser.Constants.G_TX_MIRROR) != 0) { if (OpenGlSettings.Ext_TexMirroredRepeat) GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat); }

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)Textures[TextureID].RealWidth, (int)Textures[TextureID].RealHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, TextureData); //last param needs ref??
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //Debug outputting texture data
            //DrawTextureRGBA(TextureData, (int)Textures[TextureID].RealWidth, (int)Textures[TextureID].RealHeight, string.Format("Texture{0}.bmp", F3DEXParser.GLTextureCount));

            GLTextureCount++;

            return GLTextureID[GLTextureCount-1];
            
        }


        
        #endregion


        public void UpdateStates()
        {
            if ((ChangedModes & F3DEXParser.Constants.CHANGED_GEOMETRYMODE) != 0x0)
            {
                if ((GeometryMode & F3DEXParser.Constants.F3DEX_CULL_BOTH) != 0x0)
                {
                    GL.Enable(EnableCap.CullFace);

                    if ((GeometryMode & F3DEXParser.Constants.F3DEX_CULL_BACK) != 0x0)
                        GL.CullFace(CullFaceMode.Back);
                    else
                        GL.CullFace(CullFaceMode.Front);
                }
                else
                {
                    GL.Disable(EnableCap.CullFace);
                }

                if ((GeometryMode & F3DEXParser.Constants.F3DEX_SHADING_SMOOTH) != 0x0 || (GeometryMode & F3DEXParser.Constants.G_TEXTURE_GEN_LINEAR) == 0x0)
                {
                    GL.ShadeModel(ShadingModel.Smooth);
                }
                else
                {
                    GL.ShadeModel(ShadingModel.Flat);
                }

                if ((GeometryMode & F3DEXParser.Constants.G_LIGHTING) != 0x0)
                {
                    GL.Enable(EnableCap.Lighting);
                    GL.Enable(EnableCap.Normalize);
                }
                else
                {
                    GL.Disable(EnableCap.Lighting);
                    GL.Disable(EnableCap.Normalize);
                }

                ChangedModes &= ~(uint)F3DEXParser.Constants.CHANGED_GEOMETRYMODE;
            }
            /*
                if(Gfx.GeometryMode & G_ZBUFFER)
                    glEnable(GL_DEPTH_TEST);
                else
                    glDisable(GL_DEPTH_TEST);
            */
            if ((ChangedModes & F3DEXParser.Constants.CHANGED_RENDERMODE) != 0x0)
            {
                /*		if(Gfx.OtherModeL & Z_CMP)
                            glDepthFunc(GL_LEQUAL);
                        else
                            glDepthFunc(GL_ALWAYS);
                */
                /*		if((Gfx.OtherModeL & Z_UPD) && !(Gfx.OtherModeL & ZMODE_INTER && Gfx.OtherModeL & ZMODE_XLU))
                            glDepthMask(GL_TRUE);
                        else
                            glDepthMask(GL_FALSE);
                */
                if ((OtherModeL & F3DEXParser.Constants.ZMODE_DEC) != 0x0)
                {
                    GL.Enable(EnableCap.PolygonOffsetFill);
                    GL.PolygonOffset(-3.0f, -3.0f);
                }
                else
                {
                    GL.Disable(EnableCap.PolygonOffsetFill);
                }
            }

            if ((ChangedModes & F3DEXParser.Constants.CHANGED_ALPHACOMPARE) != 0x0 || (ChangedModes & F3DEXParser.Constants.CHANGED_RENDERMODE) != 0x0)
            {
                if ((OtherModeL & F3DEXParser.Constants.ALPHA_CVG_SEL) == 0x0)
                {
                    GL.Enable(EnableCap.AlphaTest);
                    GL.AlphaFunc((BlendColor.A > 0.0f) ? AlphaFunction.Gequal : AlphaFunction.Greater, BlendColor.A);
                }
                else if ((OtherModeL & F3DEXParser.Constants.CVG_X_ALPHA) != 0x0)
                {
                    GL.Enable(EnableCap.AlphaTest);
                    GL.AlphaFunc(AlphaFunction.Gequal, 0.2f);
                }
                else
                    GL.Disable(EnableCap.AlphaTest);
            }

            if ((ChangedModes & F3DEXParser.Constants.CHANGED_RENDERMODE) != 0x0)
            {
                if ((OtherModeL & F3DEXParser.Constants.FORCE_BL) != 0x0 && (OtherModeL & F3DEXParser.Constants.ALPHA_CVG_SEL) == 0x0)
                {
                    GL.Enable(EnableCap.Blend);

                    switch (OtherModeL >> 16)
                    {
                        case 0x0448: // Add
                        case 0x055A:
                            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
                            break;
                        case 0x0C08: // 1080 Sky
                        case 0x0F0A: // Used LOTS of places
                            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.Zero);
                            break;
                        case 0xC810: // Blends fog
                        case 0xC811: // Blends fog
                        case 0x0C18: // Standard interpolated blend
                        case 0x0C19: // Used for antialiasing
                        case 0x0050: // Standard interpolated blend
                        case 0x0055: // Used for antialiasing
                            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                            break;
                        case 0x0FA5: // Seems to be doing just blend color - maybe combiner can be used for this?
                        case 0x5055: // Used in Paper Mario intro, I'm not sure if this is right...
                            GL.BlendFunc(BlendingFactorSrc.Zero, BlendingFactorDest.One);
                            break;

                        default:
                            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                            break;
                    }
                }
                else
                {
                    GL.Disable(EnableCap.Blend);
                }
            }
        }


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






        #region GFX structs/variables


        public struct sfRGBA
        {
            public float R;
            public float B;
            public float G;
            public float A;
            public float Z;
            public float DZ;
        }
        public struct sfFillColor
        {
            public float R;
            public float B;
            public float G;
            public float A;
            public float Z;
            public float DZ;
        }
        public struct sfPrimColor
        {
            public float R;
            public float B;
            public float G;
            public float A;
            public float L;
            public ushort M;
        }

        public struct sfVertex
        {
            public short X;
            public short Y;
            public short Z;
            public short S;
            public short T;
            public char R;
            public char G;
            public char B;
            public char A;
        }


        public struct sfPalette
        {
            public char R;
            public char G;
            public char B;
            public char A;
        }

        public struct sfTexture
        {
            public uint Offset;
            public uint PalOffset;

            public uint Format;
            public uint Tile;
            public uint Width;
            public uint RealWidth;
            public uint Height;
            public uint RealHeight;
            public uint ULT, ULS;
            public uint LRT, LRS;
            public uint LineSize, Palette;
            public uint MaskT, MaskS;
            public uint ShiftT, ShiftS;
            public uint CMT, CMS;
            public float ScaleT, ScaleS;
            public float ShiftScaleT, ShiftScaleS;
        }

        public struct sfTextureCache
        {
            public uint Offset;
            public uint RealWidth;
            public uint RealHeight;
            public uint TextureID;
        }

        public struct OpenGLSetting
        {
            public char[] ExtensionList;
            public char[] ExtSupported; //256
            public char[] ExtUnsupported; //256
            public bool IsExtUnsupported;
            public bool Ext_MultiTexture;
            public bool Ext_TexMirroredRepeat;
            public bool Ext_FragmentProgram;
        }



        public uint[] DLStack = new uint[16]; //16
        public int DLStackPos;

        public sfVertex[] Vertices = new sfVertex[32]; //32

        public sfPalette[] Palettes = new sfPalette[256]; //256
        public sfTexture[] Textures = new sfTexture[2]; //2
        public sfTextureCache[] TextureCache = new sfTextureCache[2048]; //2048

        public sfVertex BlankVertex = new sfVertex();
        public sfPalette BlankPalette = new sfPalette();
        public sfTexture BlankTexture = new sfTexture();
        public sfTextureCache BlankTextureCache = new sfTextureCache();

        //public static uint FragCachePosition;
        public uint TextureCachePosition;

        public OpenGLSetting OpenGlSettings;

        //public uint GLListBase;

        public uint ChangedModes;
        public uint GeometryMode;
        public uint OtherModeL;
        public uint OtherModeH;
        public uint Store_RDPHalf1, Store_RDPHalf2;
        public uint Combiner0, Combiner1;

        public sfRGBA BlendColor;
        public sfRGBA EnvColor;
        public sfRGBA FogColor;
        public sfFillColor FillColor;
        public sfPrimColor PrimColor;

        public bool IsMultiTexture;
        public int CurrentTexture;

        public uint[] GLTextureID = new uint[2048]; //2048
        public int GLTextureCount;

        public void gl_ClearRenderer(bool Full)
        {
            //if (Full)
            //{
            //    if (GL.IsList(GLListBase)) GL.DeleteLists(GLListBase, GLListBase);
            //}

            if (GLTextureID[0] != 0) GL.DeleteTextures(GLTextureCount, GLTextureID);
            GLTextureCount = 0;

            GL.GenTextures(2048, GLTextureID);
        }

        public void sv_ClearStructures(bool Full)
        {
            int i = 0;

            for (i = 0; i < Vertices.Length; i++) Vertices[i] = new sfVertex();

            Textures[0] = new sfTexture();
            Textures[1] = new sfTexture();

            for (i = 0; i < TextureCache.Length; i++) TextureCache[i] = new sfTextureCache();
            TextureCachePosition = 0;

            BlendColor = new sfRGBA();
            EnvColor = new sfRGBA();
            FogColor = new sfRGBA();
            FillColor = new sfFillColor();
            PrimColor = new sfPrimColor();

            DLStackPos = 0;

            ChangedModes = 0;
            GeometryMode = 0;
            OtherModeL = 0;
            OtherModeH = 0;
            Store_RDPHalf1 = 0; Store_RDPHalf2 = 0;
            Combiner0 = 0; Combiner1 = 0;

            Textures[0].ScaleS = 1.0f;
            Textures[0].ScaleT = 1.0f;
            Textures[1].ScaleS = 1.0f;
            Textures[1].ScaleT = 1.0f;

            Textures[0].ShiftScaleS = 1.0f;
            Textures[0].ShiftScaleT = 1.0f;
            Textures[1].ShiftScaleS = 1.0f;
            Textures[1].ShiftScaleT = 1.0f;

            //if(Full) {
            //    i = 0; j = 0;

            //    static const struct __FragmentCache FragmentCache_Empty;
            //    for(i = 0; i < ArraySize(FragmentCache); i++) FragmentCache[i] = FragmentCache_Empty;
            //    Program.FragCachePosition = 0;

            //    static const struct __Camera Camera_Empty;
            //    Camera = Camera_Empty;
            //}
        }
        
        public static class Constants
        {
            public const uint F3DEX_TEXTURE_ENABLE = 0x00000002;
            public const uint F3DEX_SHADING_SMOOTH = 0x00000200;
            public const uint F3DEX_CULL_FRONT = 0x00001000;
            public const uint F3DEX_CULL_BACK = 0x00002000;
            public const uint F3DEX_CULL_BOTH = 0x00003000;
            public const uint F3DEX_CLIPPING = 0x00800000;

            public const uint G_ZBUFFER = 0x00000001;
            public const uint G_SHADE = 0x00000004;
            public const uint G_FOG = 0x00010000;
            public const uint G_LIGHTING = 0x00020000;
            public const uint G_TEXTURE_GEN = 0x00040000;
            public const uint G_TEXTURE_GEN_LINEAR = 0x00080000;
            public const uint G_LOD = 0x00100000;

            public const byte G_MDSFT_ALPHACOMPARE = 0;
            public const byte G_MDSFT_ZSRCSEL = 2;
            public const byte G_MDSFT_RENDERMODE = 3;

            public const byte G_MDSFT_ALPHADITHER = 4;
            public const byte G_MDSFT_RGBDITHER = 6;
            public const byte G_MDSFT_COMBKEY = 8;
            public const byte G_MDSFT_TEXTCONV = 9;
            public const byte G_MDSFT_TEXTFILT = 12;
            public const byte G_MDSFT_TEXTLUT = 14;
            public const byte G_MDSFT_TEXTLOD = 16;
            public const byte G_MDSFT_TEXTDETAIL = 17;
            public const byte G_MDSFT_TEXTPERSP = 19;
            public const byte G_MDSFT_CYCLETYPE = 20;
            public const byte G_MDSFT_PIPELINE = 23;

            public const byte G_TX_WRAP = 0x00;
            public const byte G_TX_MIRROR = 0x01;
            public const byte G_TX_CLAMP = 0x02;

            public const byte G_CCMUX_COMBINED = 0;
            public const byte G_CCMUX_TEXEL0 = 1;
            public const byte G_CCMUX_TEXEL1 = 2;
            public const byte G_CCMUX_PRIMITIVE = 3;
            public const byte G_CCMUX_SHADE = 4;
            public const byte G_CCMUX_ENVIRONMENT = 5;
            public const byte G_CCMUX_CENTER = 6;
            public const byte G_CCMUX_SCALE = 6;//??????? Should this be different!?!?!
            public const byte G_CCMUX_COMBINED_ALPHA = 7;
            public const byte G_CCMUX_TEXEL0_ALPHA = 8;
            public const byte G_CCMUX_TEXEL1_ALPHA = 9;
            public const byte G_CCMUX_PRIMITIVE_ALPHA = 10;
            public const byte G_CCMUX_SHADE_ALPHA = 11;
            public const byte G_CCMUX_ENV_ALPHA = 12;
            public const byte G_CCMUX_LOD_FRACTION = 13;
            public const byte G_CCMUX_PRIM_LOD_FRAC = 14;
            public const byte G_CCMUX_NOISE = 7;
            public const byte G_CCMUX_K4 = 7;
            public const byte G_CCMUX_K5 = 15;
            public const byte G_CCMUX_1 = 6;
            public const byte G_CCMUX_0 = 31;

            public const byte G_ACMUX_COMBINED = 0;
            public const byte G_ACMUX_TEXEL0 = 1;
            public const byte G_ACMUX_TEXEL1 = 2;
            public const byte G_ACMUX_PRIMITIVE = 3;
            public const byte G_ACMUX_SHADE = 4;
            public const byte G_ACMUX_ENVIRONMENT = 5;
            public const byte G_ACMUX_LOD_FRACTION = 0;
            public const byte G_ACMUX_PRIM_LOD_FRAC = 6;
            public const byte G_ACMUX_1 = 6;
            public const byte G_ACMUX_0 = 7;

            public const uint AA_EN = 0x00000008;
            public const uint Z_CMP = 0x00000010;
            public const uint Z_UPD = 0x00000020;
            public const uint IM_RD = 0x00000040;
            public const uint CLR_ON_CVG = 0x00000080;
            public const uint CVG_DST_CLAMP = 0x00000000;
            public const uint CVG_DST_WRAP = 0x00000100;
            public const uint CVG_DST_FULL = 0x00000200;
            public const uint CVG_DST_SAVE = 0x00000300;
            public const uint ZMODE_OPA = 0x00000000;
            public const uint ZMODE_INTER = 0x00000400;
            public const uint ZMODE_XLU = 0x00000800;
            public const uint ZMODE_DEC = 0x00000C00;
            public const uint CVG_X_ALPHA = 0x00001000;
            public const uint ALPHA_CVG_SEL = 0x00002000;
            public const uint FORCE_BL = 0x00004000;

            public const byte CHANGED_GEOMETRYMODE = 0x01;
            public const byte CHANGED_RENDERMODE = 0x02;
            public const byte CHANGED_ALPHACOMPARE = 0x04;
        }
        
        #endregion


    }
}
