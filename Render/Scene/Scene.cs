using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public EventHandler OnRayCastEventHandler;

        public int Percent { get; set; }

        public int RaysCount { get; set; }

        public void OnPercentChange()
        {
            var handler = OnRayCastEventHandler;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public Scene()
        {
            RaysCount = Constants.RaysCount;
        }


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



            DetermineCycle(ref colors, lightSourceColor);
            //RandomCycle(ref colors, lightSourceColor);
        }

        private void DetermineCycle(ref Color[] colors, Color lightSourceColor)
        {
            int cycleSize = (int)Math.Sqrt(Constants.RaysCount);
            float step = 10.0f / cycleSize;

            int raysInPercent = RaysCount / 100;

            int counter = 0;

            for (float i = 0; i < 10.0f; i = i + step)
            {
                for (float j = 0; j < 10.0f; j = j + step)
                {
                    counter++;
                    if (counter % raysInPercent == 0)
                    {
                        Percent = counter / raysInPercent;
                        OnPercentChange();
                    }

                    Vector3 direction = GetDirection(LightSource, i, j);
                    Ray ray = new Ray(new Vector3(LightSource.Position.X, LightSource.Position.Y, LightSource.Position.Z), direction)
                    {
                        RayColor = lightSourceColor
                    };

                    ray.Cast(Primitives, Camera, LightSource, ref colors, 0);
                }
            }
        }
        private void RandomCycle(ref Color[] colors, Color lightSourceColor)
        {
            int raysInPercent = RaysCount / 100;
            for (int i = 0; i < Constants.RaysCount; i++)
            {
                if (i % raysInPercent == 0)
                {
                    Percent = i / raysInPercent;
                    OnPercentChange();
                }

                Vector3 direction = GetRandomDirection(10);
                Ray ray = new Ray(new Vector3(LightSource.Position.X, LightSource.Position.Y, LightSource.Position.Z), direction)
                {
                    RayColor = lightSourceColor
                };

                ray.Cast(Primitives, Camera, LightSource, ref colors, 0);
            }
        }

        private Vector3 GetDirection(LightPoint lp, float i, float j)
        {
            var aim = new Vector3(i, j, 9.5f);
            return aim - lp.Position;
        }
        private Vector3 GetRandomDirection(float multiplier)
        {
            var random = new Random();
            Vector3 result = new Vector3((float)(random.NextDouble()) * multiplier, (float)(random.NextDouble()) * multiplier, -1);

            return result;
        }
    }
}
