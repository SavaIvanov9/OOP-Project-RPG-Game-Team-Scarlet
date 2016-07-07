using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Microsoft.DirectX.Direct3D.Device device;
        Microsoft.DirectX.Direct3D.Texture texture;
        Microsoft.DirectX.Direct3D.Font font;
        public Form1()
        {
            InitializeComponent();
            InitDevice();
            InitFont();
            LoadTexture();
        }

        private void LoadTexture()
        {
            texture = TextureLoader.FromFile(device, @"..\..\Content\grass1.jpg", 
                1024, 1024, 1, 0, Format.A8R8G8B8, Pool.Managed, Filter.Point, Filter.Point,
                Color.Transparent.ToArgb());
        }

        private void InitDevice()
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);
        }

        private void InitFont()
        {
            System.Drawing.Font f = new System.Drawing.Font("Arial", 16f, FontStyle.Regular);
            font = new Microsoft.DirectX.Direct3D.Font(device, f);
        }
        private void Render()
        {
            device.Clear(ClearFlags.Target, Color.CornflowerBlue, 0, 1);
            device.BeginScene();
            using (Sprite s = new Sprite(device))
            {
                s.Begin(SpriteFlags.AlphaBlend);
                s.Draw2D(texture, new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height), 
                    new Rectangle(0, 0, 0, 0), 
                    new Point(), 0f, 
                    new Point(0, 0),  
                    Color.White);
                font.DrawText(s, "Best game ever", new Point(0, 0), Color.White);
                s.End();
            }
            device.EndScene();
            device.Present();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }
    }
}
