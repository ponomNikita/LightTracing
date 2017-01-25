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

        public Ray()
        {
        }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = Vector3.Normalize(direction);
        }

        public void Cast(List<IPrimitive> primitives, Camera camera, LightPoint lightSource, ref Color[] colors, int rayNumber)
        {
            if (rayNumber > Constants.MaxCastDepth)
            {
                return;
            }

            bool isIntersection = FindIntersection(primitives);
            if (isIntersection)
            {
                var normalAtPoint = IntersectPrimative.GetNormalAtPoint(IntersectPoint);

                Color resultColor = IntersectPrimative.Material.CulcColorByPhong(camera, lightSource, normalAtPoint,
                    IntersectPoint, RayColor);

                Ray rayToCamera = new Ray(this.IntersectPoint, camera.Eye - this.IntersectPoint)
                {
                    RayColor = resultColor,
                };

                if (IsVisible(ref rayToCamera, primitives, camera))
                {
                    int index = camera.GetHitIndex(rayToCamera.IntersectPoint);

                    if (index != Constants.OutOfRangeIndex && index < colors.Length)
                    {
                        colors[index] = resultColor;
                    }
                }

                var nextRay = new Ray(IntersectPoint,
                    IntersectPrimative.Material.GetReflectDirection(Direction, normalAtPoint))
                {
                    RayColor = resultColor
                };

                nextRay.Cast(primitives, camera, lightSource, ref colors, rayNumber + 1);
                }  
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
