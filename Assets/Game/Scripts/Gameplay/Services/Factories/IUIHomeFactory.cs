using Gameplay.UI.View.ShopHats;
using Model.StaticData;
using UnityEngine;

namespace Gameplay.Services
{
    public interface IUIHomeFactory
    {
        UIHatSlotView CreateHatSlot(HatData data, Transform container);
    }
}