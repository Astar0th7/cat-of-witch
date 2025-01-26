using Gameplay.Model;
using Gameplay.UI.Presenter;
using Gameplay.UI.Presenter.Library;
using Gameplay.UI.View;
using Gameplay.UI.View.Library;
using Model.Effects;
using UnityEngine;

namespace Gameplay.Services
{
    public interface IUIBattleFactory
    {
        TimingEffectPresenter CreateEffect(UITimingEffectView effectViewPrefab, BaseEffect effect, Transform parent);
        LibraryCardPresenter CreateLibraryCard(UILibraryCardView libraryCardPrefab, CardStaticData cardData, Transform parent);
    }
}