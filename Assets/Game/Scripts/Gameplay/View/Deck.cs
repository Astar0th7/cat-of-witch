using System;
using System.Collections.Generic;
using Gameplay.Model;
using Gameplay.Presenters;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
    public class Deck : MonoBehaviour
    {
        private PlayerBattleProxy _player;
        
        private readonly List<CardPresenter> _cards = new();
        
        public event Action<CardPresenter> CardSelectEvent;

        [Inject]
        private void Construct(PlayerBattleProxy player)
        {
            _player = player;
        }
        
        private void Start()
        {
            Camera main = Camera.main;
            Vector3 screenBottomLeft = new(0, 0, main.nearClipPlane);
            Vector3 bottomLeft = main.ScreenToWorldPoint(screenBottomLeft);
            bottomLeft.z = 0;
            transform.position = bottomLeft;

            Refresh();
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
            }
            
            _player.NumberOfCart.Value = _cards.Count;
        }

        private void OnCardSelect(CardPresenter card)
        {
            DeleteCard(card);
            CardSelectEvent?.Invoke(card);
        }
    }
}