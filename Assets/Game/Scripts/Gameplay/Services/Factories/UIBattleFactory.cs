using Gameplay.Model;
using Gameplay.UI.Presenter;
using Gameplay.UI.Presenter.Library;
using Gameplay.UI.View;
using Gameplay.UI.View.Library;
using Model.Effects;
using Root.Services.Translation;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class UIBattleFactory : IUIBattleFactory
    {
        private readonly DiContainer _container;
        private readonly ITranslationService _translationService;

        public UIBattleFactory(DiContainer container, ITranslationService translationService)
        {
            _container = container;
            _translationService = translationService;
        }

        public TimingEffectPresenter CreateEffect(UITimingEffectView effectViewPrefab, BaseEffect effect, Transform parent)
        {
            UITimingEffectView effectView = Instantiate(effectViewPrefab, parent.position, Quaternion.identity, parent);
            effectView.transform.localScale = Vector3.one;
            TimingEffectPresenter effectPresenter = new(effectView, effect);
            effectPresenter.Initialize();
            return effectPresenter;
        }

        public LibraryCardPresenter CreateLibraryCard(UILibraryCardView libraryCardPrefab, CardStaticData cardData, Transform parent)
        {
            UILibraryCardView libraryCardView = Instantiate(libraryCardPrefab, parent.position, Quaternion.identity, parent);
            libraryCardView.transform.localScale = Vector3.one;
            LibraryCardPresenter libraryCardPresenter = new(libraryCardView, cardData, _translationService);
            libraryCardPresenter.Initialize();
            return libraryCardPresenter;
        }
        
        private T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
        {
            return _container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }
    }
}