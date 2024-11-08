using Root.Services.SaveLoad;
using Root.Services.SceneLoad;
using Zenject;

namespace Root.Installers
{
    public class RootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<SceneLoadService>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<DesktopSaveLoadService>()
                .AsSingle();
        }
    }
}