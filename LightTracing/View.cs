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
using Render.Primitives;
using Plane = Render.Primitives.Plane;

namespace LightTracing
{
    public partial class View : Form
    {
        private readonly Scene Scene;
        private Color[] colors;
        public View()
        {
            InitializeComponent();

            List<IPrimitive> primitives = new List<IPrimitive>()
            {
                new Plane(new Vector3(-2, 1, 0), new Vector3(2, 4, 0)),
                new Sphere(new Vector3(0, 2, 0), 1)
            };

            Scene = new Scene()
            {
                Primitives = primitives,

                Camera = new Camera(new Vector3(0, 0, 1), new Vector3(0, 1, -0.5f), Image.Width, Image.Height),

                LightSource = new LightPoint()
                {
                    Position = new Vector3(1, 1, 5)
                }
            };
        }

        private void RenderBtn_Click(object sender, EventArgs e)
        {
            Scene.Render(out colors);

            ColorImage();
        }

        private void ColorImage()
        {
            
        }
    }
}
