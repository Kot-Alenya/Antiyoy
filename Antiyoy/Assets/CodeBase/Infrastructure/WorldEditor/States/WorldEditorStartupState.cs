﻿using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Progress;
using CodeBase.Gameplay.Progress.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;
using CodeBase.WorldEditor;

namespace CodeBase.Infrastructure.WorldEditor.States
{
    public class WorldEditorStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly WorldEditorFactory _worldEditorFactory;
        private readonly DebugWindowFactory _debugWindowFactory;
        private readonly ITerrainFactory _terrainFactory;
        private readonly IProgressSaveLoader _progressSaveLoader;
        private readonly WorldProgressSaver _worldProgressSaver;
        private readonly WorldProgressLoader _worldProgressLoader;

        public WorldEditorStartupState(CameraFactory cameraFactory, WorldEditorFactory worldEditorFactory,
            DebugWindowFactory debugWindowFactory, ITerrainFactory terrainFactory,
            IProgressSaveLoader progressSaveLoader, WorldProgressSaver worldProgressSaver,
            WorldProgressLoader worldProgressLoader)
        {
            _cameraFactory = cameraFactory;
            _worldEditorFactory = worldEditorFactory;
            _debugWindowFactory = debugWindowFactory;
            _terrainFactory = terrainFactory;
            _progressSaveLoader = progressSaveLoader;
            _worldProgressSaver = worldProgressSaver;
            _worldProgressLoader = worldProgressLoader;
        }

        public void Enter()
        {
            _terrainFactory.Create();
            _cameraFactory.Create();
            _worldEditorFactory.Create();
            _debugWindowFactory.Create();

            _progressSaveLoader.RegisterWatcher(_worldProgressSaver);
            _progressSaveLoader.RegisterWatcher(_worldProgressLoader);

            _progressSaveLoader.Load<WorldProgressData>("World");
        }
    }
}