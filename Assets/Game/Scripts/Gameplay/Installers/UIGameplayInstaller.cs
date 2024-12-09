using Gameplay.UI.Presenter;
using Gameplay.UI.View;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class UIGameplayInstaller : MonoInstaller
    {
        [SerializeField] private UIGameplayMenuView _gameplayMenuView;
        [SerializeField] private UIGameplayHUDView _gameplayHUDView;
        [SerializeField] private UIEnemyInfoView _enemyInfoView;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayMenuPresenter>()
                .AsSingle()
                .WithArguments(_gameplayMenuView)
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<GameplayHUDPresenter>()
                .AsSingle()
                .WithArguments(_gameplayHUDView)
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<EnemyInfoPresenter>()
                .AsSingle()
                .WithArguments(_enemyInfoView)
                .NonLazy();
        }
    }
}