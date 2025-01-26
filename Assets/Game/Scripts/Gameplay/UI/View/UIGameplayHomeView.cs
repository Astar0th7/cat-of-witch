using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIGameplayHomeView : MonoBehaviour
    {
        [SerializeField] private Button _libraryButton;
        [SerializeField] private Button _battleButton;
        [SerializeField] private Button _menuButton;

        public event Action LibraryButtonClickedEvent;
        public event Action BattleButtonClickedEvent;
        public event Action MenuButtonClickedEvent;


        private void OnEnable()
        {
            _libraryButton.onClick.AddListener(OnLibraryButtonClicked);
            _battleButton.onClick.AddListener(OnBattleButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            _libraryButton.onClick.RemoveListener(OnLibraryButtonClicked);
            _battleButton.onClick.RemoveListener(OnBattleButtonClicked);
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

        private void OnLibraryButtonClicked()
        {
            LibraryButtonClickedEvent?.Invoke();
        }

        private void OnBattleButtonClicked()
        {
            BattleButtonClickedEvent?.Invoke();
        }

        private void OnMenuButtonClicked()
        {
            MenuButtonClickedEvent?.Invoke();
        }
    }
}