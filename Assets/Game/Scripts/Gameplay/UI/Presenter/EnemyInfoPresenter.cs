using System;
using Gameplay.Model.Enemy;
using Gameplay.UI.Presenter.CustomUI;
using Gameplay.UI.View;
using R3;

namespace Gameplay.UI.Presenter
{
    public class EnemyInfoPresenter : IDisposable
    {
        private EnemyProxy _enemy;

        private readonly HealthSliderPresenter _healthSlider;
        private readonly UIEnemyInfoView _view;
        private CompositeDisposable _subscription = new();
        
        public EnemyInfoPresenter(UIEnemyInfoView view)
        {
            _view = view;
            _healthSlider = new HealthSliderPresenter(_view.HealthSliderView);
        }

        public void SetDataEnemy(EnemyProxy enemy)
        {
            _subscription.Dispose();
            _subscription = new CompositeDisposable();
            
            _enemy = enemy;
            _view.SetName(_enemy.NameKey);
            _subscription.Add(_enemy.CurrentHealth.Subscribe(value => _healthSlider.SetHealth(value, enemy.MaxHealth)));
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}