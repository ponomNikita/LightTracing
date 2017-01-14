using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Render;
using Render.Primitives;


namespace LightTracing
{
    public class Scene
    {
        public List<IPrimitive> Primitives { get; set; }
        public Camera Camera { get; set; }
        public LightPoint LightSource { get; set; }

        public void Render(out Color[] colors)
        {
            colors = new Color[Camera.ScreenHeight * Camera.ScreenWidth];

            Ray ray = new Ray()
            {
                Direction = new Vector3(),
                Origin = new Vector3(LightSource.Position.X, LightSource.Position.Y, LightSource.Position.Z),
                RayColor = Color.Blue
            };

            int index;
            Color color = ray.Cast(Primitives, Camera, LightSource, out index);

            if (index != Constants.OutOfRangeIndex)
                colors[index] = color;

        }
    }
}
