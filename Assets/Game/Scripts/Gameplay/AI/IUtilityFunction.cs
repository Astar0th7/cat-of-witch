using Gameplay.Model;
using Gameplay.Model.Enemies;

namespace Model.AI
{
    public interface IUtilityFunction
    {
        bool AppliesTo(Ability ability, PlayerBattleProxy player);
        float GetInput(Ability ability, PlayerBattleProxy player);
        float Score(float input, PlayerBattleProxy player);
        string Name { get; }
    }
}