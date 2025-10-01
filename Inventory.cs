using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class Inventory
    {
        Ally[] inventory;
        int count;

        public Inventory() {
            inventory = new Ally[GameManager.INVENTORY_CAPACITY];
            inventory[0] = new SwordMan();
            inventory[1] = new Scholar();
            inventory[2] = new Archer();
            count = 3;
        }

        // 로비에서 인벤토리 진입 시 실행되는 메서드
        public void EnterInventory()
        {
            GameManager.ClearAllPanel();
            OutputVisualPanelInventory();
        }
        public void SortInventory()
        {

        }
        public void OutputVisualPanelInventory()
        {
            int cursorX = GameManager.BUFFER_SIZE_WIDTH / 4;

            GameManager.ClearVisualPanel();

            for (int i = 1; i <= 3; i++) 
            { 
                for (int j = 1; j < GameManager.HORIZON_AREA; j++)
                {
                    Console.SetCursorPosition(cursorX * i, j);
                    Console.Write("|");
                }
            }
            Console.SetCursorPosition(1, GameManager.HORIZON_AREA / 2);
            for (int i = 0; i < GameManager.BUFFER_SIZE_WIDTH - 2; i++)
            {
                Console.Write("-");
            }
        }
    }
}
