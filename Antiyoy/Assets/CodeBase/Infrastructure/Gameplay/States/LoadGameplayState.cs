using CodeBase.Infrastructure.Project;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.StateMachine.States;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class LoadGameplayState : IEnterState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataProvider _staticDataProvider;

        public LoadGameplayState(ISceneLoader sceneLoader, IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _staticDataProvider = staticDataProvider;
        }

        public void Enter()
        {
            var scenesNameConfig = _staticDataProvider.Get<ProjectScenesNameConfig>();
            _sceneLoader.Load(scenesNameConfig.Gameplay, LoadSceneMode.Single);
        }
    }
}