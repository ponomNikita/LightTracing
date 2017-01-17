using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    }
}
