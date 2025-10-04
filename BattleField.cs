using System;
using System.Collections.Generic;
using System.Threading;

namespace RaidStrategy
{
    // BattleField의 역할 : 콘솔을 업데이트 하는 역할
    class BattleField
    {
        static int interval = GameManager.BUFFER_SIZE_WIDTH / 3 * 2 / 4; // 40
        static int cursorY = GameManager.HORIZON_AREA / 4;
        static int cursorX = (GameManager.BUFFER_SIZE_WIDTH / 3 * 2) + 3;
        Player Player { get; set; }
        List<Ally> cloneDeck;
        Level level;
        BattleManager battleManager;
        public BattleField(Player player, Ally[] deck)
        {
            Player = player;
            level = new Level(Player.ClearLevel + 1);
            InitCloneDeck(deck);
            battleManager = new BattleManager();
            battleManager.SkillEventRegister(cloneDeck);
        }
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

        public bool StartBattle()
        {
            PanelUpdate();
            ShowStartMessage();
            EnterToNextAction();
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

        private bool ExecuteOneTurn()
        {
            PanelUpdate();
            battleManager.OnTurnStart?.Invoke(null);
            combatInteraction();
            battleManager.OnTakenDamage?.Invoke(null);
            if (level.CheckDeath())
            {
                if (level.GetRemainEnemy() == 0) // 남은 적이 없다면 전투 종료
                {
                    return true;
                }
            }
            for (int i = 0; i < cloneDeck.Count; i++)
            {
                if (cloneDeck[i].IsAlive == false)
                {
                    cloneDeck[i].DrawingDeath(cursorX + 7 - (interval * (i + 1)), cursorY + 1);
                    cloneDeck.RemoveAt(i);
                }
            }
            battleManager.OnTurnPreEnd?.Invoke(null);
            return false;
        }

        private void combatInteraction()
        {
            string[] log = {
                cloneDeck[0].Attack(level.GetCurrentEnemy()),
                level.ExecuteAttack(cloneDeck[0])
            };
            PanelUpdate();
            GameManager.DrawCenterCommandPanel(log);
        }

        private void EnterToNextAction()
        {
            while(true)
            {
                var inputKey = Console.ReadKey();
                if (inputKey.Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
        }

        private void ShowStartMessage()
        {
            string[] msg = { 
                "엔터 키를 누를 때마다 한 턴씩 전투가 진행 됩니다.", 
                "     전투를 시작하려면 엔터 키를 눌러주세요.     " 
            };
            GameManager.DrawCenterCommandPanel(msg);
        }

        public void PanelUpdate()
        {
            GameManager.ClearAllPanel();
            VisualPanelUpdate();
            level.DrawEnemy();
            level.DrawEnemyInfo();
        }

        public void VisualPanelUpdate()
        {
            Console.SetCursorPosition(1, cursorY - 1);
            for (int i = 0; i < GameManager.BUFFER_SIZE_WIDTH / 3; i++)
            {
                Console.Write("- ");
            }
            Console.SetCursorPosition(1, cursorY + GameManager.HORIZON_AREA / 2 + 1);
            for (int i = 0; i < GameManager.BUFFER_SIZE_WIDTH / 3; i++)
            {
                Console.Write("- ");
            }
            int interval = GameManager.BUFFER_SIZE_WIDTH / 3 * 2 / 4; // 40

            for (int i = 0; i < cloneDeck.Count; i++) 
            {
                cloneDeck[i].DrawAsciiArt(cursorX + 7 - (interval * (i + 1)), cursorY + 1, false);

                int pivotX = (interval / 2) - (interval * (i + 1));
                int pivotY = GameManager.HORIZON_AREA / 2;

                Console.SetCursorPosition(cursorX + pivotX - cloneDeck[i].Name.Length, cursorY + (pivotY + 3));
                Console.Write(cloneDeck[i].Name);

                Console.SetCursorPosition(cursorX + pivotX - 10, cursorY + (pivotY + 5));
                Console.Write("공격력");

                Console.SetCursorPosition(cursorX + pivotX + 4, cursorY + (pivotY + 5));
                Console.Write("체  력");

                Console.SetCursorPosition(cursorX + pivotX - 7, cursorY + (pivotY + 6));
                Console.Write(cloneDeck[i].StatusAttack);

                Console.SetCursorPosition(cursorX + pivotX + 6, cursorY + (pivotY + 6));
                Console.Write(cloneDeck[i].StatusHealth);
            }
        }
    }
}
