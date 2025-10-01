using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidStrategy
{
    // 내 캐릭터 및 적들이 모두 공통적으로 가지는 필드와 메서드
    abstract class Character
    {
        protected string name;
        protected int statusAttack;
        protected int statusHealth;
        protected bool isAlive;

        public Character() 
        {
            isAlive = true;
        }

        public void TakeDamage(int _damage)
        {
            statusHealth -= _damage;
        }

        public void Attack(Character _target)
        {
            _target.TakeDamage(statusAttack);
        }
        public void Death()
        {
            isAlive = false;
        }
    }

    // 적은 이름과 공격력, 체력 스탯만 생성자로 초기화하고 공격 외 추가 능력은 없다.
    class Enemy : Character 
    {
        public Enemy(string _name, int _att, int _hp) 
        {
            name = _name;
            statusAttack = _att;
            statusHealth = _hp;
        }
    }
}
