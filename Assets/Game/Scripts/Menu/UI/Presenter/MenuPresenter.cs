using System;
using Menu.UI.View;
using Root.Services.SceneLoad;
using Zenject;

namespace Menu.UI.Presenter
{
    public class MenuPresenter : IInitializable, IDisposable
    {
        private readonly UIMenuView _view;
        private readonly ISceneLoadService _sceneLoadService;

        public MenuPresenter(UIMenuView view, ISceneLoadService sceneLoadService)
        {
            _view = view;
            _sceneLoadService = sceneLoadService;
        }

        void IInitializable.Initialize()
        {
            _view.PlayButtonClickedEvent += OnPlayButtonClicked;
        }

        void IDisposable.Dispose()
        {
            _view.PlayButtonClickedEvent -= OnPlayButtonClicked;
        }

        private void OnPlayButtonClicked()
        {
            _sceneLoadService.Load(SceneNames.GAMEPLAY);
        }
    }
}