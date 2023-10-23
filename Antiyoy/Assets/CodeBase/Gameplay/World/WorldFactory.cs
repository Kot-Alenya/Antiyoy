using CodeBase.Gameplay.World.Hex;
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

        public void Create()
        {
            var terrain = _terrainFactory.Create();

            _container.Bind<IWorldController>().To<WorldController>().AsSingle();

            FillTerrain(terrain);
        }

        private void FillTerrain(IWorldTerrainController terrain)
        {
            for (var y = 0; y < terrain.Size.y; y++)
            for (var x = 0; x < terrain.Size.x; x++)
            {
                var arrayIndex = new Vector2Int(x, y);
                var hex = HexMath.FromArrayIndex(arrayIndex);
                terrain.TryCreateTile(hex, RegionType.Neutral);
            }

            terrain.RecalculateChangedRegions();
        }
    }
}