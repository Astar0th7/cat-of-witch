using System;
using Root.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIGameplayHomeView : BaseView
    {
        [SerializeField] private Button _libraryButton;
        [SerializeField] private Button _battleButton;
        [SerializeField] private Button _shopHatsButton;
        [SerializeField] private Button _menuButton;

        public event Action LibraryButtonClickedEvent;
        public event Action BattleButtonClickedEvent;
        public event Action ShopHatsButtonClickedEvent;
        public event Action MenuButtonClickedEvent;


        private void OnEnable()
        {
            _libraryButton.onClick.AddListener(OnLibraryButtonClicked);
            _battleButton.onClick.AddListener(OnBattleButtonClicked);
            _shopHatsButton.onClick.AddListener(OnShopHatsButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            _libraryButton.onClick.RemoveListener(OnLibraryButtonClicked);
            _battleButton.onClick.RemoveListener(OnBattleButtonClicked);
            _shopHatsButton.onClick.RemoveListener(OnShopHatsButtonClicked);
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        private void OnLibraryButtonClicked() => 
            LibraryButtonClickedEvent?.Invoke();

        private void OnBattleButtonClicked() => 
            BattleButtonClickedEvent?.Invoke();

        private void OnShopHatsButtonClicked() => 
            ShopHatsButtonClickedEvent?.Invoke();

        private void OnMenuButtonClicked() => 
            MenuButtonClickedEvent?.Invoke();
    }
}