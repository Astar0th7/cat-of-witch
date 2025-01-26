using System.Linq;
using Gameplay.Model;
using Services.SaveLoad;

namespace Root.Services.SaveLoad
{
    public class DesktopSaveLoadService : ISaveLoadService
    {
        private readonly PlayerProxy _player;
        private readonly IStaticDataService _staticDataService;

        public DesktopSaveLoadService(PlayerProxy player, IStaticDataService staticDataService)
        {
            _player = player;
            _staticDataService = staticDataService;
        }
        
        public void Save()
        {
        }

        public void Load()
        {
            PlayerDataByDefault playerData = _staticDataService.PlayerDataByDefault;
            _player.MaxHealth.Value = playerData.MaxHealth;
            _player.MaxEnergy.Value = playerData.MaxEnergy;
            _player.Deck.AddRange(playerData.Deck.Select(x => x.Id));
        }
    }
}