using System;
using System.Collections.Generic;
using System.Threading;

namespace RaidStrategy
{
    class BattleField
    {
        Player Player { get; set; }
        List<Ally> cloneDeck;
        Level level;
        public BattleField(Player player, Ally[] deck)
        {
            Player = player;
            level = new Level(Player.ClearLevel + 1);
            cloneDeck = new List<Ally>(GameManager.DECK_CAPACITY);
            for (int i = 0; i < deck.Length; i++) {
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
            combatInteraction();
            if(level.CheckDeath())
            {
                if (level.GetRemainEnemy() == 0) // 남은 적이 없다면 전투 종료
                {
                    return true;
                }
            }
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
        }

        public void VisualPanelUpdate()
        {
            int cursorX = 0;
            int cursorY = GameManager.HORIZON_AREA / 4;
            
            Console.SetCursorPosition(1, cursorY - 1);
            for (int i = 0; i < GameManager.BUFFER_SIZE_WIDTH / 3; i++)
            {
                Console.Write("- ");
            }
            cursorY += GameManager.HORIZON_AREA / 2;
            Console.SetCursorPosition(1, cursorY + 1);
            for (int i = 0; i < GameManager.BUFFER_SIZE_WIDTH / 3; i++)
            {
                Console.Write("- ");
            }
            int interval = GameManager.BUFFER_SIZE_WIDTH / 3 * 2 / 4; // 40

            for (int i = 0; i < cloneDeck.Count; i++) 
            {
                cursorY = GameManager.HORIZON_AREA / 4;
                cursorX = (GameManager.BUFFER_SIZE_WIDTH / 3 * 2) - (interval * (i+1)) + 3;
                cloneDeck[i].DrawAsciiArt(cursorX + 7, cursorY + 1, false);

                cursorX += interval / 2;
                cursorY += GameManager.HORIZON_AREA / 2 + 3;
                Console.SetCursorPosition(cursorX - cloneDeck[i].Name.Length, cursorY);
                Console.Write(cloneDeck[i].Name);

                cursorY += 2;
                Console.SetCursorPosition(cursorX-10, cursorY);
                Console.Write("공격력");
                Console.SetCursorPosition(cursorX+4, cursorY);
                Console.Write("체  력");

                cursorY += 1;
                Console.SetCursorPosition(cursorX-7, cursorY);
                Console.Write(cloneDeck[i].StatusAttack);
                Console.SetCursorPosition(cursorX+6, cursorY);
                Console.Write(cloneDeck[i].StatusHealth);
            }
        }
    }
}
