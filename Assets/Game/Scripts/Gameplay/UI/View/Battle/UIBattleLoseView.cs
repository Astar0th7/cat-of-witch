using System;
using Root.UI.View;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIBattleLoseView : BaseView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _changeDeckButton;
        [SerializeField] private Button _homeButton;
        
        public event Action RestartButtonClickedEvent;
        public event Action ChangeDeckButtonClickedEvent;
        public event Action HomeButtonClickedEvent;
        
        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);   
            _changeDeckButton.onClick.AddListener(OnChangeDeckButtonClicked);
            _homeButton.onClick.AddListener(OnHomeButtonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _changeDeckButton.onClick.RemoveListener(OnChangeDeckButtonClicked);
            _homeButton.onClick.RemoveListener(OnHomeButtonClicked);
        }

        private void OnRestartButtonClicked() => 
            RestartButtonClickedEvent?.Invoke();

        private void OnChangeDeckButtonClicked() => 
            ChangeDeckButtonClickedEvent?.Invoke();

        private void OnHomeButtonClicked() => 
            HomeButtonClickedEvent?.Invoke();
    }
}