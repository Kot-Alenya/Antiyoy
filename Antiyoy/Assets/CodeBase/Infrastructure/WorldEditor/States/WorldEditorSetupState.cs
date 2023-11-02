using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Infrastructure.Services.StateMachine.States;
using CodeBase.WorldEditor;

namespace CodeBase.Infrastructure.WorldEditor.States
{
    public class WorldEditorSetupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly WorldEditorFactory _worldEditorFactory;
        private readonly DebugWindowFactory _debugWindowFactory;

        public WorldEditorSetupState(CameraFactory cameraFactory, WorldEditorFactory worldEditorFactory,
            DebugWindowFactory debugWindowFactory)
        {
            _cameraFactory = cameraFactory;
            _worldEditorFactory = worldEditorFactory;
            _debugWindowFactory = debugWindowFactory;
        }

        public void Enter()
        {
            _cameraFactory.Create();
            _worldEditorFactory.Create();
            _debugWindowFactory.Create();
        }
    }
}