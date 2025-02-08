using Menu.UI.Presenter;
using Menu.UI.View;
using UnityEngine;
using Zenject;

namespace Menu.Installers
{
    public class UIMenuInstaller : MonoInstaller
    {
        [SerializeField] private UISettingsView _settingsView;
        [SerializeField] private UIMenuView _menuView;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SettingsPresenter>()
                .AsSingle()
                .WithArguments(_settingsView)
                .NonLazy();
            
            Container
                .BindInterfacesTo<MenuPresenter>()
                .AsCached()
                .WithArguments(_menuView)
                .NonLazy();
        }
    }
}