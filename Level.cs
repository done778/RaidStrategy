using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class Level
    {
        List<Enemy> enemies;
        public Level(int level) 
        {
            InitLevel(level);
        }
        private void InitLevel(int level)
        {
            enemies = new List<Enemy>();
            switch (level) 
            {
                case 1:
                    enemies.Add(new Slime(3, 20));
                    break;
                case 2:
                    enemies.Add(new GreenMushroom(4, 15));
                    enemies.Add(new MushMom(6,30));
                    break;
                case 3:
                    enemies.Add(new Drake(5, 20));
                    enemies.Add(new JuniorBalrog(6, 50));
                    break;
                case 4:
                    enemies.Add(new MushMom(3, 40));
                    enemies.Add(new JuniorBalrog(4, 55));
                    enemies.Add(new Limbo(7, 80));
                    break;
            }
        }
        public void DrawEnemy()
        {
            if (enemies.Count > 0)
            {
                enemies[0].DrawAsciiArt();
                Console.ReadLine();
            }
        }
    }
}
