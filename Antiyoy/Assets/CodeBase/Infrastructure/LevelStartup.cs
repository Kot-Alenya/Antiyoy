using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class LevelStartup : MonoBehaviour
    {
        private WorldFactory _terrainFactory;
        private CameraFactory _cameraFactory;

        [Inject]
        private void Constructor(WorldFactory terrainFactory, CameraFactory cameraFactory)
        {
            _terrainFactory = terrainFactory;
            _cameraFactory = cameraFactory;
        }

        /*
        private void Start()
        {
            var terrain = _terrainFactory.Create();
            _cameraFactory.Create();
        }
        */
    }
}