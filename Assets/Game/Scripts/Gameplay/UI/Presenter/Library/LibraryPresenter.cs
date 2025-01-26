using System;
using System.Collections.Generic;
using Gameplay.Model;
using Gameplay.UI.View.Library;
using Gameplay.Services;
using Gameplay.UI.View;
using Root.Services;
using Zenject;

namespace Gameplay.UI.Presenter.Library
{
    public class LibraryPresenter : IInitializable, IDisposable
    {
        private readonly UILibraryView _view;
        private readonly IUIBattleFactory _uiBattleFactory;
        private readonly IStaticDataService _staticDataService;
        
        private readonly List<LibraryCardPresenter> _libraryCardPresenters = new();

        public LibraryPresenter(
            UILibraryView view, 
            IUIBattleFactory uiBattleFactory, 
            IStaticDataService staticDataService)
        {
            _view = view;
            _uiBattleFactory = uiBattleFactory;
            _staticDataService = staticDataService;
        }
        
        public void Initialize()
        {
            _view.SaveDeckButtonClickedEvent += OnSaveDeckButtonClicked;

            foreach (CardStaticData cardData in _staticDataService.GetAllCards())
            {
                LibraryCardPresenter libraryCardPresenter = _uiBattleFactory
                    .CreateLibraryCard(_staticDataService.LibraryCardView, cardData, _view.Container);
                _libraryCardPresenters.Add(libraryCardPresenter);
            }
        }

        public void Dispose()
        {
            _view.SaveDeckButtonClickedEvent -= OnSaveDeckButtonClicked;
        }

        public void Show()
        {
            _view.Show();
        }
        
        private void OnSaveDeckButtonClicked()
        {
            // TODO: Сохранение данных в модель
            _view.Hide();
        }
    }
}