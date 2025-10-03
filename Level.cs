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
            }
        }
        public bool CheckDeath()
        {
            if (enemies[0].IsAlive == false)
            {
                enemies[0].DrawAsciiArt(true);
                enemies.RemoveAt(0);
                return true;
            }
            return false;
        }
        public int GetRemainEnemy()
        {
            return enemies.Count;
        }
        public string ExecuteAttack(Character target)
        {
            return enemies[0].Attack(target);
        }
        public Character GetCurrentEnemy()
        {
            return enemies[0];
        }
        public void DrawEnemyInfo()
        {
            string[] template = {
                " ---------------------------------------------- ",
                "|                                              |",
                "|                                              |",
                "|         공격력                체  력         |",
                "|                                              |",
                " ---------------------------------------------- "
            };
            int cursorX = (GameManager.BUFFER_SIZE_WIDTH - template[0].Length) / 2;
            for (int i = 0; i < template.Length; i++) 
            {
                Console.SetCursorPosition(cursorX, i + 2);
                Console.Write(template[i]);
            }
            cursorX = (GameManager.BUFFER_SIZE_WIDTH / 2);
            Console.SetCursorPosition(cursorX - enemies[0].Name.Length, 3);
            Console.Write(enemies[0].Name);
            Console.SetCursorPosition(cursorX - (template[0].Length / 4 - 1), 6);
            Console.Write(enemies[0].StatusAttack);
            Console.SetCursorPosition(cursorX + (template[0].Length / 4 - 2), 6);
            Console.Write(enemies[0].StatusHealth);
        }
    }
}
