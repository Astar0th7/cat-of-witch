using UnityEngine;

namespace Gameplay.Model.Enemies
{
    public class AbilityHealing : Ability
    {
        public AbilityHealing(EnemyProxy enemy, EnemyStaticData staticData) : base(enemy, staticData) { }

        public override void Apply(PlayerBattleProxy player)
        {
            int health = Random.Range(StaticData.Health.x, StaticData.Health.y + 1);
            Enemy.Heal(health);
        }
    }
}