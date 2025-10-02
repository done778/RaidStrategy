using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class BattleField
    {

        public BattleField(Player player)
        {
            new Level(player.ClearLevel + 1);
        }

        public void StartBattle()
        {

        }
    }
}
