using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Render;
using Plane = Render.Primitives.Plane;

namespace LightTracing
{
    public class Camera
    {
        public Vector3 Eye { get; set; }
        public Vector3 Forvard { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public Plane Screen { get; set; }

        private readonly float _imageHeightDivScreenHeight;
        private readonly float _imageWidthDivScreenWidth;

        public Camera(Vector3 eye, Vector3 forvard, int screenWidth, int screenHeight, Plane screen)
        {
            if (eye == null || forvard == null || screen == null)
            {
                throw new ArgumentException("eye, forvard or screen is null");
            }

            Eye = eye;
            Forvard = forvard;
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            Screen = screen;

            _imageWidthDivScreenWidth = screenWidth / Math.Abs(screen.TriangleB.V0.X - screen.TriangleB.V1.X);
            _imageHeightDivScreenHeight = screenHeight / Math.Abs(screen.TriangleA.V0.Z - screen.TriangleA.V1.Z);
        }

        public int GetHitIndex(Vector3 intersectPoint)
        {
            int index = Constants.OutOfRangeIndex;

            int imageCol = (int)((intersectPoint.X - Screen.TriangleA.V0.X) * _imageWidthDivScreenWidth);
            int imageRow = (int)((intersectPoint.Z - Screen.TriangleA.V0.Z) * _imageHeightDivScreenHeight);

            imageCol = Math.Max(0, imageCol);
            imageCol = Math.Min(ScreenWidth - 1, imageCol);

            imageRow = Math.Max(0, imageRow);
            imageRow = Math.Min(ScreenHeight - 1, imageRow);

            index = imageRow * ScreenWidth + imageCol;

            return index;
        }
    }
}
