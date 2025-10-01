using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class MyDeck
    {
        Ally[] myDeck;

        public MyDeck()
        {
            myDeck = new Ally[GameManager.DECK_CAPACITY];
        }

        // 로비에서 내 덱 진입 시 실행되는 메서드
        public void EnterMyDeck()
        {
            GameManager.ClearAllPanel();
            OutputVisualPanelMyDeck();
        }

        public void OutputVisualPanelMyDeck()
        {
            int cursorX = GameManager.BUFFER_SIZE_WIDTH / 4;

            GameManager.ClearVisualPanel();

            for (int i = 1; i <= 3; i++)
            {
                for (int j = GameManager.HORIZON_AREA / 2; j < GameManager.HORIZON_AREA; j++)
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

            string[] draw =
            {
                "                                                                                                                                                                                                                               ",
                "                              -@@.   :@@@+                                                                                                               ..:..                                                    .%@@@@=      ",
                "                            -@@@.    +@@@*                                           :@@%%%*%######%@##*.                                          .*%@@@@@@@@@@@+.                                             +@@@@@@@=      ",
                "                          .@@@@     #@@@+                                            .@@@@@@@@@@@@@@@@@@@+                                         @@@@@@@@@@@@@@@@.                                            %@@@@@#        ",
                "                        .@@@@*.   .@@@@*.                                              :*-:.   .:+%@@@@@@+                                         %@@%=       .+@@@                                            +@@@@%.        ",
                "                      -@@@@@:.    @@@@%                                                     -%@@@@@@@@#:.                                                   .%@@@@@=                                           *@@@@*.         ",
                "                    :@@@@@#.    .@@@@%                                                .@@@@@@@@@@@#:                                                    :*@@@@@@@%:                                           =@@@@#.          ",
                "                  .%@@@@%.     :@@@@*                                                  -@@@@@@@@@@@@@*.                                              =@@@@@@@@:                                              *@@@@-            ",
                "                 #@@@@@%:..   -@@@@=.=@+.                                                -#*+-:.. ::%@@:                                         .-@@@@@@@%-                                                =@@@@#             ",
                "                 .%@@@@@@@@@@@@@@@@@@@@@@@:                                                         :@@=                                       .#@@@@@@%:                                                  -@@@@#.             ",
                "                         .%@@@@@@@@@@@@*:                                                         .=@@@=                                     :@@@@@@%.                                                    :@@@@@.              ",
                "                            %@@@@:.                                                             :@@@@@#.                                   :@@@@@@+.                                                      %@@@@-               ",
                "                           %@@@@.                                                          .:%@@@@@@@=                                   .@@@@@@:                                                        *@@@@#                ",
                "                         .@@@@@#                                                    .=*#@@@@@@@@@@%=                                    :@@@@@*    :.+#*######=                                         :@@@@@.                ",
                "                         +@@@@%.                                              -@@@@@@@@@@@@@@@@@:.                                      =@@@@@@@@@@@@@@@@@@@@@@+                                        +@@@@*                 ",
                "                        -@@@@@=                                                +@@@@@@@@@@%+                                            .@@@@@@@@@@@@@@@%##%@@@%                                        .@@@@.                 ",
                "                        @@@@@%.                                               .:*##++:                                                   .:#@+=:....         ..                                          -@@@.                 ",
                "                        .+@@@@                                                                                                                                                                                                 "
            };

            for (int i = 0; i < draw.Length; i++) 
            {
                Console.SetCursorPosition(1, i + 1);
                Console.Write(draw[i]);
            }
        }
    }
}
