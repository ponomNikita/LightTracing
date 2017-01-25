using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LightTracing;

namespace Render.Materials
{
    public class FloorMaterial : DiffuseMaterial
    {
        public FloorMaterial(Color color, float kA, float kD, float kS, float p) : base(color, kA, kD, kS, p)
        {
        }

        public FloorMaterial(Color color) : base(color)
        {
        }

        public override Color GetColorAtPoint(Vector3 point)
        {
            Color result;
            float cellSize = Constants.RoomSize/Constants.FloorCellCount;

            if ((int)(point.X / cellSize) % 2 == 0 && (int)(point.Y / cellSize) % 2 == 0
                || ((int)(point.X / cellSize) % 2 != 0 && (int)(point.Y / cellSize) % 2 != 0))
            {
                result = Color.Aqua;
            }
            else
            {
                result = Color.Red;
            }

            return result;
        }
    }
}
