using System;
using Gameplay.Services;
using Gameplay.UI.Presenter.Library;
using Gameplay.UI.View;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class BattleLosePresenter : IInitializable, IDisposable
    {
        private readonly UIBattleLoseView _view;
        private readonly LibraryPresenter _libraryPresenter;
        private readonly IGameplayStateService _gameplayStateService;

        public BattleLosePresenter(
            UIBattleLoseView view, 
            LibraryPresenter libraryPresenter, 
            IGameplayStateService gameplayStateService)
        {
            _view = view;
            _libraryPresenter = libraryPresenter;
            _gameplayStateService = gameplayStateService;
        }

        public void Initialize()
        {
            _view.RestartButtonClickedEvent += OnRestartButtonClicked;
            _view.ChangeDeckButtonClickedEvent += OnChangeDeckButtonClicked;
            _view.MenuButtonClickedEvent += OnMenuButtonClicked;
        }

        public void Dispose()
        {
            _view.RestartButtonClickedEvent -= OnRestartButtonClicked;
            _view.ChangeDeckButtonClickedEvent -= OnChangeDeckButtonClicked;
            _view.MenuButtonClickedEvent -= OnMenuButtonClicked;
        }

        public void Show()
        {
            _view.Show();
        }
        
        private void OnRestartButtonClicked()
        {
            _gameplayStateService.HomeGameplay();
            _gameplayStateService.BattleGameplay();
            
            _view.Hide();
        }

        private void OnChangeDeckButtonClicked()
        {
            _libraryPresenter.Show();
        }

        private void OnMenuButtonClicked()
        {
            _gameplayStateService.HomeGameplay();
            _view.Hide();
        }
    }
}