using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render.Primitives
{
    public class Tetrahedron : IPrimitive
    {
        public Triangle TriangleA { get; set; }
        public Triangle TriangleB { get; set; }
        public Triangle TriangleC { get; set; }
        public Triangle TriangleD { get; set; }

        public EPrimitiveType Type { get{ return EPrimitiveType.Tetrahedron;} }
        public Material Material { get; set; }
        public bool FindIntersection(ref Ray ray)
        {
            return TriangleA.FindIntersection(ref ray)
                || TriangleB.FindIntersection(ref ray)
                || TriangleC.FindIntersection(ref ray)
                || TriangleD.FindIntersection(ref ray);
        }

        public Tetrahedron(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Material material)
        {
            TriangleC = new Triangle(d, a, b, material);
            TriangleB = new Triangle(d, a, c, material);
            TriangleA = new Triangle(d, c, b, material);
            TriangleD = new Triangle(a, b, c, material);
            Material = material;
        }

        public Vector3 GetNormalAtPoint(Vector3 point)
        {
            if (TriangleA.HasPoint(point))
                return TriangleA.GetNormalAtPoint(point);

            if (TriangleB.HasPoint(point))
                return TriangleB.GetNormalAtPoint(point);

            if (TriangleC.HasPoint(point))
                return TriangleC.GetNormalAtPoint(point);

            return Vector3.Zero;
        }
    }
}
