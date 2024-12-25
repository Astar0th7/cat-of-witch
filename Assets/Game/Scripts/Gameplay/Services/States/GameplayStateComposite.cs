using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class GameplayStateComposite : MonoBehaviour, IHomeGameplayListener, IBattleGameplayListener
    {
        [Inject] 
        private GameplayStateService _gameplayStateService;

        [InjectLocal]
        private List<IGameplayListener> _listeners = new();

        private void Start()
        {
            _gameplayStateService.AddListener(this);
            
            // TODO: Вероятно это надо будет отсюда убрать, но пока пусть будет (Каппа)
            _gameplayStateService.HomeGameplay();
        }

        void IHomeGameplayListener.OnHomeGameplay()
        {
            foreach (IGameplayListener it in _listeners)
            {
                if (it is IHomeGameplayListener listener)
                    listener.OnHomeGameplay();
            }
        }

        void IBattleGameplayListener.OnBattleGameplay()
        {
            foreach (IGameplayListener it in _listeners)
            {
                if (it is IBattleGameplayListener listener)
                    listener.OnBattleGameplay();
            }
        }
    }
}