using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    enum InventoryOrderType
    {
        SortAttAscending, SortAttDescending, SortHpAscending, SortHpDescending, Quit
    }
    class Inventory
    {
        Ally[] inventory;
        int count;

        public int Count 
        { get { return count; } }

        public Inventory() {
            inventory = new Ally[GameManager.INVENTORY_CAPACITY];
            inventory[0] = new SwordMan();
            inventory[1] = new Scholar();
            inventory[2] = new Archer();
            count = 3;
        }

        // 로비에서 인벤토리 진입 시 실행되는 메서드
        public InventoryOrderType EnterInventory()
        {
            GameManager.ClearAllPanel();
            OutputVisualPanelInventory();
            return InputOrder();
        }
        // 인벤토리 정렬
        public void SortInventory(InventoryOrderType type)
        {
            Console.WriteLine("정렬");
        }
        // 인벤토리에 캐릭터 추가
        public void AddAlly(Ally character)
        {
            if (count < GameManager.INVENTORY_CAPACITY)
            {
                inventory[count] = character;
                count++;
            }
        }
        // 명령어 입력 받기
        public InventoryOrderType InputOrder()
        {
            CommandPanelDraw();
            while (true)
            {
                var inputKey = Console.ReadKey(true);
                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        return InventoryOrderType.SortAttAscending;

                    case ConsoleKey.D2:
                        return InventoryOrderType.SortAttDescending;

                    case ConsoleKey.D3:
                        return InventoryOrderType.SortHpAscending;

                    case ConsoleKey.D4:
                        return InventoryOrderType.SortHpDescending;

                    case ConsoleKey.Q:
                        return InventoryOrderType.Quit;
                }
            }
        }
        public bool CheckDecking(int index, out Ally character)
        {
            character = inventory[index];
            return inventory[index].IsDecking;
        }
        public void IsDeckingChange(Ally target)
        {
            for (int i = 0; i < inventory.Length; i++) 
            { 
                if (target.Equals(inventory[i]))
                {
                    if (inventory[i].IsDecking == true)
                    {
                        inventory[i].IsDecking = false;
                    }
                    else
                    {
                        inventory[i].IsDecking = true;
                    }
                }
            }
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
                cursorY = i > 3 ? (GameManager.BUFFER_SIZE_HEIGHT - GameManager.HORIZON_AREA) + 1 : 1;
                cursorX = (GameManager.BUFFER_SIZE_WIDTH / 4) * (i % 4) + 1;

                // 그려질 리소스는 각 캐릭터가 가지고 있음
                inventory[i].DrawAsciiArt(cursorX, cursorY);
            }
        }
        private void CommandPanelDraw()
        {
            string[] orderList = {
                "       --------------------------------------",
                "      /  1. 공격력 순으로 정렬 (오름차순)  / ",
                "     /   2. 공격력 순으로 정렬 (내림차순) /  ",
                "    /    3. 체력 순으로 정렬 (오름차순)  /   ",
                "   /     4. 체력 순으로 정렬 (내림차순) /    ",
                "  --------------------------------------     ",
                " /       Q. 로비로 이동               /      ",
                "--------------------------------------       "
            };

            GameManager.DrawCenterCommandPanel(orderList);
        }
    }
}
