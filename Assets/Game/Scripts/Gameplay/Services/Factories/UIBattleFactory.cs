using Gameplay.UI.Presenter;
using Gameplay.UI.View;
using Model.Effects;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class UIBattleFactory : IUIBattleFactory
    {
        private readonly DiContainer _container;

        public UIBattleFactory(DiContainer container)
        {
            _container = container;
        }

        public TimingEffectPresenter CreateEffect(UITimingEffectView effectViewPrefab, BaseEffect effect, Transform parent)
        {
            UITimingEffectView effectView = Instantiate(effectViewPrefab, parent.position, Quaternion.identity, parent);
            effectView.transform.localScale = Vector3.one;
            TimingEffectPresenter effectPresenter = new(effectView, effect);
            effectPresenter.Initialize();
            return effectPresenter;
        }
        
        private T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
        {
            return _container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }
    }
}