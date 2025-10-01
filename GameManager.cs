using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    static class GameManager
    {
        public const int BUFFER_SIZE_WIDTH = 240;
        public const int BUFFER_SIZE_HEIGHT = 60;
        public const int HORIZON_AREA = 40;

        // 비주얼 패널에 매개변수로 받은 문자열 배열을 중앙에 그립니다.
        public static void DrawCenterVisualPanel(string[] drawResource)
        {
            for (int i = 0; i < drawResource.Length; i++)
            {
                int Cursor_X = (BUFFER_SIZE_WIDTH - drawResource[0].Length) / 2;
                int Cursor_Y = (HORIZON_AREA - drawResource.Length) / 2;

                Console.SetCursorPosition(Cursor_X, Cursor_Y + i);
                Console.Write(drawResource[i]);
            }
        }

        // 커맨드 패널에 매개변수로 받은 문자열 배열을 중앙에 그립니다.
        public static void DrawCenterCommandPanel(string[] drawResource)
        {
            for (int i = 0; i < drawResource.Length; i++)
            {
                int Cursor_X = (BUFFER_SIZE_WIDTH - drawResource[0].Length) / 2;
                int Cursor_Y = HORIZON_AREA + ((BUFFER_SIZE_HEIGHT - HORIZON_AREA) / 2) - (drawResource.Length / 2);

                Console.SetCursorPosition(Cursor_X, Cursor_Y + i);
                Console.Write(drawResource[i]);
            }
        }

        // 위 아래 패널을 모두 지웁니다.
        public static void ClearAllPanel()
        {
            ClearVisualPanel();
            ClearCommandPanel();
        }

        // 비주얼 패널을 지웁니다. (콘솔의 윗부분)
        public static void ClearVisualPanel()
        {
            string clearString = "                                                                                                                                                                                                                                              ";

            for (int i = 0; i < HORIZON_AREA - 1; i++)
            {
                Console.SetCursorPosition(1, i + 1);
                Console.Write(clearString);
            }
        }

        // 커맨드 패널을 지웁니다. (콘솔의 아랫부분)
        public static void ClearCommandPanel()
        {
            string clearString = "                                                                                                                                                                                                                                              ";

            for (int i = 0; i < BUFFER_SIZE_HEIGHT - HORIZON_AREA - 2; i++)
            {
                Console.SetCursorPosition(1, HORIZON_AREA + i + 1);
                Console.Write(clearString);
            }
        }
    }
}
