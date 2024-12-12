using Cysharp.Threading.Tasks;
using UnityEngine.Localization.Settings;

namespace Game.Scripts.Root.Services.Translation
{
    public class TranslationService : ITranslationService
    {
        public async UniTask<string> Translate(string entryKey, string tableKey)
        {
            var asyncOperationHandle = LocalizationSettings.StringDatabase.GetTableEntryAsync(tableKey, entryKey);
            await asyncOperationHandle.Task;
            return asyncOperationHandle.Task.Result.Entry.LocalizedValue;
        } 
    }
}