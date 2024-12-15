using Gameplay.Services;
using Gameplay.Model;
using Gameplay.Presenters;
using Gameplay.Spawners;
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
        private BattleZone _battleZone;
        private PlayerSpawner _playerSpawner;
        private IStaticDataService _staticDataService;
        private IBattleFactory _factory;

        [Inject]
        private void Construct(
            Deck deck, 
            BattleZone battleZone,
            PlayerSpawner playerSpawner,
            IStaticDataService staticDataService, 
            IBattleFactory factory)
        {
            _deck = deck;
            _battleZone = battleZone;
            _playerSpawner = playerSpawner;
            _staticDataService = staticDataService;
            _factory = factory;
        }
        
        private void Start()
        {
            _battleZone.EnemyEndStepEvent += OnEnemyEndStep;

            SpawnCards();
        }

        private void OnEnemyEndStep()
        {
            SpawnCards();
        }

        private async void SpawnCards()
        {
            if (_playerSpawner.PlayerBattle.NumberOfCart.Value >= 6)
                return;
            
            CardStaticData[] cardsData = _staticDataService.GetAllCards();
            int amountCards = Random.Range(1, 6 - _playerSpawner.PlayerBattle.NumberOfCart.Value);

            for (int i = 0; i < amountCards; i++)
            {
                int index = Random.Range(0, cardsData.Length);
                CardStaticData cardData = cardsData[index];
                CardPresenter card = await _factory.CreateCard(cardData);
                _deck.AddCard(card);
            }
        }
    }
}