using Gameplay.Model;
using Zenject;

namespace Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
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
        }
    }
}