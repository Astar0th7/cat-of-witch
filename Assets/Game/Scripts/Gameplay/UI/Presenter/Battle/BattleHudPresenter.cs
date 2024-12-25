using System;
using Gameplay.Services;
using Gameplay.UI.View;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class BattleHudPresenter : IInitializable, IDisposable
    {
        private readonly UIBattleHudView _view;
        private readonly GameplayMenuPresenter _gameplayMenu;
        private readonly IGameplayStateService _gameplayStateService;

        public event Action EndStepEvent;

        public BattleHudPresenter(
            UIBattleHudView view, 
            GameplayMenuPresenter gameplayMenu, 
            IGameplayStateService gameplayStateService)
        {
            _view = view;
            _gameplayMenu = gameplayMenu;
            _gameplayStateService = gameplayStateService;
        }

        public void Initialize()
        {
            _view.EndStepClickedEvent += OnEndStepClicked;
            _view.LeaveClickedEvent += OnLeaveClicked;
            _view.MenuClickedEvent += OnMenuClicked;
        }

        public void Dispose()
        {
            _view.EndStepClickedEvent -= OnEndStepClicked;
            _view.LeaveClickedEvent -= OnLeaveClicked;
            _view.MenuClickedEvent -= OnMenuClicked;
        }

        private void OnEndStepClicked()
        {
            EndStepEvent?.Invoke();
        }

        private void OnLeaveClicked()
        {
            _gameplayStateService.HomeGameplay();
        }

        private void OnMenuClicked()
        {
            _gameplayMenu.Show();
        }
    }
}