using Gameplay.Model.Enemy;

namespace Gameplay.Model
{
    public abstract class CardBase
    {
        public string Id { get; protected set; }
        public CardType Type { get; private set; }

        public CardBase(string id, CardType type)
        {
            Id = id;
            Type = type;
        }
        
        public abstract void Apply(PlayerBattleProxy player = null, EnemyProxy enemy = null);
    }
}