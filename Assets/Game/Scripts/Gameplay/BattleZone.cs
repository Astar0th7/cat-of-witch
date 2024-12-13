using System;
using Gameplay.Model;
using Gameplay.Model.Enemy;
using Gameplay.Presenters;
using Gameplay.UI.Presenter;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class BattleZone : MonoBehaviour
    {
        private Deck _deck;
        private EnemyInfoPresenter _enemyInfoPresenter;
        private PlayerBattleProxy _player;
        private BattleHudPresenter _battleHudPresenter;

        private EnemyProxy _enemy;

        public event Action EnemyEndStepEvent;
        
        [Inject]
        private void Construct(
            Deck deck, 
            EnemyInfoPresenter enemyInfoPresenter, 
            PlayerBattleProxy playerBattleProxy,
            BattleHudPresenter battleHudPresenter)
        {
            _deck = deck;
            _enemyInfoPresenter = enemyInfoPresenter;
            _player = playerBattleProxy;
            _battleHudPresenter = battleHudPresenter;
        }

        private void Start()
        {
            EnemyData enemyData = new()
            {
                Id = "Rat",
                NameKey = "Rat",
                MaxHealth = 100,
                CurrentHealth = 100,
            };

            _enemy = new EnemyProxy(enemyData);
            _enemyInfoPresenter.SetDataEnemy(_enemy);
            _battleHudPresenter.EndStepEvent += OnEndStep;
        }

        public void ApplyCard(CardPresenter card)
        {
            if (card.TryApply(_player, _enemy) == false) 
                _deck.AddCard(card);
        }

        private void OnEndStep()
        {
            // TODO: Тут будет атака врага

            _player.Energy.Value += 3;
            EnemyEndStepEvent?.Invoke();
        }
    }
}