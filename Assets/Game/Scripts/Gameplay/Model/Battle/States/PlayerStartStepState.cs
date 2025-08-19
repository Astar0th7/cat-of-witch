using Game.Scripts.Root.Services.StateMachines;
using Gameplay.Model.Battle;
using Model.Effects;

namespace Game.Scripts.Gameplay.Model.Battle.States
{
    public class PlayerStartStepState : IState
    {
        private readonly Battler _battler;
        private readonly StateMachine _stateMachine;

        public PlayerStartStepState(Battler battler, StateMachine stateMachine)
        {
            _battler = battler;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _battler.Player.TimingEffects.CheckEndedEffects();
            
            if (_battler.Player.CurrentHealth.CurrentValue <= 0)
                return;
            
            _battler.Player.AddEnergy(3);
            foreach (BaseEffect effect in _battler.Player.TimingEffects.Effects) 
                effect.Apply(_battler.Player);
            
            _stateMachine.Enter<PlayerStepState>();
        }

        public void Tick() { }

        public void Exit() { }
    }
}