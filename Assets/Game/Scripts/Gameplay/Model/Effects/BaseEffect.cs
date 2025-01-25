using System;
using Model;
using Model.Entities;
using R3;

namespace Model.Effects
{
    public abstract class BaseEffect
    {
        private ReactiveProperty<int> _duration { get; } = new();
        
        public ReadOnlyReactiveProperty<int> Duration => _duration;
        public string Id { get; set; }

        public BaseEffect(string id, int duration)
        {
            Id = id;
            _duration.Value = duration;
        }
        
        public void AddTime(int duration)
        {
            _duration.Value += duration;
        }

        public virtual void Apply(IEntity entity)
        {
            _duration.Value--;
        }
    }
}