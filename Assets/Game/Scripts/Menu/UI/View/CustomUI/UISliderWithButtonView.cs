using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI.View
{
    public class UISliderWithButtonView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        public event Action LeftButtonClickedEvent;
        public event Action RightButtonClickedEvent;
        
        private void OnEnable()
        {
            _leftButton.onClick.AddListener(OnLeftButtonClicked);
            _rightButton.onClick.AddListener(OnRightButtonClicked);
        }

        private void OnDisable()
        {
            _leftButton.onClick.RemoveAllListeners();
            _rightButton.onClick.RemoveAllListeners();
        }

        public void SetSliderValue(float normalizeValue)
        {
            _slider.value = normalizeValue;
        }
        
        private void OnLeftButtonClicked() => 
            LeftButtonClickedEvent?.Invoke();

        private void OnRightButtonClicked() => 
            RightButtonClickedEvent?.Invoke();
    }
}