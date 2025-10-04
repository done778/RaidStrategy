using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    enum TimingCondition
    {
        TurnStart, TurnPreEnd, TakenDamage, AfterAttack, AllyDown, EnemyDown, End
    }
    // BattleManager의 역할 : 실질적인 전투 로직을 담당
    class BattleManager
    {
        public Action<Ally> OnTurnStart;
        public Action<Ally> OnTurnPreEnd;
        public Action<Ally> OnTakenDamage;
        public Action<Ally> OnAfterAttack;
        public Action<Ally> OnAllyDown;
        public Action<Ally> OnEnemyDown;
        
        public void SkillEventRegister(List<Ally> deck)
        {
            for (int i = 0; i < deck.Count; i++) 
            {
                if (deck[i] is ISpecialAbility)
                {
                    switch((deck[i] as ISpecialAbility).Timing)
                    {
                        case TimingCondition.TurnStart:
                            OnTurnStart += (deck[i] as ISpecialAbility).CastingSpecialAbility;
                            break;
                        case TimingCondition.TurnPreEnd:
                            OnTurnPreEnd += (deck[i] as ISpecialAbility).CastingSpecialAbility;
                            break;
                        case TimingCondition.TakenDamage:
                            OnTakenDamage += (deck[i] as ISpecialAbility).CastingSpecialAbility;
                            break;
                        case TimingCondition.AfterAttack:
                            OnAfterAttack += (deck[i] as ISpecialAbility).CastingSpecialAbility;
                            break;
                        case TimingCondition.AllyDown:
                            OnAllyDown += (deck[i] as ISpecialAbility).CastingSpecialAbility;
                            break;
                        case TimingCondition.EnemyDown:
                            OnEnemyDown += (deck[i] as ISpecialAbility).CastingSpecialAbility;
                            break;
                    }
                }
            }
        }
    }
}
