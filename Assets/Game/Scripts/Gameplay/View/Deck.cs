using System;
using System.Collections.Generic;
using Game.Scripts.Gameplay;
using Gameplay.Presenters;
using Gameplay.Services;
using Gameplay.Spawners;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.View
{
    public class Deck : MonoBehaviour, IHomeGameplayListener, IBattleGameplayListener
    {
        private PlayerSpawner _playerSpawner;
        private CardSpawner _cardSpawner;

        private readonly List<CardPresenter> _cards = new();
        private Camera _mainCamera;
        private BattleZone _battleZone;

        public event Action<CardPresenter> CardSelectEvent;

        [Inject]
        private void Construct(PlayerSpawner playerSpawner, CardSpawner cardSpawner, BattleZone battleZone)
        {
            _battleZone = battleZone;
            _cardSpawner = cardSpawner;
            _playerSpawner = playerSpawner;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            Vector3 screenBottomLeft = new(0, 0, _mainCamera.nearClipPlane);
            Vector3 bottomLeft = _mainCamera.ScreenToWorldPoint(screenBottomLeft);
            bottomLeft.z = 0;
            transform.position = bottomLeft;
            Refresh();
            
            _battleZone.EnemyEndStepEvent += OnEnemyEndStep;
        }

        private void OnDestroy()
        {
            _battleZone.EnemyEndStepEvent -= OnEnemyEndStep;
        }

        public async void OnBattleGameplay()
        {
            CardPresenter[] cards = await _cardSpawner.SpawnCards(3);
            foreach (CardPresenter card in cards)
                AddCard(card);
            
            Refresh();
        }

        public void OnHomeGameplay()
        {
            foreach (CardPresenter card in _cards)
            {
                card.CardApplyEvent -= DeleteCard;
                card.CardSelectEvent -= OnCardSelect;
                card.Remove();
            }
            
            _cards.Clear();
        }

        public void AddCard(CardPresenter card)
        {
            if (_cards.Contains(card))
            {
                Debug.LogError("Карта уже добавлена");
                return;
            }

            card.CardApplyEvent += DeleteCard;
            card.CardSelectEvent += OnCardSelect;
            _cards.Add(card);
            Refresh();
        }

        public void DeleteCard(CardPresenter card)
        {
            if (_cards.Contains(card) == false)
            {
                Debug.LogError("Такой карты нет в списке");
                return;
            }
            
            card.CardApplyEvent -= DeleteCard;
            card.CardSelectEvent -= OnCardSelect;
            _cards.Remove(card);
            Refresh();
        }

        private void Refresh()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                CardPresenter card = _cards[i];
                Vector3 targetPosition = transform.position;
                
                if (i == 0)
                {
                    targetPosition.x += card.Width / 2 + 0.1f;
                }
                else
                {
                    CardPresenter previousCard = _cards[i - 1];
                    targetPosition.x = previousCard.TargetPosition.x + (card.Width + 0.1f);
                }
                
                targetPosition.y += card.Height / 2 + 0.1f;
                targetPosition.z = 0;

                card.SetTargetPosition(targetPosition);
                card.SetOrderInLayer(0);
            }
            
            if (_playerSpawner.PlayerBattle != null)
                _playerSpawner.PlayerBattle.NumberOfCart.Value = _cards.Count;
        }

        private void OnCardSelect(CardPresenter card)
        {
            DeleteCard(card);
            CardSelectEvent?.Invoke(card);
        }

        private async void OnEnemyEndStep()
        {
            if (_cards.Count >= 6)
                return;

            int amount = Random.Range(1, 6 - _cards.Count + 1);
            CardPresenter[] cards = await _cardSpawner.SpawnCards(amount);
            foreach (CardPresenter card in cards)
                AddCard(card);
        }
    }
}