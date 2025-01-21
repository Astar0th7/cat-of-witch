using System.Collections.Generic;
using Model.Effects;
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
        [SerializeField] private int _damage;
        [SerializeField] private int _heal;
        [SerializeField] private List<AbilityStaticData> _abilities;
        
        
        public string Id => _id;
        public int MaxHealth => _maxHealth;
        public int Damage => _damage;
        public int Heal => _heal;
        public IReadOnlyList<AbilityStaticData> Abilities => _abilities;
    }
}