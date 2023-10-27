using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.StateMachine.States;
using CodeBase.MapEditor;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly MapEditorFactory _mapEditorFactory;
        private readonly DebugWindowFactory _debugWindowFactory;
        private readonly ITerrainFactory _terrainFactory;

        public MapEditorStartupState(CameraFactory cameraFactory, MapEditorFactory mapEditorFactory,
            DebugWindowFactory debugWindowFactory, ITerrainFactory terrainFactory)
        {
            _cameraFactory = cameraFactory;
            _mapEditorFactory = mapEditorFactory;
            _debugWindowFactory = debugWindowFactory;
            _terrainFactory = terrainFactory;
        }

        public void Enter()
        {
            _terrainFactory.Create();
            _cameraFactory.Create();
            _mapEditorFactory.Create();
            _debugWindowFactory.Create();
        }
    }
}