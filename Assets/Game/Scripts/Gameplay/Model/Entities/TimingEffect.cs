using System.Collections.Generic;
using System.Linq;
using Model.Effects;

namespace Model.Entities
{
    public class TimingEffect
    {
        public List<BaseEffect> Effects { get; private set; } = new();

        public void AddEffect(BaseEffect newEffect)
        {
            BaseEffect effect = Effects.FirstOrDefault(x => x.Type == newEffect.Type);

            if (effect != null)
            {
                effect.AddTime(newEffect.Duration);
                return;
            }            
            
            Effects.Add(newEffect);
            newEffect.TimingEffectEndedEvent += OnTimingEffectEnded;
        }

        private void OnTimingEffectEnded(BaseEffect effect)
        {
            effect.TimingEffectEndedEvent -= OnTimingEffectEnded;
            Effects.Remove(effect);
        }
    }
}