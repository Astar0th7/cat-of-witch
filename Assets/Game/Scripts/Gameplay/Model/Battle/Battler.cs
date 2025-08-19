using System;
using Game.Scripts.Gameplay;
using Game.Scripts.Gameplay.Model.Battle.States;
using Game.Scripts.Root.Services.StateMachines;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using Gameplay.Services;
using Gameplay.UI.Presenter;
using Model.Effects;
using R3;
using Root.Services.InputService;
using UnityEngine;
using Zenject;

namespace Gameplay.Model.Battle
{
    public class Battler : ITickable, IHomeGameplayListener, IBattleGameplayListener
    {
        private readonly BattleModel _original;
        private readonly EnemySpawner _enemySpawner;
        private readonly PlayerSpawner _playerSpawner;
        private readonly BattleHudPresenter _battleHudPresenter;
        private readonly BattleWinPresenter _battleWinPresenter;
        private readonly BattleLosePresenter _battleLosePresenter;
        private readonly LocationPresenter _locationPresenter;
        private readonly IInputService _inputService;

        private StateMachine _stateMachine;

        public EnemyProxy Enemy { get; private set; }
        public PlayerBattleProxy Player { get; private set; }

        public event Action RoundEndedEvent;
        public ReactiveProperty<int> Rounds { get; }

        public Battler(
            BattleModel original, 
            PlayerSpawner playerSpawner,
            EnemySpawner enemySpawner,
            BattleHudPresenter battleHudPresenter,
            BattleWinPresenter battleWinPresenter,
            BattleLosePresenter battleLosePresenter,
            LocationPresenter locationPresenter,
            IInputService inputService)
        {
            _original = original;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _battleHudPresenter = battleHudPresenter;
            _battleWinPresenter = battleWinPresenter;
            _battleLosePresenter = battleLosePresenter;
            _locationPresenter = locationPresenter;
            _inputService = inputService;

            RegisterStateMachine();
            
            Rounds = new ReactiveProperty<int>(_original.Rounds);
            
            Rounds.Skip(1).Subscribe(value => _original.Rounds = value);
        }

        private void RegisterStateMachine()
        {
            _stateMachine = new StateMachine();
            _stateMachine.RegisterState(new StartBattleState(this, _inputService, _playerSpawner, _enemySpawner, _locationPresenter));
            _stateMachine.RegisterState(new EnemyStepState(this));
            _stateMachine.RegisterState(new PlayerStartStepState(this, _stateMachine));
            _stateMachine.RegisterState(new PlayerStepState());
        }

        public void Tick()
        {
            _stateMachine?.Tick();
            
            // TODO: Cheat!!!
            if (Input.GetKeyDown(KeyCode.W) && Enemy != null) 
                Enemy.TakeDamage(Enemy.MaxHealth);
            
            if (Input.GetKeyDown(KeyCode.L) && Player != null) 
                Player.TakeDamage(Player.MaxHealth);
        }

        public void SetEntities(PlayerBattleProxy player, EnemyProxy enemy)
        {
            Player = player;
            Enemy = enemy;
        }
        
        public void OnBattleGameplay()
        {
            _stateMachine.Enter<StartBattleState>();
            Restart();
            
            Player.PlayerDieEvent += OnPlayerDie;
            Enemy.EnemyDieEvent += OnEnemyDie;
            _battleHudPresenter.EndStepEvent += EndStep;
        }

        public void OnHomeGameplay()
        {
            if (Enemy != null)
            {
                Enemy.EnemyDieEvent -= OnEnemyDie;
                Enemy.Destroy();
            }
            
            if (Player != null)
                Player.PlayerDieEvent -= OnPlayerDie;
            
            _battleHudPresenter.EndStepEvent -= EndStep;
        }

        public bool TryUseCard(CardPresenter card)
        {
            return card.TryApply(Player, Enemy);
        }

        private void EndStep()
        {
            _stateMachine.Enter<EnemyStepState>();
            if (Enemy.CurrentHealth.CurrentValue <= 0)
                return;
            
            EndRound();
            _stateMachine.Enter<PlayerStartStepState>();
        }

        private void EndRound()
        {
            Rounds.Value++;
            RoundEndedEvent?.Invoke();
        }

        private void Restart()
        {
            Rounds.Value = 0;
        }

        private void OnEnemyDie()
        {
            _inputService.DisableKeyboard();
            _inputService.DisableBattle();
            _battleWinPresenter.Show(3, null);
            Enemy.Destroy();
        }

        private void OnPlayerDie()
        {
            _inputService.DisableKeyboard();
            _inputService.DisableBattle();
            _battleLosePresenter.Show();
        }
    }
}