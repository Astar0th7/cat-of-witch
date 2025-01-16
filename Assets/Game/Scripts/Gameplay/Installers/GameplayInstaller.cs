using Game.Scripts.Gameplay;
using Gameplay.Model.Battle;
using Gameplay.Services;
using Gameplay.Spawners;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Deck _deck;
        [SerializeField] private BattleZone _battleZone;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BattleFactory>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CardSpawner>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerSpawner>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EnemySpawner>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameplayStateService>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<BattleZone>()
                .FromInstance(_battleZone)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Deck>()
                .FromInstance(_deck)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Battle>()
                .AsSingle()
                .WithArguments(new BattleModel());
        }
    }
}