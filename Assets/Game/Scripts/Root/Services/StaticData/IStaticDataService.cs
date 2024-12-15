using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.View;

namespace Root.Services
{
    public interface IStaticDataService
    {
        CardView Card { get; }
        CardStaticData GetCardData(string id);
        CardStaticData[] GetAllCards();
        EnemyStaticData GetEnemyData(string id);
        EnemyStaticData[] GetAllEnemies();
    }
}