using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    class Pellite : Sprite2D
    {
        public int worth { get; set; }
        public Pellite(Vector2 position, Vector2 scale, string directory, string tag, int worth) : base(position, scale, directory, tag)
        {
            this.worth = worth;
        }

        public new void DestroySelf()
        {
            Log.Info($"[SPRITE2D]({Tag}) - Has been Destroyed");
            ExpressedEngine.UnRegisterPellite(this);
        }
    }
}
