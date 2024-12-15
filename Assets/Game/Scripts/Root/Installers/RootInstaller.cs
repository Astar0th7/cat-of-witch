using Gameplay.Model;
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
            
            // TODO: Данные пользователя будут регистрироваться при загрузке данных, здесь для тестов
            PlayerData playerData = new()
            {
                MaxHealth = 10,
                MaxEnergy = 6
            };

            Container
                .BindInterfacesAndSelfTo<PlayerProxy>()
                .AsSingle()
                .WithArguments(playerData);
            
#if UNITY_EDITOR
            if (Application.isPlaying && SceneManager.GetActiveScene().name != SceneNames.INITIAL)
            {
                SceneManager.LoadScene(SceneNames.INITIAL);
            }
#endif
        }
    }
}