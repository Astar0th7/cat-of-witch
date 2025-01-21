using Gameplay.Model.Enemies;
using Model.Effects;

namespace Gameplay.Model
{
    public class CardTimingEffect : CardModel
    {
        private readonly BaseEffect _effect;

        public CardTimingEffect(CardStaticData originalData, BaseEffect effect) : base(originalData)
        {
            _effect = effect;
        }

        protected override void Use(PlayerBattleProxy player, EnemyProxy enemy)
        {
            if (_originalData.EffectData.BaseType == BaseEffectType.Positive)
                player.TimingEffects.AddEffect(_effect);
            else
                enemy.TimingEffects.AddEffect(_effect);
        }
    }
}