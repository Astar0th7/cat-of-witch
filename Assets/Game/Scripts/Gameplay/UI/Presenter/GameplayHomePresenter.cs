using System;
using Gameplay.Services;
using Gameplay.UI.Presenter.Library;
using Gameplay.UI.View;
using NaughtyAttributes.Test;
using Root.Services.SceneLoad;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class GameplayHomePresenter : IInitializable, IDisposable, IHomeGameplayListener, IBattleGameplayListener
    {
        private readonly UIGameplayHomeView _view;
        private readonly LibraryPresenter _libraryPresenter;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IGameplayStateService _gameplayStateService;

        public GameplayHomePresenter(
            UIGameplayHomeView view, 
            LibraryPresenter libraryPresenter, 
            ISceneLoadService sceneLoadService,
            IGameplayStateService gameplayStateService)
        {
            _view = view;
            _libraryPresenter = libraryPresenter;
            _sceneLoadService = sceneLoadService;
            _gameplayStateService = gameplayStateService;
        }

        public void Initialize()
        {
            _view.LibraryButtonClickedEvent += OnLibraryButtonClicked;
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

        private void OnLibraryButtonClicked()
        {
            _libraryPresenter.Show();
        }

        private void OnBattleButtonClicked()
        {
            _gameplayStateService.BattleGameplay();
        }

        private void OnMenuButtonClicked()
        {
            _sceneLoadService.Load(SceneNames.MENU);
        }
    }
}