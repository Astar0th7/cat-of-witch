using Model.Effects;
using Model.Entities;

namespace Gameplay.Model.Enemies
{
    public class AbilityEffect : Ability
    {
        public AbilityEffect(AbilityStaticData data) : base(data) { }

        public override void Apply(IEntity entity, int modify)
        {
            BaseEffect effect = EffectCreator.Creator[Data.Effect.Id](Data.Effect);
            entity.TimingEffects.AddEffect(effect);
        }
    }
}