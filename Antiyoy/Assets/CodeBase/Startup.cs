using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Camera.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private TileStaticData _tileStaticData;
        [SerializeField] private TerrainStaticData _terrainStaticData;
        [SerializeField] private CameraStaticData _cameraStaticData;

        private void Start()
        {
            CreateTerrain();
            CreateCamera();
        }

        private void CreateTerrain()
        {
            var tileFactory = new TileFactory();
            tileFactory.Initialize(_tileStaticData);

            var terrainFactory = new TerrainFactory(tileFactory);
            terrainFactory.Initialize(_terrainStaticData);

            terrainFactory.Create();
        }

        private void CreateCamera()
        {
            var cameraFactory = new CameraFactory();
            cameraFactory.Initialize(_cameraStaticData);
            cameraFactory.Create();
        }
    }
}