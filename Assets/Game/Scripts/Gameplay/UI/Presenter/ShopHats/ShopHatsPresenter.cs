using System;
using System.Collections.Generic;
using Gameplay.Services;
using Gameplay.UI.View.ShopHats;
using Model.StaticData;
using Root.Services;
using Zenject;

namespace Gameplay.UI.Presenter.ShopHats
{
    public class ShopHatsPresenter : IInitializable, IDisposable
    {
        private readonly UIShopHatsView _view;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIHomeFactory _homeFactory;
        private readonly List<HatSlotPresenter> _hatSlotPresenters = new();

        public ShopHatsPresenter(UIShopHatsView view, IStaticDataService staticDataService, IUIHomeFactory homeFactory)
        {
            _view = view;
            _staticDataService = staticDataService;
            _homeFactory = homeFactory;
        }
        
        public void Initialize()
        {
            _view.CloseButtonClickedEvent += OnCloseButtonClicked;

            foreach (HatData hatData in _staticDataService.HatsData)
            {
                UIHatSlotView hatSlotView = _homeFactory.CreateHatSlot(hatData, _view.Container);
                HatSlotPresenter hatSlotPresenter = new HatSlotPresenter(hatSlotView, hatData);
                hatSlotPresenter.Initialize();
                _hatSlotPresenters.Add(hatSlotPresenter);
            }

            foreach (HatSlotPresenter hatSlotPresenter in _hatSlotPresenters)
            {
                hatSlotPresenter.HatSelectButtonClickedEvent += OnHatSelectButtonClicked;
                hatSlotPresenter.HatUseButtonClickedEvent += OnHatUseButtonClicked;
            }
        }

        public void Dispose()
        {
            _view.CloseButtonClickedEvent -= OnCloseButtonClicked;
            
            foreach (HatSlotPresenter hatSlotPresenter in _hatSlotPresenters)
            {
                hatSlotPresenter.HatSelectButtonClickedEvent -= OnHatSelectButtonClicked;
                hatSlotPresenter.HatUseButtonClickedEvent -= OnHatUseButtonClicked;
                hatSlotPresenter.Dispose();
            }
        }

        public void Show()
        {
            _view.Show();
        }

        private void Hide()
        {
            _view.Hide();
        }

        private void OnCloseButtonClicked() => 
            Hide();

        private void OnHatSelectButtonClicked(string hatId)
        {
        }

        private void OnHatUseButtonClicked(string hatId)
        {
        }
    }
}