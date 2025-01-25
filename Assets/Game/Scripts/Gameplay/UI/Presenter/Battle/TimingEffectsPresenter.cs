using System.Collections.Generic;
using System.Linq;
using Gameplay.Services;
using Gameplay.UI.View;
using Model.Effects;
using Root.Services;

namespace Gameplay.UI.Presenter
{
    public class TimingEffectsPresenter
    {
        private readonly UITimingEffectsView _view;
        private readonly IUIBattleFactory _uiBattleFactory;
        private readonly IStaticDataService _staticDataService;

        private readonly List<TimingEffectPresenter> _timingEffects = new();
        
        public TimingEffectsPresenter(
            UITimingEffectsView view, 
            IUIBattleFactory uiBattleFactory, 
            IStaticDataService staticDataService)
        {
            _view = view;
            _uiBattleFactory = uiBattleFactory;
            _staticDataService = staticDataService;
        }

        public void AddTimingEffect(BaseEffect effect)
        {
            TimingEffectPresenter effectPresenter = _uiBattleFactory
                .CreateEffect(_staticDataService.TimingEffectPrefab, effect, _view.Container);
            
            _timingEffects.Add(effectPresenter);
        }

        public void RemoveTimingEffect(BaseEffect effect)
        {
            TimingEffectPresenter effectPresenter = _timingEffects.FirstOrDefault(x => x.Id == effect.Id);
            if (effectPresenter == null)
                return;

            effectPresenter.Dispose();
            _timingEffects.Remove(effectPresenter);
        }
    }
}