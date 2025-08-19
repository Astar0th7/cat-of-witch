using System;
using System.Collections.Generic;

namespace Gameplay.Model.Enemies
{
    public class AbilityCreator
    {
        public static readonly Dictionary<AbilityTypeId, Func<AbilityStaticData, Ability>> Creator = new()
        {
            {AbilityTypeId.Punch, data => new AbilityAttack(data)},
            {AbilityTypeId.Healing, data => new AbilityHealing(data)},
            {AbilityTypeId.Bleeding, data => new AbilityEffect(data)},
        };
    }
}