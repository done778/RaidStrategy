using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    enum EntranceOrder
    {
        Enter, EditDeck, Quit
    }
    class FieldEntrance
    {
        Player player;
        public FieldEntrance(Player visitant) {
            player = visitant;
        }

        public EntranceOrder EnterEntrance()
        {
            CommandPanelDraw();
            return InputOrder();
        }

        public void EnterBattleField()
        {
            BattleField battleField = new BattleField(player);
            battleField.StartBattle();
        }

        private EntranceOrder InputOrder()
        {
            CommandPanelDraw();
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

        private void CommandPanelDraw()
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
        }
    }
}
