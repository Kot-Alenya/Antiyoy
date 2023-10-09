using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Terrain;
using UnityEngine;

namespace CodeBase.Gameplay.World
{
    public class WorldFactory
    {
        private readonly TerrainFactory _terrainFactory;

        public WorldFactory(TerrainFactory terrainFactory) => _terrainFactory = terrainFactory;

        public WorldController Create()
        {
            var terrainModel = _terrainFactory.Create();
            var world = new WorldController(terrainModel);
            
            CreateTiles(world, terrainModel.Size);
            world.RecalculateChangedRegions();

            return world;
        }

        private void CreateTiles(WorldController world, Vector2Int size)
        {
            for (var y = 0; y < size.y; y++)
            for (var x = 0; x < size.x; x++)
            {
                var arrayIndex = new Vector2Int(x, y);
                var hex = HexMath.FromArrayIndex(arrayIndex);
                world.CreateTile(hex, RegionType.Neutral);
            }
        }
    }
}