using System;
using System.Collections.Generic;

namespace Model.Effects
{
    public static class EffectCreator
    {
        public static readonly Dictionary<string, Func<EffectStaticData, BaseEffect>> Creator = new()
        {
            {"Bleeding", data => new BleedingEffect(data.EffectType, data.Duration, data.Damage)},
        };
    }
}