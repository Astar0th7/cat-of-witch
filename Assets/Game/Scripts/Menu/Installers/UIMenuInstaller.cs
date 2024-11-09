using Menu.UI.Presenter;
using Menu.UI.View;
using UnityEngine;
using Zenject;

namespace Menu.Installers
{
    public class UIMenuInstaller : MonoInstaller
    {
        [SerializeField] private UIMenuView _menuView;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MenuPresenter>()
                .AsCached()
                .WithArguments(_menuView)
                .NonLazy();
        }
    }
}