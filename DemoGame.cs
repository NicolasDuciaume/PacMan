﻿using ExpressedEngine.ExpressedEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressedEngine
{
    class DemoGame : ExpressedEngine.ExpressedEngine
    {
        Sprite2D player;

        bool left;
        bool right;
        bool up;
        bool down;
        string previousAccepted = "right";
        string awaitingAccepted = "right";

        string[,] Map =
        {
            {"g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g"},
            {"g",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","g"},
            {"g",".","g","g",".","g","g","g",".","g",".","g",".","g",".","g","g","g",".","g","g",".","g"},
            {"g",".","g","g",".","g","g","g",".","g",".","g",".","g",".","g","g","g",".","g","g",".","g"},
            {"g",".",".",".",".",".",".",".",".","g",".","g",".","g",".",".",".",".",".",".",".",".","g"},
            {"g",".","g","g",".","g",".","g","g","g",".","g",".","g",".","g",".","g",".","g","g",".","g"},
            {"g",".",".",".",".","g",".",".",".",".",".",".",".",".",".",".",".","g",".",".",".",".","g"},
            {"g","g","g","g",".","g","g","g",".","g","g",".","g","g",".","g","g","g",".","g","g","g","g"},
            {".",".",".",".",".",".",".",".",".","g",".",".",".","g",".",".",".",".",".",".",".",".","."},
            {"g","g","g","g",".","g","g","g",".","g","g","g","g","g",".","g","g","g",".","g","g","g","g"},
            {"g",".",".",".",".","g",".",".",".",".",".",".",".",".",".",".",".","g",".",".",".",".","g"},
            {"g",".","g","g",".","g",".","g","g","g",".","g",".","g","g","g",".","g",".","g","g",".","g"},
            {"g",".",".",".",".",".",".",".",".","g",".","g",".","g",".",".",".",".",".",".",".",".","g"},
            {"g",".","g","g",".","g","g","g",".","g",".","g",".","g",".","g","g","g",".","g","g",".","g"},
            {"g",".","g","g",".","g","g","g",".","g",".","g",".","g",".","g","g","g",".","g","g",".","g"},
            {"g",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","g"},
            {"g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g"},
        };
        public DemoGame() : base(new Vector2(615, 515), "Expressed Engine Demo") { }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { up = false; }
            if (e.KeyCode == Keys.A) { left = false; }
            if (e.KeyCode == Keys.S) { down = false; }
            if (e.KeyCode == Keys.D) { right = false; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { awaitingAccepted = "up"; }
            if (e.KeyCode == Keys.A) { awaitingAccepted = "left"; }
            if (e.KeyCode == Keys.S) { awaitingAccepted = "down"; }
            if (e.KeyCode == Keys.D) { awaitingAccepted = "right"; }
        }

        public override void OnDraw()
        {
            
        }

        public override void OnLoad()
        {
            BackgroundColor = Color.Black;


            //player = new Shape2D(new Vector2(10,10),new Vector2(10,10),"Test");
            player = new Sprite2D(new Vector2(25,25),new Vector2(25,25),"pacman_open_right","Player");

            for(int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i*25, j*25), new Vector2(25, 25), "BlockTile", "Ground");
                    }
                }
            }
        }

        public override void OnUpdate()
        {
            if(player.Position.X > 0 && player.Position.X < 5)
            {
                if(player.Position.Y > (8 * 25) - 2.5 && player.Position.Y < (8 * 25) + 2.5)
                {
                    player.Position.X = (22*25) - 7;
                }
            }

            if (player.Position.X > (22 * 25) && player.Position.X < (22 * 25) + 5)
            {
                if (player.Position.Y > (8 * 25) - 2.5 && player.Position.Y < (8 * 25) + 2.5)
                {
                    player.Position.X = 7;
                }
            }

            if(previousAccepted != awaitingAccepted)
            {
                bool is_in_way2 = false;
                for (int i = 0; i < Map.GetLength(1); i++)
                {
                    for (int j = 0; j < Map.GetLength(0); j++)
                    {
                        if (Map[j, i] == "g")
                        {
                            float Block_X = i * 25;
                            float Block_Y = j * 25;

                            if (awaitingAccepted =="up")
                            {
                                if (player.Position.X > Block_X - 25 && player.Position.X < Block_X + 25 && player.Position.Y - 1f > Block_Y - 25 && player.Position.Y - 1f < Block_Y + 25)
                                {
                                    is_in_way2 = true;
                                    break;
                                }

                            }

                            if (awaitingAccepted == "down")
                            {
                                if (player.Position.X > Block_X - 25 && player.Position.X < Block_X + 25 && player.Position.Y + 1f > Block_Y - 25 && player.Position.Y + 1f < Block_Y + 25)
                                {
                                    is_in_way2 = true;
                                    break;
                                }
                            }

                            if (awaitingAccepted=="right")

                            {
                                if (player.Position.X + 1f > Block_X - 25 && player.Position.X + 1f < Block_X + 25 && player.Position.Y > Block_Y - 25 && player.Position.Y < Block_Y + 25)
                                {
                                    is_in_way2 = true;
                                    break;
                                }
                            }

                            if (awaitingAccepted=="left")
                            {
                                if (player.Position.X - 1f > Block_X - 25 && player.Position.X - 1f < Block_X + 25 && player.Position.Y > Block_Y - 25 && player.Position.Y < Block_Y + 25)
                                {
                                    is_in_way2 = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (is_in_way2)
                    {
                        break;
                    }
                }

                if (!is_in_way2) {
                    switch (awaitingAccepted)
                    {
                        case "right":
                            right = true; left = false; up = false; down = false; break;
                        case "left":
                            right = false; left = true; up = false; down = false; break;
                        case "up":
                            right = false; left = false; up = true; down = false; break;
                        case "down":
                            right = false; left = false; up = false; down = true; break;
                        default:
                            Console.WriteLine("error");
                            break;

                    }
                    previousAccepted = awaitingAccepted;
                }
                
               
            }



            if(!up && !down && !left && !right)
            {
                switch (previousAccepted)
                {
                    case "right":
                        right = true; left = false; up = false; down = false; break;
                    case "left":
                        right = false; left = true; up = false; down = false; break;
                    case "up":
                        right = false; left = false; up = true; down = false; break;
                    case "down":
                        right = false; left = false; up = false; down = true; break;
                    default:
                        Console.WriteLine("error");
                        break;

                }
            }

            bool is_in_way = false;
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        float Block_X = i * 25;
                        float Block_Y = j * 25;
                        
                        if (up)
                        {
                            if (player.Position.X > Block_X - 25 && player.Position.X < Block_X + 25 && player.Position.Y - 1f > Block_Y - 25 && player.Position.Y - 1f < Block_Y + 25)
                            {
                                awaitingAccepted = "up";
                                is_in_way = true;
                                break;
                            }
                            
                        }

                        if (down)
                        {
                            if (player.Position.X > Block_X - 25 && player.Position.X < Block_X + 25 && player.Position.Y + 1f > Block_Y - 25 && player.Position.Y + 1f < Block_Y + 25)
                            {
                                awaitingAccepted = "down";
                                is_in_way = true;
                                break;
                            }
                        }

                        if (right)
                        {
                            if (player.Position.X + 1f > Block_X - 25 && player.Position.X + 1f < Block_X + 25 && player.Position.Y > Block_Y - 25 && player.Position.Y < Block_Y + 25)
                            {
                                awaitingAccepted = "right";
                                is_in_way = true;
                                break;
                            }
                        }

                        if (left)
                        {
                            if (player.Position.X - 1f > Block_X - 25 && player.Position.X -1f < Block_X + 25 && player.Position.Y > Block_Y - 25 && player.Position.Y < Block_Y + 25)
                            {
                                awaitingAccepted = "left";
                                is_in_way = true;
                                break;
                            }
                        }
                    }
                }
                if (is_in_way)
                {
                    break;
                }
            }

            if (up)
            {
                previousAccepted = "up";
                player.UpdateSprite("pacman_open_up");
                if (!is_in_way) { player.Position.Y -= 1f; }
                
            }
            if (down)
            {
                previousAccepted = "down";
                player.UpdateSprite("pacman_open_down");
                if (!is_in_way) { player.Position.Y += 1f; }
            }
            if (right)
            {
                previousAccepted = "right";
                player.UpdateSprite("pacman_open_right");
                if (!is_in_way) { player.Position.X += 1f; }
            }
            if (left)
            {
                previousAccepted = "left";
                player.UpdateSprite("pacman_open_left");
                if (!is_in_way) { player.Position.X -= 1f; }
            }

            up = false;
            down = false;
            left = false;
            right = false;

            
        }
    }
}