using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Ecs;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.UI;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly GameplayUIFactory _gameplayUIFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly GameplayEcsSystems _gameplayEcsSystems;
        private readonly DebugWindowFactory _debugWindowFactory;

        public GameplayStartupState(CameraFactory cameraFactory, GameplayUIFactory gameplayUIFactory,
            PlayerFactory playerFactory, GameplayEcsSystems gameplayEcsSystems,DebugWindowFactory debugWindowFactory)
        {
            _cameraFactory = cameraFactory;
            _gameplayUIFactory = gameplayUIFactory;
            _playerFactory = playerFactory;
            _gameplayEcsSystems = gameplayEcsSystems;
            _debugWindowFactory = debugWindowFactory;
        }

        public void Enter()
        {
            _cameraFactory.Create();
            _gameplayUIFactory.Create();
            _playerFactory.Create();
            _gameplayEcsSystems.Initialize();
            _debugWindowFactory.Create();
        }
    }
}