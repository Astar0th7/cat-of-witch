using System;
using Cysharp.Threading.Tasks;
using Root.Services.Translation;
using Gameplay.Model;
using Gameplay.Presenters;
using Gameplay.View;
using Root.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class BattleFactory : IBattleFactory
    {
        private readonly DiContainer _container;
        private readonly ITranslationService _translationService;
        private readonly IStaticDataService _staticDataService;

        public BattleFactory(
            DiContainer container, 
            ITranslationService translationService, 
            IStaticDataService staticDataService)
        {
            _container = container;
            _translationService = translationService;
            _staticDataService = staticDataService;
        }

        public async UniTask<CardPresenter> CreateCard(CardStaticData data)
        {
            CardView cardView = Instantiate(_staticDataService.Card, Vector3.zero, Quaternion.identity);
            int otherStats = data.Type switch
            {
                CardType.None => 0,
                CardType.Attack => data.Damage,
                CardType.Ability => data.OtherStats,
                CardType.Healing => data.Health,
                _ => 0
            };
                
            CardModel cardModel = data.Type switch
            {
                CardType.None => null,
                CardType.Attack => new CardAttack(data),
                CardType.Ability => throw new ArgumentOutOfRangeException(),
                CardType.Healing => new CardHealing(data),
                _ => throw new ArgumentOutOfRangeException()
            };

            CardPresenter card = new(cardView, cardModel);
            card.Initialize();

            string nameCard = await _translationService.Translate($"{data.Id}_name", "Cards");
            string descriptionCard = await _translationService.Translate($"{data.Id}_description", "Cards");
                
            cardView.SetBaseInfo(data.Background, data.Icon, nameCard, descriptionCard, data.Cost);
            cardView.SetOtherStats(otherStats, data.StatsIcon);

            return card;
        }

        private T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
        {
            return _container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }
    }
}