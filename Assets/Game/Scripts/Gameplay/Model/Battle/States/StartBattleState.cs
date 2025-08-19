using Game.Scripts.Root.Services.StateMachines;
using Gameplay.Model;
using Gameplay.Model.Battle;
using Gameplay.Model.Enemies;
using Gameplay.UI.Presenter;
using Root.Services.InputService;

namespace Game.Scripts.Gameplay.Model.Battle.States
{
    public class StartBattleState : IState
    {
        private readonly Battler _battler;
        private readonly IInputService _inputService;
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly LocationPresenter _locationPresenter;

        private PlayerBattleProxy _player;
        private EnemyProxy _enemy;

        public StartBattleState(
            Battler battler,
            IInputService inputService, 
            PlayerSpawner playerSpawner, 
            EnemySpawner enemySpawner,
            LocationPresenter locationPresenter)
        {
            _battler = battler;
            _inputService = inputService;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _locationPresenter = locationPresenter;
        }
        
        public void Enter()
        {
            _inputService.EnableKeyboard();
            _inputService.EnableBattle();
            _player = _playerSpawner.SpawnPlayer();
            _enemy = _enemySpawner.Spawn(_locationPresenter.BattleLocation.EnemySpawnPoint);
            _battler.SetEntities(_player, _enemy);
        }

        public void Tick() { }
        public void Exit() { }
    }
}