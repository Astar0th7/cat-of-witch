using System;
using Gameplay.Services;
using Gameplay.UI.View;
using Root.Services.InputService;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class BattleWinPresenter : IInitializable, IDisposable
    {
        private readonly UIBattleWinView _view;
        private readonly IGameplayStateService _gameplayStateService;

        public BattleWinPresenter(
            UIBattleWinView view, 
            IGameplayStateService gameplayStateService)
        {
            _view = view;
            _gameplayStateService = gameplayStateService;
        }
        
        public void Initialize()
        {
            _view.ContinueButtonClickedEvent += OnContinueButtonClicked;
        }

        public void Dispose()
        {
            _view.ContinueButtonClickedEvent -= OnContinueButtonClicked;   
        }

        public void Show(int amount, Sprite icon)
        {
            _view.SetReward(amount, icon);
            _view.Show();
        }
        
        private void OnContinueButtonClicked()
        {
            _view.Hide();
            _gameplayStateService.HomeGameplay();
        }
    }
}