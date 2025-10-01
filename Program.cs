using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class Program
    {
        public const int BUFFER_SIZE_WIDTH = 240;
        public const int BUFFER_SIZE_HEIGHT = 60;
        public const int HORIZON_AREA = 40;

        static void Main(string[] args)
        {
            Console.SetBufferSize(BUFFER_SIZE_WIDTH, BUFFER_SIZE_HEIGHT);
            
            Console.WriteLine("================================================================================================================================================================================================================================================");
            Console.WriteLine("\n                   위 선이 콘솔 창에 한 줄에 딱 맞게 표시되도록 창 크기를 늘려 주세요.\n                             세로 역시 충분히 늘려주시기 바랍니다.\n                              준비되었다면 엔터 키를 눌러주세요.");
            Console.ReadLine();

            for (int i = 1; i < BUFFER_SIZE_HEIGHT - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(BUFFER_SIZE_WIDTH-1, i);
                Console.Write("|");
            }
            Console.WriteLine("================================================================================================================================================================================================================================================");
            Console.SetCursorPosition(0, HORIZON_AREA);
            Console.WriteLine("================================================================================================================================================================================================================================================");

            Console.SetCursorPosition(BUFFER_SIZE_WIDTH / 2 - 45, HORIZON_AREA / 2);
            Console.WriteLine("화면의 테두리가 잘 나타났다면 엔터를 눌러주세요. 그렇지 않다면 창 크기를 조절해 주세요.");

            Console.ReadLine();

            Title title = new Title();
            title.TitleScreen();

            Console.ReadKey(true);

        }
    }
}
