using System;
using Cysharp.Threading.Tasks;
using R3;
using Root.Model.Settings;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Root.Services.Translation
{
    public class TranslationService : ITranslationService, IInitializable, IDisposable
    {
        private readonly SettingsProxy _settings;
        private IDisposable _localeDisposable;

        public TranslationService(SettingsProxy settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            _localeDisposable = _settings.LocaleIndex.Subscribe(ChangeLocale);
        }

        public void Dispose()
        {
            _localeDisposable?.Dispose();
        }

        public async UniTask<string> Translate(string entryKey, string tableKey)
        {
            var asyncOperationHandle = LocalizationSettings.StringDatabase.GetTableEntryAsync(tableKey, entryKey);
            await asyncOperationHandle.Task;
            return asyncOperationHandle.Task.Result.Entry.LocalizedValue;
        }

        private void ChangeLocale(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}