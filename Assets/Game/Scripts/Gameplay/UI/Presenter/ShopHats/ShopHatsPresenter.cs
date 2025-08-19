using System;
using System.Collections.Generic;
using Gameplay.Services;
using Gameplay.UI.View.ShopHats;
using Model.StaticData;
using Root.Model;
using Root.Services;
using Zenject;

namespace Gameplay.UI.Presenter.ShopHats
{
    public class ShopHatsPresenter : IInitializable, IDisposable
    {
        private readonly UIShopHatsView _view;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIHomeFactory _homeFactory;
        private readonly PlayerProxy _playerProxy;
        private readonly List<HatSlotPresenter> _hatSlotPresenters = new();

        private string _hatSelected;
        
        public ShopHatsPresenter(
            UIShopHatsView view, 
            IStaticDataService staticDataService, 
            IUIHomeFactory homeFactory,
            PlayerProxy playerProxy)
        {
            _view = view;
            _staticDataService = staticDataService;
            _homeFactory = homeFactory;
            _playerProxy = playerProxy;
        }
        
        public void Initialize()
        {
            _view.CloseButtonClickedEvent += OnCloseButtonClicked;

            foreach (HatData hatData in _staticDataService.HatsData)
            {
                UIHatSlotView hatSlotView = _homeFactory.CreateHatSlot(hatData, _view.Container);
                HatSlotPresenter hatSlotPresenter = new(hatSlotView, hatData);
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
            _hatSelected = _playerProxy.HatId.CurrentValue;
            SelectButton();
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
            _hatSelected = hatId;
            SelectButton();
        }

        private void OnHatUseButtonClicked(string hatId)
        {
            _hatSelected = hatId;
            _playerProxy.HatId.Value = hatId;
            _playerProxy.TryAddHat(hatId);
            SelectButton();
        }

        private void SelectButton()
        {
            foreach (HatSlotPresenter hatSlotPresenter in _hatSlotPresenters)
            {
                if (hatSlotPresenter.HatId == _hatSelected)
                    hatSlotPresenter.Select();
                else
                    hatSlotPresenter.Deselect();
            }
        }
    }
}