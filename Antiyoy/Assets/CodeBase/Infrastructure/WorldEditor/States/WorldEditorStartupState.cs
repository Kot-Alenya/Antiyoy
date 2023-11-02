using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.StateMachine.States;
using CodeBase.WorldEditor;

namespace CodeBase.Infrastructure.WorldEditor.States
{
    public class WorldEditorStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly WorldEditorFactory _worldEditorFactory;
        private readonly DebugWindowFactory _debugWindowFactory;
        private readonly TerrainFactory _terrainFactory;

        public WorldEditorStartupState(CameraFactory cameraFactory, WorldEditorFactory worldEditorFactory,
            DebugWindowFactory debugWindowFactory, TerrainFactory terrainFactory)
        {
            _cameraFactory = cameraFactory;
            _worldEditorFactory = worldEditorFactory;
            _debugWindowFactory = debugWindowFactory;
            _terrainFactory = terrainFactory;
        }

        public void Enter()
        {
            _terrainFactory.Create();
            _cameraFactory.Create();
            _worldEditorFactory.Create();
            _debugWindowFactory.Create();
        }
    }
}