using Gameplay.Model;
using Gameplay.Model.Enemy;
using Gameplay.UI.Presenter;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class BattleZone : MonoBehaviour
    {
        private EnemyInfoPresenter _enemyInfoPresenter;
        private EnemyProxy _enemy;
        private PlayerBattleProxy _player;

        [Inject]
        private void Construct(EnemyInfoPresenter enemyInfoPresenter, PlayerBattleProxy playerBattleProxy)
        {
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
        
        public void ApplyCard(CardView cardView)
        {
            cardView.Card.Apply(_player, _enemy);
            cardView.Apply();
        }
    }
}