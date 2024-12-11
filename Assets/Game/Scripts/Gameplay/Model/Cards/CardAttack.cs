using System;
using Gameplay.Model.Enemy;

namespace Gameplay.Model
{
    public class CardAttack : CardBase
    {
        private readonly CardStaticData _data;

        public CardAttack(CardStaticData data) : base(data.Id, data.Type)
        {
            _data = data;
        }

        public override void Apply(PlayerBattleProxy player = null, EnemyProxy enemy = null)
        {
            if (enemy == null)
                throw new ArgumentNullException("Враг не может быть null", nameof(enemy));
            
            enemy.TakeDamage(_data.Damage);
        }
    }
}