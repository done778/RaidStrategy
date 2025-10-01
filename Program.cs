using System;
using System.Collections.Generic;
using System.Reflection.Emit;

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
            Title title = new Title();
            string playerName;
            string playerAffillation;

            title.GameStart();
            playerName = title.InputPlayerName();
            playerAffillation = title.InputPlayerAff();

            Player player = new Player(playerName, playerAffillation);

            Lobby lobby = new Lobby();
            lobby.EnterLobby();
        }
    }
}
