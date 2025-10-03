using System;
using System.Collections.Generic;

namespace RaidStrategy
{
    class BattleField
    {
        Player Player { get; set; }
        List<Ally> allies;
        public BattleField(Player player, Ally[] deck)
        {
            Player = player;
            new Level(Player.ClearLevel + 1);
            allies = new List<Ally>(GameManager.DECK_CAPACITY);
            for (int i = 0; i < deck.Length; i++) {
                if (deck[i] != null)
                {
                    allies.Add(deck[i].GetClone());
                }
            }
        }

        public void StartBattle()
        {
        }
    }
}
