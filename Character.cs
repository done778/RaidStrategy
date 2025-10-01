using System;
using System.Collections.Generic;

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
        }

        public void Attack(Character target)
        {
            target.TakeDamage(StatusAttack);
            Console.WriteLine($"{this.Name} 이(가) {target.Name} 에게 {StatusAttack} 의 피해를 줌!");
        }
        public void Death()
        {
            IsAlive = false;
        }
    }

    // 특수 능력이 있는 캐릭터는 이 인터페이스를 상속 받는다.
    interface ISpecialAbility
    {
        void UniqueAbility();
    }

    // 적 클래스는 이름과 공격력, 체력 스탯만 생성자로 초기화하고 공격 외 추가 능력은 없다.
    class Enemy : Character 
    {
        public Enemy(string name, int att, int hp) : base(name, att, hp) { }
    }

    // 아군 클래스가 공통적으로 가질 요소는 편성 여부
    class Ally : Character
    {
        public bool IsDecking { get; set; }  
        public Ally(string name, int att, int hp) : base(name, att, hp) { }
    }

    // 8종류의 각 캐릭터들은 처음 생성시 고정된 이름과 스탯을 가진다.
    class SwordMan : Ally
    {
        // 기본 스탯이 높은 탱커
        public SwordMan() : base("검사", 5, 12) { }
    }
    class Fighter : Ally
    {
        // 기본 스탯이 높은 딜러
        public Fighter() : base("싸움꾼", 10, 8) { }

    }
    class Berserker : Ally, ISpecialAbility
    {
        public Berserker() : base("광전사", 2, 15) { }

        public void UniqueAbility()
        {
            // 피해를 받으면 공격력이 현재 수치의 2배가 됨.
        }
    }
    class Archer : Ally, ISpecialAbility
    {
        public Archer() : base("궁사", 6, 5) { }

        public void UniqueAbility()
        {
            // 맨 앞에 있지 않으면 지원 공격을 함.
        }
    }
    class Boxer : Ally, ISpecialAbility
    {
        public Boxer() : base("격투가", 7, 7) { }

        public void UniqueAbility()
        {
            // 공격 후 자신 뒤에 있는 아군에게 1의 피해를 줌.
            // 특수 능력 트리거로서 활용
        }
    }
    class Magician : Ally, ISpecialAbility
    {
        public Magician() : base("마법사", 12, 2) { }

        public void UniqueAbility()
        {
            // 적을 쓰러뜨리면 현재 공격력이 3배가 됨.
            // 엄청난 공격력 뻥튀기, 낮은 체력.
            // 후반에 뻥튀기된 공격력으로 적에게 원기옥 모아서 원킬을 노리는 역할
        }
    }
    class Scholar : Ally, ISpecialAbility
    {
        public Scholar() : base("학자", 4, 8) { }

        public void UniqueAbility()
        {
            // 매 턴 맨 앞에 위치한 아군의 체력 +2
        }
    }
    class Oracle : Ally, ISpecialAbility
    {
        public Oracle() : base("점술사", 2, 8) { }

        public void UniqueAbility()
        {
            // 아군이 쓰러지면
        }
    }
}
