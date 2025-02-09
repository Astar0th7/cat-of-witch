using System;
using R3;
using Root.Model.Settings;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Object = UnityEngine.Object;

namespace Root.Services.Audio
{
    public class AudioService : IInitializable, IDisposable
    {
        private readonly SettingsProxy _settings;
        private readonly IStaticDataService _staticDataService;
        private readonly AudioMixer _mixer;

        private AudioProvider _audioProvider;
        private IDisposable _volumeDisposable;

        public AudioService(SettingsProxy settings, IStaticDataService staticDataService)
        {
            _settings = settings;
            _staticDataService = staticDataService;
            _mixer = _staticDataService.AudioMixer;
        }
        
        public void Initialize()
        {
            _audioProvider = Object.Instantiate(_staticDataService.AudioProvider);
            Object.DontDestroyOnLoad(_audioProvider.gameObject);

            _volumeDisposable = _settings.SoundsVolume.Subscribe(ChangeVolume);
        }

        public void Dispose()
        {
            _volumeDisposable?.Dispose();
        }

        private void ChangeVolume(float value)
        {
            float volume = value == 0 ? -80 : Mathf.Log10(value) * 20;
            _mixer.SetFloat("Master", volume);
        }
    }
}