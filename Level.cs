using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    // Level의 역할 : 단계 별로 적들을 인스턴스화
    class Level
    {
        List<Enemy> enemies;
        public Level(int level) 
        {
            InitLevel(level);
        }
        // level 값에 따라 몬스터들을 인스턴스화 하여 리스트에 담습니다.
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

        // 현재 남은 적의 수를 반환
        public int GetRemainEnemy()
        {
            return enemies.Count;
        }

        // 현재 상대중인 적(맨 앞에 있는 적) 반환
        public Enemy GetCurrentEnemy()
        {
            return enemies[0];
        }
        
        public void removeEnemy()
        {
            enemies.RemoveAt(0);
        }
    }
}
