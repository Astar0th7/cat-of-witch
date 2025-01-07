using Gameplay.Model.Enemies;
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
        public ReactiveProperty<Vector3> TargetPosition { get; set; } = new(Vector3.zero);
        public ReactiveProperty<Quaternion> TargetRotation { get; set; } = new(Quaternion.identity);

        public CardModel(CardStaticData originalData)
        {
            _originalData = originalData;
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