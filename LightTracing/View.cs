using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
        private readonly Color _lightSourceColor = Color.AliceBlue;
        private Color[] _colors;

        public IntegerProperty _progressBarValueProperty;

        public View()
        {
            InitializeComponent();

            _bitmap = new Bitmap(Image.Width, Image.Height);

            _scene = DefaultBuilder.BuildScene(Image.Width, Image.Height);

            progressBar.Maximum = 100;

            _scene.OnRayCastEventHandler += OnRayCast;
        }

        private void RenderBtn_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
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

            progressBar.Value = 0;
            progressBar.Visible = false;
        }

        private void OnRayCast(object sender, EventArgs eventArgs)
        {
            var s = sender as Scene;
            if (s == null)
                return;
            
            if (s.Percent <= progressBar.Maximum)
                progressBar.Value = s.Percent;
        }
    }
}
