using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        public void Load(string sceneName, LoadSceneMode loadMode);

        public AsyncOperation LoadAsync(string sceneName, LoadSceneMode loadMode);
    }
}