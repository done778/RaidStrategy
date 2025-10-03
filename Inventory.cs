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


        public int Count 
        { get; private set; }


        public Inventory() {
            inventory = new Ally[GameManager.INVENTORY_CAPACITY];
            inventory[0] = new SwordMan();
            inventory[1] = new Scholar();
            inventory[2] = new Archer();
            Count = 3;
        }

        // 로비에서 인벤토리 진입 시 실행되는 메서드
        public void EnterInventory()
        {
            GameManager.ClearAllPanel();
            OutputVisualPanelInventory();
            InventoryOrderType order = InputOrder();
            switch (order)
            {
                case InventoryOrderType.SortAttAscending:
                    SortInventory();
                    break;
                case InventoryOrderType.SortAttDescending:
                    SortInventory();
                    break;
                case InventoryOrderType.SortHpAscending:
                    SortInventory();
                    break;
                case InventoryOrderType.SortHpDescending:
                    SortInventory();
                    break;
                case InventoryOrderType.Quit:
                    return;
            }
        }
        public void SortInventory()
        {
            Console.WriteLine("정렬");
        }
        public void AddAlly(Ally character)
        {
            if (Count < GameManager.INVENTORY_CAPACITY)
            {
                inventory[Count] = character;
                Count++;
            }
        }
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

            for (int i = 0; i < Count; i++)
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
        public Ally GetAllyAt(int index)
        {
            if (index < Count)
            { return inventory[index]; }
            else 
            { return null; }
        }

        // 인벤토리에서 슬롯 하나를 입력받아 반환함.
        public int SelectSlotFromInven()
        {
            while (true)
            {
                var inputKey = Console.ReadKey(true);
                switch (inputKey.Key)
                {
                    case ConsoleKey.Q:
                        return -1;
                    case ConsoleKey.D1:
                        return 0;
                    case ConsoleKey.D2:
                        return 1;
                    case ConsoleKey.D3:
                        return 2;
                    case ConsoleKey.D4:
                        return 3;
                    case ConsoleKey.D5:
                        return 4;
                    case ConsoleKey.D6:
                        return 5;
                    case ConsoleKey.D7:
                        return 6;
                    case ConsoleKey.D8:
                        return 7;
                }
            }
        }

        private void CommandPanelDraw()
        {
            string[] orderList = {
                "         -----------------------------------------",
                "        /                                        /",
                "       /    1. 공격력 순으로 정렬 (오름차순)    / ",
                "      /     2. 공격력 순으로 정렬 (내림차순)   /  ",
                "     /      3. 체력 순으로 정렬 (오름차순)    /   ",
                "    /       4. 체력 순으로 정렬 (내림차순)   /    ",
                "   /                                        /     ",
                "  -----------------------------------------       ",
                " /             Q. 로비로 이동            /        ",
                "-----------------------------------------         "
            };

            GameManager.DrawCenterCommandPanel(orderList);
        }
    }
}
