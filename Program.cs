using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetBufferSize(GameManager.BUFFER_SIZE_WIDTH, GameManager.BUFFER_SIZE_HEIGHT);
            
            Title title = new Title();
            string playerName;
            string playerAffillation;

            title.GameStart();
            playerName = title.InputPlayerName();
            playerAffillation = title.InputPlayerAff();

            Player player = new Player(playerName, playerAffillation);

            Console.CursorVisible = false;

            Lobby lobby = new Lobby();
            lobby.EnterLobby();

            Console.ReadLine();
        }
    }
}
