using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    enum OrderType
    {
        Battle, MyDeck, Inventory, Quit
    }
    class Lobby
    {
        public OrderType MainLobby(Player player)
        {
            CommandPanelDraw();

            while (true)
            {
                var inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        return OrderType.Battle;

                    case ConsoleKey.D2:
                        return OrderType.MyDeck;

                    case ConsoleKey.D3:
                        return OrderType.Inventory;

                    case ConsoleKey.Q:
                        return OrderType.Quit;
                }
            }
        }

        // 다른 곳에서 로비 진입은 이 메서드를 통해서 합니다.
        public void EnterLobby(Player player)
        {
            while (true) {
                GameManager.ClearAllPanel();
                DrawLobbyCharacter();
                OrderType order = MainLobby(player);
                switch (order)
                {
                    case OrderType.Battle:
                        Console.WriteLine("전투 시작 선택됨");
                        break;
                    case OrderType.MyDeck:
                        player.OpenMyDeck();
                        break;
                    case OrderType.Inventory:
                        player.OpenInventory();
                        break;
                    case OrderType.Quit:
                        return;
                }
            }
        }

        private void DrawLobbyCharacter()
        {
            string[] characterArt = {                
               "     .*@@@@@%=.     ",
               "   .%@+     -@@:    ",       
               "   +@         #%.   ",      
               "   @@         *@-   ",
               "   +@:       .%%.   ",        
               "   .#@*.   .=@@.    ",       
               "     :*@@@@@#=      ",      
               "       .%@@..       ",
               "      :@@@@@=       ",
               "     =@#.@--@%.     ",        
               "   :@%. .@-*@=.     ",
               "  :@*   .@-   =@=   ",        
               " .#@:   .@-    %@.  ",       
               " -@*    .@-    :@#  ",      
               " =@:    .@-    .%%. ",     
               "       .#@@.        ",    
               "      .*@*@%:       ",   
               "     .%@:  %@:      ",  
               "     @@     #@-     ", 
               "   .%%.      =@.    ",
               "   -@=       :%#.   ",
               "   %@         #@:   ",
               " =@@-          @@%. "
            };

            GameManager.DrawCenterVisualPanel(characterArt);  
        }

        private void CommandPanelDraw()
        {
            string[] orderList = {
                "      -----------------------------",
                "     /   1. 전투 시작하기        / ",
                "    /    2. 내 덱 조회 / 변경   /  ",
                "   /     3. 인벤토리 조회      /   ",
                "  -----------------------------    ",
                " /       Q. 종료하기         /     ",
                "----------------------------       "
            };

            GameManager.DrawCenterCommandPanel(orderList); 
        }
    }
}
