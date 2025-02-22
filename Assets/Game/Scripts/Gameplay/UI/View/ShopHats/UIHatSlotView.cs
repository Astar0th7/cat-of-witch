using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View.ShopHats
{
    public class UIHatSlotView : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _useButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _priceText;

        public event Action SelectButtonClickedEvent;
        public event Action UseButtonClickedEvent;
        
        private void OnEnable()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
            _useButton.onClick.AddListener(OnUseButtonClicked);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
            _useButton.onClick.RemoveListener(OnUseButtonClicked);
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }

        public void SetPrice(int price)
        {
            _priceText.text = price.ToString();
        }

        private void OnSelectButtonClicked() => 
            SelectButtonClickedEvent?.Invoke();

        private void OnUseButtonClicked() => 
            UseButtonClickedEvent?.Invoke();
    }
}