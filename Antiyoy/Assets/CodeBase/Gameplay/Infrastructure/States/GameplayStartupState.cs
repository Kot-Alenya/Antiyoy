using CodeBase.Gameplay.GameplayCamera;
using CodeBase.ProjectInfrastructure.ProjectStateMachine;

namespace CodeBase.Gameplay.Infrastructure.States
{
    public class GameplayStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;

        public GameplayStartupState(CameraFactory cameraFactory) => _cameraFactory = cameraFactory;

        public void Enter() => _cameraFactory.Create();
    }
}