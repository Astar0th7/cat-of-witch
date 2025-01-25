using Model;
using Model.Entities;

namespace Model.Effects
{
    public class BleedingEffect : BaseEffect
    {
        private readonly ITakeDamage _takeDamage;
        private readonly int _damage;

        public BleedingEffect(string id, int duration, int damage) : base(id, duration)
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