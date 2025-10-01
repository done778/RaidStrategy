using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidStrategy
{
    class Lobby
    {
        public void MainLobby()
        {
            LobbyOrder();
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

            for (int i = 0; i < characterArt.Length; i++)
            {
                int Cursor_X = (GameManager.BUFFER_SIZE_WIDTH - characterArt[0].Length) / 2;
                int Cursor_Y = (GameManager.HORIZON_AREA - characterArt.Length) / 2;

                Console.SetCursorPosition(Cursor_X, Cursor_Y + i);
                Console.Write(characterArt[i]);
            }
        }

        private void LobbyOrder()
        {
            string[] orderList = {
                "1. 전투 시작하기    ",
                "2. 내 덱 조회 / 변경",
                "3. 인벤토리 조회    ",
                "4. 종료하기         "
            };

            for (int i = 0; i < orderList.Length; i++)
            {
                int Cursor_X = (GameManager.BUFFER_SIZE_WIDTH - orderList[0].Length) / 2;
                int Cursor_Y = (GameManager.HORIZON_AREA - orderList.Length) / 2;

                Console.SetCursorPosition(Cursor_X, Cursor_Y + i);
                Console.Write(orderList[i]);
            }
        }
    }
}
