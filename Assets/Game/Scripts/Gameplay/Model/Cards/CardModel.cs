using Gameplay.Model.Enemies;
using R3;
using UnityEngine;

namespace Gameplay.Model
{
    public abstract class CardModel
    {
        protected readonly CardStaticData _data;
        public string Id => _data.Id;
        public CardType Type => _data.Type;

        public ReactiveProperty<Vector3> TargetPosition { get; set; } = new(Vector3.zero);

        public CardModel(CardStaticData data)
        {
            _data = data;
        }

        public bool TryApply(PlayerBattleProxy player, EnemyProxy enemy)
        {
            if (player.TryCast(_data.Cost) == false) 
                return false;
            
            Use(player, enemy);
            return true;
        }
        
        protected abstract void Use(PlayerBattleProxy player, EnemyProxy enemy);
    }
}