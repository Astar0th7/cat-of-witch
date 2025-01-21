using System;
using System.Collections.Generic;

namespace Gameplay.Model.Enemies
{
    public class AbilityCreator
    {
        public static readonly Dictionary<string, Func<AbilityStaticData, Ability>> Creator = new()
        {
            {"Punch", data => new AbilityAttack(data)},
            {"Heal", data => new AbilityHealing(data)},
        };
    }
}