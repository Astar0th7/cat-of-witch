using System;
using Root.Model.Settings;

namespace Root.Model
{
    [Serializable]
    public class SaveData
    {
        public SettingsData Settings;
        public PlayerData Player;
    }
}