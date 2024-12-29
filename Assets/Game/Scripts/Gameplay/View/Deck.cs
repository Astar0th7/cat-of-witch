using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Gameplay;
using Gameplay.Presenters;
using Gameplay.Services;
using Gameplay.Spawners;
using NaughtyAttributes;
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
            Vector3 targetPosition = new(0.5f, 0, 0);
            Vector3 bottomLeft = _mainCamera.ViewportToWorldPoint(targetPosition);
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
            int cardCount = _cards.Count;
            if (cardCount == 0) 
                return;
            
            float spacing = _cards[0].Width / 1.5f;
            float totalWidth = (cardCount - 1) * spacing;
            float startX = -totalWidth / 2;

            for (int i = 0; i < cardCount; i++)
            {
                float xPos = startX + i * spacing;
                float yPos = Mathf.Pow(xPos / 3.5f, 2) * -1;
                float rotationAngle = cardCount == 1 ? 0 : Mathf.Lerp(15, -15, (float)i / (cardCount - 1));
                CardPresenter card = _cards[i];

                Vector3 targetPosition = new Vector3(xPos, yPos, 0) + transform.position + (card.Height * 0.5f * Vector3.up);
                targetPosition.z = 0;
                card.SetTargetPosition(targetPosition);
                
                Quaternion targetRotate = Quaternion.Euler(0, 0, Mathf.Clamp(rotationAngle, -90f, 90f));
                card.SetTargetRotation(targetRotate);
                card.SetOrderInLayer(0);
            }
            
            _cards.Sort((a, b) => a.TargetPosition.x.CompareTo(b.TargetPosition.x));
            for (int i = 0; i < _cards.Count; i++) 
                _cards[i].SetOrderInLayer(i);
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