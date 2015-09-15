using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.OpenGL
{
    public interface IGLRenderable
    {
        float GL_X { get; }
        float GL_Y { get; }
        float GL_Z { get; }
        float GL_XRot { get; }
        float GL_YRot { get; }
        float GL_ZRot { get; }
        float GL_XScale { get; }
        float GL_YScale { get; }
        float GL_ZScale { get; }

        int GL_DisplayListIndex { get; }
    }
}
