using Zenject;

namespace Initial.Installers
{
    public class InitialInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<EntryPoint>()
                .AsCached();
        }
    }
}