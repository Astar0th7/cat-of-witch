using Model.Effects;
using NaughtyAttributes;
using UnityEngine;

namespace Gameplay.Model.Enemies
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "StaticData/EnemyAbility")]
    public class AbilityStaticData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private BaseAbilityType _baseType;
        [SerializeField] private AbilityType _type;

        [ShowIf("_type", AbilityType.Attack)]
        [SerializeField] private int _damage;

        [ShowIf("_type", AbilityType.Healing)]
        [SerializeField] private int _health;
        
        [ShowIf("_type", AbilityType.Effect)]
        [SerializeField] private EffectStaticData _effect;
        
        public string Id => _id;
        public BaseAbilityType BaseType => _baseType;
        public AbilityType Type => _type;
        public int Damage => _damage;
        public int Health => _health;
        public EffectStaticData Effect => _effect;
    }
}