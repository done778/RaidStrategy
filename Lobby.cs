using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidStrategy
{
    enum OrderType
    {
        Battle, MyDeck, Inventory, Quit
    }
    class Lobby
    {
        public void MainLobby()
        {
            OrderType order = InputOrder();
            
            switch (order)
            {
                case OrderType.Battle:
                    Console.WriteLine("전투 시작 선택됨");
                    break;
                case OrderType.MyDeck:
                    Console.WriteLine("내 덱 조회/변경 선택됨");
                    break;
                case OrderType.Inventory:
                    Console.WriteLine("인벤토리 조회 선택됨");
                    break;
                case OrderType.Quit:
                    return;
            }

        }

        // 다른 곳에서 로비 진입은 이 메서드를 통해서 합니다.
        public void EnterLobby()
        {
            GameManager.ClearAllPanel();
            DrawLobbyCharacter();
            MainLobby();
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
                "1. 전투 시작하기    ",
                "2. 내 덱 조회 / 변경",
                "3. 인벤토리 조회    ",
                "4. 종료하기         "
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
