using Gameplay.Model.Enemies;

namespace Gameplay.Model
{
    public class CardHealing : CardModel
    {
        public CardHealing(CardStaticData originalData) : base(originalData) {}

        protected override void Use(PlayerBattleProxy player, EnemyProxy enemy)
        {
            player.Healing(_originalData.Health);
        }
    }
}