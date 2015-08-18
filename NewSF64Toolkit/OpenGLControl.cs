using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace NewSF64Toolkit
{
    public partial class OpenGLControl : UserControl
    {
        public OpenGLControl()
        {
            InitializeComponent();

            SFCamera.UpdateCamera += UpdateCamera;
        }

        public void UpdateCamera()
        {
            ReDraww();
        }

        public bool GLLoaded { get; private set; }

        private void glDisplay_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            SFCamera.Reset();
            //If there are unitialized-related errors, it's probably setting this too early
            GL.ClearColor(Color.CornflowerBlue);
            SetupViewport();
            GLLoaded = true;

        }

        private void glDisplay_Paint(object sender, PaintEventArgs e)
        {
            //if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            if (!GLLoaded)
                return;

            gl_DrawScene();

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Lighting);

            glDisplay.SwapBuffers();

        }

        private void SetupViewport()
        {
            gl_InitRenderer();
            gl_ResizeScene(glDisplay.Width, glDisplay.Height);
        }

        public void ReDraww()
        {
            glDisplay.Invalidate();
        }
        private void OpenGLControl_Resize(object sender, EventArgs e)
        {
            if (GLLoaded)
                gl_ResizeScene(glDisplay.Width, glDisplay.Height);
        }

        #region draw.c functions

        void gl_Perspective(double fovy, double aspect, double zNear, double zFar)
        {
	        double xmin, xmax, ymin, ymax;

	        ymax = zNear * Math.Tan(fovy * Math.PI / 360.0);
	        ymin = -ymax;
	        xmin = ymin * aspect;
	        xmax = ymax * aspect;

	        GL.Frustum(xmin, xmax, ymin, ymax, zNear, zFar);
        }

        private static double hypot(double a, double b)
	    {
	        return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
	    }

        void gl_LookAt(double p_EyeX, double p_EyeY, double p_EyeZ, double p_CenterX, double p_CenterY, double p_CenterZ)
        {
	        double l_X = p_EyeX - p_CenterX;
	        double l_Y = p_EyeY - p_CenterY;
	        double l_Z = p_EyeZ - p_CenterZ;

	        if(l_X == l_Y && l_Y == l_Z && l_Z == 0.0f) return;

	        if(l_X == l_Z && l_Z == 0.0f) {
		        if(l_Y < 0.0f)
			        GL.Rotate(-90.0f, 1, 0, 0);
		        else
			        GL.Rotate(90.0f, 1, 0, 0);
		        GL.Translate(-l_X, -l_Y, -l_Z);
		        return;
	        }

	        double l_rX = 0.0f;
	        double l_rY = 0.0f;

	        double l_hA = (l_X == 0.0f) ? l_Z : hypot(l_X, l_Z);
	        double l_hB;
	        if(l_Z == 0.0f)
		        l_hB = hypot(l_X, l_Y);
	        else
		        l_hB = (l_Y == 0.0f) ? l_hA : hypot(l_Y, l_hA);

	        l_rX = Math.Asin(l_Y / l_hB) * (180 / Math.PI);
	        l_rY = Math.Asin(l_X / l_hA) * (180 / Math.PI);

	        GL.Rotate(l_rX, 1, 0, 0);
	        if(l_Z < 0.0f)
		        l_rY += 180.0f;
	        else
		        l_rY = 360.0f - l_rY;

	        GL.Rotate(l_rY, 0, 1, 0);
	        GL.Translate(-p_EyeX, -p_EyeY, -p_EyeZ);
        }

        void gl_InitRenderer()
        {
            GL.MatrixMode(MatrixMode.Projection);

            int w = glDisplay.Width;
            int h = glDisplay.Height;
            GL.LoadIdentity();
            GL.Ortho(0, w, 0, h, -1, 1); // Bottom-left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, w, h); // Use all of the glControl painting area

	        GL.ShadeModel(ShadingModel.Smooth);
	        GL.Enable(EnableCap.PointSmooth);
	        GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

	        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

	        GL.ClearColor(0.2f, 0.5f, 0.7f, 1.0f);

            //Having issues with one computer at this line, it's generating an AccessViolationException. Appears to work okay without it
	        //GL.ClearDepth(5.0f);

	        GL.DepthFunc(DepthFunction.Lequal);
	        GL.Enable(EnableCap.DepthTest);

	        GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            int i = 0;
            for(i = 0; i < 4; i++) {
                SFGfx.LightAmbient[i] = 1.0f;
                SFGfx.LightDiffuse[i] = 1.0f;
                SFGfx.LightSpecular[i] = 1.0f;
                SFGfx.LightPosition[i] = 1.0f;
            }

	        GL.Light(LightName.Light0, LightParameter.Ambient, SFGfx.LightAmbient);
	        GL.Light(LightName.Light0, LightParameter.Diffuse, SFGfx.LightDiffuse);
	        GL.Light(LightName.Light0, LightParameter.Specular, SFGfx.LightSpecular);
	        GL.Light(LightName.Light0, LightParameter.Position, SFGfx.LightPosition);
	        GL.Enable(EnableCap.Light0);

	        GL.Enable(EnableCap.Lighting);
	        GL.Enable(EnableCap.Normalize);

	        GL.Enable(EnableCap.CullFace);
	        GL.CullFace(CullFaceMode.Back);

	        GL.Enable(EnableCap.Blend);
	        GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //if(OpenGL.Ext_FragmentProgram) {
            //    GL.ProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 0, Gfx.EnvColor.R, Gfx.EnvColor.G, Gfx.EnvColor.B, Gfx.EnvColor.A);
            //    GL.ProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 1, Gfx.PrimColor.R, Gfx.PrimColor.G, Gfx.PrimColor.B, Gfx.PrimColor.A);
            //    GL.ProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 2, Gfx.BlendColor.R, Gfx.BlendColor.G, Gfx.BlendColor.B, Gfx.BlendColor.A);
            //    GL.ProgramEnvParameter4fARB(GL_FRAGMENT_PROGRAM_ARB, 3, Gfx.PrimColor.L, Gfx.PrimColor.L, Gfx.PrimColor.L, Gfx.PrimColor.L);
            //}
        }

        //void gl_InitExtensions()
        //{
        //    SFGfx.OpenGlSettings.IsExtUnsupported = false;

        //    SFGfx.OpenGlSettings.ExtensionList = strdup((const char*)glGetString(GL_EXTENSIONS));
        //    int i;
        //    for(i = 0; i < strlen(SFGfx.OpenGlSettings.ExtensionList); i++) {
        //        if(SFGfx.OpenGlSettings.ExtensionList[i] == ' ') SFGfx.OpenGlSettings.ExtensionList[i] = '\n';
        //    }

        //    if(strstr(SFGfx.OpenGlSettings.ExtensionList, "GL_ARB_texture_mirrored_repeat")) {
        //        SFGfx.OpenGlSettings.Ext_TexMirroredRepeat = true;
        //        sprintf(SFGfx.OpenGlSettings.ExtSupported, "%sGL_ARB_texture_mirrored_repeat\n", SFGfx.OpenGlSettings.ExtSupported);
        //    } else {
        //        SFGfx.OpenGlSettings.IsExtUnsupported = true;
        //        SFGfx.OpenGlSettings.Ext_TexMirroredRepeat = false;
        //        sprintf(SFGfx.OpenGlSettings.ExtUnsupported, "%sGL_ARB_texture_mirrored_repeat\n", SFGfx.OpenGlSettings.ExtUnsupported);
        //    }

        //    if(strstr(SFGfx.OpenGlSettings.ExtensionList, "GL_ARB_multitexture")) {
        //        SFGfx.OpenGlSettings.Ext_MultiTexture = true;

        //        glMultiTexCoord1fARB		= (PFNGLMULTITEXCOORD1FARBPROC) wglGetProcAddress("glMultiTexCoord1fARB");
        //        glMultiTexCoord2fARB		= (PFNGLMULTITEXCOORD2FARBPROC) wglGetProcAddress("glMultiTexCoord2fARB");
        //        glMultiTexCoord3fARB		= (PFNGLMULTITEXCOORD3FARBPROC) wglGetProcAddress("glMultiTexCoord3fARB");
        //        glMultiTexCoord4fARB		= (PFNGLMULTITEXCOORD4FARBPROC) wglGetProcAddress("glMultiTexCoord4fARB");
        //        glActiveTextureARB			= (PFNGLACTIVETEXTUREARBPROC) wglGetProcAddress("glActiveTextureARB");
        //        glClientActiveTextureARB	= (PFNGLCLIENTACTIVETEXTUREARBPROC) wglGetProcAddress("glClientActiveTextureARB");

        //        sprintf(SFGfx.OpenGlSettings.ExtSupported, "%sGL_ARB_multitexture\n", SFGfx.OpenGlSettings.ExtSupported);
        //    } else {
        //        SFGfx.OpenGlSettings.IsExtUnsupported = true;
        //        SFGfx.OpenGlSettings.Ext_MultiTexture = false;
        //        sprintf(SFGfx.OpenGlSettings.ExtUnsupported, "%sGL_ARB_multitexture\n", SFGfx.OpenGlSettings.ExtUnsupported);
        //    }

        //    if(strstr(SFGfx.OpenGlSettings.ExtensionList, "GL_ARB_fragment_program")) {
        //        SFGfx.OpenGlSettings.Ext_FragmentProgram = true;

        //        glGenProgramsARB				= (PFNGLGENPROGRAMSARBPROC) wglGetProcAddress("glGenProgramsARB");
        //        glBindProgramARB				= (PFNGLBINDPROGRAMARBPROC) wglGetProcAddress("glBindProgramARB");
        //        glDeleteProgramsARB				= (PFNGLDELETEPROGRAMSARBPROC) wglGetProcAddress("glDeleteProgramsARB");
        //        glProgramStringARB				= (PFNGLPROGRAMSTRINGARBPROC) wglGetProcAddress("glProgramStringARB");
        //        glProgramEnvParameter4fARB		= (PFNGLPROGRAMENVPARAMETER4FARBPROC) wglGetProcAddress("glProgramEnvParameter4fARB");
        //        glProgramLocalParameter4fARB	= (PFNGLPROGRAMLOCALPARAMETER4FARBPROC) wglGetProcAddress("glProgramLocalParameter4fARB");

        //        sprintf(SFGfx.OpenGlSettings.ExtSupported, "%sGL_ARB_fragment_program\n", SFGfx.OpenGlSettings.ExtSupported);
        //    } else {
        //        SFGfx.OpenGlSettings.IsExtUnsupported = true;
        //        SFGfx.OpenGlSettings.Ext_FragmentProgram = false;
        //        sprintf(SFGfx.OpenGlSettings.ExtUnsupported, "%sGL_ARB_fragment_program\n", SFGfx.OpenGlSettings.ExtUnsupported);
        //    }
        //}

        void gl_ResizeScene(int Width, int Height)
        {
	        GL.Viewport(0, 0, Width, Height);

	        GL.MatrixMode(MatrixMode.Projection);
	        GL.LoadIdentity();
	        gl_Perspective(60.0f, (float)Width / (float)Height, 0.1f, 100.0f);

	        GL.MatrixMode(MatrixMode.Modelview);
	        GL.LoadIdentity();
        }

        void gl_DrawScene()
        {
	        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

	        GL.LoadIdentity();

            gl_LookAt(SFCamera.X, SFCamera.Y, SFCamera.Z, SFCamera.X + SFCamera.LX, SFCamera.Y + SFCamera.LY, SFCamera.Z + SFCamera.LZ);

	        GL.Disable(EnableCap.Texture2D);
	        GL.Color3(0.9f, 0.9f, 0.9f);
            //glEnable(GL_TEXTURE_2D);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 1);

	        GL.Begin(PrimitiveType.Quads);
		        GL.Vertex3(-12.0f, -0.01f,-1000.0f);
		        GL.Vertex3(-12.0f, -0.01f,   10.0f);
		        GL.Vertex3( 12.0f, -0.01f,   10.0f);
		        GL.Vertex3( 12.0f, -0.01f,-1000.0f);
	        GL.End();
	        GL.Disable(EnableCap.Texture2D);
	        GL.Enable(EnableCap.Texture2D);

            GL.Scale(0.004f, 0.004f, 0.004f);

	        int ObjectNo = 0;
            while (ObjectNo < SFGfx.GameObjCount)
            {
                GL.CallList((uint)ObjectNo);
                ObjectNo++;
            }
        }

        public void UpdateStates()
        {
            if ((SFGfx.ChangedModes & SFGfx.Constants.CHANGED_GEOMETRYMODE) != 0x0)
            {
                if ((SFGfx.GeometryMode & SFGfx.Constants.F3DEX_CULL_BOTH) != 0x0)
                {
                    GL.Enable(EnableCap.CullFace);

                    if ((SFGfx.GeometryMode & SFGfx.Constants.F3DEX_CULL_BACK) != 0x0)
                        GL.CullFace(CullFaceMode.Back);
                    else
                        GL.CullFace(CullFaceMode.Front);
                }
                else
                {
                    GL.Disable(EnableCap.CullFace);
                }

                if ((SFGfx.GeometryMode & SFGfx.Constants.F3DEX_SHADING_SMOOTH) != 0x0 || (SFGfx.GeometryMode & SFGfx.Constants.G_TEXTURE_GEN_LINEAR) == 0x0)
                {
                    GL.ShadeModel(ShadingModel.Smooth);
                }
                else
                {
                    GL.ShadeModel(ShadingModel.Flat);
                }

                if ((SFGfx.GeometryMode & SFGfx.Constants.G_LIGHTING) != 0x0)
                {
                    GL.Enable(EnableCap.Lighting);
                    GL.Enable(EnableCap.Normalize);
                }
                else
                {
                    GL.Disable(EnableCap.Lighting);
                    GL.Disable(EnableCap.Normalize);
                }

                SFGfx.ChangedModes &= ~(uint)SFGfx.Constants.CHANGED_GEOMETRYMODE;
            }
            /*
                if(Gfx.GeometryMode & G_ZBUFFER)
                    glEnable(GL_DEPTH_TEST);
                else
                    glDisable(GL_DEPTH_TEST);
            */
            if ((SFGfx.ChangedModes & SFGfx.Constants.CHANGED_RENDERMODE) != 0x0)
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
                if ((SFGfx.OtherModeL & SFGfx.Constants.ZMODE_DEC) != 0x0)
                {
                    GL.Enable(EnableCap.PolygonOffsetFill);
                    GL.PolygonOffset(-3.0f, -3.0f);
                }
                else
                {
                    GL.Disable(EnableCap.PolygonOffsetFill);
                }
            }

            if ((SFGfx.ChangedModes & SFGfx.Constants.CHANGED_ALPHACOMPARE) != 0x0 || (SFGfx.ChangedModes & SFGfx.Constants.CHANGED_RENDERMODE) != 0x0)
            {
                if ((SFGfx.OtherModeL & SFGfx.Constants.ALPHA_CVG_SEL) == 0x0)
                {
                    GL.Enable(EnableCap.AlphaTest);
                    GL.AlphaFunc((SFGfx.BlendColor.A > 0.0f) ? AlphaFunction.Gequal : AlphaFunction.Greater, SFGfx.BlendColor.A);
                }
                else if ((SFGfx.OtherModeL & SFGfx.Constants.CVG_X_ALPHA) != 0x0)
                {
                    GL.Enable(EnableCap.AlphaTest);
                    GL.AlphaFunc(AlphaFunction.Gequal, 0.2f);
                }
                else
                    GL.Disable(EnableCap.AlphaTest);
            }

            if ((SFGfx.ChangedModes & SFGfx.Constants.CHANGED_RENDERMODE) != 0x0)
            {
                if ((SFGfx.OtherModeL & SFGfx.Constants.FORCE_BL) != 0x0 && (SFGfx.OtherModeL & SFGfx.Constants.ALPHA_CVG_SEL) == 0x0)
                {
                    GL.Enable(EnableCap.Blend);

                    switch (SFGfx.OtherModeL >> 16)
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

        private void glDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            bool hasFocus = this.Focused;
            if (!hasFocus)
            {
                foreach (Control ctl in this.Controls)
                {
                    if (ctl.Focused)
                    {
                        hasFocus = true;
                        break;
                    }
                }
            }
            if (hasFocus)
            {
                //Move camera here

                if(e.KeyData == Keys.W)
                    SFCamera.Movement(false, 6.0f);

                if (e.KeyData == Keys.S)
                    SFCamera.Movement(false, -6.0f);

                if (e.KeyData == Keys.A)
                    SFCamera.Movement(true, -6.0f);

                if (e.KeyData == Keys.D)
                    SFCamera.Movement(true, 6.0f);

                if (e.KeyData == Keys.T)
                    SFCamera.Movement(false, 24.0f);

                if (e.KeyData == Keys.G)
                    SFCamera.Movement(false, -24.0f);

                if (e.KeyData == Keys.F)
                    SFCamera.Movement(true, -24.0f);

                if (e.KeyData == Keys.H)
                    SFCamera.Movement(true, 24.0f);

            }
        }

        private void glDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse.IsClicked = true;
            Mouse.X = e.X;
            Mouse.Y = e.Y;
        }

        private void glDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse.IsClicked = false;
        }

        private void glDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.IsClicked)
            {
                SFCamera.MouseMove(e.X, e.Y);
            }
        }

        //int gl_FinishScene()
        //{
        //    //#ifdef WIN32
        //    SwapBuffers(hDC); return 1;
        //    //#else
        //    //glXSwapBuffers(dpy, win); return EXIT_SUCCESS;
        //    //#endif
        //}

        #endregion
    }
}
