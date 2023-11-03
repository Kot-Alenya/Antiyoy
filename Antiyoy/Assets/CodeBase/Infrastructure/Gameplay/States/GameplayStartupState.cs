using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.UI;
using CodeBase.Gameplay.World.Terrain.Region.Rebuilder;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly GameplayUIFactory _gameplayUIFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly RegionCoinsRebuilder _regionCoinsRebuilder;

        public GameplayStartupState(CameraFactory cameraFactory, GameplayUIFactory gameplayUIFactory,
            PlayerFactory playerFactory, RegionCoinsRebuilder regionCoinsRebuilder)
        {
            _cameraFactory = cameraFactory;
            _gameplayUIFactory = gameplayUIFactory;
            _playerFactory = playerFactory;
            _regionCoinsRebuilder = regionCoinsRebuilder;
        }

        public void Enter()
        {
            _cameraFactory.Create();
            _gameplayUIFactory.Create();
            _playerFactory.Create();
            _regionCoinsRebuilder.RebuildAll();
        }
    }
}