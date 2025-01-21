using Model.Entities;
using UnityEngine;

namespace Gameplay.Model.Enemies
{
    public class AbilityAttack : Ability
    {
        public AbilityAttack(AbilityStaticData data) : base(data) {}
        
        public override void Apply(IEntity entity, int modify)
        {
            int damage = Random.Range(Data.Damage, Data.Damage + modify + 1);
            entity.TakeDamage(damage);
        }
    }
}