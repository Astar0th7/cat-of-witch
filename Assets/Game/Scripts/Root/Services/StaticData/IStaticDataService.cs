using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.View;

namespace Root.Services
{
    public interface IStaticDataService
    {
        CardStaticData GetCardData(string id);
        CardStaticData[] GetAllCards();
        CardView Card { get; }
        EnemyStaticData GetEnemyData(string id);
        EnemyStaticData[] GetAllEnemies();
    }
}