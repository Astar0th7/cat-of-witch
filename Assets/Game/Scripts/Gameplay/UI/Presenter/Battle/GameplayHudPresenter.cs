using System;
using Game.Scripts.Gameplay;
using Gameplay.Model;
using Gameplay.UI.View;
using R3;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class GameplayHUDPresenter : IInitializable, IDisposable
    {
        private readonly UIPlayerInfoView _view;
        private readonly PlayerSpawner _playerSpawner;
        private readonly CompositeDisposable _subscriptions = new(); 
        
        public GameplayHUDPresenter(UIPlayerInfoView view, PlayerSpawner playerSpawner)
        {
            _view = view;
            _playerSpawner = playerSpawner;
        }

        public void Initialize()
        {
            if (_playerSpawner.PlayerBattle != null)
                OnPlayerSpawned(_playerSpawner.PlayerBattle);
            else 
                _playerSpawner.PlayerSpawnedEvent += OnPlayerSpawned;
        }

        private void OnPlayerSpawned(PlayerBattleProxy playerBattle)
        {
            _playerSpawner.PlayerSpawnedEvent -= OnPlayerSpawned;
            
            _subscriptions.Add(playerBattle.CurrentHealth.Subscribe(value => _view.ChangeHealth(value)));
            _subscriptions.Add(playerBattle.CurrentEnergy.Subscribe(value => _view.ChangeEnergy(value)));
            _subscriptions.Add(playerBattle.NumberOfCart.Subscribe(value => _view.ChangeNumberOfCard(value)));
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}