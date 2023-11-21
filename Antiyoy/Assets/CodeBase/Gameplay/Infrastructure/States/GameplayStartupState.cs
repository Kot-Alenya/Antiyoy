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

        public GameplayStartupState(CameraFactory cameraFactory, TerrainFactory terrainFactory,
            GameplayEcsFactory ecsFactory)
        {
            _cameraFactory = cameraFactory;
            _terrainFactory = terrainFactory;
            _ecsFactory = ecsFactory;
        }

        public void Enter()
        {
            _ecsFactory.Create();
            _cameraFactory.Create();
            _terrainFactory.Initialize();
            _terrainFactory.Create();
        }
    }
}