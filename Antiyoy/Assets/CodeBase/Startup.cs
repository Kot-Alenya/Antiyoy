using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Tile;
using UnityEngine;

namespace CodeBase
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private TileStaticData _staticData;
        
        private void Start()
        {
            var tileFactory = new TileFactory();
            var terrainFactory = new TerrainFactory(tileFactory);
            
            tileFactory.Initialize(_staticData);
            terrainFactory.Create();
        }
    }
}