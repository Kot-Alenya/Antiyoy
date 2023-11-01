using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Player;
using CodeBase.Gameplay.Progress;
using CodeBase.Gameplay.Progress.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.UI;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;
using CodeBase.Infrastructure.Services.StateMachine.States;

namespace CodeBase.Infrastructure.Gameplay.States
{
    public class GameplayStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly ITerrainFactory _terrainFactory;
        private readonly IProgressSaveLoader _progressSaveLoader;
        private readonly WorldProgressSaver _worldProgressSaver;
        private readonly WorldProgressLoader _worldProgressLoader;
        private readonly GameplayUIFactory _gameplayUIFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly RegionCoinsCounter _regionCoinsCounter;

        public GameplayStartupState(CameraFactory cameraFactory, ITerrainFactory terrainFactory,
            IProgressSaveLoader progressSaveLoader, WorldProgressSaver worldProgressSaver,
            WorldProgressLoader worldProgressLoader, GameplayUIFactory gameplayUIFactory,
            PlayerFactory playerFactory, RegionCoinsCounter regionCoinsCounter)
        {
            _cameraFactory = cameraFactory;
            _terrainFactory = terrainFactory;
            _progressSaveLoader = progressSaveLoader;
            _worldProgressSaver = worldProgressSaver;
            _worldProgressLoader = worldProgressLoader;
            _gameplayUIFactory = gameplayUIFactory;
            _playerFactory = playerFactory;
            _regionCoinsCounter = regionCoinsCounter;
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

            _regionCoinsCounter.RecountAllRegions();
        }
    }
}