using System;

namespace Gameplay.Model.Enemies
{
    [Flags]
    public enum AbilityType
    {
        None = 0,
        Attack = 1 << 0,
        Healing = 1 << 1,
        Defence = 1 << 2
    }
}