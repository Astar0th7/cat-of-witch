using System.Collections.Generic;

namespace Gameplay.Services
{
    public class GameplayStateService : IGameplayStateService
    {
        private readonly List<IGameplayListener> _listeners = new();
        
        public GameplayState State { get; private set; }
        
        public void AddListener(IGameplayListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IGameplayListener listener)
        {
            _listeners.Remove(listener);
        }

        public void HomeGameplay()
        {
            foreach (IGameplayListener it in _listeners)
            {
                if (it is IHomeGameplayListener listener)
                    listener.OnHomeGameplay();
            }
            
            State = GameplayState.Home;
        }

        public void BattleGameplay()
        {
            foreach (IGameplayListener it in _listeners)
            {
                if (it is IBattleGameplayListener listener)
                    listener.OnBattleGameplay();
            }
            
            State = GameplayState.Battle;
        }
    }
}