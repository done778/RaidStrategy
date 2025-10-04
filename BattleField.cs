using System;
using System.Collections.Generic;
using System.Threading;

namespace RaidStrategy
{
    // BattleField의 역할 : 콘솔을 업데이트 하는 역할
    class BattleField
    {
        int interval;
        int cursorY;
        int cursorX;

        // 콘솔에 그림을 그릴 위치의 기준 초기화
        public BattleField()
        {
            interval = GameManager.BUFFER_SIZE_WIDTH / 3 * 2 / 4; // 40
            cursorY = GameManager.HORIZON_AREA / 4;
            cursorX = (GameManager.BUFFER_SIZE_WIDTH / 3 * 2) + 3;
        }

        // 패널 업데이트, 전장을 그리고, 현재 아군 캐릭터와, 적을 그립니다.
        public void PanelUpdate(List<Ally> allies, Enemy enemy, string[] log = null)
        {
            GameManager.ClearAllPanel();
            DrawBattleField();
            DrawCharacter(allies);
            DrawEnemy(enemy);
            if (log != null) 
            {
                GameManager.ClearCommandPanel();
                GameManager.DrawCenterCommandPanel(log);
            }
        }

        // 아군 캐릭터를 모두 그립니다.
        public void DrawCharacter(List<Ally> allies)
        {
            for (int i = 0; i < allies.Count; i++) 
            {
                allies[i].DrawAsciiArt(cursorX + 7 - (interval * (i + 1)), cursorY + 1, false);

                int pivotX = (interval / 2) - (interval * (i + 1));
                int pivotY = GameManager.HORIZON_AREA / 2;

                Console.SetCursorPosition(cursorX + pivotX - allies[i].Name.Length, cursorY + (pivotY + 3));
                Console.Write(allies[i].Name);

                Console.SetCursorPosition(cursorX + pivotX - 10, cursorY + (pivotY + 5));
                Console.Write("공격력");

                Console.SetCursorPosition(cursorX + pivotX + 4, cursorY + (pivotY + 5));
                Console.Write("체  력");

                Console.SetCursorPosition(cursorX + pivotX - 7, cursorY + (pivotY + 6));
                Console.Write(allies[i].StatusAttack);

                Console.SetCursorPosition(cursorX + pivotX + 6, cursorY + (pivotY + 6));
                Console.Write(allies[i].StatusHealth);
            }
        }

        // 아군 캐릭터의 죽는 애니메이션을 그립니다.
        // 매개 변수는 어느 위치에 그릴지 결정합니다.
        public void DrawDeathAlly(int index)
        {
            int startX = cursorX + 7 - (interval * (index + 1));
            int startY = cursorY + 1;
            string[] drawAscii =
            {
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                                ",
                "                   :%%**%#-     ",
                "                  *%:    :*+    ",
                "                  #-      =*:   ",
                "             .-%= *%     :*+    ",
                "          .#@#.    :%@+*%#-     ",
                "        .*%-          .         ",
                "       .#*.          %+         ",
                "       .*..#####:.   %*         ",
                "     .=#: ....:+%-   +@         ",
                "    .:*=.   ..+%-.   =@.        ",
                "  -=+*%-    .##:.    .@-.       ",
                " .:-:       .-:.      --.       ",
                "                                "
            };
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < drawAscii.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(drawAscii[i]);
            }
            Console.ResetColor();
            for (int i = 0; i < drawAscii.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                for (int j = 0; j < drawAscii[0].Length; j++)
                {
                    Console.Write(" ");
                }
                Thread.Sleep(50);
            }
        }

        // 적 캐릭터를 그립니다. Death 변수가 true면 죽는 애니메이션으로 바뀝니다.
        public void DrawEnemy(Enemy enemy, bool Death = false)
        {
            enemy.DrawAsciiArt(Death);
            if (!Death) { DrawEnemyInfo(enemy); }
        }

        // 적 정보를 그립니다.
        public void DrawEnemyInfo(Enemy enemy)
        {
            string[] template = {
                " ---------------------------------------------- ",
                "|                                              |",
                "|                                              |",
                "|         공격력                체  력         |",
                "|                                              |",
                " ---------------------------------------------- "
            };
            int cursorX = (GameManager.BUFFER_SIZE_WIDTH - template[0].Length) / 2;
            for (int i = 0; i < template.Length; i++)
            {
                Console.SetCursorPosition(cursorX, i + 2);
                Console.Write(template[i]);
            }
            cursorX = (GameManager.BUFFER_SIZE_WIDTH / 2);
            Console.SetCursorPosition(cursorX - enemy.Name.Length, 3);
            Console.Write(enemy.Name);
            Console.SetCursorPosition(cursorX - (template[0].Length / 4 - 1), 6);
            Console.Write(enemy.StatusAttack);
            Console.SetCursorPosition(cursorX + (template[0].Length / 4 - 2), 6);
            Console.Write(enemy.StatusHealth);
        }

        // 전장의 틀을 그립니다.
        public void DrawBattleField()
        {
            Console.SetCursorPosition(1, cursorY - 1);
            for (int i = 0; i < GameManager.BUFFER_SIZE_WIDTH / 3; i++)
            {
                Console.Write("- ");
            }
            Console.SetCursorPosition(1, cursorY + GameManager.HORIZON_AREA / 2 + 1);
            for (int i = 0; i < GameManager.BUFFER_SIZE_WIDTH / 3; i++)
            {
                Console.Write("- ");
            }
        }

        // 최초 전투 시작 시 안내 메시지
        public void ShowStartMessage()
        {
            string[] msg = {
                "엔터 키를 누를 때마다 한 턴씩 전투가 진행 됩니다.",
                "     전투를 시작하려면 엔터 키를 눌러주세요.     "
            };
            GameManager.DrawCenterCommandPanel(msg);
        }
    }
}
