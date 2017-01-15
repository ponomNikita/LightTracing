using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render.Primitives
{
    public class Plane : IPrimitive
    {
        public EPrimitiveType Type { get { return EPrimitiveType.Plane; } }
        public Material Material { get; set; }

        public Triangle TriangleA { get; set; }
        public Triangle TriangleB { get; set; }

        public bool FindIntersection(ref Ray ray)
        {
            return TriangleA.FindIntersection(ref ray) || TriangleB.FindIntersection(ref ray);
        }

        public Plane(Triangle triangleA, Triangle triangleB, Material material)
        {
            if (triangleA == null || triangleB == null)
            {
                throw new ArgumentException("triangleA or triangleB is null");
            }

            Material = material;

            TriangleA = triangleA;
            TriangleB = triangleB;
        }
    }
}
