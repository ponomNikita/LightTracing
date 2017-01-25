using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render.Materials
{
    public class DiffuseMaterial : Material
    {
        public override MaterialType Type
        {
            get { return MaterialType.Deffuse; }
        }
        public DiffuseMaterial(Color color, float kA, float kD, float kS, float p)
            : base(color, kA, kD, kS, p)
        {
        }

        public DiffuseMaterial(Color color)
            : base(color)
        {
            _kA = 0.4f;
            _kD = 0.9f;
            _kS = 0.8f;
            _p = 2.0f;

            _reflectionCoef = 0.5f;
            _refractionCoef = 1.0f;
        }

        public override Vector3 GetReflectDirection(Vector3 oldDirection, Vector3 normalAtPoint)
        {
            var random  = new Random(DateTime.Now.Millisecond);

            return MapSampleToCosineDistribution((float)random.NextDouble(), (float)random.NextDouble());
        }

        public static Vector3 MapSampleToCosineDistribution(float r1, float r2)
        {
            var sinPhi = (float)Math.Sin(2 * r1 * Math.PI);
            var cosPhi = (float)Math.Cos(2 * r1 * Math.PI);

            var cosTheta = (float)Math.Pow(1 - r2, 0.5);
            var sinTheta = (float)Math.Sqrt(1 - cosTheta * cosTheta);

            var x = sinTheta * cosPhi;
            var y = sinTheta * sinPhi;
            var z = cosTheta;

            return new Vector3(x, y, z);
        }
    }
}
