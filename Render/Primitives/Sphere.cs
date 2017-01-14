using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render.Primitives
{
    public class Sphere : IPrimitive
    {
        public Vector3 Center { get; set; }
        public float Radius { get; set; } 
        public EPrimitiveType Type { get{ return EPrimitiveType.Sphere; } }
        public Material Material { get; set; }

        public Sphere(Vector3 center, float radius)
        {
            if (center == null)
            {
                throw new ArgumentException("center is null");
            }
            Center = center;
            Radius = radius;
        }
    }
}
