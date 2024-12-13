using System;
using Gameplay.UI.View;
using Root.Services.SceneLoad;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class GameplayMenuPresenter : IInitializable, IDisposable
    {
        private readonly UIGameplayMenuView _view;
        private readonly ISceneLoadService _sceneLoadService;

        public GameplayMenuPresenter(UIGameplayMenuView view, ISceneLoadService sceneLoadService)
        {
            _view = view;
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            _view.ContinueButtonClickedEvent += OnContinueButtonClicked;
            _view.MenuButtonClickedEvent += OnMenuButtonClicked;
        }

        public void Dispose()
        {
            _view.ContinueButtonClickedEvent -= OnContinueButtonClicked;
            _view.MenuButtonClickedEvent -= OnMenuButtonClicked;
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }

        private void OnContinueButtonClicked()
        {
            Hide();
        }

        private void OnMenuButtonClicked()
        {
            _sceneLoadService.Load(SceneNames.MENU);
        }
    }
}