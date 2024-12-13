using System;
using Game.Scripts.Root.Services.Translation;
using Gameplay.Model;
using Gameplay.Presenters;
using Gameplay.View;
using Root.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class CardSpawner : MonoBehaviour
    {
        private Deck _deck;
        private IStaticDataService _staticDataService;
        private ITranslationService _translationService;

        [Inject]
        private void Construct(Deck deck, IStaticDataService staticDataService, ITranslationService translationService)
        {
            _deck = deck;
            _staticDataService = staticDataService;
            _translationService = translationService;
        }
        
        private async void Start()
        {
            CardStaticData[] cardsData = _staticDataService.GetAllCards();
            for (int i = 0; i < 6; i++)
            {
                int index = Random.Range(0, cardsData.Length);
                CardStaticData cardData = cardsData[index];
                CardView cardView = Instantiate(_staticDataService.Card, Vector3.zero, Quaternion.identity);
                int otherStats = cardData.Type switch
                {
                    CardType.None => 0,
                    CardType.Attack => cardData.Damage,
                    CardType.Ability => cardData.OtherStats,
                    CardType.Healing => cardData.Health,
                    _ => 0
                };
                
                CardModel cardModel = cardData.Type switch
                {
                    CardType.None => null,
                    CardType.Attack => new CardAttack(cardData),
                    CardType.Ability => throw new ArgumentOutOfRangeException(),
                    CardType.Healing => new CardHealing(cardData),
                    _ => throw new ArgumentOutOfRangeException()
                };

                CardPresenter card = new(cardView, cardModel);
                card.Initialize();

                string nameCard = await _translationService.Translate($"{cardData.Id}_name", "Cards");
                string descriptionCard = await _translationService.Translate($"{cardData.Id}_description", "Cards");
                
                cardView.SetBaseInfo(cardData.Background, cardData.Icon, nameCard, descriptionCard, cardData.Cost);
                cardView.SetOtherStats(otherStats, cardData.StatsIcon);
                _deck.AddCard(card);
            }
        }
    }
}