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
        private static bool IsAlive = true;

        private static List<Sprite2D> AllSprites = new List<Sprite2D>();
        private static List<Pellite> AllPellites = new List<Pellite>();
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

        public static void RegisterSprite(Sprite2D sprite)
        {
            if (sprite.Tag == "Pellite")
            {
                AllPellites.Add(sprite as  Pellite);
            }
            else
            {
                AllSprites.Add(sprite);
            }
           
        }

        public static void UnRegisterPellite(Sprite2D pellite)
        {
            Pellite pellite1 = pellite as Pellite;
            score += pellite1.worth;
            AllPellites.Remove(pellite1); 
        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }

        public static void IsDead()
        {
            IsAlive = false;
        }

        public static bool IsWon()
        {
            if (AllPellites.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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

            if(IsAlive) {
                if (!IsWon()) {
                    foreach (Pellite pellite in AllPellites.ToList())
                    {
                        g.DrawImage(pellite.Sprite, pellite.Position.X, pellite.Position.Y, pellite.Scale.X, pellite.Scale.Y);
                    }

                    foreach (Sprite2D sprite in AllSprites.ToList())
                    {
                        g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
                    }

                    g.DrawString($"Points:{score}", new Font("Calibri", 20), new SolidBrush(Color.Red), 200, 430);
                }
                else
                {
                    g.Clear(BackgroundColor);
                    g.DrawString("You Won! Game Over!", new Font("Calibri", 20), new SolidBrush(Color.Red), 256, 256);
                    Log.Normal("Game Over");
                }
            }
            else
            {
                g.Clear(BackgroundColor);
            }

            
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();

        public abstract void OnDraw();

        public abstract void GetKeyUp(KeyEventArgs e);

        public abstract void GetKeyDown(KeyEventArgs e);
    }
}
