using System;
using Game.Scripts.Gameplay;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using Gameplay.Services;
using Gameplay.UI.Presenter;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Gameplay.Spawners
{
    public class BattleZone : MonoBehaviour, IHomeGameplayListener, IBattleGameplayListener
    {
        private Deck _deck;
        private PlayerSpawner _playerSpawner;
        private BattleHudPresenter _battleHudPresenter;
        private EnemySpawner _enemySpawner;
        private IGameplayStateService _gameplayStateService;

        private EnemyProxy _enemy;
        private PlayerBattleProxy _playerBattle;

        public event Action EnemyEndStepEvent;
        
        [Inject]
        private void Construct(
            Deck deck, 
            PlayerSpawner playerSpawner,
            BattleHudPresenter battleHudPresenter,
            EnemySpawner enemySpawner,
            IGameplayStateService gameplayStateService)
        {
            _deck = deck;
            _playerSpawner = playerSpawner;
            _battleHudPresenter = battleHudPresenter;
            _enemySpawner = enemySpawner;
            _gameplayStateService = gameplayStateService;
        }

        private void Update()
        {
            // TODO: Cheat!!!

            if (Input.GetKeyDown(KeyCode.D) && _enemy != null) 
                _enemy.TakeDamage(_enemy.MaxHealth);
        }

        public void OnBattleGameplay()
        {
            _playerBattle = _playerSpawner.SpawnPlayer();
            _enemy = _enemySpawner.Spawn();
            _enemy.EnemyDieEvent += OnEnemyDie;
            _battleHudPresenter.EndStepEvent += OnEndStep;
        }

        public void OnHomeGameplay()
        { 
            if (_enemy != null) 
                _enemy.EnemyDieEvent -= OnEnemyDie;
            
            _enemy = null;
            _playerBattle = null;
            _battleHudPresenter.EndStepEvent -= OnEndStep;
        }

        public void ApplyCard(CardPresenter card)
        {
            if (card.TryApply(_playerBattle, _enemy) == false) 
                _deck.AddCard(card);
        }

        private void OnEndStep()
        {
            _enemy.UseAbility(_playerBattle);

            _playerBattle.AddEnergy(3);
            EnemyEndStepEvent?.Invoke();
        }

        private void OnEnemyDie()
        {
            _gameplayStateService.HomeGameplay();
        }
    }
}