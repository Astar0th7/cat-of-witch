using Model.Entities;

namespace Model.Effects
{
    public class BleedingEffect : BaseEffect
    {
        private readonly ITakeDamage _takeDamage;
        private readonly int _damage;

        public BleedingEffect(EffectType type, ITakeDamage takeDamage, int damage) : base(type)
        {
            _takeDamage = takeDamage;
            _damage = damage;
        }
        
        public override void Apply()
        {
            _takeDamage.TakeDamage(_damage);
            base.Apply();
        }
    }
}