using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Model
{
    [CreateAssetMenu(fileName = "New PlayerDataByDefault", menuName = "StaticData/PlayerDataByDefault")]
    public class PlayerDataByDefault : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _maxEnergy;
        [SerializeField] private List<CardStaticData> _deck;
        
        public int MaxHealth => _maxHealth;
        public int MaxEnergy => _maxEnergy;
        public List<CardStaticData> Deck => _deck;
    }
}