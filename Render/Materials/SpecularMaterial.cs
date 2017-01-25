using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Render
{
    public class SpecularMaterial : Material
    {
        public override MaterialType Type
        {
            get { return MaterialType.Specular; }
        }

        public SpecularMaterial(Color color, float kA, float kD, float kS, float p) 
            : base(color, kA, kD, kS, p)
        {
        }

        public SpecularMaterial(Color color) 
            : base(color)
        {
            _kA = 0.6f;
            _kD = 0.9f;
            _kS = 0.4f;
            _p = 6.0f;

            _reflectionCoef = 0.5f;
            _refractionCoef = 1.0f;
        }
    }
}
