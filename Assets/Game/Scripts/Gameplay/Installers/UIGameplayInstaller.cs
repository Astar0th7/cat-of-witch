using Gameplay.UI.Presenter;
using Gameplay.UI.Presenter.Library;
using Gameplay.UI.Presenter.ShopHats;
using Gameplay.UI.View;
using Gameplay.UI.View.Library;
using Gameplay.UI.View.ShopHats;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class UIGameplayInstaller : MonoInstaller
    {
        [Header("Home")]
        [SerializeField] private UIGameplayHomeView _gameplayHomeView;
        [SerializeField] private UILibraryView _libraryCardView;
        [SerializeField] private UIShopHatsView _shopHatsView;
        
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
            BindHomeInterface();
            BindBattle();
        }

        private void BindHomeInterface()
        {
            Container.BindInterfacesAndSelfTo<LibraryPresenter>().AsSingle().WithArguments(_libraryCardView).NonLazy();
            Container.BindInterfacesAndSelfTo<ShopHatsPresenter>().AsCached().WithArguments(_shopHatsView).NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayHomePresenter>().AsSingle().WithArguments(_gameplayHomeView).NonLazy();
        }

        private void BindBattle()
        {
            Container.BindInterfacesAndSelfTo<GameplayBattlePresenter>().AsSingle().WithArguments(_gameplayBattleView).NonLazy();
            Container.BindInterfacesAndSelfTo<BattlePausePresenter>().AsSingle().WithArguments(_battlePauseView).NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayHUDPresenter>().AsSingle().WithArguments(_gameplayHUDView).NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyInfoPresenter>().AsSingle().WithArguments(_enemyInfoView).NonLazy();
            Container.BindInterfacesAndSelfTo<BattleHudPresenter>().AsSingle().WithArguments(_battleHudView).NonLazy();
            Container.BindInterfacesAndSelfTo<CardInfoPresenter>().AsSingle().WithArguments(_cardInfoView).NonLazy();
            Container.BindInterfacesAndSelfTo<BattleWinPresenter>().AsSingle().WithArguments(_battleWinView).NonLazy();
            Container.BindInterfacesAndSelfTo<BattleLosePresenter>().AsSingle().WithArguments(_battleLoseView).NonLazy();
        }
    }
}