using UnityEngine;

namespace Root.Model.Settings
{
    [CreateAssetMenu(fileName = "New SettingsDataByDefault", menuName = "StaticData/SettingsDataByDefault")]
    public class SettingsDataByDefault : ScriptableObject
    {
        [field: SerializeField] public float SoundsVolume { get; private set; }
        [field: SerializeField] public int LocaleIndex { get; private set; }
    }
}