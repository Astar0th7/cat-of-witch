using NaughtyAttributes;
using UnityEngine;

namespace Model.Effects
{
    [CreateAssetMenu(fileName = "New Effect", menuName = "StaticData/Effect")]
    public class EffectStaticData : ScriptableObject
    {
        [SerializeField] private BaseEffectType _baseType;
        [SerializeField] private EffectType _effectType;
        [SerializeField] private int _duration;
        
        [ShowIf("_effectType", EffectType.Attack)]
        [SerializeField] private int _damage;

        [ShowIf("_effectType", EffectType.Healing)]
        [SerializeField] private int _healing;
        
        [ShowIf("_effectType", EffectType.Defense)]
        [SerializeField] private int _defence;
        
        [ShowIf("_effectType", EffectType.Buff)]
        [SerializeField] private int _otherStats;

        public BaseEffectType BaseType => _baseType;
        public EffectType EffectType => _effectType;
        public int Duration => _duration;
        public int Damage => _damage;
        public int Healing => _healing;
        public int Defence => _defence;
        public int OtherStats => _otherStats;
    }
}