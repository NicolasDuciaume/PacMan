using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    class Player : Sprite2D
    {
        public int lives { get; set; }
        public Vector2 startingPosition = null;
        public Player(Vector2 position, Vector2 scale, string directory, string tag, int lives) : base(position, scale, directory, tag)
        {
            this.lives = lives;
            startingPosition = position;
        }

        public void UpdateSprite(string directory)
        {
            this.Directory = directory;
            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);

            Sprite = sprite;
        }

        public void Death()
        {
            Log.Info($"[Player]({Tag}) - Has been Killed");
            ExpressedEngine.IsDead();
        }

    }
}
