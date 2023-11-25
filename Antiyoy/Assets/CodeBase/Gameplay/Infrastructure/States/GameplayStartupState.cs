using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.GameplayCamera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.ProjectStateMachine;

namespace CodeBase.Gameplay.Infrastructure.States
{
    public class GameplayStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly TerrainFactory _terrainFactory;
        private readonly GameplayEcsFactory _ecsFactory;
        private readonly GameplayStaticDataProvider _staticDataProvider;

        public GameplayStartupState(CameraFactory cameraFactory, TerrainFactory terrainFactory,
            GameplayEcsFactory ecsFactory,GameplayStaticDataProvider staticDataProvider)
        {
            _cameraFactory = cameraFactory;
            _terrainFactory = terrainFactory;
            _ecsFactory = ecsFactory;
            _staticDataProvider = staticDataProvider;
        }

        public void Enter()
        {
            _staticDataProvider.Initialize();
            _ecsFactory.Create();
            _cameraFactory.Create();
            _terrainFactory.Initialize();
            _terrainFactory.Create();
        }
    }
}