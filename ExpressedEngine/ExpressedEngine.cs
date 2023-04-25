using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ExpressedEngine.ExpressedEngine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }

    public abstract class ExpressedEngine
    {
        private Vector2 ScreenSize = new Vector2(512,512);
        private string Title = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;


        private static List<Shape2D> AllShapes = new List<Shape2D>();
        private static List<Sprite2D> AllSprites = new List<Sprite2D>();
        private static int score = 0;

        public Vector2 CameraPosition = Vector2.Zero();

        public Color BackgroundColor = Color.Beige;

        public ExpressedEngine(Vector2 ScreenSize, string Title) 
        {
            Log.Info("Game is starting");
            this.ScreenSize = ScreenSize;
            this.Title = Title;

            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
            Window.Text = this.Title;
            Window.Paint += Renderer;
            Window.KeyUp += Window_KeyUP;
            Window.KeyDown += Window_KeyDown;

            ///OnLoad();

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
                
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyUP(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            AllSprites.Add(sprite);
        }

        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {
            if(sprite.Tag == "Pellite")
            {
                score += 1;
            }
            AllSprites.Remove(sprite);
        }

        private void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(5);
                }
                catch { Log.Error("Window has not been found...Waiting..."); }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            g.Clear(BackgroundColor);

            g.TranslateTransform(CameraPosition.X, CameraPosition.Y);

            foreach (Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(Color.Red),shape.Position.X, shape.Position.Y,shape.Scale.X,shape.Scale.Y);
            }

            foreach(Sprite2D sprite in AllSprites.ToList())
            {
                g.DrawImage(sprite.Sprite,sprite.Position.X,sprite.Position.Y,sprite.Scale.X,sprite.Scale.Y);
            }

            g.DrawString($"Points:{score}", new Font("Calibri", 20), new SolidBrush(Color.Red), 200, 430);
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();

        public abstract void OnDraw();

        public abstract void GetKeyUp(KeyEventArgs e);

        public abstract void GetKeyDown(KeyEventArgs e);
    }
}
