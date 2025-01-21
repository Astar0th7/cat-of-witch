using Model;
using Model.Entities;

namespace Model.Effects
{
    public class BleedingEffect : BaseEffect
    {
        private readonly ITakeDamage _takeDamage;
        private readonly int _damage;

        public BleedingEffect(EffectType type, int duration, int damage) : base(type, duration)
        {
            _damage = damage;
        }
        
        public override void Apply(IEntity entity)
        {
            entity.TakeDamage(_damage);
            base.Apply(entity);
        }
    }
}