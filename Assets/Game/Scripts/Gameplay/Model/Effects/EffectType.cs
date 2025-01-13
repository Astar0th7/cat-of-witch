using System;

namespace Model.Effects
{
    public enum EffectType
    {
        None = 0,
        Attack = 1,
        Healing = 2,
        Defense = 3,
        Buff = 4
    }

    public enum BaseEffectType
    {
        None = 0,
        Negative = 1,
        Positive = 2
    }
}