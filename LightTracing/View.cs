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
using Render.Scene;
using Plane = Render.Primitives.Plane;

namespace LightTracing
{
    public partial class View : Form
    {
        private readonly Scene _scene;
        private readonly Bitmap _bitmap;
        private readonly Color _lightSourceColor = Color.White;
        private Color[] _colors;


        public View()
        {
            InitializeComponent();

            _bitmap = new Bitmap(Image.Width, Image.Height);

            _scene = DefaultBuilder.BuildScene(Image.Width, Image.Height);

        }

        private void RenderBtn_Click(object sender, EventArgs e)
        {
            _scene.Render(out _colors, _lightSourceColor);

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
                    _bitmap.SetPixel(i, j, _colors[i * heigth + j]);
                }
            }

            Image.Image = _bitmap;
        }
    }
}
