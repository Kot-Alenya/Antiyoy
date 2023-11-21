using CodeBase.Gameplay.PlayerCamera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Infrastructure.ProjectStateMachine;

namespace CodeBase.Gameplay.Infrastructure.States
{
    public class GameplayStartupState : IEnterState
    {
        private readonly CameraFactory _cameraFactory;
        private readonly TerrainFactory _terrainFactory;

        public GameplayStartupState(CameraFactory cameraFactory, TerrainFactory terrainFactory)
        {
            _cameraFactory = cameraFactory;
            _terrainFactory = terrainFactory;
        }

        public void Enter()
        {
            _cameraFactory.Create();
            _terrainFactory.Initialize();
            _terrainFactory.Create();
        }
    }
}