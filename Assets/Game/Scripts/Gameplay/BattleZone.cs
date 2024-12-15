using System;
using Game.Scripts.Gameplay;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using Gameplay.UI.Presenter;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Gameplay.Spawners
{
    public class BattleZone : MonoBehaviour
    {
        private Deck _deck;
        private PlayerSpawner _playerSpawner;
        private BattleHudPresenter _battleHudPresenter;
        private EnemySpawner _enemySpawner;

        private EnemyProxy _enemy;
        private PlayerBattleProxy _playerBattle;

        public event Action EnemyEndStepEvent;
        
        [Inject]
        private void Construct(
            Deck deck, 
            PlayerSpawner playerSpawner,
            BattleHudPresenter battleHudPresenter,
            EnemySpawner enemySpawner)
        {
            _deck = deck;
            _playerSpawner = playerSpawner;
            _battleHudPresenter = battleHudPresenter;
            _enemySpawner = enemySpawner;
        }

        private void Start()
        {
            _playerBattle = _playerSpawner.SpawnPlayer();
            
            _enemy = _enemySpawner.Spawn();
            _battleHudPresenter.EndStepEvent += OnEndStep;
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
    }
}