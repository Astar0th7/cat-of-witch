using System;
using Root.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI.View
{
    public class UIMenuView : BaseView
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        public event Action PlayButtonClickedEvent;
        public event Action SettingsButtonClickedEvent;
        public event Action QuitButtonClickedEvent;
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked() => 
            PlayButtonClickedEvent?.Invoke();

        private void OnSettingsButtonClicked() => 
            SettingsButtonClickedEvent?.Invoke();

        private void OnQuitButtonClicked() => 
            QuitButtonClickedEvent?.Invoke();
    }
}