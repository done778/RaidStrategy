using System;
using System.Collections.Generic;

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

        public void StartBattle()
        {
            GameManager.ClearAllPanel();
            DrawVisualPanel();
        }
        public void DrawVisualPanel()
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

            Console.ReadLine();
        }
    }
}
