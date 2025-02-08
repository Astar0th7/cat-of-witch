using System;
using Menu.UI.View;
using Root.Model;
using Root.Model.Settings;
using Root.Services.InputService;
using Services.SaveLoad;
using Zenject;

namespace Menu.UI.Presenter
{
    public class SettingsPresenter : IInitializable, IDisposable
    {
        private readonly UISettingsView _view;
        private readonly SettingsProxy _settings;
        private readonly PlayerProxy _player;
        private readonly IInputService _inputService;
        private readonly ISaveLoadService _saveLoadService;

        private readonly SliderWithButtonPresenter _sliderWithButtonPresenter;
        private readonly DropdownWithButtonPresenter _dropdownWithButtonPresenter;

        public SettingsPresenter(
            UISettingsView view, 
            SettingsProxy settings, 
            PlayerProxy player, 
            IInputService inputService,
            ISaveLoadService saveLoadService)
        {
            _view = view;
            _settings = settings;
            _player = player;
            _inputService = inputService;
            _saveLoadService = saveLoadService;

            _sliderWithButtonPresenter = new SliderWithButtonPresenter(_view.SliderWithButtonView, _settings);
            _dropdownWithButtonPresenter = new DropdownWithButtonPresenter(_view.DropdownWithButtonView, _settings);
        }

        public void Initialize()
        {
            _sliderWithButtonPresenter.Initialize();
            _dropdownWithButtonPresenter.Initialize();
            
            _view.ResetProgressButtonClickedEvent += OnResetProgressButtonClicked;
            _view.ReportButtonClickedEvent += OnReportButtonClicked;
        }

        public void Dispose()
        {
            _sliderWithButtonPresenter?.Dispose();
            _dropdownWithButtonPresenter?.Dispose();
            
            _view.ResetProgressButtonClickedEvent -= OnResetProgressButtonClicked;
            _view.ReportButtonClickedEvent -= OnReportButtonClicked;
        }

        public void Show()
        {
            _inputService.MenuButtonClickedEvent += Hide;
            _view.Show();
        }

        private void Hide()
        {
            _inputService.MenuButtonClickedEvent -= Hide;
            _saveLoadService.Save();
            _view.Hide();
        }

        private void OnResetProgressButtonClicked()
        {
            // TODO: Сброс прогресса игрока
        }

        private void OnReportButtonClicked()
        {
            // TODO: Отправить репорт об ошибке
        }
    }
}