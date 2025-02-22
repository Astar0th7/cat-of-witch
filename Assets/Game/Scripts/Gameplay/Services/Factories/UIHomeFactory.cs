using Gameplay.UI.View.ShopHats;
using Model.StaticData;
using Root.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Services
{
    public class UIHomeFactory : IUIHomeFactory
    {
        private readonly IStaticDataService _staticDataService;

        public UIHomeFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public UIHatSlotView CreateHatSlot(HatData data, Transform container)
        {
            return Object.Instantiate(_staticDataService.HatSlotView, container);
        }
    }
}