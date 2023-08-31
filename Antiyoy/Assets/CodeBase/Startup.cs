using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private TileStaticData _tileStaticData;
        [SerializeField] private TerrainStaticData _terrainStaticData;
        
        private void Start()
        {
            var tileFactory = new TileFactory();
            tileFactory.Initialize(_tileStaticData);

            var terrainFactory = new TerrainFactory(tileFactory);
            terrainFactory.Initialize(_terrainStaticData);
            
            terrainFactory.Create();
        }
    }
}