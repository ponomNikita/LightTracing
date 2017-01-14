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
        public Vector3 LeftDown { get; set; }
        public Vector3 RightTop { get; set; }
        public EPrimitiveType Type { get { return EPrimitiveType.Plane; } }
        public Material Material { get; set; }
        public bool FindIntersection(ref Ray ray)
        {
            return false;
        }

        public Plane(Vector3 leftDown, Vector3 rightTop)
        {
            if (leftDown == null || rightTop == null)
            {
                throw new ArgumentException("leftDown or rightTop is null");
            }

            LeftDown = leftDown;
            RightTop = rightTop;
        }
    }
}
