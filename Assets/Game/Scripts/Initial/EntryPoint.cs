using Root.Services.SceneLoad;
using Services.SaveLoad;
using Zenject;

namespace Initial
{
    public class EntryPoint : IInitializable
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ISaveLoadService _saveLoadService;

        public EntryPoint(ISceneLoadService sceneLoadService, ISaveLoadService saveLoadService)
        {
            _sceneLoadService = sceneLoadService;
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            _saveLoadService.Load();

            _sceneLoadService.Load(SceneNames.MENU);
        }
    }
}