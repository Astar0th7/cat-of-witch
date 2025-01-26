using System;
using Gameplay.Services;
using Gameplay.UI.Presenter.Library;
using Gameplay.UI.View;
using Root.Services.InputService;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class BattlePausePresenter : IInitializable, IDisposable
    {
        private readonly UIBattlePauseView _view;
        private readonly IInputService _inputService;
        private readonly IGameplayStateService _gameplayStateService;
        private readonly LibraryPresenter _libraryPresenter;

        public BattlePausePresenter(
            UIBattlePauseView view, 
            IInputService inputService,
            IGameplayStateService gameplayStateService, 
            LibraryPresenter libraryPresenter)
        {
            _view = view;
            _inputService = inputService;
            _gameplayStateService = gameplayStateService;
            _libraryPresenter = libraryPresenter;
        }

        public void Initialize()
        {
            _view.RestartButtonClickedEvent += OnRestartButtonClicked;
            _view.LibraryButtonClickedEvent += OnLibraryButtonClicked;
            _view.LeaveButtonClickedEvent += OnLeaveButtonClickedEvent;
            
            _inputService.MenuButtonClickedEvent += OnMenuButtonClicked;
        }

        public void Dispose()
        {
            _view.RestartButtonClickedEvent -= OnRestartButtonClicked;
            _view.LibraryButtonClickedEvent -= OnLibraryButtonClicked;
            _view.LeaveButtonClickedEvent -= OnLeaveButtonClickedEvent;
            
            _inputService.MenuButtonClickedEvent -= OnMenuButtonClicked;
        }
        private void OnRestartButtonClicked()
        {
            _gameplayStateService.HomeGameplay();
            _gameplayStateService.BattleGameplay();
            
            _view.Hide();
        }

        private void OnLibraryButtonClicked()
        {
            _libraryPresenter.Show();
        }

        private void OnLeaveButtonClickedEvent()
        {
            _gameplayStateService.HomeGameplay();
            _view.FastHide();
        }

        private void OnMenuButtonClicked()
        {
            if (_gameplayStateService.State != GameplayState.Battle)
                return;
            
            if (_view.IsActive)
            {
                _view.Hide();
                return;
            }
            
            _view.Show();
        }
    }
}