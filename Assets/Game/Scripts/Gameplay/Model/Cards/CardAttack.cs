using System;
using Gameplay.Model.Enemies;

namespace Gameplay.Model
{
    public class CardAttack : CardModel
    {
        public CardAttack(CardStaticData data) : base(data) {}

        protected override void Use(PlayerBattleProxy player, EnemyProxy enemy)
        {
            enemy.TakeDamage(_data.Damage);
        }
    }
}