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
        private CompositeDisposable _subscription = new();
        
        public EnemyInfoPresenter(UIEnemyInfoView view)
        {
            _view = view;
            _healthSlider = new SliderPresenter(_view.HealthSliderView);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        public void SetDataEnemy(EnemyProxy enemy)
        {
            _subscription.Dispose();
            _subscription = new CompositeDisposable();
            
            _enemy = enemy;
            _subscription.Add(_enemy.CurrentHealth.Subscribe(value => _healthSlider.SetValue(value, enemy.MaxHealth)));
        }
    }
}