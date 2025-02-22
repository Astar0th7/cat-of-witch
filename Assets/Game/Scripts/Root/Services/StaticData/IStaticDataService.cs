using System.Collections.Generic;
using Root.Model;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.UI.View;
using Gameplay.UI.View.Library;
using Gameplay.UI.View.ShopHats;
using Gameplay.View;
using Model.StaticData;
using Root.Model.Settings;
using Root.Services.Audio;
using UnityEngine.Audio;

namespace Root.Services
{
    public interface IStaticDataService
    {
        CardView Card { get; }
        UITimingEffectView TimingEffectPrefab { get; }
        UILibraryCardView LibraryCardView { get; }
        PlayerDataByDefault PlayerDataByDefault { get; }
        SettingsDataByDefault SettingsDataByDefault { get; }
        AudioMixer AudioMixer { get; }
        AudioProvider AudioProvider { get; }
        UIHatSlotView HatSlotView { get; }
        IReadOnlyList<HatData> HatsData { get; }
        CardStaticData GetCardData(string id);
        CardStaticData[] GetAllCards();
        EnemyStaticData GetEnemyData(string id);
        EnemyStaticData[] GetAllEnemies();
    }
}