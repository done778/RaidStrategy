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

        BattleField battleField; // UI 업데이트 담당
        Level level; // 적 관련 데이터
        Player thePlayer { get; set; } // 플레이어
        List<Ally> cloneDeck; // 플레이어가 가져온 덱을 복제하여 리스트에 담음 (원본 유지)
        Ally currentAlly;
        Enemy currentEnemy;
        string[] log;

        public BattleManager(Player player, Ally[] deck)
        {
            thePlayer = player;
            battleField = new BattleField();
            level = new Level(thePlayer.ClearLevel + 1);
            InitCloneDeck(deck);
        }

        // 원본 데이터는 유지되어야 하기에 복제를 해서 리스트에 담음.
        private void InitCloneDeck(Ally[] deck)
        {
            cloneDeck = new List<Ally>(GameManager.DECK_CAPACITY);
            for (int i = 0; i < deck.Length; i++)
            {
                if (deck[i] != null)
                {
                    cloneDeck.Add(deck[i].GetClone());
                }
            }
        }
        // 전투 첫 진입 시 실행 메서드
        public bool InitBattle()
        {
            battleField.PanelUpdate(cloneDeck, level.GetCurrentEnemy());
            battleField.ShowStartMessage();
            SkillEventRegister(cloneDeck);
            EnterToNextAction();
            return StartBattle();
        }

        // 전투 시작. 반환형인 bool은 승패 여부
        public bool StartBattle()
        {
            bool EndBattle = false; // 전투 종료 여부
            bool isVictory = false; // 전투 승리 or 패배 여부
            while (!EndBattle) // 전투가 종료될 때까지 반복
            {
                EndBattle = ExecuteOneTurn();
                EnterToNextAction();
            }

            if (level.GetRemainEnemy() == 0)
            {
                isVictory = true;
            }
            return isVictory;
        }

        // 턴 한 번동안 일어나는 일들을 순서대로 실행하는 메서드
        private bool ExecuteOneTurn()
        {
            currentAlly = cloneDeck[0];
            currentEnemy = level.GetCurrentEnemy();
            // 턴 시작 알림! 예를 들어 학자가 있으면 맨 앞 아군 체력 증가 시킴.
            OnTurnStart?.Invoke(null);
            // 패널 업데이트. 로그는 어떻게 표시하지?
            battleField.PanelUpdate(cloneDeck, currentEnemy);

            log = combatInteraction(currentAlly, currentEnemy); // 서로 일반 공격 주고 받음
            battleField.PanelUpdate(cloneDeck, currentEnemy, log);

            // 턴 종료 알림!
            OnTurnPreEnd?.Invoke(null);

            // 현재 턴 종료와 다음 턴 시작 사이에 죽은 애 체크 & 처리를 해야함.
            if (currentEnemy.IsAlive == false)
            {
                battleField.DrawEnemy(currentEnemy, true);
                level.removeEnemy();
                if (level.GetRemainEnemy() == 0) // 남은 적이 없다면 전투 종료
                {
                    return true;
                }
                // 적이 쓰러지는 조건인 특수 능력 발동
                OnEnemyDown?.Invoke(null);
            }
            // 리스트 요소 삭제로 인덱스가 꼬일 수 있으므로 뒤에서 부터 체크
            for (int i = cloneDeck.Count - 1; i >= 0; i--) 
            {
                // 아군 중에 죽은 애가 있다면
                if (cloneDeck[i].IsAlive == false)
                {
                    // 죽는 애니메이션 출력
                    battleField.DrawDeathAlly(i);

                    // 죽은 아군의 특수 능력을 델리게이트에서 제거 해야함.
                    SkillEventDelete(currentAlly);

                    // 처리가 끝났으니 덱에서 제거.
                    cloneDeck.RemoveAt(i);
                    
                    // 아군이 쓰러지는 조건인 특수 능력 발동
                    OnAllyDown?.Invoke(null);
                }
            }
            
            return false;
        }

        // 아군과 적 서로 일반 공격 주고 받기
        private string[] combatInteraction(Ally ally, Enemy enemy)
        {
            string[] log = {
                ally.Attack(enemy),
                enemy.Attack(ally)
            };
            // 공격의 한 단위는 동시에 서로의 체력이 깎인다.

            // 공격 후와 피해를 받은 시점이 동시에 발동한다.
            // 그럼 공격 후랑 피해 받는 시점을 왜 굳이 나눴느냐?
            // 특수 능력 중엔 아군에게 피해를 주는 효과가 있는데 이 땐 OnTakenDamage 이벤트만 발생해야한다.
            OnAfterAttack?.Invoke(ally);
            OnTakenDamage?.Invoke(ally);
            return log;
        }

        // 엔터 키 누를 때마다 한 턴이 지나감.
        private void EnterToNextAction()
        {
            while (true)
            {
                var inputKey = Console.ReadKey();
                if (inputKey.Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
        }

        // 특수 능력을 보유한 캐릭터는 발동 시점 기준으로 델리게이트에 메서드를 등록함.
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

        // 사망한 아군은 델리게이트에서 특수 능력 메서드를 제거한다.
        public void SkillEventDelete(Ally ally)
        {
            if (ally is ISpecialAbility)
            {
                switch ((ally as ISpecialAbility).Timing)
                {
                    case TimingCondition.TurnStart:
                        OnTurnStart -= (ally as ISpecialAbility).CastingSpecialAbility;
                        break;
                    case TimingCondition.TurnPreEnd:
                        OnTurnPreEnd -= (ally as ISpecialAbility).CastingSpecialAbility;
                        break;
                    case TimingCondition.TakenDamage:
                        OnTakenDamage -= (ally as ISpecialAbility).CastingSpecialAbility;
                        break;
                    case TimingCondition.AfterAttack:
                        OnAfterAttack -= (ally as ISpecialAbility).CastingSpecialAbility;
                        break;
                    case TimingCondition.AllyDown:
                        OnAllyDown -= (ally as ISpecialAbility).CastingSpecialAbility;
                        break;
                    case TimingCondition.EnemyDown:
                        OnEnemyDown -= (ally as ISpecialAbility).CastingSpecialAbility;
                        break;
                }
            }
        }
    }
}
