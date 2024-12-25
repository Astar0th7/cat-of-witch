using Cysharp.Threading.Tasks;
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
        
        private CardSpawner(IStaticDataService staticDataService, IBattleFactory factory)
        {
            _staticDataService = staticDataService;
            _factory = factory;
        }

        public async UniTask<CardPresenter[]> SpawnCards(int amount)
        {
            CardPresenter[] cards = new CardPresenter[amount];
            CardStaticData[] cardsData = _staticDataService.GetAllCards();

            for (int i = 0; i < amount; i++)
            {
                int index = Random.Range(0, cardsData.Length);
                CardStaticData cardData = cardsData[index];
                CardPresenter card = await _factory.CreateCard(cardData);
                cards[i] = card;
            }

            return cards;
        }
    }
}