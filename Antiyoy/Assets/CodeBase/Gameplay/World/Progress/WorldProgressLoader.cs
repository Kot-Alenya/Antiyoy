using CodeBase.Gameplay.World.Progress.SavedData;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Terrain.Tile;
using CodeBase.Gameplay.World.Terrain.Unit;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Infrastructure.Services.ProgressSaveLoader.Watcher;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Progress
{
    public class WorldProgressLoader : IProgressReader<WorldProgressData>
    {
        private readonly TerrainFactory _terrainFactory;
        private readonly ITerrain _terrain;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly TileFactory _tileFactory;
        private readonly UnitFactory _unitFactory;

        public WorldProgressLoader(TerrainFactory terrainFactory, TileFactory tileFactory, ITerrain terrain,
            IStaticDataProvider staticDataProvider)
        {
            _terrainFactory = terrainFactory;
            _tileFactory = tileFactory;
            _terrain = terrain;
            _staticDataProvider = staticDataProvider;
        }

        public void OnProgressLoad(WorldProgressData progress)
        {
            var terrainSize = GetTerrainSize(progress);
            var terrainInstance = _terrainFactory.Create(terrainSize);

            _tileFactory.Initialize(terrainInstance.transform);
            _terrain.Initialize(new TileArray(terrainSize));

            FillTerrain(progress);
        }

        private Vector2Int GetTerrainSize(WorldProgressData progress)
        {
            var terrainSize = progress.TerrainSize.FromSaved();

            if (terrainSize == default)
                terrainSize = _staticDataProvider.Get<TerrainConfig>().DefaultSize;

            return terrainSize;
        }

        private void FillTerrain(WorldProgressData progress)
        {
            foreach (var savedTile in progress.SavedTiles)
            {
                var hex = savedTile.Hex.FromSaved();

                _terrain.CreateTile(hex, savedTile.RegionType);

                if (savedTile.UnitType != UnitType.None)
                    _terrain.CreateUnit(_terrain.GetTile(hex), savedTile.UnitType, true);
            }
        }
    }
}