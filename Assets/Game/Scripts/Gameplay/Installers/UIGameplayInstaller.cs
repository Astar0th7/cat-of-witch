using Gameplay.UI.Presenter;
using Gameplay.UI.Presenter.Library;
using Gameplay.UI.View;
using Gameplay.UI.View.Library;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class UIGameplayInstaller : MonoInstaller
    {
        [Header("Home")]
        [SerializeField] private UIGameplayHomeView _gameplayHomeView;
        [SerializeField] private UILibraryView _libraryCardView;
        
        [Header("Battle")]
        [SerializeField] private UIGameplayBattleView _gameplayBattleView;
        [SerializeField] private UIBattlePauseView _battlePauseView;
        [SerializeField] private UIPlayerInfoView _gameplayHUDView;
        [SerializeField] private UIEnemyInfoView _enemyInfoView;
        [SerializeField] private UIBattleHudView _battleHudView;
        [SerializeField] private UICardInfoView _cardInfoView;
        [SerializeField] private UIBattleWinView _battleWinView;
        [SerializeField] private UIBattleLoseView _battleLoseView;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<LibraryPresenter>()
                .AsSingle()
                .WithArguments(_libraryCardView)
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
                .BindInterfacesAndSelfTo<BattlePausePresenter>()
                .AsSingle()
                .WithArguments(_battlePauseView)
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

            Container
                .BindInterfacesAndSelfTo<BattleWinPresenter>()
                .AsSingle()
                .WithArguments(_battleWinView)
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<BattleLosePresenter>()
                .AsSingle()
                .WithArguments(_battleLoseView)
                .NonLazy();
        }
    }
}