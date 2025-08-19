using Game.Scripts.Root.Services.StateMachines;
using Gameplay.Model.Battle;
using Model.Effects;

namespace Game.Scripts.Gameplay.Model.Battle.States
{
    public class EnemyStepState : IState
    {
        private readonly Battler _battler;

        public EnemyStepState(Battler battler)
        {
            _battler = battler;
        }
        
        public void Enter()
        {
            _battler.Enemy.TimingEffects.CheckEndedEffects();
            
            if (_battler.Enemy.CurrentHealth.CurrentValue <= 0)
                return;
            
            _battler.Enemy.UseAbility(_battler.Player);
            foreach (BaseEffect effect in _battler.Enemy.TimingEffects.Effects) 
                effect.Apply(_battler.Enemy);
        }

        public void Tick() { }

        public void Exit() { }
    }
}