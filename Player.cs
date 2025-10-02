using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class Player
    {
        public string Name { get; set; }
        public string Affiliation { get; set; }

        Inventory inventory;
        MyDeck deck;

        public Player(string _name, string _aff) 
        {
            Name = _name;
            Affiliation = _aff;
            inventory = new Inventory();
            deck = new MyDeck();
        }
        public void OpenInventory()
        {
            while (true) 
            {
                InventoryOrderType order = inventory.EnterInventory();
                if (order == InventoryOrderType.Quit) 
                { return; }
                else 
                { inventory.SortInventory(order); }
            }
        }
        public void OpenMyDeck()
        {
            while (true)
            {
                ChangeSlot order = deck.EnterMyDeck(); // 바꿀 슬롯 번호를 받음. Q는 나가기
                if (order == ChangeSlot.Quit)
                { return; }
                else
                {
                    SelectPanelUpdate();

                    // 인벤토리에서 덱에 넣을 애를 선택함.
                    int selectSlot = SelectToDeckFromInven();

                    // 슬롯에 편성 가능한지 검사.
                    // 1. 이미 덱에 편성되어 있을 경우 -> 교체 -> 슬롯이 같으면 아무 일 없음
                    // 2. 덱에 편성되어 있지 않을 경우 -> 선택한 캐릭은 해당 슬롯에 삽입
                    //                                 -> IsDecking 값 변경
                    DeckUpdate(selectSlot, order);
                }
            }  
        }
        public void SelectPanelUpdate()
        {
            // 비주얼, 커맨드 패널 갱신
            GameManager.ClearAllPanel();
            inventory.OutputVisualPanelInventory();
            deck.SlotChangeCommandPanelDraw();
        }
        public int SelectToDeckFromInven()
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
        public void DeckUpdate(int selectSlot, ChangeSlot order)
        {
            Ally selected;
            if (selectSlot == -1) // Q 입력 처리
            {
                return;
            }
            if (selectSlot < inventory.Count) // 비어있는 칸을 선택하지 않음.
            {
                if (inventory.CheckDecking(selectSlot, out selected)) // 이미 있다. -> 스왑 처리
                {
                    int target = deck.SearchIndex(selected);
                    if (target != -1)
                    {
                        deck.ChangeSlotCharacter((int)order, target);
                    }
                    else
                    {
                        Console.WriteLine("뭔가 크게 잘못됨.");
                    }
                }
                else // 없다. -> IsDecking 값 바꾸고 덱에 삽입
                {
                    // 일단 들어갈 애 먼저 IsDecking 을 바꿔.
                    inventory.IsDeckingChange(selected);
                    // 그 다음 덱에 넣고 원래 덱에 누가 있었나 확인
                    Ally previous = deck.InsertCharacter(selected, (int)order);
                    if (previous != null) // 누가 있었다면 빠지는 애도 IsDecking을 바꿔.
                    {
                        inventory.IsDeckingChange(previous);
                    }
                }
            }
            else // 비어있는 칸 선택시 덱에서 빼기만 함.
            {
                Ally previous = deck.InsertCharacter(null, (int)order);
                if (previous != null)
                {
                    inventory.IsDeckingChange(previous);
                }
            }
        }
    }
}
