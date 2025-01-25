using System;
using Gameplay.UI.View;
using Model.Effects;
using R3;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class TimingEffectPresenter
    {
        private readonly UITimingEffectView _view;
        private readonly BaseEffect _effect;

        private readonly CompositeDisposable _disposables = new();
        
        public string Id => _effect.Id;
        
        public TimingEffectPresenter(UITimingEffectView view, BaseEffect effect)
        {
            _view = view;
            _effect = effect;
        }

        public void Initialize()
        {
            _disposables.Add(_effect.Duration.Subscribe(value => _view.SetTime(value)));
        }

        public void Dispose()
        {
            _view.Remove();
            _disposables?.Dispose();
        }
    }
}