using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LightTracing
{
    public class Camera
    {
        public Vector3 Eye { get; set; }
        public Vector3 Forvard { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public Camera(Vector3 eye, Vector3 forvard, int screenWidth, int screenHeight)
        {
            if (eye == null || forvard == null)
            {
                throw new ArgumentException("eye or forvard is null");
            }
            Eye = eye;
            Forvard = forvard;
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
        }
    }
}
