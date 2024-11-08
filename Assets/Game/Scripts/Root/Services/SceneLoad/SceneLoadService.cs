using System.Collections;
using Root.Services.Utils;
using UnityEngine.SceneManagement;

namespace Root.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoadService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(SceneNames.CLEAN);
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}