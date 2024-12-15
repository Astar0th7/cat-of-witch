using System;
using Gameplay.Model;
using Gameplay.Services;

namespace Game.Scripts.Gameplay
{
    public class PlayerSpawner
    {
        private readonly IBattleFactory _factory;
        public PlayerBattleProxy PlayerBattle { get; private set; }
        
        public event Action<PlayerBattleProxy> PlayerSpawnedEvent; 
 
        public PlayerSpawner(IBattleFactory factory)
        {
            _factory = factory;
        }

        public PlayerBattleProxy SpawnPlayer()
        {
            PlayerBattle = _factory.CreatePlayer();
            PlayerSpawnedEvent?.Invoke(PlayerBattle);
            return PlayerBattle;
        }
    }
}