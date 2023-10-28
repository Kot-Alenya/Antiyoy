using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Project.Services.StateMachine.States;
using CodeBase.MapEditor;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly MapEditorFactory _mapEditorFactory;
        private readonly DebugWindowFactory _debugWindowFactory;
        private readonly ITerrainFactory _terrainFactory;
        private readonly IProgressSaveLoader _progressSaveLoader;
        private readonly WorldProgressSaver _worldProgressSaver;

        public MapEditorStartupState(CameraFactory cameraFactory, MapEditorFactory mapEditorFactory,
            DebugWindowFactory debugWindowFactory, ITerrainFactory terrainFactory,
            IProgressSaveLoader progressSaveLoader, WorldProgressSaver worldProgressSaver)
        {
            _cameraFactory = cameraFactory;
            _mapEditorFactory = mapEditorFactory;
            _debugWindowFactory = debugWindowFactory;
            _terrainFactory = terrainFactory;
            _progressSaveLoader = progressSaveLoader;
            _worldProgressSaver = worldProgressSaver;
        }

        public void Enter()
        {
            _terrainFactory.Create();
            _cameraFactory.Create();
            _mapEditorFactory.Create();
            _debugWindowFactory.Create();

            _progressSaveLoader.RegisterWatcher(_worldProgressSaver);
        }
    }
}