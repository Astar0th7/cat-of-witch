using System;
using Gameplay.Model.Enemies;
using Gameplay.Services;
using Gameplay.UI.Presenter.CustomUI;
using Gameplay.UI.View;
using R3;
using Root.Services.Translation;

namespace Gameplay.UI.Presenter
{
    public class EnemyInfoPresenter : IDisposable
    {
        private EnemyProxy _enemy;

        private readonly SliderPresenter _healthSlider;
        private readonly UIEnemyInfoView _view;
        private readonly ITranslationService _translationService;
        private CompositeDisposable _subscription = new();
        
        public EnemyInfoPresenter(UIEnemyInfoView view, ITranslationService translationService)
        {
            _view = view;
            _translationService = translationService;
            _healthSlider = new SliderPresenter(_view.HealthSliderView);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        public async void SetDataEnemy(EnemyProxy enemy)
        {
            _subscription.Dispose();
            _subscription = new CompositeDisposable();
            
            _enemy = enemy;
            string name = await _translationService.Translate($"{_enemy.Id}_name", "Enemies");
            _view.SetName(name);
            _subscription.Add(_enemy.CurrentHealth.Subscribe(value => _healthSlider.SetValue(value, enemy.MaxHealth)));
        }
    }
}