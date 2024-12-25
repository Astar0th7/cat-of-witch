using System;
using Gameplay.Services;
using Gameplay.UI.View;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class GameplayHomePresenter : IInitializable, IDisposable, IHomeGameplayListener, IBattleGameplayListener
    {
        private readonly UIGameplayHomeView _view;
        private readonly IGameplayStateService _gameplayStateService;
        private readonly GameplayMenuPresenter _gameplayMenuPresenter;

        public GameplayHomePresenter(
            UIGameplayHomeView view, 
            IGameplayStateService gameplayStateService, 
            GameplayMenuPresenter gameplayMenuPresenter)
        {
            _view = view;
            _gameplayStateService = gameplayStateService;
            _gameplayMenuPresenter = gameplayMenuPresenter;
        }

        public void Initialize()
        {
            _view.BattleButtonClickedEvent += OnBattleButtonClicked;
            _view.MenuButtonClickedEvent += OnMenuButtonClicked;
        }

        public void Dispose()
        {
            _view.BattleButtonClickedEvent -= OnBattleButtonClicked;
            _view.MenuButtonClickedEvent -= OnMenuButtonClicked;
        }

        public void OnHomeGameplay()
        {
            Show();
        }

        public void OnBattleGameplay()
        {
            Hide();
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }

        private void OnBattleButtonClicked()
        {
            _gameplayStateService.BattleGameplay();
        }

        private void OnMenuButtonClicked()
        {
            _gameplayMenuPresenter.Show();
        }
    }
}