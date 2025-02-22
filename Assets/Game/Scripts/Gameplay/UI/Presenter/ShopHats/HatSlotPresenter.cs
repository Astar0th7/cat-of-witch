using System;
using Gameplay.UI.View.ShopHats;
using Model.StaticData;
using Zenject;

namespace Gameplay.UI.Presenter.ShopHats
{
    public class HatSlotPresenter : IInitializable, IDisposable
    {
        private readonly UIHatSlotView _view;
        private readonly HatData _hat;
        
        public string HatId => _hat.Id;
        
        public event Action<string> HatSelectButtonClickedEvent; 
        public event Action<string> HatUseButtonClickedEvent;
        
        public HatSlotPresenter(UIHatSlotView view, HatData hat)
        {
            _view = view;
            _hat = hat;
        }

        public void Initialize()
        {
            _view.SelectButtonClickedEvent += OnSelectButtonClicked;   
            _view.UseButtonClickedEvent += OnUseButtonClicked;
            
            _view.SetIcon(_hat.Icon);
            _view.SetPrice(_hat.Price);
        }

        public void Dispose()
        {
            _view.SelectButtonClickedEvent -= OnSelectButtonClicked;
            _view.UseButtonClickedEvent -= OnUseButtonClicked;
        }

        private void OnSelectButtonClicked() => 
            HatSelectButtonClickedEvent?.Invoke(HatId);

        private void OnUseButtonClicked() => 
            HatUseButtonClickedEvent?.Invoke(HatId);
    }
}