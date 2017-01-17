using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LightTracing;

namespace Render
{
    public class Material
    {
        public Color Color { get; set; }

        public float _kA;      // коэффициент фонового освещения
        public float _kD;      // коэффициент диффузного освещения
        public float _kS;      // коэффициент зеркального блика
        public float _p;        // коэффициент резкости бликов

        public float _reflectionCoef = 1;
        public float _refractionCoef = 1;

        public virtual MaterialType Type{ get { return MaterialType.Deffuse; } }

        public Material(Color color, float kA, float kD, float kS, float p)
        {
            Color = color;
            _kA = kA;
            _kD = kD;
            _kS = kS;
            _p = p;
        }

        public Material(Color color)
        {
            Color = color;
        }

        public virtual Color CulcColorByPhong(Camera camera, LightPoint lightpoint, Vector3 normal, Vector3 intersectionPoint, Color rayColor)
        {
            Color currColor;
            if (Type != MaterialType.Mirror)
            {
                currColor = Color;
            }
            else
            {
                currColor = rayColor;
            }

            Vector3 light = Vector3.Normalize(lightpoint.Position - intersectionPoint);
            Vector3 view = Vector3.Normalize(camera.Eye - intersectionPoint);
            Vector3 reflected = Vector3.Normalize(Vector3.Reflect(-view, normal));

            float diffuse = Math.Max(0, Vector3.Dot(light, normal));
            float specular = (float)Math.Pow(Math.Max(0, Vector3.Dot(reflected, light)), _p);
            float resultKoof = (_kA + _kD * diffuse) + _kS * specular;

            float R = currColor.R * resultKoof;
            float G = currColor.G * resultKoof;
            float B = currColor.B * resultKoof;

            Color resultColor = Color.FromArgb(
                Math.Max(0, Math.Min(255, (int)R)),
                Math.Max(0, Math.Min(255, (int)G)),
                Math.Max(0, Math.Min(255, (int)B))
                );

            return resultColor;
        }
    }
}
