using System;
using Game.Scripts.Gameplay;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using Gameplay.Services;
using Model;
using Gameplay.UI.Presenter;
using Model.Effects;
using R3;
using Root.Services.InputService;
using UnityEngine;
using Zenject;

namespace Gameplay.Model.Battle
{
    public class Battle : ITickable, IHomeGameplayListener, IBattleGameplayListener
    {
        private readonly BattleModel _original;
        private readonly EnemySpawner _enemySpawner;
        private readonly PlayerSpawner _playerSpawner;
        private readonly BattleHudPresenter _battleHudPresenter;
        private readonly BattleWinPresenter _battleWinPresenter;
        private readonly BattleLosePresenter _battleLosePresenter;
        private readonly IInputService _inputService;
        private readonly IGameplayStateService _gameplayStateService;

        private EnemyProxy _enemy;
        private PlayerBattleProxy _playerBattle;
        
        public event Action RoundEndedEvent;
        public ReactiveProperty<int> Rounds { get; }

        public Battle(
            BattleModel original, 
            PlayerSpawner playerSpawner,
            EnemySpawner enemySpawner,
            BattleHudPresenter battleHudPresenter,
            BattleWinPresenter battleWinPresenter,
            BattleLosePresenter battleLosePresenter,
            IInputService inputService, 
            IGameplayStateService gameplayStateService)
        {
            _original = original;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _battleHudPresenter = battleHudPresenter;
            _battleWinPresenter = battleWinPresenter;
            _battleLosePresenter = battleLosePresenter;
            _inputService = inputService;
            _gameplayStateService = gameplayStateService;

            Rounds = new ReactiveProperty<int>(_original.Rounds);
            
            Rounds.Skip(1).Subscribe(value => _original.Rounds = value);
        }

        public void Tick()
        {
            // TODO: Cheat!!!
            if (Input.GetKeyDown(KeyCode.W) && _enemy != null) 
                _enemy.TakeDamage(_enemy.MaxHealth);
            
            if (Input.GetKeyDown(KeyCode.L) && _playerBattle != null) 
                _playerBattle.TakeDamage(_playerBattle.MaxHealth);
        }

        public void OnBattleGameplay()
        {
            _inputService.EnableKeyboard();
            _inputService.EnableBattle();
            _playerBattle = _playerSpawner.SpawnPlayer();
            _enemy = _enemySpawner.Spawn();
            Restart();
            
            _playerBattle.PlayerDieEvent += OnPlayerDie;
            _enemy.EnemyDieEvent += OnEnemyDie;
            _battleHudPresenter.EndStepEvent += EndStep;
        }

        public void OnHomeGameplay()
        {
            if (_enemy != null)
                _enemy.EnemyDieEvent -= OnEnemyDie;
            
            if (_playerBattle != null)
                _playerBattle.PlayerDieEvent -= OnPlayerDie;
            
            _battleHudPresenter.EndStepEvent -= EndStep;
        }

        private void EndStep()
        {
            EnemyStep();
            
            if (_enemy.CurrentHealth.CurrentValue <= 0)
                return;
            
            EndRound();
            PlayerStartStep();
        }

        private void EnemyStep()
        {
            _enemy.TimingEffects.CheckEndedEffects();
            
            if (_enemy.CurrentHealth.CurrentValue <= 0)
                return;
            
            _enemy.UseAbility(_playerBattle);
            foreach (BaseEffect effect in _enemy.TimingEffects.Effects) 
                effect.Apply(_enemy);
        }

        private void PlayerStartStep()
        {
            _playerBattle.TimingEffects.CheckEndedEffects();
            
            if (_playerBattle.CurrentHealth.CurrentValue <= 0)
                return;
            
            _playerBattle.AddEnergy(3);
            foreach (BaseEffect effect in _playerBattle.TimingEffects.Effects) 
                effect.Apply(_playerBattle);
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
        }

        private void OnPlayerDie()
        {
            _inputService.DisableKeyboard();
            _inputService.DisableBattle();
            _battleLosePresenter.Show();
        }

        public bool TryUseCard(CardPresenter card)
        {
            return card.TryApply(_playerBattle, _enemy);
        }
    }
}