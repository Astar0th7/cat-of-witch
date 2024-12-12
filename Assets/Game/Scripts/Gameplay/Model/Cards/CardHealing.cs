using Gameplay.Model.Enemy;

namespace Gameplay.Model
{
    public class CardHealing : CardModel
    {
        public CardHealing(CardStaticData data) : base(data) {}

        protected override void Use(PlayerBattleProxy player, EnemyProxy enemy)
        {
            player.Healing(_data.Health);
        }
    }
}