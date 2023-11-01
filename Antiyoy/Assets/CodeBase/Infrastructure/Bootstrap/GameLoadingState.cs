using CodeBase.Infrastructure.Project;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.StateMachine.States;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class GameLoadingState : IEnterState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataProvider _staticDataProvider;

        public GameLoadingState(ISceneLoader sceneLoader, IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _staticDataProvider = staticDataProvider;
        }

        public void Enter()
        {
            var scenesNameConfig = _staticDataProvider.Get<ProjectScenesNameConfig>();
            _sceneLoader.Load(scenesNameConfig.GameLoading, LoadSceneMode.Single);
        }
    }
}