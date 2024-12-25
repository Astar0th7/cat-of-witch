using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIGameplayHomeView : MonoBehaviour
    {
        [SerializeField] private Button _battleButton;
        [SerializeField] private Button _menuButton;

        public event Action BattleButtonClickedEvent;
        public event Action MenuButtonClickedEvent;
        
        private void OnEnable()
        {
            _battleButton.onClick.AddListener(OnBattleButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
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