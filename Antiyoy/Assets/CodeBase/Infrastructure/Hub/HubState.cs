﻿using CodeBase.Infrastructure.Project.Data;
using CodeBase.Infrastructure.Project.Services.SceneLoader;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Hub
{
    public class HubState : IEnterState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataProvider _staticDataProvider;

        public HubState(ISceneLoader sceneLoader, IStaticDataProvider staticDataProvider)
        {
            _sceneLoader = sceneLoader;
            _staticDataProvider = staticDataProvider;
        }

        public void Enter()
        {
            var scenesNameConfig = _staticDataProvider.Get<ProjectScenesNameConfig>();
            _sceneLoader.LoadAsync(scenesNameConfig.GameHub, LoadSceneMode.Single);
        }
    }
}