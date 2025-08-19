using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Root.Model
{
    [Serializable]
    public class PlayerData
    {
        public int MaxHealth;
        public int MaxEnergy;
        public List<string> Deck = new();
        public string HatId;
        [FormerlySerializedAs("HatsBuyedId")] public List<string> HatsBuyId = new();
    }
}