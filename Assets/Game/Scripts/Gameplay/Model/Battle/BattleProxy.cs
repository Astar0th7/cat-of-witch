using System;
using Gameplay.Services;
using R3;

namespace Gameplay.Model.Battle
{
    public class BattleProxy : IBattleGameplayListener
    {
        private readonly BattleModel _original;

        public ReactiveProperty<int> Rounds { get; }
        
        public event Action RoundEndedEvent; 
        
        public BattleProxy(BattleModel original)
        {
            _original = original;
            
            Rounds = new ReactiveProperty<int>(_original.Rounds);
            
            Rounds.Skip(1).Subscribe(value => _original.Rounds = value);
        }

        public void EndRound()
        {
            Rounds.Value++;
            RoundEndedEvent?.Invoke();
        }

        public void OnBattleGameplay()
        {
            Restart();
        }

        private void Restart()
        {
            Rounds.Value = 0;
        }
    }
}