using CodeBase.Gameplay.World.Change.Handler;
using CodeBase.Gameplay.World.Change.Recorder;
using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Terrain;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.World
{
    public class WorldFactory
    {
        private readonly DiContainer _container;
        private readonly TerrainFactory _terrainFactory;

        public WorldFactory(DiContainer container, TerrainFactory terrainFactory)
        {
            _container = container;
            _terrainFactory = terrainFactory;
        }

        public IWorldController Create()
        {
            var terrain = CreateTerrain();
            var recorder = new WorldChangeRecorder();
            var handler = new WorldChangeHandler(recorder, terrain);
            var controller = new WorldController(terrain, handler, recorder);

            _container.Bind<IWorldController>().FromInstance(controller).AsSingle();
            
            return controller;
        }

        private IWorldTerrainController CreateTerrain()
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