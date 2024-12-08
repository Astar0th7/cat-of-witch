using Gameplay.Model;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Deck _deck;
        
        public override void InstallBindings()
        {
            PlayerBattleData playerBattleData = new()
            {
                Health = 10,
                Energy = 5,
                NumberOfCart = 3
            };

            Container
                .BindInterfacesAndSelfTo<PlayerBattleProxy>()
                .AsSingle()
                .WithArguments(playerBattleData);
            
            Container
                .BindInterfacesAndSelfTo<Deck>()
                .FromInstance(_deck)
                .AsSingle();
        }
    }
}