using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model.Effects;
using ObservableCollections;

namespace Model.Entities
{
    public class TimingEffect
    {
        public ObservableList<BaseEffect> Effects { get; } = new();
        
        public void AddEffect(BaseEffect newEffect)
        {
            BaseEffect effect = Effects.FirstOrDefault(x => x.Id == newEffect.Id);

            if (effect != null)
            {
                effect.AddTime(newEffect.Duration.CurrentValue);
                return;
            }            
            
            Effects.Add(newEffect);
        }

        public void CheckEndedEffects()
        {
            BaseEffect[] effects = Effects.Where(x => x.Duration.CurrentValue <= 0).ToArray();
            foreach (BaseEffect effect in effects)
                Effects.Remove(effect);
        }
    }
}