using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Model.Enemies
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private int _maxHealth;
        
        [EnumFlags]
        [SerializeField] private AbilityType _abilities;

        [ShowIf("_abilities", AbilityType.Attack)]
        [MinMaxSlider(0, 100)]
        [SerializeField] private Vector2Int _damage;
        
        [FormerlySerializedAs("Health")]
        [ShowIf("_abilities", AbilityType.Healing)]
        [MinMaxSlider(0, 100)]
        [SerializeField] private Vector2Int _health;
        
        public string Id => _id;
        public int MaxHealth => _maxHealth;
        public AbilityType Abilities => _abilities;
        public Vector2Int Damage => _damage;
        public Vector2Int Health => _health;
    }
}