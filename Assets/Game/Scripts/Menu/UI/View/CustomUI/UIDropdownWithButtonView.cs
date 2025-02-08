using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI.View
{
    public class UIDropdownWithButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
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
            _leftButton.onClick.RemoveListener(OnLeftButtonClicked);
            _rightButton.onClick.RemoveListener(OnRightButtonClicked);
        }

        public void SetDropdownValue(int localeIndex)
        {
            _dropdown.value = localeIndex;
        }
        
        private void OnLeftButtonClicked() => 
            LeftButtonClickedEvent?.Invoke();

        private void OnRightButtonClicked() => 
            RightButtonClickedEvent?.Invoke();
    }
}