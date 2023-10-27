using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Version;
using CodeBase.Infrastructure.Services.StateMachine.States;
using CodeBase.MapEditor;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorStartupState : IEnterState
    {
        private readonly WorldFactory _worldFactory;
        private readonly CameraFactory _cameraFactory;
        private readonly WorldVersionControllerFactory _worldVersionControllerFactory;
        private readonly MapEditorFactory _mapEditorFactory;
        private readonly DebugWindowFactory _debugWindowFactory;

        public MapEditorStartupState(WorldFactory worldFactory, CameraFactory cameraFactory,
            WorldVersionControllerFactory worldVersionControllerFactory, MapEditorFactory mapEditorFactory,
            DebugWindowFactory debugWindowFactory)
        {
            _worldFactory = worldFactory;
            _cameraFactory = cameraFactory;
            _worldVersionControllerFactory = worldVersionControllerFactory;
            _mapEditorFactory = mapEditorFactory;
            _debugWindowFactory = debugWindowFactory;
        }

        public void Enter()
        {
            CreateWorld();

            _cameraFactory.Create();

            _mapEditorFactory.Create();

            _debugWindowFactory.Create();
        }

        private void CreateWorld()
        {
            _worldFactory.Create();
            _worldVersionControllerFactory.Create();
        }
    }
}