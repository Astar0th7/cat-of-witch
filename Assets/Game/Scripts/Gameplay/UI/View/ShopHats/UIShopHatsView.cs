using System;
using Root.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View.ShopHats
{
    public class UIShopHatsView : BaseView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _container;
        
        public Transform Container => _container;

        public event Action CloseButtonClickedEvent;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked() => 
            CloseButtonClickedEvent?.Invoke();
    }
}