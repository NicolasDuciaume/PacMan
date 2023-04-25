using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    class Player : Sprite2D
    {
        public int lives { get; set; }
        public Player(Vector2 position, Vector2 scale, string directory, string tag, int lives) : base(position, scale, directory, tag)
        {
            this.lives = lives;
        }

        public void Death()
        {
            Log.Info($"[Player]({Tag}) - Has been Killed");
            ExpressedEngine.IsDead();
        }

    }
}
