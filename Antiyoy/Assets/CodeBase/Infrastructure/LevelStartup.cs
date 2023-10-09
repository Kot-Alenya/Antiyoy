using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Map;
using CodeBase.Gameplay.Terrain;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class LevelStartup : MonoBehaviour
    {
        private MapFactory _terrainFactory;
        private CameraFactory _cameraFactory;

        [Inject]
        private void Constructor(MapFactory terrainFactory, CameraFactory cameraFactory)
        {
            _terrainFactory = terrainFactory;
            _cameraFactory = cameraFactory;
        }

        private void Start()
        {
            var terrain = _terrainFactory.Create();
            _cameraFactory.Create();
        }
    }
}