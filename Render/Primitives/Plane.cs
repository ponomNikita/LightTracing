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

        public Material Material
        {
            get { return TriangleA.Material; }
            set
            {
                TriangleA.Material = value;
                TriangleB.Material = value;
            }
        }

        public Triangle TriangleA { get; set; }
        public Triangle TriangleB { get; set; }

        public bool FindIntersection(ref Ray ray)
        {
            return TriangleA.FindIntersection(ref ray) || TriangleB.FindIntersection(ref ray);
        }

        public Vector3 GetNormalAtPoint(Vector3 point)
        {
            return TriangleA.GetNormalAtPoint(point);
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

        public Plane(Vector3 leftDown, Vector3 leftTop, Vector3 rightTop, Vector3 rightDown, Material material)
        {
            TriangleA = new Triangle(leftDown, leftTop, rightTop, material);
            TriangleB = new Triangle(leftDown, rightTop, rightDown, material);
        }
    }
}
