using System;
using System.Collections.Generic;

namespace Gameplay.Model
{
    [Serializable]
    public class PlayerData
    {
        public int MaxHealth;
        public int MaxEnergy;
        public List<string> Deck;
    }
}