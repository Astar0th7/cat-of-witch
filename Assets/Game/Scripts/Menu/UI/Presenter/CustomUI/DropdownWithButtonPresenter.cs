using System;
using Menu.UI.View;
using R3;
using Root.Model.Settings;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Menu.UI.Presenter
{
    public class DropdownWithButtonPresenter : IInitializable, IDisposable
    {
        private readonly UIDropdownWithButtonView _view;
        private readonly SettingsProxy _settings;
        private IDisposable _localeIndexDisposable;

        public DropdownWithButtonPresenter(UIDropdownWithButtonView view, SettingsProxy settings)
        {
            _view = view;
            _settings = settings;
        }

        public void Initialize()
        {
            _view.LeftButtonClickedEvent += OnLeftButtonClicked;
            _view.RightButtonClickedEvent += OnRightButtonClicked;

            _localeIndexDisposable = _settings.LocaleIndex.Subscribe(value => _view.SetDropdownValue(value));
        }

        public void Dispose()
        {
            _view.LeftButtonClickedEvent -= OnLeftButtonClicked;
            _view.RightButtonClickedEvent -= OnRightButtonClicked;
            
            _localeIndexDisposable?.Dispose();
        }

        private void OnLeftButtonClicked()
        {
            int localeIndex = _settings.LocaleIndex.CurrentValue - 1;
            int maxLocale = LocalizationSettings.AvailableLocales.Locales.Count - 1;
            localeIndex = localeIndex < 0 ? maxLocale : localeIndex;
            _settings.LocaleIndex.Value = Mathf.Clamp(localeIndex, 0, maxLocale);
        }

        private void OnRightButtonClicked()
        {
            int localeIndex = _settings.LocaleIndex.CurrentValue + 1;
            int maxLocale = LocalizationSettings.AvailableLocales.Locales.Count - 1;
            localeIndex = localeIndex > maxLocale ? 0 : localeIndex;
            _settings.LocaleIndex.Value = Mathf.Clamp(localeIndex, 0, maxLocale);
        }
    }
}