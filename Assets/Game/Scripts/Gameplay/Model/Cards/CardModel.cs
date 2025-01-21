using System;
using Gameplay.Model.Enemies;
using Model.Effects;
using R3;
using UnityEngine;

namespace Gameplay.Model
{
    public abstract class CardModel
    {
        protected readonly CardStaticData _originalData;
        
        public CardStaticData OriginalData => _originalData;
        public string Id => _originalData.Id;
        public CardType Type => _originalData.Type;
        
        public int Stats { get; private set; }
        public ReactiveProperty<Vector3> TargetPosition { get; set; } = new(Vector3.zero);
        public ReactiveProperty<Quaternion> TargetRotation { get; set; } = new(Quaternion.identity);

        public CardModel(CardStaticData originalData)
        {
            _originalData = originalData;

            SetCardStats();
        }

        private void SetCardStats()
        {
            Stats = _originalData.Type switch
            {
                CardType.None => 0,
                CardType.Attack => _originalData.Damage,
                CardType.Ability => _originalData.OtherStats,
                CardType.Healing => _originalData.Health,
                _ => 0
            };

            if (_originalData.Type == CardType.Effect)
            {
                Stats = _originalData.EffectData.EffectType switch
                {
                    EffectType.Attack => _originalData.EffectData.Damage,
                    EffectType.Healing => _originalData.EffectData.Healing,
                    EffectType.Defense => _originalData.EffectData.Defence,
                    EffectType.Buff => _originalData.EffectData.OtherStats,
                };
            }
        }


        public bool TryApply(PlayerBattleProxy player, EnemyProxy enemy)
        {
            if (player.TryCast(_originalData.Cost) == false) 
                return false;
            
            Use(player, enemy);
            return true;
        }
        
        protected abstract void Use(PlayerBattleProxy player, EnemyProxy enemy);
    }
}