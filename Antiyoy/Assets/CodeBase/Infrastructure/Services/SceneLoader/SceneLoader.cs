using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void Load(string sceneName, LoadSceneMode loadMode) => SceneManager.LoadScene(sceneName, loadMode);

        public AsyncOperation LoadAsync(string sceneName, LoadSceneMode loadMode) =>
            SceneManager.LoadSceneAsync(sceneName, loadMode);
    }
}