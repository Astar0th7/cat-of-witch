using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIGameplayMenuView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _menuButton;

        public event Action ContinueButtonClickedEvent;
        public event Action MenuButtonClickedEvent;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnContinueButtonClicked()
        {
            ContinueButtonClickedEvent?.Invoke();
        }

        private void OnMenuButtonClicked()
        {
            MenuButtonClickedEvent?.Invoke();
        }
    }
}