using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LightTracing;
using Render.Primitives;
using Plane = Render.Primitives.Plane;

namespace Render.Scene
{
    public static class DefaultBuilder
    {
        private static Camera BuildDefaultCamera(int imageWidth, int imageHeight)
        {
            Material screenMaterial = new Material();
            
            var leftDown = new Vector3(0, 0, 0);
            var leftTop = new Vector3(0, 0, 10);
            var rightTop = new Vector3(10, 0, 10);
            var rightDown = new Vector3(0, 0, 10);

            IPrimitive screen = new Plane(leftDown, leftTop, rightTop, rightDown, screenMaterial);

            Camera camera = new Camera(new Vector3(0, -1, 0.5f), new Vector3(0, 1, -0.5f), imageWidth, imageHeight,
                (Plane)screen);

            return camera;
        }

        public static LightTracing.Scene BuildScene(int imageWidth, int imageHeight)
        {
            var camera = BuildDefaultCamera(imageWidth, imageHeight);
            IPrimitive floor;
            IPrimitive leftWall;
            IPrimitive rightWall;
            IPrimitive backWall;
            IPrimitive ceiling;
            LightPoint lightSource;
            IPrimitive sphere;
            LightTracing.Scene scene;

            #region Build floor

            {
                var leftDown = new Vector3(0, 0, 0);
                var leftTop = new Vector3(0, 10, 0);
                var rightTop = new Vector3(10, 10, 0);
                var rightDown = new Vector3(10, 0, 0);

                Material material = new Material() {Color = Color.Aqua};
                floor = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build left wall

            {
                var leftDown = new Vector3(0, 0, 0);
                var leftTop = new Vector3(0, 0, 10);
                var rightTop = new Vector3(0, 10, 10);
                var rightDown = new Vector3(0, 10, 0);

                Material material = new Material() { Color = Color.Red };
                leftWall = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build right wall

            {
                var leftDown = new Vector3(10, 10, 0);
                var leftTop = new Vector3(10, 10, 10);
                var rightTop = new Vector3(10, 0, 10);
                var rightDown = new Vector3(10, 0, 0);

                Material material = new Material() { Color = Color.Red };
                rightWall = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build back wall

            {
                var leftDown = new Vector3(0, 10, 0);
                var leftTop = new Vector3(0, 10, 10);
                var rightTop = new Vector3(10, 10, 10);
                var rightDown = new Vector3(10, 10, 0);

                Material material = new Material() { Color = Color.GreenYellow };
                backWall = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build ceiling

            {
                var leftDown = new Vector3(0, 10, 10);
                var leftTop = new Vector3(0, 0, 10);
                var rightTop = new Vector3(10, 0, 10);
                var rightDown = new Vector3(10, 10, 10);

                Material material = new Material() { Color = Color.BlueViolet };
                ceiling = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build light source

            lightSource = new LightPoint()
            {
                Position = new Vector3(5, 5, 10)
            };

            #endregion

            #region Build sphere

            {
                Material material = new Material() { Color = Color.Blue };
                sphere = new Sphere(new Vector3(4, 4, 3), 3, material);
            }

            #endregion

            scene= new LightTracing.Scene()
            {
                Camera = camera,
                LightSource = lightSource,
                Primitives = new List<IPrimitive>()
                {
                    floor,
                    leftWall,
                    backWall,
                    rightWall,
                    ceiling,
                    sphere
                }
            };

            return scene;
        }
    }
}
