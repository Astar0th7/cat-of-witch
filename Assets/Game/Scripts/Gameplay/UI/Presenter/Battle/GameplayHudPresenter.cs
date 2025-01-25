using System;
using Game.Scripts.Gameplay;
using Gameplay.Model;
using Gameplay.Services;
using Gameplay.UI.Presenter.CustomUI;
using Gameplay.UI.View;
using ObservableCollections;
using R3;
using Root.Services;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class GameplayHUDPresenter : IInitializable, IDisposable
    {
        private readonly UIPlayerInfoView _view;
        private readonly PlayerSpawner _playerSpawner;
        private readonly IUIBattleFactory _uiBattleFactory;
        private readonly IStaticDataService _staticDataService;

        private SliderPresenter _healthSlider;
        private SliderPresenter _energySlider;
        private TimingEffectsPresenter _timingEffects;
        
        private CompositeDisposable _subscriptions = new();

        public GameplayHUDPresenter(
            UIPlayerInfoView view, 
            PlayerSpawner playerSpawner,
            IUIBattleFactory uiBattleFactory,
            IStaticDataService staticDataService)
        {
            _view = view;
            _playerSpawner = playerSpawner;
            _uiBattleFactory = uiBattleFactory;
            _staticDataService = staticDataService;
            _timingEffects = new TimingEffectsPresenter(_view.TimingEffectsView, _uiBattleFactory, _staticDataService);
        }

        public void Initialize()
        {
            _healthSlider = new SliderPresenter(_view.HealthSlider);
            _energySlider = new SliderPresenter(_view.EnergySlider);
            
            _playerSpawner.PlayerSpawnedEvent += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _playerSpawner.PlayerSpawnedEvent -= OnPlayerSpawned;
            _subscriptions?.Dispose();
        }

        private void OnPlayerSpawned(PlayerBattleProxy playerBattle)
        {
            _subscriptions.Dispose();
            _subscriptions = new CompositeDisposable();
            
            _subscriptions.Add(playerBattle.CurrentHealth
                .Subscribe(value => _healthSlider.SetValue(value, playerBattle.MaxHealth)));
            
            _subscriptions.Add(playerBattle.CurrentEnergy
                .Subscribe(value => _energySlider.SetValue(value, playerBattle.MaxEnergy)));
            
            _subscriptions.Add(playerBattle.TimingEffects.Effects.ObserveAdd()
                .Subscribe(value => _timingEffects.AddTimingEffect(value.Value)));
            
            _subscriptions.Add(playerBattle.TimingEffects.Effects.ObserveRemove()
                .Subscribe(value => _timingEffects.RemoveTimingEffect(value.Value)));
        }
    }
}