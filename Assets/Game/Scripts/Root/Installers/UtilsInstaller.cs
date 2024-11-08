using Root.Services.Utils;
using UnityEngine;
using Zenject;

namespace Root.Installers
{
    public class UtilsInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(_coroutineRunner)
                .AsSingle();
        }
    }
}