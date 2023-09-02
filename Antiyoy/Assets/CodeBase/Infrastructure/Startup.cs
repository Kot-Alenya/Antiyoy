using CodeBase.Gameplay.Camera;
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
        private void Constructor(TerrainFactory TerrainFactory, CameraFactory cameraFactory)
        {
            _terrainFactory = TerrainFactory;
            _cameraFactory = cameraFactory;
        }

        private void Start()
        {
            _terrainFactory.Create();
            _cameraFactory.Create();
        }
    }
}