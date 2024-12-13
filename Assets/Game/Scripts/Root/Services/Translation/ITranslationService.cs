using Cysharp.Threading.Tasks;

namespace Root.Services.Translation
{
    public interface ITranslationService
    {
        UniTask<string> Translate(string entryKey, string tableKey);
    }
}