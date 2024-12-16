using Gameplay.UI.Presenter;
using Gameplay.UI.View;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class UIGameplayInstaller : MonoInstaller
    {
        [SerializeField] private UIGameplayMenuView _gameplayMenuView;
        
        [Header("Gameplay")]
        [SerializeField] private UIGameplayView _gameplayView;
        
        [Header("Battle")]
        [SerializeField] private UIGameplayBattleView _gameplayBattleView;
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
                .BindInterfacesAndSelfTo<GameplayPresenter>()
                .AsSingle()
                .WithArguments(_gameplayView)
                .NonLazy();
            
            InitialBattle();
        }

        private void InitialBattle()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayBattlePresenter>()
                .AsSingle()
                .WithArguments(_gameplayBattleView)
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