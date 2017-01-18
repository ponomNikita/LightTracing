using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render.Primitives
{
    public class Triangle : IPrimitive
    {
        public Vector3 V0 { get; set; }
        public Vector3 V1 { get; set; }
        public Vector3 V2 { get; set; }
        public EPrimitiveType Type { get { return EPrimitiveType.Triangle; } }
        public Material Material { get; set; }

        private Vector3 _normal;

        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2, Material material)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;

            _normal = GetNormal(v0, v1, v2);

            Material = material;
        }

        public Triangle(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            V0 = v0;
            V1 = v1;
            V2 = v2;

            _normal = GetNormal(v0, v1, v2);
        }
        public bool FindIntersection(ref Ray ray)
        {
            Vector3 u, v, n;
            Vector3 w0, w;
            float r, a, b;

            u = V1 - V0;
            v = V2 - V0;
            n = Vector3.Cross(u, v);

            w0 = ray.Origin - V0;

            a = -Vector3.Dot(n, w0);
            b = Vector3.Dot(n, ray.Direction);

            if (Math.Abs(b) < Constants.Eps)
                return false;

            r = a / b;

            if (r < 0.0f)
                return false;

            var p = ray.Origin + (ray.Direction * r);

            float uu, uv, vv, wu, wv, D;
            uu = Vector3.Dot(u, u);
            uv = Vector3.Dot(u, v);
            vv = Vector3.Dot(v, v);

            w = p - V0;
            wu = Vector3.Dot(w, u);
            wv = Vector3.Dot(w, v);
            D = uv * uv - uu * vv;

            float s, t;
            s = (uv * wv - vv * wu) / D;
            if (s < 0.0f || s > 1.0f)
                return false;
            t = (uv * wu - uu * wv) / D;
            if (t < 0.0f || (s + t) > 1.0f)
                return false;

            ray.LastIntersectDistance = (p - ray.Origin).Length();

            return true;
        }

        public Vector3 GetNormalAtPoint(Vector3 point)
        {
            return _normal;
        }

        private Vector3 GetNormal(Vector3 V0, Vector3 V1, Vector3 V2)
        {
            Vector3 u = V1 - V0;
            Vector3 v = V2 - V0;
            return Vector3.Cross(u, v);
        }

        public bool HasPoint(Vector3 point)
        {
            var normal = Vector3.Cross(V0 - V1, V2 - V1);
            var scalarMult = Vector3.Dot(normal, point - V1);
            var result = Math.Abs(scalarMult) < 0.00001;
            return result;
        }
    }
}
