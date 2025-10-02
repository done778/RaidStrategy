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
            int cursorY;
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

            for (int i = 0; i < count; i++)
            {
                // 캐릭터들을 슬롯에 그리는건 캐릭터 클래스에서
                // 여기선 시작 커서만 지정해줌
                // 위 4칸 아래 4칸이므로 조건에 따라 Y축 커서를 조절해줌
                cursorY = i > 3 ? GameManager.HORIZON_AREA + 1 : 1;
                cursorX = (GameManager.BUFFER_SIZE_WIDTH / 4) * (i % 4) + 1;

                // 그려질 리소스는 각 캐릭터가 가지고 있음
                // inventory[i].DrawAsciiArt(cursorX, cursorY);
            }
        }
    }
}
