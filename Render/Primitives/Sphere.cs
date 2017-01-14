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
        public bool FindIntersection(ref Ray ray)
        {
            var dirToSphere = Center - ray.Origin;

            var v = Vector3.Dot(dirToSphere, ray.Direction); 

            var hitDistance = v * v - Vector3.Dot(dirToSphere, dirToSphere) + Radius * Radius;

            if (hitDistance < 0.0f)
                return false;

            hitDistance = (float)Math.Sqrt(hitDistance);

            var hD0 = v - hitDistance;
            var hD1 = v + hitDistance;

            if (hD0 > Constants.Eps)
                hitDistance = hD0;
            else if (hD1 > Constants.Eps)
                hitDistance = hD1;
            else
                hitDistance = 0.0f;

            ray.LastIntersectDistance = hitDistance;

            return true;
        }

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
