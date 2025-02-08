using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Root.Model;
using Gameplay.Services;
using Gameplay.Model;
using Gameplay.Presenters;
using Root.Services;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class CardSpawner
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IBattleFactory _factory;
        private readonly PlayerProxy _player;

        private CardSpawner(IStaticDataService staticDataService, IBattleFactory factory, PlayerProxy player)
        {
            _staticDataService = staticDataService;
            _factory = factory;
            _player = player;
        }

        public async UniTask<CardPresenter[]> SpawnCards(int amount)
        {
            CardPresenter[] cards = new CardPresenter[amount];
            List<CardStaticData> cardsData = new();

            foreach (string cardId in _player.Deck) 
                cardsData.Add(_staticDataService.GetCardData(cardId));

            if (cardsData.Count == 0) 
                cardsData.Add(_staticDataService.GetCardData("PawPunch"));

            for (int i = 0; i < amount; i++)
            {
                int index = Random.Range(0, cardsData.Count);
                CardStaticData cardData = cardsData[index];
                CardPresenter card = await _factory.CreateCard(cardData);
                cards[i] = card;
            }

            return cards;
        }
    }
}