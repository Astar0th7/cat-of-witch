using Gameplay.Services;
using Zenject;

namespace Gameplay.Installers
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BattleFactory>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<UIBattleFactory>()
                .AsSingle();
        }
    }
}