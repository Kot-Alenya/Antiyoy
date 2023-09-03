using _dev.Country;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private CapitalObjectStaticData _capitalPrefab;

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
            var terrain = _terrainFactory.Create();
            _cameraFactory.Create();

            //var countryFactory = new CountryFactory(_capitalPrefab);
            //countryFactory.Create(terrain, new(0, 0), Color.green);
        }
    }
}