using System;
using Menu.UI.View;
using R3;
using Root.Model.Settings;
using UnityEngine;
using Zenject;

namespace Menu.UI.Presenter
{
    public class SliderWithButtonPresenter : IInitializable, IDisposable
    {
        private readonly UISliderWithButtonView _view;
        private readonly SettingsProxy _settings;
        
        private IDisposable _soundsVolumeDisposable;

        public SliderWithButtonPresenter(UISliderWithButtonView view, SettingsProxy settings)
        {
            _view = view;
            _settings = settings;
        }

        public void Initialize()
        {
            _view.LeftButtonClickedEvent += OnLeftButtonClicked;
            _view.RightButtonClickedEvent += OnRightButtonClicked;

            _soundsVolumeDisposable = _settings.SoundsVolume.Subscribe(value => _view.SetSliderValue(value));
        }

        public void Dispose()
        {
            _view.LeftButtonClickedEvent -= OnLeftButtonClicked;
            _view.RightButtonClickedEvent -= OnRightButtonClicked;
            
            _soundsVolumeDisposable?.Dispose();
        }

        private void OnLeftButtonClicked()
        {
            float soundsVolume = _settings.SoundsVolume.CurrentValue - 0.1f;
            _settings.SoundsVolume.Value = Mathf.Max(soundsVolume, 0f);
        }

        private void OnRightButtonClicked()
        {
            float soundsVolume = _settings.SoundsVolume.CurrentValue + 0.1f;
            _settings.SoundsVolume.Value = Mathf.Min(soundsVolume, 1f);
        }
    }
}