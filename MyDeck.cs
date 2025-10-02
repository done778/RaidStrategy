using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    enum ChangeSlot
    {
        First, Second, Third, Fourth, Quit
    }
    class MyDeck
    {
        Ally[] myDeck;

        public MyDeck()
        {
            myDeck = new Ally[GameManager.DECK_CAPACITY];
        }

        // 로비에서 내 덱 진입 시 실행되는 메서드
        public ChangeSlot EnterMyDeck()
        {
            GameManager.ClearAllPanel();
            OutputVisualPanelMyDeck();
            return InputOrder();
        }
        // 덱에서 바꿀 슬롯의 번호나 나가기 명령을 입력 받음
        private ChangeSlot InputOrder()
        {
            CommandPanelDraw();
            while (true)
            {
                var inputKey = Console.ReadKey(true);
                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        return ChangeSlot.First;

                    case ConsoleKey.D2:
                        return ChangeSlot.Second;

                    case ConsoleKey.D3:
                        return ChangeSlot.Third;

                    case ConsoleKey.D4:
                        return ChangeSlot.Fourth;

                    case ConsoleKey.Q:
                        return ChangeSlot.Quit;
                }
            } 
        }

        // 선택된 캐릭터를 덱의 슬롯에 삽입을 시도함.
        // 1. 이미 덱에 편성되어 있을 경우 -> 교체 -> 슬롯이 같으면 아무 일 없음
        // 2. 덱에 편성되어 있지 않을 경우 -> 선택한 캐릭은 해당 슬롯에 삽입
        //                                 -> IsDecking 값 변경
        public void DeckUpdate(Ally selectSlot, ChangeSlot order)
        {
            // 이미 덱에 있는가? -> -1이면 없음
            int index = SearchIndex(selectSlot);

            if (index == -1) 
            {
                Ally prevChar = InsertCharacter(selectSlot, (int)order);
                if (prevChar != null) prevChar.IsDecking = false;
                if (selectSlot != null) selectSlot.IsDecking = true;
            }
            else
            {
                ChangeSlotCharacter(index, (int)order);
            }
        }

        // 매개변수 index위치에 target 캐릭터를 덱에 삽입
        public Ally InsertCharacter(Ally target, int index)
        {
            Ally previousChar = null;
            if (myDeck[index] != null)
            {
                previousChar = myDeck[index];
            }
            myDeck[index] = target;
            return previousChar;
        }

        // 매개변수 캐릭터를 찾아 위치를 반환
        public int SearchIndex(Ally target)
        {
            if (target == null) return -1;
            for (int i = 0; i < myDeck.Length; i++) {
                if (target.Equals(myDeck[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        // 덱 안에서 순서만 바꿈
        public void ChangeSlotCharacter(int changeA, int changeB)
        {
            Ally temp = myDeck[changeA];
            myDeck[changeA] = myDeck[changeB];
            myDeck[changeB] = temp;
        }


        // 덱 화면 출력
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

            int cursorY = (GameManager.BUFFER_SIZE_HEIGHT - GameManager.HORIZON_AREA) + 1;
            for (int i = 0; i < myDeck.Length; i++)
            {
                cursorX = (GameManager.BUFFER_SIZE_WIDTH / 4) * (myDeck.Length - i - 1) + 1;

                if (myDeck[i] == null)
                {
                    Console.SetCursorPosition(cursorX + (GameManager.BUFFER_SIZE_WIDTH / 8) - 8, cursorY + (GameManager.HORIZON_AREA / 4) - 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("편성되지  않음");
                    Console.ResetColor();
                }
                else
                {
                    myDeck[i].DrawAsciiArt(cursorX, cursorY);
                }
            }
        }
        private void CommandPanelDraw()
        {
            string[] orderList = {
                "       --------------------------------------",
                "      /                                    / ",
                "     /    변경 하고자 하는 슬롯 번호를    /  ",
                "    /            입력해 주세요.          /   ",
                "   /                                    /    ",
                "  --------------------------------------     ",
                " /          Q. 로비로 이동            /      ",
                "--------------------------------------       "
            };

            GameManager.DrawCenterCommandPanel(orderList);
        }

        public void SlotChangeCommandPanelDraw()
        {
            string[] orderList = {
                " --------------------------------------- ",
                "|    슬롯에 담을 캐릭터를 선택하세요.   |",
                "|                                       |",
                "|   |   1   |   2   |   3   |   4   |   |",
                "|   ---------------------------------   |",
                "|   |   5   |   6   |   7   |   8   |   |",
                "|--------------------------------------- ",
                "|               Q. 취소                 |",
                "---------------------------------------- "
            };

            GameManager.DrawCenterCommandPanel(orderList);
        }
    }
}
