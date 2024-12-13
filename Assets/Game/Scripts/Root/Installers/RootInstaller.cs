using Root.Services;
using Root.Services.InputService;
using Root.Services.SaveLoad;
using Root.Services.SceneLoad;
using Root.Services.Translation;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                .BindInterfacesAndSelfTo<SaveLoadListener>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<DesktopSaveLoadService>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<DesktopInputService>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<StaticDataService>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<TranslationService>()
                .AsSingle();
            
#if UNITY_EDITOR
            if (Application.isPlaying && SceneManager.GetActiveScene().name != SceneNames.INITIAL)
            {
                SceneManager.LoadScene(SceneNames.INITIAL);
            }
#endif
        }
    }
}