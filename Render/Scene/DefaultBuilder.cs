using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LightTracing;
using Render.Materials;
using Render.Primitives;
using Plane = Render.Primitives.Plane;

namespace Render.Scene
{
    public static class DefaultBuilder
    {
        private static Camera BuildDefaultCamera(int imageWidth, int imageHeight)
        {
            Material screenMaterial = new DiffuseMaterial(new Color());
            
            var leftDown = new Vector3(0, 0, 0);
            var leftTop = new Vector3(0, 0, Constants.RoomHeight);
            var rightTop = new Vector3(10, 0, Constants.RoomHeight);
            var rightDown = new Vector3(10, 0, 0);

            IPrimitive screen = new Plane(leftDown, leftTop, rightTop, rightDown, screenMaterial);

            var eyePosition = new Vector3(5, -15, Constants.RoomHeight / 2);

            Camera camera = new Camera(eyePosition, imageWidth, imageHeight,
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
            LightSquare lightSource;
            IPrimitive sphere;
            IPrimitive tetrahedron;
            LightTracing.Scene scene;

            #region Build floor

            {
                var leftDown = new Vector3(0, 0, 0);
                var leftTop = new Vector3(0, 10, 0);
                var rightTop = new Vector3(10, 10, 0);
                var rightDown = new Vector3(10, 0, 0);

                Material material = new FloorMaterial(Color.Aqua);
                floor = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build left wall

            {
                var leftDown = new Vector3(0, 0, 0);
                var leftTop = new Vector3(0, 0, Constants.RoomHeight);
                var rightTop = new Vector3(0, 10, Constants.RoomHeight);
                var rightDown = new Vector3(0, 10, 0);

                Material material = new MirrorMaterial(Color.Red);
                leftWall = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build right wall

            {
                var leftDown = new Vector3(10, 10, 0);
                var leftTop = new Vector3(10, 10, Constants.RoomHeight);
                var rightTop = new Vector3(10, 0, Constants.RoomHeight);
                var rightDown = new Vector3(10, 0, 0);

                Material material = new SpecularMaterial(Color.Red);
                rightWall = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build back wall

            {
                var leftDown = new Vector3(0, 10, 0);
                var leftTop = new Vector3(0, 10, Constants.RoomHeight);
                var rightTop = new Vector3(10, 10, Constants.RoomHeight);
                var rightDown = new Vector3(10, 10, 0);

                Material material = new DiffuseMaterial(Color.GreenYellow);
                backWall = new Plane(leftDown, leftTop, rightTop, rightDown, material);
            }

            #endregion

            #region Build ceiling

            {
                Plane Plane1;
                Plane Plane2;
                Plane Plane3;
                Plane Plane4;
                Material material = new DiffuseMaterial(Color.BlueViolet);

                {
                    var leftDown = new Vector3(0, 0, Constants.RoomHeight);
                    var leftTop = new Vector3(0, 10, Constants.RoomHeight);
                    var rightTop = new Vector3(3, 10, Constants.RoomHeight);
                    var rightDown = new Vector3(3, 0, Constants.RoomHeight);

                    Plane1 = new Plane(leftDown, leftTop, rightTop, rightDown, material);
                }

                {
                    var leftDown = new Vector3(3, 7, Constants.RoomHeight);
                    var leftTop = new Vector3(3, 10, Constants.RoomHeight);
                    var rightTop = new Vector3(7, 10, Constants.RoomHeight);
                    var rightDown = new Vector3(7, 7, Constants.RoomHeight);

                    Plane2 = new Plane(leftDown, leftTop, rightTop, rightDown, material);
                }

                {
                    var leftDown = new Vector3(7, 0, Constants.RoomHeight);
                    var leftTop = new Vector3(7, 10, Constants.RoomHeight);
                    var rightTop = new Vector3(10, 10, Constants.RoomHeight);
                    var rightDown = new Vector3(10, 0, Constants.RoomHeight);

                    Plane3 = new Plane(leftDown, leftTop, rightTop, rightDown, material);
                }

                {
                    var leftDown = new Vector3(3, 0, Constants.RoomHeight);
                    var leftTop = new Vector3(3, 3, Constants.RoomHeight);
                    var rightTop = new Vector3(7, 3, Constants.RoomHeight);
                    var rightDown = new Vector3(7, 0, Constants.RoomHeight);

                    Plane4 = new Plane(leftDown, leftTop, rightTop, rightDown, material);
                }

                ceiling = new Ceiling(material)
                {
                    Plane1 = Plane1,
                    Plane2 = Plane2,
                    Plane3 = Plane3,
                    Plane4 = Plane4
                };


            }

            #endregion

            #region Build light source

            {
                List<LightPoint> lights = new List<LightPoint>();

                /*//Random random = new Random(DateTime.Now.Millisecond);
                //for (int i = 0; i < Constants.LightCount; i++)
                //{
                //    LightPoint light = new LightPoint()
                //    {
                //        Position = new Vector3(random.Next(3, 7), random.Next(3, 7), Constants.RoomHeight + 2)
                //    };

                //    lights.Add(light);
                //}
                */

                LightPoint light = new LightPoint()
                {
                    Position = new Vector3(7, 5, Constants.RoomHeight + 0.5f)
                };

                LightPoint light2 = new LightPoint()
                {
                    Position = new Vector3(3, 4, Constants.RoomHeight + 0.5f)
                };

                lights.Add(light);
                lights.Add(light2);

                lightSource = new LightSquare()
                {
                    Lights = lights
                };
            }

            #endregion

            #region Build sphere

            {
                Material material = new SpecularMaterial(Color.Blue);
                sphere = new Sphere(new Vector3(3, 6, 2), 1.0f, material);
            }

            #endregion

            #region Built tetrahedron

            {

                Material material = new SpecularMaterial(Color.Yellow);
                tetrahedron = new Tetrahedron(
                    new Vector3(6, 6, 0),
                    new Vector3(8, 6, 0), 
                    new Vector3(7, 4.5f, 0),
                    new Vector3(7, 5.25f, 1.5f), material);
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
                    sphere,
                    tetrahedron
                }
            };

            return scene;
        }
    }
}
