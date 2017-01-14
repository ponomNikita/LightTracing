using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LightTracing;
using Render.Primitives;

namespace Render
{
    public class Ray
    {
        public Vector3 Direction { get; set; }

        public Vector3 Origin { get; set; }

        private int _intersectionCount;

        public Ray()
        {
            _intersectionCount = 0;
        }

        public Color Cast(List<IPrimitive> primitives, Camera camera, LightPoint lightSource, out int index)
        {
            _intersectionCount++;

            index = Constants.OutOfRangeIndex;
            return new Color();
        }
    }
}
