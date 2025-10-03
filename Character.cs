using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

namespace RaidStrategy
{
    // 내 캐릭터 및 적들이 모두 공통적으로 가지는 필드와 메서드
    abstract class Character
    {
        public string Name { get; protected set; }
        public int StatusAttack { get; protected set; }
        public int StatusHealth { get; protected set; }
        public bool IsAlive { get; protected set; }

        public Character(string name, int att, int hp) 
        {
            Name = name;
            StatusAttack = att;
            StatusHealth = hp;
            IsAlive = true;
        }

        public void TakeDamage(int damage)
        {
            StatusHealth -= damage;
            if(StatusHealth <= 0)
            {
                Death();
            }
        }

        public string Attack(Character target)
        {
            target.TakeDamage(StatusAttack);
            return $"{this.Name} 이(가) {target.Name} 에게 {StatusAttack} 의 피해를 줌!";
        }
        public void Death()
        {
            IsAlive = false;
        }
    }
}
