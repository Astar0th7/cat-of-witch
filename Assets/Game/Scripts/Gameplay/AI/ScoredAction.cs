using Gameplay.Model.Enemies;

namespace Model.AI
{
    public class ScoredAction : EnemyAction
    {
        public float Score { get; }
        
        public ScoredAction(Ability ability, float score)
        {
            AbilityTypeId = ability.Data.TypeId;
            Kind = ability.Data.Kind;
            Score = score;
        }
    }
}