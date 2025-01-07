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
        [SerializeField] private UIGameplayHomeView _gameplayHomeView;
        
        [Header("Battle")]
        [SerializeField] private UIGameplayBattleView _gameplayBattleView;
        [SerializeField] private UIPlayerInfoView _gameplayHUDView;
        [SerializeField] private UIEnemyInfoView _enemyInfoView;
        [SerializeField] private UIBattleHudView _battleHudView;
        [SerializeField] private UICardInfoView _cardInfoView;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayMenuPresenter>()
                .AsSingle()
                .WithArguments(_gameplayMenuView)
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<GameplayHomePresenter>()
                .AsSingle()
                .WithArguments(_gameplayHomeView)
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

            Container
                .BindInterfacesAndSelfTo<CardInfoPresenter>()
                .AsSingle()
                .WithArguments(_cardInfoView)
                .NonLazy();
        }
    }
}