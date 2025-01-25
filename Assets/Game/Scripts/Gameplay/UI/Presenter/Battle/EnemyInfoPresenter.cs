using System;
using Gameplay.Model.Enemies;
using Gameplay.Services;
using Gameplay.UI.Presenter.CustomUI;
using Gameplay.UI.View;
using ObservableCollections;
using R3;
using Root.Services;
using Root.Services.Translation;

namespace Gameplay.UI.Presenter
{
    public class EnemyInfoPresenter : IDisposable
    {
        private EnemyProxy _enemy;

        private readonly SliderPresenter _healthSlider;
        private readonly TimingEffectsPresenter _timingEffects;
        private readonly UIEnemyInfoView _view;
        private readonly IUIBattleFactory _uiBattleFactory;
        private readonly IStaticDataService _staticDataService;

        private CompositeDisposable _subscription = new();
        
        public EnemyInfoPresenter(
            UIEnemyInfoView view, 
            IUIBattleFactory uiBattleFactory, 
            IStaticDataService staticDataService)
        {
            _view = view;
            _uiBattleFactory = uiBattleFactory;
            _staticDataService = staticDataService;
            _healthSlider = new SliderPresenter(_view.HealthSliderView);
            _timingEffects = new TimingEffectsPresenter(_view.TimingEffectsView, _uiBattleFactory, _staticDataService);
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
            _subscription.Add(_enemy.TimingEffects.Effects.ObserveAdd()
                .Subscribe(value => _timingEffects.AddTimingEffect(value.Value)));
            _subscription.Add(_enemy.TimingEffects.Effects.ObserveRemove()
                .Subscribe(value => _timingEffects.RemoveTimingEffect(value.Value)));
        }
    }
}