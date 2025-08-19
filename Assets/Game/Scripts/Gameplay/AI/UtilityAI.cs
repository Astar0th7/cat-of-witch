using System.Collections.Generic;
using System.Linq;
using Game.Scripts;
using Gameplay.Model;
using Gameplay.Model.Enemies;

namespace Model.AI
{
    public class UtilityAI
    {
        private ICollection<IUtilityFunction> _utilityFunctions;
        
        public EnemyAction MakeBestDecision(EnemyProxy enemy, PlayerBattleProxy player)
        {
            List<ScoredAction> choices = GetScoredActions(enemy, player, ReadyAbilities(enemy)).ToList();
            return choices.FindMax(x => x.Score);
        }

        private IEnumerable<ScoredAction> GetScoredActions(EnemyProxy enemy, PlayerBattleProxy player, IEnumerable<Ability> abilities)
        {
            foreach (Ability ability in abilities)
            {
                float? score = CalculateScore(enemy, player, ability);
                if (score.HasValue == false)
                    continue;
                
                yield return new ScoredAction(ability, score.Value);
            }
        }

        private float? CalculateScore(EnemyProxy enemy, PlayerBattleProxy player, Ability ability)
        {
            List<ScoreFactor> scoreFactors = 
                (from utilityFunction in _utilityFunctions 
                    where utilityFunction.AppliesTo(ability, player)
                    let input = utilityFunction.GetInput(ability, player)
                    let score = utilityFunction.Score(input, player)
                    select new ScoreFactor(utilityFunction.Name, score))
                .ToList();
            
            return scoreFactors.Select(x => x.Score).SumOrNull();
        }
        
        private IEnumerable<Ability> ReadyAbilities(EnemyProxy enemy)
        {
            return enemy.Abilities
                .Where(x => x.IsReady)
                .Select(x => x);
        }
    }
}