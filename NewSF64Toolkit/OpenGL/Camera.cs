﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSF64Toolkit.OpenGL
{
    public class SFCamera
    {
        public delegate void UpdateCameraEvent();
        public UpdateCameraEvent UpdateCamera;

        public float AngleX, AngleY;
        public float X, Y, Z;
        public float LX, LY, LZ;

        public SFCamera()
        {
            Reset();
        }
        
        public void Reset()
        {
            AngleX = 0;
            AngleY = 0;
            X = 0;
            Y = 1.5f;
            Z = 5.0f;
            LX = 0;
            LY = 0;
            LZ = -1.0f;
        }

        public void Orientation(float angle, float angle2)
        {
            LX = (float)Math.Sin(angle);
            LY = angle2;
            LZ = (float)-Math.Cos(angle);
        }

        public void Movement(bool strafe, float speed)
        {
            if (!strafe)
            {
                X += LX * 0.025f * speed;
                Y += LY * 0.025f * speed;
                Z += LZ * 0.025f * speed;
            }
            else
            {
                X += (float)Math.Cos(AngleX) * (0.025f * speed);
                Z += (float)Math.Sin(AngleX) * (0.025f * speed);
            }
            UpdateCamera();
        }

        public void MouseMove(int x, int y)
        {
            AngleX += (0.01f * (x - Mouse.X));
            AngleY -= (0.01f * (y - Mouse.Y));

            Orientation(AngleX, AngleY);

            Mouse.X = x;
            Mouse.Y = y;

            UpdateCamera();
        }

        public void MoveCameraTo(float x, float y, float z)
        {
            X = x * 0.004f;
            Y = y * 0.004f;
            Z = z * 0.004f;

            //Step backwards
            Movement(false, -200);
        }

    }
}
