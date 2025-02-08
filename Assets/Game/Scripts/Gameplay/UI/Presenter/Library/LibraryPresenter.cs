using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Model;
using Gameplay.UI.View.Library;
using Gameplay.Services;
using Gameplay.UI.View;
using ObservableCollections;
using R3;
using Root.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Presenter.Library
{
    public class LibraryPresenter : IInitializable, IDisposable
    {
        private readonly UILibraryView _view;
        private readonly IUIBattleFactory _uiBattleFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameplayStateService _gameplayStateService;
        private readonly PlayerProxy _player;

        private readonly List<LibraryCardPresenter> _libraryCardPresenters = new();
        private readonly CompositeDisposable _disposable = new();

        public LibraryPresenter(
            UILibraryView view, 
            IUIBattleFactory uiBattleFactory, 
            IStaticDataService staticDataService,
            IGameplayStateService gameplayStateService,
            PlayerProxy player)
        {
            _view = view;
            _uiBattleFactory = uiBattleFactory;
            _staticDataService = staticDataService;
            _gameplayStateService = gameplayStateService;
            _player = player;
        }
        
        public void Initialize()
        {
            _view.SaveDeckButtonClickedEvent += OnSaveDeckButtonClicked;

            foreach (CardStaticData cardData in _staticDataService.GetAllCards())
            {
                LibraryCardPresenter libraryCardPresenter = _uiBattleFactory
                    .CreateLibraryCard(_staticDataService.LibraryCardView, cardData, _view.Container);
                libraryCardPresenter.CardClickedEvent += OnCardClicked;
                _libraryCardPresenters.Add(libraryCardPresenter);
            }
            
            _disposable.Add(_player.Deck.ObserveAdd().Subscribe(context => CardSelected(context.Value)));
            _disposable.Add(_player.Deck.ObserveRemove().Subscribe(context => CardDeselected(context.Value)));

            foreach (LibraryCardPresenter libraryCardPresenter in _libraryCardPresenters) 
                libraryCardPresenter.Deselect();
            
            foreach (string id in _player.Deck) 
                CardSelected(id);
        }

        public void Dispose()
        {
            _view.SaveDeckButtonClickedEvent -= OnSaveDeckButtonClicked;

            foreach (LibraryCardPresenter libraryCardPresenter in _libraryCardPresenters) 
                libraryCardPresenter.CardClickedEvent -= OnCardClicked;

            _libraryCardPresenters.Clear();
            _disposable?.Dispose();
        }

        public void Show()
        {
            _view.Show();
        }

        private void OnSaveDeckButtonClicked()
        {
            // TODO: Сохранение данных
            _view.Hide(); 
        }

        private void OnCardClicked(string cardId)
        {
            if (_player.Deck.Contains(cardId) == false) 
                _player.AddCardToDeck(cardId);
            else
                _player.RemoveCardFromDeck(cardId);
        }

        private void CardSelected(string cardId)
        {
            LibraryCardPresenter libraryCardPresenter = _libraryCardPresenters.FirstOrDefault(x => x.Id == cardId);
            
            if (libraryCardPresenter == null)
            {
                Debug.LogError($"Отстутствует презентер для карты - {cardId}");
                return;
            }

            libraryCardPresenter.Select();
        }
        
        private void CardDeselected(string cardId)
        {
            LibraryCardPresenter libraryCardPresenter = _libraryCardPresenters.FirstOrDefault(x => x.Id == cardId);
            if (libraryCardPresenter == null)
            {
                Debug.LogError($"Отстутствует презентер для карты - {cardId}");
                return;
            }

            libraryCardPresenter.Deselect();
        }
    }
}