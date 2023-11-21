using CodeBase.Gameplay.GameplayCamera;
using CodeBase.Gameplay.Terrain;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Startup : MonoBehaviour
    {
        private TerrainFactory _terrainFactory;
        private CameraFactory _cameraFactory;

        [Inject]
        private void Constructor(TerrainFactory terrainFactory, CameraFactory cameraFactory)
        {
            _terrainFactory = terrainFactory;
            _cameraFactory = cameraFactory;
        }

        private void Start()
        {
            _terrainFactory.Create();
            _cameraFactory.Create();
        }
    }
}