using System;
using Root.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIBattleLoseView : BaseView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _changeDeckButton;
        [SerializeField] private Button _menuButton;
        
        public event Action RestartButtonClickedEvent;
        public event Action ChangeDeckButtonClickedEvent;
        public event Action MenuButtonClickedEvent;
        
        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);   
            _changeDeckButton.onClick.AddListener(OnChangeDeckButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _changeDeckButton.onClick.RemoveListener(OnChangeDeckButtonClicked);
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        private void OnRestartButtonClicked() => 
            RestartButtonClickedEvent?.Invoke();

        private void OnChangeDeckButtonClicked() => 
            ChangeDeckButtonClickedEvent?.Invoke();

        private void OnMenuButtonClicked() => 
            MenuButtonClickedEvent?.Invoke();
    }
}