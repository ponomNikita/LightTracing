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

        public int _intersectionCount;

        public Ray()
        {
            _intersectionCount = 0;
        }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = Vector3.Normalize(direction);
            _intersectionCount = 0;
        }

        public Color Cast(List<IPrimitive> primitives, Camera camera, LightPoint lightSource, out int index)
        {
            _intersectionCount++;
            index = Constants.OutOfRangeIndex;

            if (_intersectionCount > Constants.MaxCastDepth)
                return RayColor;

            bool isIntersection = FindIntersection(primitives);
            if (!isIntersection)
            {
                return RayColor;
            }

            var nextRay = IntersectPrimative.Material.ReflectRay(this,
                IntersectPrimative.GetNormalAtPoint(IntersectPoint));

            Ray rayToCamera = new Ray(this.IntersectPoint, camera.Eye - this.IntersectPoint)
            {
                RayColor = nextRay.RayColor,
            };

            if (IsVisible(ref rayToCamera, primitives, camera))
            {
                index = camera.GetHitIndex(rayToCamera.IntersectPoint);
            }

            //Cast(primitives, camera, lightSource, out index);
            return IntersectPrimative.Material.Color;
        }


        private bool FindIntersection(List<IPrimitive> primitives)
        {
            this.ClosestIntersectDistance = float.MaxValue;
            this.LastIntersectDistance = float.MaxValue;
            bool isIntersection = false;
            foreach (IPrimitive item in primitives)
            {
                Ray ray = this;
                if (item.FindIntersection(ref ray))
                {
                    if (ray.LastIntersectDistance > Constants.Eps)
                    {
                        IntersectPrimative = item;
                        isIntersection = true;

                        this.ClosestIntersectDistance = ray.LastIntersectDistance < this.ClosestIntersectDistance
                            ? ray.LastIntersectDistance
                            : this.ClosestIntersectDistance;
                    }
                }
            }

            if(isIntersection)
                this.IntersectPoint = this.Origin + this.Direction * this.ClosestIntersectDistance;

            return isIntersection;
        }

        private bool IsVisible(ref Ray ray, List<IPrimitive> primitives, Camera camera)
        {
            Ray checkVisible = new Ray(ray.Origin, ray.Direction);
            checkVisible.FindIntersection(primitives);

            if (checkVisible.IntersectPrimative != null)
                return false;

            
            camera.Screen.FindIntersection(ref ray);
            ray.IntersectPoint = ray.Origin + ray.Direction * ray.LastIntersectDistance;

            return true;
        }
    }
}
