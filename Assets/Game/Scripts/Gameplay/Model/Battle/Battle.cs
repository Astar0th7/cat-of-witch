using System;
using Game.Scripts.Gameplay;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using Gameplay.Services;
using Model;
using Gameplay.UI.Presenter;
using Model.Effects;
using R3;
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
        private readonly GameplayStateService _gameplayStateService;

        private EnemyProxy _enemy;
        private PlayerBattleProxy _playerBattle;
        
        public event Action RoundEndedEvent;
        public ReactiveProperty<int> Rounds { get; }

        public Battle(
            BattleModel original, 
            PlayerSpawner playerSpawner,
            EnemySpawner enemySpawner,
            BattleHudPresenter battleHudPresenter, 
            GameplayStateService gameplayStateService)
        {
            _original = original;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _battleHudPresenter = battleHudPresenter;
            _gameplayStateService = gameplayStateService;

            Rounds = new ReactiveProperty<int>(_original.Rounds);
            
            Rounds.Skip(1).Subscribe(value => _original.Rounds = value);
        }

        public void Tick()
        {
            // TODO: Cheat!!!
            if (Input.GetKeyDown(KeyCode.D) && _enemy != null) 
                _enemy.TakeDamage(_enemy.MaxHealth);
        }

        public void OnHomeGameplay()
        {
            if (_enemy != null)
                _enemy.EnemyDieEvent -= OnEnemyDie;
            
            _battleHudPresenter.EndStepEvent -= EndStep;
        }

        public void OnBattleGameplay()
        {
            _playerBattle = _playerSpawner.SpawnPlayer();
            _enemy = _enemySpawner.Spawn();
            Restart();
            
            _enemy.EnemyDieEvent += OnEnemyDie;
            _battleHudPresenter.EndStepEvent += EndStep;
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
            _enemy.UseAbility(_playerBattle);
            foreach (BaseEffect effect in _enemy.TimingEffects.Effects) 
                effect.Apply(_enemy);
            
            _enemy.TimingEffects.CheckEndedEffects();
        }

        private void PlayerStartStep()
        {
            _playerBattle.AddEnergy(3);
            foreach (BaseEffect effect in _playerBattle.TimingEffects.Effects) 
                effect.Apply(_playerBattle);
            
            _playerBattle.TimingEffects.CheckEndedEffects();
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
            _gameplayStateService.HomeGameplay();
        }

        public bool TryUseCard(CardPresenter card)
        {
            return card.TryApply(_playerBattle, _enemy);
        }
    }
}