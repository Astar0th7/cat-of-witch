using System;
using Model;
using Model.Entities;

namespace Model.Effects
{
    public abstract class BaseEffect
    {
        public int Duration { get; private set; }
        
        public EffectType Type { get; set; }

        public BaseEffect(EffectType type, int duration)
        {
            Type = type;
            Duration = duration;
        }
        
        public void AddTime(int duration)
        {
            Duration += duration;
        }

        public virtual void Apply(IEntity entity)
        {
            Duration--;
        }
    }
}