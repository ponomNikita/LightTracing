using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Render;
using Render.Primitives;
using Plane = Render.Primitives.Plane;

namespace LightTracing
{
    public partial class View : Form
    {
        private readonly Scene Scene;
        private Color[] colors;
        private Bitmap bitmap;
        public View()
        {
            InitializeComponent();

            bitmap = new Bitmap(Image.Width, Image.Height);

            #region Build camera

            Material screenMaterial = new Material();
            IPrimitive screentA = new Triangle(new Vector3(-2, 0, -1), new Vector3(-2, 0, 2), new Vector3(2, 0, 2),
                screenMaterial);
            IPrimitive screentB = new Triangle(new Vector3(-2, 0, -1), new Vector3(2, 0, -1), new Vector3(2, 0, 2),
                screenMaterial);

            IPrimitive screen = new Plane((Triangle)screentA, (Triangle)screentB, screenMaterial);
            Camera camera = new Camera(new Vector3(0, -1, 0.5f), new Vector3(0, 1, -0.5f), Image.Width, Image.Height,
                (Plane) screen);

            #endregion

            #region Build floor

            Material floorMaterial = new Material();
            IPrimitive tfA = new Triangle(new Vector3(-2, 1, 0), new Vector3(-2, 4, 0), new Vector3(2, 4, 0),
                floorMaterial);
            IPrimitive tfB = new Triangle(new Vector3(-2, 1, 0), new Vector3(2, 1, 0), new Vector3(2, 4, 0),
                floorMaterial);

            IPrimitive floor = new Plane((Triangle)tfA, (Triangle)tfB, floorMaterial);

            #endregion

            #region Build scene
            List<IPrimitive> primitives = new List<IPrimitive>()
            {
                floor,
                //new Sphere(new Vector3(0, 2, 0), 1)
            };

            Scene = new Scene()
            {
                Primitives = primitives,

                Camera = camera,

                LightSource = new LightPoint()
                {
                    Position = new Vector3(0, 1.5f, 2)
                }
            };
            #endregion

        }

        private void RenderBtn_Click(object sender, EventArgs e)
        {
            Scene.Render(out colors);

            ColorImage();
        }

        private void ColorImage()
        {
            int width = Image.Width;
            int heigth = Image.Height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    bitmap.SetPixel(i, j, colors[i * heigth + j]);
                }
            }

            Image.Image = bitmap;
        }
    }
}
