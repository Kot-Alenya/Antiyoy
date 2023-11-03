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

        public GameplayStartupState(CameraFactory cameraFactory, GameplayUIFactory gameplayUIFactory,
            PlayerFactory playerFactory,GameplayEcsSystems gameplayEcsSystems)
        {
            _cameraFactory = cameraFactory;
            _gameplayUIFactory = gameplayUIFactory;
            _playerFactory = playerFactory;
            _gameplayEcsSystems = gameplayEcsSystems;
        }

        public void Enter()
        {
            _cameraFactory.Create();
            _gameplayUIFactory.Create();
            _playerFactory.Create();
            _gameplayEcsSystems.Initialize();
        }
    }
}