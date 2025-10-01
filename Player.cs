using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidStrategy
{
    class Player
    {
        public string Name { get; set; }
        public string Affiliation { get; set; }

        Inventory inventory;
        MyDeck deck;

        public Player(string _name, string _aff) 
        {
            Name = _name;
            Affiliation = _aff;
            inventory = new Inventory();
            deck = new MyDeck();
        }
    }
}
