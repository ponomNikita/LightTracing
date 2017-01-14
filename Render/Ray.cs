using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LightTracing;
using Render.Primitives;

namespace Render
{
    public class Ray
    {
        public Vector3 Direction { get; set; }

        public Vector3 Origin { get; set; }

        public Vector3 IntersectPoint { get; set; } 

        public float LastIntersectDistance { get; set; }

        public float ClosestIntersectDistance { get; set; }

        public Color RayColor { get; set; }

        public IPrimitive IntersectPrimative { get; set; }

        private int _intersectionCount;

        public Ray()
        {
            _intersectionCount = 0;
        }

        public Color Cast(List<IPrimitive> primitives, Camera camera, LightPoint lightSource, out int index)
        {
            _intersectionCount++;
            index = Constants.OutOfRangeIndex;

            if (_intersectionCount > Constants.MaxCastDepth)
                return RayColor;

            FindIntersection(primitives);

            Ray rayToCamera = new Ray()
            {
                Origin = this.IntersectPoint,
                Direction = camera.Eye - this.IntersectPoint,
                RayColor = this.RayColor
            };

            if (IsVisible(rayToCamera, primitives))
            {
                
            }

            return RayColor;
        }


        private void FindIntersection(List<IPrimitive> primitives)
        {
            this.ClosestIntersectDistance = float.MaxValue;
            this.LastIntersectDistance = float.MaxValue;
            foreach (IPrimitive item in primitives)
            {
                Ray ray = this;
                if (item.FindIntersection(ref ray))
                    IntersectPrimative = item;

                this.ClosestIntersectDistance = ray.LastIntersectDistance < this.ClosestIntersectDistance
                    ? ray.LastIntersectDistance
                    : this.ClosestIntersectDistance;
            }

            this.IntersectPoint = this.Origin + this.Direction * this.ClosestIntersectDistance;
        }

        private bool IsVisible(Ray ray, List<IPrimitive> primitives)
        {
            ray.FindIntersection(primitives);

            if (ray.IntersectPrimative != null)
                return true;

            return false;
        }
    }
}
