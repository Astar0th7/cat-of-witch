using System;
using Game.Scripts.Gameplay;
using Gameplay.Model;
using Gameplay.Services;
using Gameplay.UI.Presenter.CustomUI;
using Gameplay.UI.View;
using R3;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class GameplayHUDPresenter : IInitializable, IDisposable
    {
        private readonly UIPlayerInfoView _view;
        private readonly PlayerSpawner _playerSpawner;
        private CompositeDisposable _subscriptions = new(); 
        
        private SliderPresenter _healthSlider;
        private SliderPresenter _energySlider;
        
        public GameplayHUDPresenter(UIPlayerInfoView view, PlayerSpawner playerSpawner)
        {
            _view = view;
            _playerSpawner = playerSpawner;
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
                .Subscribe(value => _healthSlider.SetValue(value, playerBattle.MaxHealth.CurrentValue)));
            _subscriptions.Add(playerBattle.CurrentEnergy
                .Subscribe(value => _energySlider.SetValue(value, playerBattle.MaxEnergy.CurrentValue)));
        }
    }
}