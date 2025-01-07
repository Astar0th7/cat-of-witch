using System;
using Gameplay.Model.Enemies;

namespace Gameplay.Model
{
    public class CardAttack : CardModel
    {
        public CardAttack(CardStaticData originalData) : base(originalData) {}

        protected override void Use(PlayerBattleProxy player, EnemyProxy enemy)
        {
            enemy.TakeDamage(_originalData.Damage);
        }
    }
}