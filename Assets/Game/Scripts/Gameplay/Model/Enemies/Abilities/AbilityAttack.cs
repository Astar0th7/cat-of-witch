using UnityEngine;

namespace Gameplay.Model.Enemies
{
    public class AbilityAttack : Ability
    {
        public AbilityAttack(EnemyProxy enemy, EnemyStaticData staticData) : base(enemy, staticData)
        {
        }
        
        public override void Apply(PlayerBattleProxy player)
        {
            int damage = Random.Range(StaticData.Damage.x, StaticData.Damage.y + 1);
            player.TakeDamage(damage);
        }
    }
}