using System;
using Gameplay.Services;
using Gameplay.UI.View;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class BattleHudPresenter : IInitializable, IDisposable
    {
        private readonly UIBattleHudView _view;

        public event Action EndStepEvent;

        public BattleHudPresenter(UIBattleHudView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.EndStepClickedEvent += OnEndStepClicked;
        }

        public void Dispose()
        {
            _view.EndStepClickedEvent -= OnEndStepClicked;
        }

        private void OnEndStepClicked()
        {
            EndStepEvent?.Invoke();
        }
    }
}