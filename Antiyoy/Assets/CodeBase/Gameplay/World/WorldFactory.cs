using CodeBase.Gameplay.World.Change.Handler;
using CodeBase.Gameplay.World.Change.Recorder;
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

        public IWorldController Create()
        {
            var terrain = CreateTerrainController();
            var recorder = new WorldChangeRecorder();
            var handler = new WorldChangeHandler(recorder, terrain);
            var controller = new WorldController(terrain, handler, recorder);

            return controller;
        }

        private IWorldTerrainController CreateTerrainController()
        {
            var terrain = _terrainFactory.Create();

            for (var y = 0; y < terrain.Size.y; y++)
            for (var x = 0; x < terrain.Size.x; x++)
            {
                var arrayIndex = new Vector2Int(x, y);
                var hex = HexMath.FromArrayIndex(arrayIndex);
                terrain.TryCreateTile(hex, RegionType.Neutral);
            }

            terrain.RecalculateChangedRegions();

            return terrain;
        }
    }
}