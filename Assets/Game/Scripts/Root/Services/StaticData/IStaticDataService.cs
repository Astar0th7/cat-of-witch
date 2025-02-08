using Root.Model;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.UI.View;
using Gameplay.UI.View.Library;
using Gameplay.View;
using Root.Model.Settings;

namespace Root.Services
{
    public interface IStaticDataService
    {
        CardView Card { get; }
        UITimingEffectView TimingEffectPrefab { get; }
        UILibraryCardView LibraryCardView { get; }
        PlayerDataByDefault PlayerDataByDefault { get; }
        SettingsDataByDefault SettingsDataByDefault { get; }
        CardStaticData GetCardData(string id);
        CardStaticData[] GetAllCards();
        EnemyStaticData GetEnemyData(string id);
        EnemyStaticData[] GetAllEnemies();
    }
}