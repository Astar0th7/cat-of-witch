using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIGameplayMenuView : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;

        public event Action MenuButtonClickedEvent;
        
        private void OnEnable()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        private void OnMenuButtonClicked()
        {
            MenuButtonClickedEvent?.Invoke();
        }
    }
}