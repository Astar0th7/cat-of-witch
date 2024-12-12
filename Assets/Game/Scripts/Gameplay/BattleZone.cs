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
        
        private EnemyProxy _enemy;

        [Inject]
        private void Construct(Deck deck, EnemyInfoPresenter enemyInfoPresenter, PlayerBattleProxy playerBattleProxy)
        {
            _deck = deck;
            _enemyInfoPresenter = enemyInfoPresenter;
            _player = playerBattleProxy;
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
        }
        
        public void ApplyCard(CardPresenter card)
        {
            if (card.TryApply(_player, _enemy) == false) 
                _deck.AddCard(card);
        }
    }
}