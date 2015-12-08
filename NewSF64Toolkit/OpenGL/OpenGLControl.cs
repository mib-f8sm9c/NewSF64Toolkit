using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using NewSF64Toolkit.OpenGL.F3DEX;
using NewSF64Toolkit.DataStructures;
using NewSF64Toolkit.DataStructures.DMA;
using NewSF64Toolkit.Settings;
using NewSF64Toolkit.DataStructures.DataObjects;

namespace NewSF64Toolkit.OpenGL
{
    public partial class OpenGLControl : UserControl
    {
        List<IGLRenderable> Renderables;

        public bool GLLoaded { get; private set; }

        public float[] LightAmbient = new float[4]; //4
        public float[] LightDiffuse = new float[4]; //4
        public float[] LightSpecular = new float[4]; //4
        public float[] LightPosition = new float[4]; //4

        public static uint ChangedModes;
        public static uint GeometryMode;
        public static uint OtherModeL;
        public static uint OtherModeH;
        public static uint Store_RDPHalf1, Store_RDPHalf2;
        public static uint Combiner0, Combiner1;

        public List<SFLevelObject> LevelObjects;
        public int SelectedObjectIndex;

        public int[] SingleObjectDLIndices;

        public SFCamera Camera;

        public enum DisplayMode
        {
            LevelView,
            SingleModelView
        }
        public DisplayMode Mode;

        public OpenGLControl()
        {
            InitializeComponent();

            Renderables = new List<IGLRenderable>();

            Mode = DisplayMode.LevelView;

            Camera = new SFCamera();
            Camera.UpdateCamera += UpdateCamera;
        }

        public void UpdateCamera()
        {
            ReDraww();
        }

        private void glDisplay_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Camera.Reset();

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
                LightAmbient[i] = 1.0f;
                LightDiffuse[i] = 1.0f;
                LightSpecular[i] = 1.0f;
                LightPosition[i] = 1.0f;
            }

	        GL.Light(LightName.Light0, LightParameter.Ambient, LightAmbient);
	        GL.Light(LightName.Light0, LightParameter.Diffuse, LightDiffuse);
	        GL.Light(LightName.Light0, LightParameter.Specular, LightSpecular);
	        GL.Light(LightName.Light0, LightParameter.Position, LightPosition);
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
        //    F3DEXParser.OpenGlSettings.IsExtUnsupported = false;

        //    F3DEXParser.OpenGlSettings.ExtensionList = strdup((const char*)glGetString(GL_EXTENSIONS));
        //    int i;
        //    for(i = 0; i < strlen(F3DEXParser.OpenGlSettings.ExtensionList); i++) {
        //        if(F3DEXParser.OpenGlSettings.ExtensionList[i] == ' ') F3DEXParser.OpenGlSettings.ExtensionList[i] = '\n';
        //    }

        //    if(strstr(F3DEXParser.OpenGlSettings.ExtensionList, "GL_ARB_texture_mirrored_repeat")) {
        //        F3DEXParser.OpenGlSettings.Ext_TexMirroredRepeat = true;
        //        sprintf(F3DEXParser.OpenGlSettings.ExtSupported, "%sGL_ARB_texture_mirrored_repeat\n", F3DEXParser.OpenGlSettings.ExtSupported);
        //    } else {
        //        F3DEXParser.OpenGlSettings.IsExtUnsupported = true;
        //        F3DEXParser.OpenGlSettings.Ext_TexMirroredRepeat = false;
        //        sprintf(F3DEXParser.OpenGlSettings.ExtUnsupported, "%sGL_ARB_texture_mirrored_repeat\n", F3DEXParser.OpenGlSettings.ExtUnsupported);
        //    }

        //    if(strstr(F3DEXParser.OpenGlSettings.ExtensionList, "GL_ARB_multitexture")) {
        //        F3DEXParser.OpenGlSettings.Ext_MultiTexture = true;

