using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render.Primitives
{
    public class Ceiling : IPrimitive
    {
        public Plane Plane1 { get; set; }
        public Plane Plane2 { get; set; }
        public Plane Plane3 { get; set; }
        public Plane Plane4 { get; set; }
        public EPrimitiveType Type { get { return EPrimitiveType.Ceiling; } }
        public Material Material { get; set; }

        public bool FindIntersection(ref Ray ray)
        {
            return Plane1.FindIntersection(ref ray) 
                || Plane2.FindIntersection(ref ray) 
                || Plane3.FindIntersection(ref ray) 
                || Plane4.FindIntersection(ref ray);
        }

        public Ceiling(Material material)
        {
            Material = material;
        }

        public Vector3 GetNormalAtPoint(Vector3 point)
        {
            return Plane1.GetNormalAtPoint(point);
        }
    }
}
