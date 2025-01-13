using System;

namespace Model.Effects
{
    public abstract class BaseEffect
    {
        public int Duration { get; private set; }

        public event Action<BaseEffect> TimingEffectEndedEvent;
        
        public EffectType Type { get; set; }

        public BaseEffect(EffectType type)
        {
            Type = type;
        }
        
        public void AddTime(int duration)
        {
            Duration += duration;
        }

        public virtual void Apply()
        {
            Duration--;
            
            if (Duration <= 0)
                TimingEffectEndedEvent?.Invoke(this);
        }
    }
}