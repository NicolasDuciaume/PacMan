using ExpressedEngine.ExpressedEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpressedEngine
{
    class DemoGame : ExpressedEngine.ExpressedEngine
    {
        Player player;
        private static List<Pellite> AllPellittes = new List<Pellite>();
        private static List<Enemy> AllEnemys = new List<Enemy>();
        private static List<Sprite2D> Health = new List<Sprite2D>();

        bool left;
        bool right;
        bool up;
        bool down;
        string previousAccepted = "";
        string awaitingAccepted = "";
        int mouth_tics = 1;

        string[,] Map =
        {
            {"g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g"},
            {"g",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","g"},
            {"g",".","g","g",".","g","g","g",".","g",".","g",".","g",".","g","g","g",".","g","g",".","g"},
            {"g",".","g","g",".","g","g","g",".","g",".","g",".","g",".","g","g","g",".","g","g",".","g"},
            {"g",".",".",".",".",".",".",".",".","g",".","g",".","g",".",".",".",".",".",".",".",".","g"},
            {"g",".","g","g",".","g",".","g","g","g",".","g",".","g","g","g",".","g",".","g","g",".","g"},
            {"g",".",".",".",".","g",".",".",".",".",".",".",".",".",".",".",".","g",".",".",".",".","g"},
            {"g","g","g","g",".","g","g","g",".","g","g","a1","g","g",".","g","g","g",".","g","g","g","g"},
            {"t",".",".",".",".",".",".",".",".","g","a3","a2","a4","g",".",".",".",".",".",".",".",".","t"},
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

            for(int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i*25, j*25), new Vector2(25, 25), "BlockTile", "Ground");
                    }
                    if (Map[j, i] == ".")
                    {
                        AllPellittes.Add(new Pellite(new Vector2(i * 25, j * 25), new Vector2(25, 25), "Pellite", "Pellite",1));
                    }
                    if (Map[j, i] == "a1" || Map[j, i] == "a2")
                    {
                        AllEnemys.Add(new Enemy(new Vector2(i * 25, j * 25), new Vector2(25, 25), "Enemy_up", "Enemy","up"));
                    }
                    if (Map[j, i] == "a3")
                    {
                        AllEnemys.Add(new Enemy(new Vector2(i * 25, j * 25), new Vector2(25, 25), "Enemy_right", "Enemy", "right"));
                    }
                    if (Map[j, i] == "a4")
                    {
                        AllEnemys.Add(new Enemy(new Vector2(i * 25, j * 25), new Vector2(25, 25), "Enemy_left", "Enemy", "left"));
                    }
                }
            }

            player = new Player(new Vector2(275, 250), new Vector2(25, 25), "Pacman_mid_right", "Player",3);

            Health.Add(new Sprite2D(new Vector2(500, 430), new Vector2(15, 15), "Pacman_mid_right", "Health"));
            Health.Add(new Sprite2D(new Vector2(525, 430), new Vector2(15, 15), "Pacman_mid_right", "Health"));
            Health.Add(new Sprite2D(new Vector2(550, 430), new Vector2(15, 15), "Pacman_mid_right", "Health"));
        }

        public void PelliteHitDetection()
        {
            for (int pel = 0; pel < AllPellittes.Count; pel++)
            {
                if (player.Position.X == AllPellittes[pel].Position.X && player.Position.Y == AllPellittes[pel].Position.Y) { AllPellittes[pel].DestroySelf(); AllPellittes.RemoveAt(pel); }
            }
        }

        public void TeleporterHitDetection(Sprite2D character)
        {
            if (character.Position.X > 0 && character.Position.X < 5)
            {
                if (character.Position.Y > (8 * 25) - 2.5 && character.Position.Y < (8 * 25) + 2.5)
                {
                    character.Position.X = (22 * 25) - 7;
                }
            }

            if (character.Position.X > (22 * 25) && character.Position.X < (22 * 25) + 5)
            {
                if (character.Position.Y > (8 * 25) - 2.5 && character.Position.Y < (8 * 25) + 2.5)
                {
                    character.Position.X = 7;
                }
            }
        }

        public void PlayerMovement()
        {
            if (previousAccepted != awaitingAccepted)
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

                            if (awaitingAccepted == "up")
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

                            if (awaitingAccepted == "right")

                            {
                                if (player.Position.X + 1f > Block_X - 25 && player.Position.X + 1f < Block_X + 25 && player.Position.Y > Block_Y - 25 && player.Position.Y < Block_Y + 25)
                                {
                                    is_in_way2 = true;
                                    break;
                                }
                            }

                            if (awaitingAccepted == "left")
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

                if (!is_in_way2)
                {
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
                            break;

                    }
                    previousAccepted = awaitingAccepted;
                }


            }


            if (!up && !down && !left && !right)
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
                            if (player.Position.X - 1f > Block_X - 25 && player.Position.X - 1f < Block_X + 25 && player.Position.Y > Block_Y - 25 && player.Position.Y < Block_Y + 25)
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
                //player.UpdateSprite("pacman_open_up");
                if (!is_in_way) { player.Position.Y -= 1f; }

            }
            if (down)
            {
                previousAccepted = "down";
                //player.UpdateSprite("pacman_open_down");
                if (!is_in_way) { player.Position.Y += 1f; }
            }
            if (right)
            {
                previousAccepted = "right";
                //player.UpdateSprite("pacman_open_right");
                if (!is_in_way) { player.Position.X += 1f; }
            }
            if (left)
            {
                previousAccepted = "left";
                //player.UpdateSprite("pacman_open_left");
                if (!is_in_way) { player.Position.X -= 1f; }
            }

            up = false;
            down = false;
            left = false;
            right = false;
        }

        public string EnemyMovementOptions(string awaitingAcceptedEnemy, Enemy enemy)
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

                        if (awaitingAcceptedEnemy == "up")
                        {
                            if (enemy.Position.X > Block_X - 25 && enemy.Position.X < Block_X + 25 && enemy.Position.Y - 1f > Block_Y - 25 && enemy.Position.Y - 1f < Block_Y + 25)
                            {
                                is_in_way2 = true;
                                break;
                            }

                        }

                        if (awaitingAcceptedEnemy == "down")
                        {
                            if (enemy.Position.X > Block_X - 25 && enemy.Position.X < Block_X + 25 && enemy.Position.Y + 1f > Block_Y - 25 && enemy.Position.Y + 1f < Block_Y + 25)
                            {
                                is_in_way2 = true;
                                break;
                            }
                        }

                        if (awaitingAcceptedEnemy == "right")

                        {
                            if (enemy.Position.X + 1f > Block_X - 25 && enemy.Position.X + 1f < Block_X + 25 && enemy.Position.Y > Block_Y - 25 && enemy.Position.Y < Block_Y + 25)
                            {
                                is_in_way2 = true;
                                break;
                            }
                        }

                        if (awaitingAcceptedEnemy == "left")
                        {
                            if (enemy.Position.X - 1f > Block_X - 25 && enemy.Position.X - 1f < Block_X + 25 && enemy.Position.Y > Block_Y - 25 && enemy.Position.Y < Block_Y + 25)
                            {
                                is_in_way2 = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (!is_in_way2) 
            { 
                return awaitingAcceptedEnemy;
            }
            else {  return ""; }
        }

        public void EnemyMovement()
        {
            foreach (Enemy enemy in AllEnemys.ToList())
            {
                List<string> movementsEnemy = new List<string>();

                if (EnemyMovementOptions("right", enemy) != "")
                {
                    movementsEnemy.Add("right");
                }
                if (EnemyMovementOptions("left", enemy) != "")
                {
                    movementsEnemy.Add("left");
                }
                if (EnemyMovementOptions("up", enemy) != "")
                {
                    movementsEnemy.Add("up");
                }
                if (EnemyMovementOptions("down", enemy) != "")
                {
                    movementsEnemy.Add("down");
                }

                switch (enemy.pickMovement(movementsEnemy.ToArray()))
                {
                    case "right":
                        enemy.UpdateSprite("Enemy_right");
                        enemy.Position.X += 1f;
                        break;
                    case "left":
                        enemy.UpdateSprite("Enemy_left");
                        enemy.Position.X -= 1f;
                        break;
                    case "up":
                        enemy.UpdateSprite("Enemy_up");
                        enemy.Position.Y -= 1f;
                        break;
                    case "down":
                        enemy.UpdateSprite("Enemy_down");
                        enemy.Position.Y += 1f;
                        break;
                    default: break;
                }
                
            }
        }

        public void PlayerMouthAnimation()
        {
            mouth_tics += 1;
            switch (mouth_tics)
            {
                case 1:
                    switch (previousAccepted)
                    {
                        case "right":
                            player.UpdateSprite("PacMan_mid_right");
                            break;
                        case "left":
                            player.UpdateSprite("PacMan_mid_left");
                            break;
                        case "up":
                            player.UpdateSprite("PacMan_mid_up");
                            break;
                        case "down":
                            player.UpdateSprite("PacMan_mid_down");
                            break;
                        default:
                            break;

                    }
                    break;
                case 8:
                    switch (previousAccepted)
                    {
                        case "right":
                            player.UpdateSprite("PacMan_open_right");
                            break;
                        case "left":
                            player.UpdateSprite("PacMan_open_left");
                            break;
                        case "up":
                            player.UpdateSprite("PacMan_open_up");
                            break;
                        case "down":
                            player.UpdateSprite("PacMan_open_down");
                            break;
                        default:
                            break;

                    }
                    break;
                case 13:
                    switch (previousAccepted)
                    {
                        case "right":
                            player.UpdateSprite("PacMan_mid_right");
                            break;
                        case "left":
                            player.UpdateSprite("PacMan_mid_left");
                            break;
                        case "up":
                            player.UpdateSprite("PacMan_mid_up");
                            break;
                        case "down":
                            player.UpdateSprite("PacMan_mid_down");
                            break;
                        default:
                            break;

                    }
                    break;
                case 18:
                    player.UpdateSprite("PacMan_closed");
                    break;
                case 24:
                    mouth_tics = 0;
                    break;
                default:
                    break;

            }
        }

        public override void OnUpdate()
        {

            PelliteHitDetection();

            PlayerMovement();

            TeleporterHitDetection(player);

            PlayerMouthAnimation();

            EnemyMovement();

            foreach(Enemy enemy in AllEnemys)
            {
                TeleporterHitDetection(enemy);
                if(enemy.Position.X > player.Position.X) {
                    if(enemy.Position.Y > player.Position.Y)
                    {
                        if ((enemy.Position.X - player.Position.X < 10 && enemy.Position.Y - player.Position.Y < 10))
                        {
                            int livesPrevious = player.lives;
                            player.DestroySelf();
                            Health[Health.Count - 1].DestroySelf();
                            Health.RemoveAt(Health.Count - 1);
                            player = new Player(new Vector2(275, 250), new Vector2(25, 25), "Pacman_mid_right", "Player", livesPrevious - 1);
                            break;
                        }
                    }
                    else
                    {
                        if ((enemy.Position.X - player.Position.X < 10 && player.Position.Y - enemy.Position.Y < 10))
                        {
                            int livesPrevious = player.lives;
                            player.DestroySelf();
                            Health[Health.Count - 1].DestroySelf();
                            Health.RemoveAt(Health.Count - 1);
                            player = new Player(new Vector2(275, 250), new Vector2(25, 25), "Pacman_mid_right", "Player", livesPrevious - 1);
                            break;
                        }
                    }
                }
                else
                {
                    if (enemy.Position.Y > player.Position.Y)
                    {
                        if ((player.Position.X - enemy.Position.X < 10 && enemy.Position.Y - player.Position.Y < 10))
                        {
                            int livesPrevious = player.lives;
                            player.DestroySelf();
                            Health[Health.Count - 1].DestroySelf();
                            Health.RemoveAt(Health.Count - 1);
                            player = new Player(new Vector2(275, 250), new Vector2(25, 25), "Pacman_mid_right", "Player", livesPrevious - 1);
                            break;
                        }
                    }
                    else
                    {
                        if ((player.Position.X - enemy.Position.X < 10 && player.Position.Y - enemy.Position.Y < 10))
                        {
                            int livesPrevious = player.lives;
                            player.DestroySelf();
                            Health[Health.Count - 1].DestroySelf();
                            Health.RemoveAt(Health.Count - 1);
                            player = new Player(new Vector2(275, 250), new Vector2(25, 25), "Pacman_mid_right", "Player", livesPrevious - 1);
                            break;
                        }
                    }
                }
                
            }

            ///TO DO Add Enemy Character to fetch if player has been hit and needs to be killed
            ///
            

            if (player.lives == 0)
            {
                player.Death();
            }
        }
    }
}
