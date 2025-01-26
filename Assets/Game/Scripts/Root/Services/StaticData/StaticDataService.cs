using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Root;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.UI.View;
using Gameplay.UI.View.Library;
using Gameplay.View;
using UnityEngine;

namespace Root.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<string, EnemyStaticData> _enemiesData;
        private readonly Dictionary<string, CardStaticData> _cardsData;
        public CardView Card { get; }
        public UITimingEffectView TimingEffectPrefab { get; }
        public UILibraryCardView LibraryCardView { get; }

        public StaticDataService()
        {
            TimingEffectPrefab = Resources.Load<UITimingEffectView>(AssetsPath.TIMING_EFFECT_PREFAB_PATH);
            LibraryCardView = Resources.Load<UILibraryCardView>(AssetsPath.LIBRARY_CARD_PREFAB_PATH);
            
            _cardsData = Resources.LoadAll<CardStaticData>(AssetsPath.CARDS_DATA_PATH).ToDictionary(x => x.Id, x => x);
            Card = Resources.Load<CardView>(AssetsPath.CARD_VIEW_PATH);
            
            _enemiesData = Resources.LoadAll<EnemyStaticData>(AssetsPath.ENEMIES_DATA_PATH).ToDictionary(x => x.Id, x => x);
        }
        
        public CardStaticData GetCardData(string id)
        {
            if (_cardsData.TryGetValue(id, out CardStaticData card) == false)
                throw new Exception($"Карата с таким id - {id} не найдена");
            
            return card;
        }

        public CardStaticData[] GetAllCards()
        {
            return _cardsData.Values.ToArray();
        }

        public EnemyStaticData GetEnemyData(string id)
        {
            if (_enemiesData.TryGetValue(id, out EnemyStaticData enemy) == false)
                throw new Exception($"Врага с таким id - {id} не найдено");
            
            return enemy;
        }

        public EnemyStaticData[] GetAllEnemies()
        {
            return _enemiesData.Values.ToArray();
        }
    }
}