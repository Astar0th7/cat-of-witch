using System;
using Gameplay.Model;
using Gameplay.View;
using Root.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class CardSpawner : MonoBehaviour
    {
        private Deck _deck;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(Deck deck, IStaticDataService staticDataService)
        {
            _deck = deck;
            _staticDataService = staticDataService;
        }
        
        private void Start()
        {
            foreach (CardStaticData cardData in _staticDataService.GetAllCards())
            {
                CardView cardView = Instantiate(_staticDataService.Card, _deck.transform);
                int otherStats = cardData.Type switch
                {
                    CardType.None => 0,
                    CardType.Attack => cardData.Damage,
                    CardType.Ability => cardData.OtherStats,
                    CardType.Healing => cardData.Health,
                    _ => 0
                };
                
                CardBase card = cardData.Type switch
                {
                    CardType.None => null,
                    CardType.Attack => new CardAttack(cardData),
                    CardType.Ability => throw new ArgumentOutOfRangeException(),
                    CardType.Healing => throw new ArgumentOutOfRangeException(),
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                cardView.SetBaseInfo(card, cardData.Icon, cardData.Id, cardData.Id, cardData.Cost);
                cardView.SetOtherStats(otherStats, cardData.StatsIcon);
            }
            
            _deck.Refresh();
        }
    }
}