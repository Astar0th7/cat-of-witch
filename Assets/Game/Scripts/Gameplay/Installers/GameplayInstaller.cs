using Game.Scripts.Gameplay;
using Gameplay.Model.Battle;
using Gameplay.Services;
using Gameplay.UI.Presenter;
using Gameplay.UI.View;
using Model;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Deck _deck;
        [SerializeField] private BattleZone _battleZone;
        [SerializeField] private Location _homeLocation;
        [SerializeField] private Location _battleLocation;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UIHomeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayStateService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleZone>().FromInstance(_battleZone).AsSingle();
            Container.BindInterfacesAndSelfTo<Deck>().FromInstance(_deck).AsSingle();
            Container.BindInterfacesAndSelfTo<Battler>().AsSingle().WithArguments(new BattleModel());
            Container.BindInterfacesAndSelfTo<LocationPresenter>().AsSingle().WithArguments(_homeLocation, _battleLocation).NonLazy();
        }
    }
}