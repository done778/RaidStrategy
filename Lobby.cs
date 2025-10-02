using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    enum LobbyOrderType
    {
        Battle, MyDeck, Inventory, Quit
    }
    class Lobby
    {
        
        public LobbyOrderType MainLobby()
        {
            CommandPanelDraw();

            while (true)
            {
                var inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        return LobbyOrderType.Battle;

                    case ConsoleKey.D2:
                        return LobbyOrderType.MyDeck;

                    case ConsoleKey.D3:
                        return LobbyOrderType.Inventory;

                    case ConsoleKey.Q:
                        return LobbyOrderType.Quit;
                }
            }
        }

        // 다른 곳에서 로비 진입은 이 메서드를 통해서 합니다.
        public void EnterLobby(Player player)
        {
            while (true) {
                GameManager.ClearAllPanel();
                DrawLobbyCharacter();
                LobbyOrderType order = MainLobby();
                switch (order)
                {
                    case LobbyOrderType.Battle:
                        Console.WriteLine("전투 시작 선택됨");
                        break;
                    case LobbyOrderType.MyDeck:
                        player.OpenMyDeck();
                        break;
                    case LobbyOrderType.Inventory:
                        player.OpenInventory();
                        break;
                    case LobbyOrderType.Quit:
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
