using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Render;
using Render.Primitives;
using Plane = Render.Primitives.Plane;


namespace LightTracing
{
    public class Scene
    {
        public List<IPrimitive> Primitives { get; set; }
        public Camera Camera { get; set; }
        public LightPoint LightSource { get; set; }

        public void Render(out Color[] colors, Color lightSourceColor)
        {
            if (Primitives == null || Camera == null || LightSource == null)
            {
                throw new ArgumentException();
            }


            #region Color init

            colors = new Color[Camera.ScreenHeight * Camera.ScreenWidth];
            for (int i = 0; i < Camera.ScreenWidth * Camera.ScreenHeight; i++)
            {
                colors[i] = Color.Black;
            }

            #endregion

            int maxIndex = Camera.ScreenHeight*Camera.ScreenWidth;

            for (int i = 0; i < Constants.RaysCount; i++)
            {
                Vector3 direction = GetRandomDirection(1);
                Ray ray = new Ray(new Vector3(LightSource.Position.X, LightSource.Position.Y, LightSource.Position.Z), direction)
                {
                    RayColor = lightSourceColor
                };

                if (i % 100 == 0)
                    Console.WriteLine("ray {0}", i);

                int index;
                Color color = ray.Cast(Primitives, Camera, LightSource, out index);

                if (index != Constants.OutOfRangeIndex && index < maxIndex)
                    colors[index] = color;
            }
        }

        private Vector3 GetRandomDirection(float multiplier)
        {
            var random = new Random();
            Vector3 result = new Vector3((float)(random.NextDouble()) * multiplier, (float)(random.NextDouble()) * multiplier, -1);

            return result;
        }
    }
}
