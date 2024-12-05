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
        }
    }
}