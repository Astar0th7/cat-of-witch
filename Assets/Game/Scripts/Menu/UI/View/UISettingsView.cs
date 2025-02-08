using System;
using Root.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.UI.View
{
    public class UISettingsView : BaseView
    {
        [SerializeField] private UISliderWithButtonView _sliderWithButtonView;
        [SerializeField] private UIDropdownWithButtonView _dropdownWithButtonView;
        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private Button _reportButton;

        public UISliderWithButtonView SliderWithButtonView => _sliderWithButtonView;
        public UIDropdownWithButtonView DropdownWithButtonView => _dropdownWithButtonView;
        
        public event Action ResetProgressButtonClickedEvent;
        public event Action ReportButtonClickedEvent;
        
        private void OnEnable()
        {
            _resetProgressButton.onClick.AddListener(OnResetProgressButtonClicked);
            _reportButton.onClick.AddListener(OnReportButtonClicked);
        }

        private void OnDisable()
        {
            _resetProgressButton.onClick.RemoveListener(OnResetProgressButtonClicked);
            _reportButton.onClick.RemoveListener(OnReportButtonClicked);
        }

        private void OnResetProgressButtonClicked() => 
            ResetProgressButtonClickedEvent?.Invoke();

        private void OnReportButtonClicked() => 
            ReportButtonClickedEvent?.Invoke();
    }
}