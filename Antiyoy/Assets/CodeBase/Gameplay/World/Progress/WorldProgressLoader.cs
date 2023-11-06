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
            if (progress == null)
                CreateTerrain(_staticDataProvider.Get<TerrainConfig>().DefaultSize);
            else
                LoadTerrain(progress);
        }

        private void CreateTerrain(Vector2Int terrainSize)
        {
            var terrainInstance = _terrainFactory.Create(terrainSize);

            _tileFactory.Initialize(terrainInstance.transform);
            _terrain.Initialize(new TileArray(terrainSize));
        }

        private void LoadTerrain(WorldProgressData progress)
        {
            var terrainSize = progress.TerrainSize.FromSaved();

            CreateTerrain(terrainSize);
            FillTerrain(progress);
        }

        private void FillTerrain(WorldProgressData progress)
        {
            foreach (var savedTile in progress.SavedTiles)
            {
                var hex = savedTile.Hex.FromSaved();
                var unit = savedTile.Unit;

                _terrain.CreateTile(hex, savedTile.RegionType);

                if (unit.Type != UnitType.None)
                    _terrain.CreateUnit(_terrain.GetTile(hex), unit.Type, unit.IsCanMove);
            }
        }
    }
}