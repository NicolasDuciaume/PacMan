using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    class Enemy : Sprite2D
    {
        public string[] previousOptions;
        public string previousPick = "";
        public Enemy(Vector2 position, Vector2 scale, string directory, string tag, string StartPick) : base(position, scale, directory, tag)
        {
        }

        public void UpdateSprite(string directory)
        {
            this.Directory = directory;
            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);

            Sprite = sprite;
        }

        public string pickMovement(string[] movements)
        {
            Random rnd = new Random();
            int pick = rnd.Next(movements.Length);

            if(previousPick == movements[pick])
            {
                return previousPick;
            }
            else
            {
                if(previousPick =="up") 
                {
                    if (movements.Length != 1)
                    {
                        while (movements[pick] == "down")
                        {
                            pick = rnd.Next(movements.Length);
                        }
                        previousPick = movements[pick];
                        return previousPick;
                    }
                    else
                    {
                        previousPick = "down";
                        return previousPick;
                    }
                }
                else if(previousPick == "down")
                {
                    if (movements.Length != 1)
                    {
                        while (movements[pick] == "up")
                        {
                            pick = rnd.Next(movements.Length);
                        }
                        previousPick = movements[pick];
                        return previousPick;
                    }
                    else
                    {
                        previousPick = "up";
                        return previousPick;
                    }
                }
                else if (previousPick == "right")
                {
                    if (movements.Length != 1)
                    {
                        while (movements[pick] == "left")
                        {
                            pick = rnd.Next(movements.Length);
                        }
                        previousPick = movements[pick];
                        return previousPick;
                    }
                    else
                    {
                        previousPick = "left";
                        return previousPick;
                    }
                }
                else
                {
                    if (movements.Length != 1)
                    {
                        while (movements[pick] == "right")
                        {
                            pick = rnd.Next(movements.Length);
                        }
                        previousPick = movements[pick];
                        return previousPick;
                    }
                    else
                    {
                        previousPick = "right";
                        return previousPick;
                    }
                }
            }
         
        }
    }
}
