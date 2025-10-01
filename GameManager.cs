using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidStrategy
{
    static class GameManager
    {
        public const int BUFFER_SIZE_WIDTH = 240;
        public const int BUFFER_SIZE_HEIGHT = 60;
        public const int HORIZON_AREA = 40;

        // 위 아래 패널을 모두 지웁니다.
        public static void ClearAllPanel()
        {
            ClearVisualPanel();
            ClearCommandPanel();
        }

        // 비주얼 패널을 싹 지웁니다. (콘솔의 윗부분)
        public static void ClearVisualPanel()
        {
            string clearString = "                                                                                                                                                                                                                                              ";

            for (int i = 0; i < HORIZON_AREA - 1; i++)
            {
                Console.SetCursorPosition(1, i + 1);
                Console.Write(clearString);
            }
        }

        // 커맨드 패널을 싹 지웁니다. (콘솔의 아랫부분)
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
