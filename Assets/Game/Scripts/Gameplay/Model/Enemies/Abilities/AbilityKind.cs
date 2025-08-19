using System;

namespace Gameplay.Model.Enemies
{
    public enum AbilityKind
    {
        None = 0,
        Damage = 1,
        Healing = 2,
        Effect = 3,
    }

    public enum BaseAbilityType
    {
        None = 0,
        Negative = 1,
        Positive = 2
    }

    public enum AbilityTypeId
    {
        None = 0,
        Healing = 1,
        Punch = 2,
        Bleeding = 3
    }
}