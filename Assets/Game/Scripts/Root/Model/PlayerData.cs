using System;
using System.Collections.Generic;

namespace Root.Model
{
    [Serializable]
    public class PlayerData
    {
        public int MaxHealth;
        public int MaxEnergy;
        public List<string> Deck = new();
    }
}