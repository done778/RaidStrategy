using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidStrategy
{
    static class GameManager
    {
        // 패널을 모두 지웁니다.
        public static void ClearAllPanel()
        {
            ClearVisualPanel();
            ClearCommandPanel();
        }

        // 비주얼 패널을 싹 지웁니다. (콘솔의 윗부분)
        public static void ClearVisualPanel()
        {
            string clearString = "                                                                                                                                                                                                                                              ";

            for (int i = 0; i < Program.HORIZON_AREA - 1; i++)
            {
                Console.SetCursorPosition(1, i + 1);
                Console.Write(clearString);
            }
        }

        // 커맨드 패널을 싹 지웁니다. (콘솔의 아랫부분)
        public static void ClearCommandPanel()
        {
            string clearString = "                                                                                                                                                                                                                                              ";

            for (int i = 0; i < Program.BUFFER_SIZE_HEIGHT - Program.HORIZON_AREA - 2; i++)
            {
                Console.SetCursorPosition(1, Program.HORIZON_AREA + i + 1);
                Console.Write(clearString);
            }
        }
    }
}