        //        glMultiTexCoord1fARB		= (PFNGLMULTITEXCOORD1FARBPROC) wglGetProcAddress("glMultiTexCoord1fARB");
        //        glMultiTexCoord2fARB		= (PFNGLMULTITEXCOORD2FARBPROC) wglGetProcAddress("glMultiTexCoord2fARB");
        //        glMultiTexCoord3fARB		= (PFNGLMULTITEXCOORD3FARBPROC) wglGetProcAddress("glMultiTexCoord3fARB");
        //        glMultiTexCoord4fARB		= (PFNGLMULTITEXCOORD4FARBPROC) wglGetProcAddress("glMultiTexCoord4fARB");
        //        glActiveTextureARB			= (PFNGLACTIVETEXTUREARBPROC) wglGetProcAddress("glActiveTextureARB");
        //        glClientActiveTextureARB	= (PFNGLCLIENTACTIVETEXTUREARBPROC) wglGetProcAddress("glClientActiveTextureARB");

        //        sprintf(F3DEXParser.OpenGlSettings.ExtSupported, "%sGL_ARB_multitexture\n", F3DEXParser.OpenGlSettings.ExtSupported);
        //    } else {
        //        F3DEXParser.OpenGlSettings.IsExtUnsupported = true;
        //        F3DEXParser.OpenGlSettings.Ext_MultiTexture = false;
        //        sprintf(F3DEXParser.OpenGlSettings.ExtUnsupported, "%sGL_ARB_multitexture\n", F3DEXParser.OpenGlSettings.ExtUnsupported);
        //    }

        //    if(strstr(F3DEXParser.OpenGlSettings.ExtensionList, "GL_ARB_fragment_program")) {
        //        F3DEXParser.OpenGlSettings.Ext_FragmentProgram = true;

        //        glGenProgramsARB				= (PFNGLGENPROGRAMSARBPROC) wglGetProcAddress("glGenProgramsARB");
        //        glBindProgramARB				= (PFNGLBINDPROGRAMARBPROC) wglGetProcAddress("glBindProgramARB");
        //        glDeleteProgramsARB				= (PFNGLDELETEPROGRAMSARBPROC) wglGetProcAddress("glDeleteProgramsARB");
        //        glProgramStringARB				= (PFNGLPROGRAMSTRINGARBPROC) wglGetProcAddress("glProgramStringARB");
        //        glProgramEnvParameter4fARB		= (PFNGLPROGRAMENVPARAMETER4FARBPROC) wglGetProcAddress("glProgramEnvParameter4fARB");
        //        glProgramLocalParameter4fARB	= (PFNGLPROGRAMLOCALPARAMETER4FARBPROC) wglGetProcAddress("glProgramLocalParameter4fARB");

        //        sprintf(F3DEXParser.OpenGlSettings.ExtSupported, "%sGL_ARB_fragment_program\n", F3DEXParser.OpenGlSettings.ExtSupported);
        //    } else {
        //        F3DEXParser.OpenGlSettings.IsExtUnsupported = true;
        //        F3DEXParser.OpenGlSettings.Ext_FragmentProgram = false;
        //        sprintf(F3DEXParser.OpenGlSettings.ExtUnsupported, "%sGL_ARB_fragment_program\n", F3DEXParser.OpenGlSettings.ExtUnsupported);
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

            gl_LookAt(Camera.X, Camera.Y, Camera.Z, Camera.X + Camera.LX, Camera.Y + Camera.LY, Camera.Z + Camera.LZ);

