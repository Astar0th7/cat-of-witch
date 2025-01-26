using System;
using Root.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View.Library
{
    public class UILibraryView : BaseView
    {
        [SerializeField] private Button _saveDeckButton;
        [SerializeField] private Transform _container;
        
        public Transform Container => _container;

        public event Action SaveDeckButtonClickedEvent;
        
        private void OnEnable()
        {
            _saveDeckButton.onClick.AddListener(OnSaveDeckClicked);
        }

        private void OnDisable()
        {
            _saveDeckButton.onClick.RemoveListener(OnSaveDeckClicked);
        }

        private void OnSaveDeckClicked()
        {
            SaveDeckButtonClickedEvent?.Invoke();
        }
    }
}