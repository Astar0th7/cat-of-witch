using System.Collections;
using Root.Services.Utils;
using Root.UI.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private UILoadingScreenView _loadingScreen;

        public SceneLoadService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName)
        {
            if (_loadingScreen == null)
            {
                UILoadingScreenView loadingScreenView = Resources.Load<UILoadingScreenView>("Prefabs/UI/LoadingScreen");
                _loadingScreen = Object.Instantiate(loadingScreenView);
                Object.DontDestroyOnLoad(_loadingScreen.gameObject);
            }
            
            _coroutineRunner.StartCoroutine(LoadScene(sceneName));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            _loadingScreen.Show();
            yield return SceneManager.LoadSceneAsync(SceneNames.CLEAN);
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitForSeconds(1);
            _loadingScreen.Hide();
        }
    }
}