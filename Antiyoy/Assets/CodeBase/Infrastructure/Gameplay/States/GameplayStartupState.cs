using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.UI;
using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly TerrainFactory _terrainFactory;
        private readonly IProgressSaveLoader _progressSaveLoader;
        private readonly WorldProgressSaver _worldProgressSaver;
        private readonly WorldProgressLoader _worldProgressLoader;
        private readonly GameplayUIFactory _gameplayUIFactory;
        private readonly PlayerFactory _playerFactory;

        public GameplayStartupState(CameraFactory cameraFactory, TerrainFactory terrainFactory,
            IProgressSaveLoader progressSaveLoader, WorldProgressSaver worldProgressSaver,
            WorldProgressLoader worldProgressLoader, GameplayUIFactory gameplayUIFactory,
            PlayerFactory playerFactory)
        {
            _cameraFactory = cameraFactory;
            _terrainFactory = terrainFactory;
            _progressSaveLoader = progressSaveLoader;
            _worldProgressSaver = worldProgressSaver;
            _worldProgressLoader = worldProgressLoader;
            _gameplayUIFactory = gameplayUIFactory;
            _playerFactory = playerFactory;
        }

        public void Enter()
        {
            _terrainFactory.Create();
            _cameraFactory.Create();
            _gameplayUIFactory.Create();
            _playerFactory.Create();

            _progressSaveLoader.RegisterWatcher(_worldProgressSaver);
            _progressSaveLoader.RegisterWatcher(_worldProgressLoader);

            _progressSaveLoader.Load<WorldProgressData>("World");

            //_regionCoinsCounter.RecountAllRegions();
        }
    }
}