using System;
using Gameplay.UI.View;
using Root.Services.SceneLoad;
using Services.SaveLoad;
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
        
        void IInitializable.Initialize()
        {
            _view.MenuButtonClickedEvent += OnMenuButtonClicked;
        }

        void IDisposable.Dispose()
        {
            _view.MenuButtonClickedEvent -= OnMenuButtonClicked;
        }

        private void OnMenuButtonClicked()
        {
            _sceneLoadService.Load(SceneNames.MENU);
        }
    }
}