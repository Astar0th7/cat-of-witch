using Gameplay.Model.Enemies;
using Gameplay.Services;
using Gameplay.UI.Presenter;
using Root.Services;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class EnemySpawner
    {
        private readonly EnemyInfoPresenter _enemyInfoPresenter;
        private readonly IStaticDataService _staticDataService;
        private readonly IBattleFactory _factory;
        
        public EnemySpawner(
            EnemyInfoPresenter enemyInfoPresenter, 
            IStaticDataService staticDataService, 
            IBattleFactory factory)
        {
            _enemyInfoPresenter = enemyInfoPresenter;
            _staticDataService = staticDataService;
            _factory = factory;
        }

        public EnemyProxy Spawn()
        {
            EnemyStaticData[] enemiesData = _staticDataService.GetAllEnemies();
            EnemyStaticData enemyData = enemiesData[Random.Range(0, enemiesData.Length)];
            EnemyProxy enemy = _factory.CreateEnemy(enemyData);
            _enemyInfoPresenter.SetDataEnemy(enemy);

            return enemy;
        }
    }
}