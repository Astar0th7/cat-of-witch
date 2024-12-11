using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Model
{
    [CreateAssetMenu(fileName = "New CardData", menuName = "StaticData/Card")]
    public class CardStaticData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private CardType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _cost;
        
        [HideIf("_type", CardType.None)] 
        [SerializeField] private Sprite _statsIcon;
        
        [ShowIf("_type", CardType.Attack)] 
        [SerializeField] private int _damage;
        
        [ShowIf("_type", CardType.Healing)] 
        [SerializeField] private int _health;
        
        [ShowIf("_type", CardType.Ability)] 
        [SerializeField] private int _otherStats;
        
        public string Id => _id;
        public CardType Type => _type;
        public Sprite Icon => _icon;
        public int Cost => _cost;
        public int Damage => _damage;
        public int Health => _health;
        public int OtherStats => _otherStats;
        public Sprite StatsIcon => _statsIcon;
    }
}