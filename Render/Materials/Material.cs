using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render
{
    public class Material
    {
        public Color Color { get; set; }

        public virtual MaterialType Type { get{ return MaterialType.Deffuse;} }
        public virtual Ray ReflectRay(Ray incomingRay, Vector3 normal)
        {
            Color oldColor = incomingRay.RayColor;
            Vector3 direction = Vector3.Reflect(incomingRay.Direction, normal);
            Ray outcommingRay = new Ray(incomingRay.IntersectPoint, direction);

            outcommingRay.RayColor = oldColor;
            outcommingRay._intersectionCount = incomingRay._intersectionCount;
            return outcommingRay;
        }
    }
}
