using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Root.Model;
using Root.Model.Settings;
using Services.SaveLoad;
using UnityEngine;
using Path = System.IO.Path;

namespace Root.Services.SaveLoad
{
    public class DesktopSaveLoadService : ISaveLoadService
    {
        private readonly SettingsProxy _settingsData;
        private readonly PlayerProxy _playerData;
        private readonly IStaticDataService _staticDataService;

        private SaveData _saveData;
        
        public DesktopSaveLoadService(
            SettingsProxy settingsData, 
            PlayerProxy playerData, 
            IStaticDataService staticDataService)
        {
            _settingsData = settingsData;
            _playerData = playerData;
            _staticDataService = staticDataService;
        }
        
        public void Save()
        {
            _saveData.Player = _playerData.OriginalData;
            _saveData.Settings = _settingsData.OriginalData;
            
            string json = JsonConvert.SerializeObject(_saveData);
            using (StreamWriter fileStream = new(SavePath()))
                fileStream.Write(json);
        }

        public void Load()
        {
            string path = SavePath();
            if (File.Exists(path) == false)
            {
                PlayerDataByDefault playerDataByDefault = _staticDataService.PlayerDataByDefault;
                SettingsDataByDefault settingsDataByDefault = _staticDataService.SettingsDataByDefault;
                
                SetPlayerData(
                    playerDataByDefault.MaxHealth, 
                    playerDataByDefault.MaxEnergy, 
                    playerDataByDefault.Deck.Select(x => x.Id).ToList());
                
                SetSettingsData(settingsDataByDefault.SoundsVolume, settingsDataByDefault.LocaleIndex);

                _saveData = new SaveData();
                _saveData.Player = _playerData.OriginalData;
                _saveData.Settings = _settingsData.OriginalData;
                return;
            }

            string json;
            using (StreamReader fileStream = new(path)) 
                json = fileStream.ReadToEnd();
            
            _saveData = JsonConvert.DeserializeObject<SaveData>(json);
            
            SetPlayerData(_saveData.Player.MaxHealth, _saveData.Player.MaxEnergy, _saveData.Player.Deck);
            SetSettingsData(_saveData.Settings.SoundsVolume, _saveData.Settings.LocaleIndex);
        }

        private void SetPlayerData(int maxHealth, int maxEnergy, List<string> deck)
        {
            _playerData.MaxHealth.Value = maxHealth;
            _playerData.MaxEnergy.Value = maxEnergy;
            _playerData.Deck.AddRange(deck);
        }

        private void SetSettingsData(float soundsVolume, int localeIndex)
        {
            _settingsData.SoundsVolume.Value = soundsVolume;
            _settingsData.LocaleIndex.Value = localeIndex;
        }

        private static string SavePath() => 
            Path.Combine(Application.persistentDataPath, "Save.json");
    }
}