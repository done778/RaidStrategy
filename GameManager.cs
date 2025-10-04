using System;
using System.Collections.Generic;
using System.Linq;

namespace RaidStrategy
{
    static class GameManager
    {
        public const int BUFFER_SIZE_WIDTH = 240;
        public const int BUFFER_SIZE_HEIGHT = 60;
        public const int HORIZON_AREA = 40;
        public const int INVENTORY_CAPACITY = 8;
        public const int DECK_CAPACITY = 4;
        public const int BATTLE_INTERVAL_TIME = 2500;
        public const int LOG_QUEUE_CAPACITY = 16;
        private static string[] LOG_QUEUE = new string[LOG_QUEUE_CAPACITY];
        private static int LOG_HEAD = 0;
        private static int LOG_TAIL = 0;

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

        // 전투에서 사용될 로그 내역을 큐처럼 동작하는 배열
        // (용량 다차면 오래된 것부터 덮어씌움)
        public static void AddLogInQueue(string[] logResource)
        {
            for (int i = 0; i < logResource.Length; i++) 
            {
                LOG_QUEUE[LOG_TAIL] = logResource[i];
                if (LOG_TAIL < LOG_HEAD)
                {
                    LOG_HEAD++;
                    if (LOG_HEAD >= LOG_QUEUE_CAPACITY)
                    {
                        LOG_HEAD = 0;
                    }
                }
                LOG_TAIL++;
                if (LOG_TAIL >= LOG_QUEUE_CAPACITY)
                {
                    LOG_TAIL = 0;
                    LOG_HEAD = 1;
                }
            }
            if (logResource[0] == "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ")
            {
                return;
            }
            AddLogInQueue(new string[] { "ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ" });
            OutputLogQueue();
        }
        // 로그 출력
        public static void OutputLogQueue()
        {
            int cursorX = BUFFER_SIZE_WIDTH / 2;
            int cursorY = (BUFFER_SIZE_HEIGHT - HORIZON_AREA) / 2 + HORIZON_AREA;
            ClearCommandPanel();

            if (LOG_HEAD <= LOG_TAIL)
            {
                cursorY -= (LOG_TAIL - LOG_HEAD) / 2;
                for (int i = LOG_HEAD; i < LOG_TAIL; i++)
                {
                    Console.SetCursorPosition(cursorX - LOG_QUEUE[i].Length, cursorY++);
                    Console.Write(LOG_QUEUE[i]);
                }
            }
            else
            {
                int index = 0;
                cursorY -= LOG_QUEUE_CAPACITY / 2;
                if (LOG_HEAD - 1 < 0)
                {
                    index = LOG_QUEUE_CAPACITY - 1;
                }
                else
                {
                    index = LOG_HEAD -1;
                }

                for (int i = 0; i < LOG_QUEUE_CAPACITY; i++)
                {
                    Console.SetCursorPosition(cursorX - LOG_QUEUE[index].Length, cursorY++);
                    Console.Write(LOG_QUEUE[index]);
                    index++;
                    if (index >= LOG_QUEUE_CAPACITY)
                    {
                        index = 0;
                    }
                }
            }
        }
    }
}