	        GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Lighting);

            if (Mode == DisplayMode.LevelView)
            {
                /*GL.Color3(0.5f, 0.5f, 0.5f);
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, 1);

                GL.Begin(PrimitiveType.Quads);
                GL.Vertex3(-12.0f, -0.01f, -1000.0f);
                GL.Vertex3(-12.0f, -0.01f, 10.0f);
                GL.Vertex3(12.0f, -0.01f, 10.0f);
                GL.Vertex3(12.0f, -0.01f, -1000.0f);
                GL.End();*/
            }

            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Scale(0.004f, 0.004f, 0.004f);

	        //int ObjectNo = 0;
            //while (ObjectNo < F3DEXParser.GameObjCount)
            if(Mode == DisplayMode.LevelView)
            {
                IGLRenderable renderable;

                if (LevelObjects == null || LevelObjects.Count == 0)
                    return;

                for (int i = 0; i < LevelObjects.Count; i++)
                {
                    //F3DEXParser.GameObject gameObject = F3DEXParser.GameObjects[ObjectNo];
                    renderable = LevelObjects[i];

                    if (Math.Abs(Camera.Z * 250 - renderable.GL_Z) > 30000)
                    {
                        continue;
                    }

                    if (ToolSettings.Instance.UseWireframe)
                    {
                        GL.PushMatrix();
                        if (i == SelectedObjectIndex)
                        {
                            GL.Color3(0.0f, 1.0f, 0.0f);
                        }
                        else
                        {
                            GL.Color3(1.0f, 1.0f, 1.0f);
                        }

                        GL.Disable(EnableCap.Lighting);

                        GL.Translate((float)renderable.GL_X, (float)renderable.GL_Y, (float)renderable.GL_Z);
                        GL.Rotate((float)renderable.GL_XRot, 1.0f, 0, 0);
                        GL.Rotate((float)renderable.GL_YRot, 0, 1.0f, 0);
                        GL.Rotate((float)renderable.GL_ZRot, 0, 0, 1.0f);

                        GL.CallList(renderable.GL_DisplayListIndex[2]);//F3DEXParser.WireframeGameObjectDListIndices[renderable.DListOffset]);

                        GL.Enable(EnableCap.Lighting);

                        GL.PopMatrix();
                    }
                    else
                    {
                        if (i == SelectedObjectIndex)
                        {
                            GL.PushMatrix();

                            GL.Translate((float)renderable.GL_X, (float)renderable.GL_Y, (float)renderable.GL_Z);
                            GL.Rotate((float)renderable.GL_XRot, 1.0f, 0, 0);
                            GL.Rotate((float)renderable.GL_YRot, 0, 1.0f, 0);
                            GL.Rotate((float)renderable.GL_ZRot, 0, 0, 1.0f);

                            GL.CallList(renderable.GL_DisplayListIndex[1]);//F3DEXParser.SelectedGameObjectDListIndices[renderable.DListOffset]);

                            GL.PopMatrix();
                        }
                        else
                        {
                            GL.PushMatrix();

                            GL.Translate((float)renderable.GL_X, (float)renderable.GL_Y, (float)renderable.GL_Z);
                            GL.Rotate((float)renderable.GL_XRot, 1.0f, 0, 0);
                            GL.Rotate((float)renderable.GL_YRot, 0, 1.0f, 0);
                            GL.Rotate((float)renderable.GL_ZRot, 0, 0, 1.0f);

                            GL.CallList(renderable.GL_DisplayListIndex[0]);//F3DEXParser.GameObjectDListIndices[gameObject.DListOffset]);

                            GL.PopMatrix();
                        }
                    }
                }
            }
            else if (Mode == DisplayMode.SingleModelView)
            {
                if (SingleObjectDLIndices == null)
                    return;

                GL.PushMatrix();

                if (ToolSettings.Instance.UseWireframe)
                {
                    GL.Color3(1.0f, 1.0f, 1.0f);
                    GL.CallList(SingleObjectDLIndices[2]);
                }
                else
                    GL.CallList(SingleObjectDLIndices[0]);


                GL.PopMatrix();
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
                    Camera.Movement(false, 6.0f);

                if (e.KeyData == Keys.S)
                    Camera.Movement(false, -6.0f);

                if (e.KeyData == Keys.A)
                    Camera.Movement(true, -6.0f);

                if (e.KeyData == Keys.D)
                    Camera.Movement(true, 6.0f);

                if (e.KeyData == Keys.T)
                    Camera.Movement(false, 24.0f);

                if (e.KeyData == Keys.G)
                    Camera.Movement(false, -24.0f);

                if (e.KeyData == Keys.F)
                    Camera.Movement(true, -24.0f);

                if (e.KeyData == Keys.H)
                    Camera.Movement(true, 24.0f);

            }
        }

        private void glDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse.IsClicked = true;
            Mouse.X = e.X;
            Mouse.Y = e.Y;

            #region Sample code online for ray-selecting, not functional at all

            //if (_mouseType == MouseType.Select)
            //{
            //    GL.
            //    var inverseWorldViewProjection = Matrix4.Invert(worldViewProjection)
            //    var rayStart = Vector3.Unproject(new Vector3(mouseX, mouseY, 0), viewportX, viewportY, viewportWidth, viewportHeight, viewportNearZ, viewportFarZ, inverseWorldViewProjection)
            //    var rayEnd = Vector3.Unproject(new Vector3(mouseX, mouseY, 1), viewportX, viewportY, viewportWidth, viewportHeight, viewportNearZ, viewportFarZ, inverseWorldViewProjection)
                
            //}


            //if (!GLLoaded) return; // Play nice   

            //int[] viewport = new int[4];
            //double[] modelViewMatrix = new double[16];
            //double[] projectionMatrix = new double[16];

            //if (true)//checkBoxSelectPoints.Checked == true)
            //{
            //    int mouseX = e.X;
            //    int mouseY = e.Y;

            //    //Get Matrix
            //    OpenTK.Graphics.OpenGL.GL.GetInteger(OpenTK.Graphics.OpenGL.GetPName.Viewport, viewport);
            //    OpenTK.Graphics.OpenGL.GL.GetDouble(OpenTK.Graphics.OpenGL.GetPName.ModelviewMatrix, modelViewMatrix);
            //    OpenTK.Graphics.OpenGL.GL.GetDouble(OpenTK.Graphics.OpenGL.GetPName.ProjectionMatrix, projectionMatrix);

            //    //Calculate NearPlane point and FarPlane point. One will get the two end points of a straight line
            //    //that "almost" intersects the plotted point you "clicked".
            //    Vector3 win = new Vector3(mouseX, viewport[3] - mouseY, -1.0f); //Set this to -1
            //    Vector3 worldPositionNear;
            //    OpenTK.Graphics.Glu.UnProject(win, modelViewMatrix, projectionMatrix, viewport, out worldPositionNear);
            //    win.Z = 1.0f;
            //    Vector3 worldPositionFar;
            //    OpenTK.Graphics.Glu.UnProject(win, modelViewMatrix, projectionMatrix, viewport, out worldPositionFar);
                

            //    //Calculate the lenght of the straigh line (the distance between both points).
            //    double distanceNF = Math.Sqrt(Math.Pow(worldPositionNear.X - worldPositionFar.X, 2) +
            //                                  Math.Pow(worldPositionNear.Y - worldPositionFar.Y, 2) +
            //                                  Math.Pow(worldPositionNear.Z - worldPositionFar.Z, 2));
            //    double minDist = distanceNF;


            //    //Calculate which of the plotted points is closest to the line. In other words,
            //    // look for the point you tried to select. Calculate the distance between the 2 endpoints that passes through
            //    // each plotted point. The one that is most similar with the straight line will be the selected point.
            //    int selectedPoint = 0;
            //    for (int i = 0; i < F3DEXParser.TestVertices.Length; i++)
            //    {
            //        double d1 = Math.Sqrt(Math.Pow(worldPositionNear.X - PointsInfo[i].Position.X, 2) +
            //                              Math.Pow(worldPositionNear.Y - PointsInfo[i].Position.Y, 2) +
            //                              Math.Pow(worldPositionNear.Z - PointsInfo[i].Position.Z, 2));

            //        double d2 = Math.Sqrt(Math.Pow(PointsInfo[i].Position.X - worldPositionFar.X, 2) +
            //                              Math.Pow(PointsInfo[i].Position.Y - worldPositionFar.Y, 2) +
            //                              Math.Pow(PointsInfo[i].Position.Z - worldPositionFar.Z, 2));

            //        if (((d1 + d2) - distanceNF) <= minDist)
            //        {
            //            minDist = (d1 + d2) - distanceNF;
            //            selectedPoint = i;
            //        }
            //    }

            //    //Just select/unselect points if the "click" was really close to a point. Not just by clicking anywhere in the screen
            //    if (minDist < 0.000065)
            //    {
            //        //if (selectedPoints.Contains(selectedPoint))
            //        //    selectedPoints.Remove(selectedPoint);
            //        //else
            //        //    selectedPoints.Add(selectedPoint);

            //        glDisplay.Invalidate();  //paint again 
            //    }
            //}

            #endregion
        }

        #region Sample code online for ray-selecting, not functional at all

        ////Projects a 3D vector from object space into screen space. Reference page contains links to related code samples.
        ////Parameters
        ////source
        ////The vector to project.
        ////projection
        ////The projection matrix.
        ////view
        ////The view matrix.
        ////world
        ////The world matrix.
 
        //public Vector3 Project(Vector3 source, Matrix3 projection, Matrix3 view, Matrix3 world)
        //{
        //    Quaternion matrix = Quaternion.Mult(Matrix3.Mult(world, view), projection);
        //    Vector3 vector = Vector3.Transform(source, matrix);
        //    float a = (((source.X * matrix.M14) + (source.Y * matrix.M24)) + (source.Z * matrix.M34)) + matrix.M44;
        //    if (!WithinEpsilon(a, 1f))
        //    {
        //        vector = (Vector3) (vector / a);
        //    }
        //    vector.X = (((vector.X + 1f) * 0.5f) * this.Width) + this.X;
        //    vector.Y = (((-vector.Y + 1f) * 0.5f) * this.Height) + this.Y;
        //    vector.Z = (vector.Z * (this.MaxDepth - this.MinDepth)) + this.MinDepth;
        //    return vector;
        //}
 
 
 
        ////Converts a screen space point into a corresponding point in world space.
        ////Parameters
        ////source
        ////The vector to project.
        ////projection
        ////The projection matrix.
        ////view
        ////The view matrix.
        ////world
        ////The world matrix.
        //public Vector3 Unproject(Vector3 source, Matrix projection, Matrix view, Matrix world)
        //{
        //    Matrix matrix = Matrix.Invert(Matrix.Multiply(Matrix.Multiply(world, view), projection));
        //    source.X = (((source.X - this.X) / ((float) this.Width)) * 2f) - 1f;
        //    source.Y = -((((source.Y - this.Y) / ((float) this.Height)) * 2f) - 1f);
        //    source.Z = (source.Z - this.MinDepth) / (this.MaxDepth - this.MinDepth);
        //    Vector3 vector = Vector3.Transform(source, matrix);
        //    float a = (((source.X * matrix.M14) + (source.Y * matrix.M24)) + (source.Z * matrix.M34)) + matrix.M44;
        //    if (!WithinEpsilon(a, 1f))
        //    {
        //        vector = (Vector3) (vector / a);
        //    }
        //    return vector;
        //}
 
        //private static bool WithinEpsilon(float a, float b)
        //{
        //    float num = a - b;
        //    return ((-1.401298E-45f <= num) && (num <= float.Epsilon));
        //}

        #endregion

        private void glDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse.IsClicked = false;
        }

        private void glDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.IsClicked)
            {
                Camera.MouseMove(e.X, e.Y);
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
