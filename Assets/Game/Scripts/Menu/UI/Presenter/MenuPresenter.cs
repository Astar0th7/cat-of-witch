using System;
using Menu.UI.View;
using Root.Services.InputService;
using Root.Services.SceneLoad;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Menu.UI.Presenter
{
    public class MenuPresenter : IInitializable, IDisposable
    {
        private readonly UIMenuView _view;
        private readonly SettingsPresenter _settingsPresenter;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly IInputService _inputService;

        public MenuPresenter(
            UIMenuView view, 
            SettingsPresenter settingsPresenter, 
            ISceneLoadService sceneLoadService,
            IInputService inputService)
        {
            _view = view;
            _settingsPresenter = settingsPresenter;
            _sceneLoadService = sceneLoadService;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _view.PlayButtonClickedEvent += OnPlayButtonClicked;
            _view.SettingsButtonClickedEvent += OnSettingsButtonClicked;
            _view.QuitButtonClickedEvent += OnQuitButtonClicked;
            
            _inputService.EnableKeyboard();
        }

        public void Dispose()
        {
            _view.PlayButtonClickedEvent -= OnPlayButtonClicked;
            _view.SettingsButtonClickedEvent -= OnSettingsButtonClicked;
            _view.QuitButtonClickedEvent -= OnQuitButtonClicked;
            
            _inputService.DisableKeyboard();
        }

        private void Show()
        {
            _inputService.MenuButtonClickedEvent -= Show;
            _view.Show();
        }

        private void OnPlayButtonClicked()
        {
            _sceneLoadService.Load(SceneNames.GAMEPLAY);
        }

        private void OnSettingsButtonClicked()
        {
            _inputService.MenuButtonClickedEvent += Show;
            _view.Hide();
            _settingsPresenter.Show();
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}