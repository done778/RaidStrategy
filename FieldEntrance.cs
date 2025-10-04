using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    enum EntranceOrder
    {
        Enter, EditDeck, Quit
    }
    // FieldEntrance 전투 진입 전 덱 재 확인 담당.
    class FieldEntrance
    {
        Player player;
        public FieldEntrance(Player visitant) {
            player = visitant;
        }

        public EntranceOrder EnterEntrance(bool warning)
        {
            CommandPanelDraw(warning);
            return InputOrder();
        }

        public void EnterBattleField(Ally[] deck)
        {
            BattleManager battleManager = new BattleManager(player, deck);
            battleManager.InitBattle();
        }

        private EntranceOrder InputOrder()
        {
            while (true)
            {
                var inputKey = Console.ReadKey(true);
                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        return EntranceOrder.Enter;

                    case ConsoleKey.D2:
                        return EntranceOrder.EditDeck;

                    case ConsoleKey.Q:
                        return EntranceOrder.Quit;
                }
            }
        }

        private void CommandPanelDraw(bool warning)
        {
            GameManager.ClearCommandPanel();

            string[] orderList = {
                " ---------------------------------------- ",
                "|               진입 단계                |",
                "|                                        |",
                $"|                   {player.ClearLevel+1}                    |",
                "|                                        |",
                "|  현재 덱으로 전투를 시작하시겠습니까?  |",
                "|                                        |",
                "|      1. 진입          2. 덱 편성       |",
                " ---------------------------------------- ",
                "|           Q. 로비로 돌아가기           |",
                " ---------------------------------------- "
            };
            GameManager.DrawCenterCommandPanel(orderList);
            if (warning)
            {
                Console.SetCursorPosition(GameManager.BUFFER_SIZE_WIDTH / 2 - 12, GameManager.BUFFER_SIZE_HEIGHT - 3);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("!! 덱이 비어있습니다 !!");
                Console.ResetColor();
            }
        }
    }
}
