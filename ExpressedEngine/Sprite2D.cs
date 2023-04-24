using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ExpressedEngine.ExpressedEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;

        public string Directory = "";
        public string Tag = "";

        public Bitmap Sprite = null;

        public Sprite2D(Vector2 position, Vector2 scale, string directory, string tag)
        {
            this.Position = position;
            this.Scale = scale;
            this.Directory = directory;
            this.Tag = tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp,(int) this.Scale.X,(int)this.Scale.Y);

            Sprite = sprite;


            Log.Info($"[SPRITE2D]({Tag}) - Has been registered");
            ExpressedEngine.RegisterSprite(this);

        }

        public void UpdateSprite(string directory)
        {
            this.Directory = directory;
            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);

            Sprite = sprite;
        }

        public void DestroySelf()
        {
            Log.Info($"[SPRITE2D]({Tag}) - Has been Destroyed");
            ExpressedEngine.UnRegisterSprite(this);
        }
    }
}
