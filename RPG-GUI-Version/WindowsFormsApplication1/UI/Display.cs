using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;

namespace WindowsFormsApplication1
{
    public partial class Display : Form
    {
        Microsoft.DirectX.Direct3D.Device device;
        Microsoft.DirectX.DirectInput.Device keyboard;
        Microsoft.DirectX.Direct3D.Texture texture, texture2;
        Microsoft.DirectX.Direct3D.Font font;
        int x = 0;
        int y = 0;
        float rotation = 0;
        int fps = 0;
        int frames =0;
        private long timeStarted = Environment.TickCount;
        private Thread thread;
        private float cameraX, cameraY, cameraZ;

        public Display()
        {
            InitializeComponent();
            InitDevice();
            InitKeyboard();
            InitFont();
            LoadTexture();
        }

        private void UpdateCamera()
        {
            cameraX = x;
            cameraY = y;

            device.Transform.Projection = Matrix.OrthoLH(
                device.Viewport.Width,
                device.Viewport.Height,
                0.1f, 1000f);
            device.Transform.View = Matrix.LookAtLH(
                new Vector3(cameraX, cameraY, 50),
                new Vector3(x, y, 0),
                new Vector3(0, -1, 0));
        }

        private void LoadTexture()
        {
            texture = TextureLoader.FromFile(device, @"..\..\Resources\grass1.jpg",
                1024, 1024, 1, 0, Format.A8R8G8B8, Pool.Managed, Filter.Point, Filter.Point,
                Color.Transparent.ToArgb());
            texture2 = TextureLoader.FromFile(device, @"..\..\Resources\wiz2.png",
                169, 130, 1, 0, Format.A8R8G8B8, Pool.Managed, Filter.Point, Filter.Point,
                Color.Transparent.ToArgb());
        }

        private void InitDevice()
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            device = new Microsoft.DirectX.Direct3D.Device(0, Microsoft.DirectX.Direct3D.DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);
        }

        private void InitFont()
        {
            System.Drawing.Font f = new System.Drawing.Font("Arial", 16f, FontStyle.Regular);
            font = new Microsoft.DirectX.Direct3D.Font(device, f);
        }

        private void InitKeyboard()
        {
            keyboard = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Keyboard);
            keyboard.SetCooperativeLevel(this, CooperativeLevelFlags.NonExclusive |
                CooperativeLevelFlags.Background);
            keyboard.Acquire();

        }

        private void UpdateInput()
        {
            foreach (Key k in keyboard.GetPressedKeys())
            {
                if (k == Key.D)
                {
                    x += 5;
                }
                if (k == Key.S)
                {
                    y += 5;
                }
                if (k == Key.A)
                {
                    x -= 5;
                }
                if (k == Key.W)
                {
                    y -= 5;
                }
                if (k == Key.Left)
                {
                    rotation -= 0.1f;
                }
                if (k == Key.Right)
                {
                    rotation += 0.1f;
                }
            }
        }

        private void Render()
        {
            while (true)
            {
                UpdateInput();

                device.Clear(ClearFlags.Target, Color.CornflowerBlue, 0, 1);
                device.BeginScene();
                using (Sprite s = new Sprite(device))
                {
                    s.Begin(SpriteFlags.AlphaBlend);
                    s.Draw2D(texture, new Rectangle(0, 0, 0, 0),
                        new SizeF(device.Viewport.Width, device.Viewport.Height),
                        new Point(0, 0), 0f,
                        new Point(0, 0),
                        Color.White);

                    Matrix matrix = new Matrix();
                    matrix = Matrix.Transformation2D(
                        new Vector2(0, 0), 0.0f,
                        new Vector2(1.0f, 1.0f),
                        new Vector2(x, y),
                        rotation, new Vector2(0, 0));
                    s.Transform = matrix;

                    //s.Draw2D(texture2, new Rectangle(x, y, device.Viewport.Width, device.Viewport.Height),
                    //  new SizeF(),
                    //  new Point(), 0f,
                    //  new Point(0, 0),
                    //  Color.White);

                    s.Draw(texture2,
                        new Rectangle(0, 0, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(x, y, 0),
                        Color.White);

                    //font.DrawText(s, "Best game ever", new Point(0, 0), Color.White);
                    UpdateCamera();
                    s.End();
                }
                using (Sprite b = new Sprite(device))
                {
                    b.Begin(SpriteFlags.AlphaBlend);
                    font.DrawText(b, "Best game ever", new Point(0, 0), Color.White);
                    font.DrawText(b, "FPS: " + fps, new Point(0, 30), Color.White);
                    b.End();
                }
                device.EndScene();
                device.Present();

                if (Environment.TickCount >= timeStarted + 1000)
                {
                    fps = frames;
                    frames = 0;
                    timeStarted = Environment.TickCount;
                }

                frames++;
            }
        }
        
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    UpdateInput();
        //    Render();
        //}

        private void StartThread()
        {
            thread = new Thread(new ThreadStart(Render));
            thread.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            StartThread();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopThread();
        }

        private void StopThread()
        {
            thread.Abort();
        }
    }
}
