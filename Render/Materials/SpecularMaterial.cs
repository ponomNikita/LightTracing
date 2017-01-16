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
        private Color _diffuse;
        private Color _specular;

        public override MaterialType Type { get { return MaterialType.Specular; } }

        public SpecularMaterial(Color diffuse, Color specular)
        {
            _diffuse = diffuse;
            _specular = specular;
        }
        public override Ray ReflectRay(Ray incomingRay, Vector3 normal)
        {
            Color oldColor = incomingRay.RayColor;
            Vector3 direction = Vector3.Reflect(incomingRay.Direction, normal);
            Ray outcommingRay = new Ray(incomingRay.IntersectPoint, direction);

            Color newColor = Color.FromArgb(
                oldColor.A,
                _diffuse.R + oldColor.R * _specular.R,
                _diffuse.G + oldColor.G * _specular.G,
                _diffuse.B + oldColor.B * _specular.B);


            outcommingRay.RayColor = newColor;
            outcommingRay._intersectionCount = incomingRay._intersectionCount;
            return outcommingRay;
        }
    }
}
