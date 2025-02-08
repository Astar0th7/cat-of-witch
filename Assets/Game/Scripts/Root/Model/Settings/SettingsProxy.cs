using R3;

namespace Root.Model.Settings
{
    public class SettingsProxy
    {
        private readonly SettingsData _originalData;

        public ReactiveProperty<float> SoundsVolume { get; }
        public ReactiveProperty<int> LocaleIndex { get; }

        public SettingsData OriginalData => _originalData;
        
        public SettingsProxy(SettingsData originalData)
        {
            _originalData = originalData;
            
            SoundsVolume = new ReactiveProperty<float>(_originalData.SoundsVolume);
            LocaleIndex = new ReactiveProperty<int>(_originalData.LocaleIndex);
            
            SoundsVolume.Skip(1).Subscribe(value => _originalData.SoundsVolume = value);
            LocaleIndex.Skip(1).Subscribe(value => _originalData.LocaleIndex = value);
        }
    }
}