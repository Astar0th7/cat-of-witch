using System;
using Gameplay.Model;
using Gameplay.UI.View;
using R3;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class GameplayHUDPresenter : IInitializable, IDisposable
    {
        private readonly UIGameplayHUDView _view;
        private readonly PlayerBattleProxy _playerBattleProxy;
        private readonly CompositeDisposable _subscriptions = new(); 
        
        public GameplayHUDPresenter(UIGameplayHUDView view, PlayerBattleProxy playerBattleProxy)
        {
            _view = view;
            _playerBattleProxy = playerBattleProxy;
        }
        
        void IInitializable.Initialize()
        {
            _subscriptions.Add(_playerBattleProxy.CurrentHealth.Subscribe(value => _view.ChangeHealth(value)));
            _subscriptions.Add(_playerBattleProxy.Energy.Subscribe(value => _view.ChangeEnergy(value)));
            _subscriptions.Add(_playerBattleProxy.NumberOfCart.Subscribe(value => _view.ChangeNumberOfCard(value)));
        }

        void IDisposable.Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}