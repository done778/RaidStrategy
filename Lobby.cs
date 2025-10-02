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
        public void MainLobby(Player player)
        {
            OrderType order = InputOrder();
            
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

        // 다른 곳에서 로비 진입은 이 메서드를 통해서 합니다.
        public void EnterLobby(Player player)
        {
            GameManager.ClearAllPanel();
            DrawLobbyCharacter();
            MainLobby(player);
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

        private OrderType InputOrder()
        {
            string[] orderList = {
                "     -----------------------------",
                "    /   1. 전투 시작하기        / ",
                "   /    2. 내 덱 조회 / 변경   /  ",
                "  /     3. 인벤토리 조회      /   ",
                " /      4. 종료하기          /    ",
                "----------------------------      "
            };

            GameManager.DrawCenterCommandPanel(orderList);

            while (true) {
                var inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        return OrderType.Battle;

                    case ConsoleKey.D2:
                        return OrderType.MyDeck;

                    case ConsoleKey.D3:
                        return OrderType.Inventory;

                    case ConsoleKey.D4:
                        return OrderType.Quit;
                }
            }
        }
    }
}
