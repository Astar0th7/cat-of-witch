using Model.Effects;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Model.Enemies
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "StaticData/EnemyAbility")]
    public class AbilityStaticData : ScriptableObject
    {
        [field: SerializeField] public AbilityTypeId TypeId { get; private set; }
        [field: SerializeField] public AbilityKind Kind { get; private set; }
        
        [SerializeField] private BaseAbilityType _baseType;

        [ShowIf("_type", AbilityKind.Damage)]
        [SerializeField] private int _damage;

        [ShowIf("_type", AbilityKind.Healing)]
        [SerializeField] private int _health;
        
        [ShowIf("_type", AbilityKind.Effect)]
        [SerializeField] private EffectStaticData _effect;
        
        public BaseAbilityType BaseType => _baseType;
        public int Damage => _damage;
        public int Health => _health;
        public EffectStaticData Effect => _effect;
    }
}