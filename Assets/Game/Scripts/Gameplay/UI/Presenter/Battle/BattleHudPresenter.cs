using System;
using Gameplay.Services;
using Gameplay.UI.View;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class BattleHudPresenter : IInitializable, IDisposable
    {
        private readonly UIBattleHudView _view;
        private readonly GameplayMenuPresenter _gameplayMenu;

        public event Action EndStepEvent;

        public BattleHudPresenter(
            UIBattleHudView view, 
            GameplayMenuPresenter gameplayMenu)
        {
            _view = view;
            _gameplayMenu = gameplayMenu;
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