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
                    int selectSlot = inventory.SelectSlotFromInven();
                    if (selectSlot == -1) // Q 입력 처리
                    {
                        continue;
                    }
                    // 인덱스에 있는 캐릭터를 받아옴. null일수도 있음.
                    Ally selected = inventory.GetAllyAt(selectSlot);
                    // 덱 클래스에서 처리
                    deck.DeckUpdate(selected, order);
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
    }
}
