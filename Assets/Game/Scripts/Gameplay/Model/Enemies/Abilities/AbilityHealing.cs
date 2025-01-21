using Model.Entities;
using UnityEngine;

namespace Gameplay.Model.Enemies
{
    public class AbilityHealing : Ability
    {
        public AbilityHealing(AbilityStaticData data) : base(data) { }

        public override void Apply(IEntity entity, int modify)
        {
            int health = Random.Range(Data.Health, Data.Health + modify + 1);
            entity.Heal(health);
        }
    }
}