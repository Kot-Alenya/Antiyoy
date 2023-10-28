using CodeBase.Infrastructure.Project.Data;
using CodeBase.Infrastructure.Project.Services.SceneLoader;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.MapEditor
{
    public class WorldEditorState : IEnterState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataProvider _staticDataProvider;

        public WorldEditorState(ISceneLoader sceneLoader, IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _staticDataProvider = staticDataProvider;
        }

        public void Enter()
        {
            var scenesNameConfig = _staticDataProvider.Get<ProjectScenesNameConfig>();
            _sceneLoader.Load(scenesNameConfig.WorldEditor, LoadSceneMode.Single);
        }
    }
}