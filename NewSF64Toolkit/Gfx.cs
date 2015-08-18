using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace NewSF64Toolkit
{
    public struct RGBA
    {
        public float R;
        public float B;
        public float G;
        public float A;
        public float Z;
        public float DZ;
    }
    public struct FillColor
    {
        public float R;
        public float B;
        public float G;
        public float A;
        public float Z;
        public float DZ;
    }
    public struct PrimColor
    {
        public float R;
        public float B;
        public float G;
        public float A;
        public float L;
        public ushort M;
    }
    
    public struct Vertex
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

    
    public struct Palette {
	    public char R;
	    public char G;
	    public char B;
	    public char A;
    }

    public struct Texture {
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

    public struct TextureCache {
	    public uint Offset;
	    public uint RealWidth;
	    public uint RealHeight;
	    public uint TextureID;
    }

    public struct OpenGLSetting {
        public char[] ExtensionList;
        public char[] ExtSupported; //256
        public char[] ExtUnsupported; //256
        public bool IsExtUnsupported;
        public bool Ext_MultiTexture;
        public bool Ext_TexMirroredRepeat;
        public bool Ext_FragmentProgram;
    }



    public static class SFGfx
    {
        public static uint[] DLStack = new uint[16]; //16
        public static int DLStackPos;

        public static Vertex[] Vertices = new Vertex[32]; //32
        
        public static Palette[] Palettes = new Palette[256]; //256
        public static Texture[] Textures = new Texture[2]; //2
        public static TextureCache[] TextureCache = new TextureCache[2048]; //2048

        public static Vertex BlankVertex = new Vertex();
        public static Palette BlankPalette = new Palette();
        public static Texture BlankTexture = new Texture();
        public static TextureCache BlankTextureCache = new TextureCache();

        //public static uint FragCachePosition;
	    public static uint TextureCachePosition;

        public static OpenGLSetting OpenGlSettings;

        public static uint GLListCount;

        public static int GameObjCount;

        public static uint ChangedModes;
        public static uint GeometryMode;
        public static uint OtherModeL;
        public static uint OtherModeH;
        public static float[] LightAmbient = new float[4]; //4
        public static float[] LightDiffuse = new float[4]; //4
        public static float[] LightSpecular = new float[4]; //4
        public static float[] LightPosition = new float[4]; //4
        public static uint Store_RDPHalf1, Store_RDPHalf2;
        public static uint Combiner0, Combiner1;

        public static RGBA BlendColor;
        public static RGBA EnvColor;
        public static RGBA FogColor;
        public static FillColor FillColor;
        public static PrimColor PrimColor;

        public static bool IsMultiTexture;
        public static int CurrentTexture;

        public static uint[] GLTextureID = new uint[2048]; //2048
        public static int GLTextureCount;


        public static void gl_ClearRenderer(bool Full)
        {
            if (Full)
            {
                if (GL.IsList(SFGfx.GLListCount)) GL.DeleteLists(SFGfx.GLListCount, SFGfx.GameObjCount);
            }

            if (SFGfx.GLTextureID[0] != 0) GL.DeleteTextures(SFGfx.GLTextureCount, SFGfx.GLTextureID);
            SFGfx.GLTextureCount = 0;

            GL.GenTextures(2048, SFGfx.GLTextureID);
        }

        public static void sv_ClearStructures(bool Full)
        {
            int i = 0;

            for (i = 0; i < SFGfx.Vertices.Length; i++) SFGfx.Vertices[i] = new Vertex();

            SFGfx.Textures[0] = new Texture();
            SFGfx.Textures[1] = new Texture();

            for (i = 0; i < SFGfx.TextureCache.Length; i++) SFGfx.TextureCache[i] = new TextureCache();
            SFGfx.TextureCachePosition = 0;

            SFGfx.BlendColor = new RGBA();
            SFGfx.EnvColor = new RGBA();
            SFGfx.FogColor = new RGBA();
            SFGfx.FillColor = new FillColor();
            SFGfx.PrimColor = new PrimColor();

            SFGfx.DLStackPos = 0;

            SFGfx.ChangedModes = 0;
            SFGfx.GeometryMode = 0;
            SFGfx.OtherModeL = 0;
            SFGfx.OtherModeH = 0;
            SFGfx.Store_RDPHalf1 = 0; SFGfx.Store_RDPHalf2 = 0;
            SFGfx.Combiner0 = 0; SFGfx.Combiner1 = 0;

            SFGfx.Textures[0].ScaleS = 1.0f;
            SFGfx.Textures[0].ScaleT = 1.0f;
            SFGfx.Textures[1].ScaleS = 1.0f;
            SFGfx.Textures[1].ScaleT = 1.0f;

            SFGfx.Textures[0].ShiftScaleS = 1.0f;
            SFGfx.Textures[0].ShiftScaleT = 1.0f;
            SFGfx.Textures[1].ShiftScaleS = 1.0f;
            SFGfx.Textures[1].ShiftScaleT = 1.0f;

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

    }

}
