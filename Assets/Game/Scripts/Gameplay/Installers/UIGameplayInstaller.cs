using Gameplay.UI.Presenter;
using Gameplay.UI.View;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Gameplay.Installers
{
    public class UIGameplayInstaller : MonoInstaller
    {
        [SerializeField] private UIGameplayMenuView _gameplayMenuView;
        [SerializeField] private UIPlayerInfoView _gameplayHUDView;
        [SerializeField] private UIEnemyInfoView _enemyInfoView;
        [SerializeField] private UIBattleHudView _battleHudView;
        
        
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
            
            Container
                .BindInterfacesAndSelfTo<BattleHudPresenter>()
                .AsSingle()
                .WithArguments(_battleHudView)
                .NonLazy();
        }
    }
}