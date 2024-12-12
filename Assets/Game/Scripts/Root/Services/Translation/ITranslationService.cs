using Cysharp.Threading.Tasks;

namespace Game.Scripts.Root.Services.Translation
{
    public interface ITranslationService
    {
        UniTask<string> Translate(string entryKey, string tableKey);
    }
}