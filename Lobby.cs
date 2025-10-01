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
            InputOrder();
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

        private void InputOrder()
        {
            string[] orderList = {
                "1. 전투 시작하기    ",
                "2. 내 덱 조회 / 변경",
                "3. 인벤토리 조회    ",
                "4. 종료하기         "
            };

            GameManager.DrawCenterCommandPanel(orderList);
        }
    }
}
