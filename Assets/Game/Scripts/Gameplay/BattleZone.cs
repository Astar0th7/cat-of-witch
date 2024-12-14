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
        private PlayerBattleProxy _player;
        private BattleHudPresenter _battleHudPresenter;
        private EnemySpawner _enemySpawner;

        private EnemyProxy _enemy;

        public event Action EnemyEndStepEvent;
        
        [Inject]
        private void Construct(
            Deck deck, 
            PlayerBattleProxy playerBattleProxy,
            BattleHudPresenter battleHudPresenter,
            EnemySpawner enemySpawner)
        {
            _deck = deck;
            _player = playerBattleProxy;
            _battleHudPresenter = battleHudPresenter;
            _enemySpawner = enemySpawner;
        }

        private void Start()
        {
            _enemy = _enemySpawner.Spawn();
            _battleHudPresenter.EndStepEvent += OnEndStep;
        }

        public void ApplyCard(CardPresenter card)
        {
            if (card.TryApply(_player, _enemy) == false) 
                _deck.AddCard(card);
        }

        private void OnEndStep()
        {
            _enemy.UseAbility(_player);

            _player.Energy.Value += 3;
            EnemyEndStepEvent?.Invoke();
        }
    }
}