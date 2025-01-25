using Gameplay.UI.Presenter;
using Gameplay.UI.View;
using Model.Effects;
using UnityEngine;

namespace Gameplay.Services
{
    public interface IUIBattleFactory
    {
        TimingEffectPresenter CreateEffect(UITimingEffectView effectViewPrefab, BaseEffect effect, Transform parent);
    }
}