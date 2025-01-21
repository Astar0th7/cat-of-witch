using Model.Entities;

namespace Gameplay.Model.Enemies
{
    public abstract class Ability
    {
        public AbilityStaticData Data { get; }

        public Ability(AbilityStaticData data)
        {
            Data = data;
        }
        
        public abstract void Apply(IEntity player, int modify);
    }
}