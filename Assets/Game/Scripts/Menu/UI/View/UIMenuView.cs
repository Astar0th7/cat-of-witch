using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI.View
{
    public class UIMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        public event Action PlayButtonClickedEvent;
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            PlayButtonClickedEvent?.Invoke();
        }
    }
}