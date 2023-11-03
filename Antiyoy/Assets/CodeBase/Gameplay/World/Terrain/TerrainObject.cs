using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Region.Rebuilder;
using CodeBase.Gameplay.World.Terrain.Tile;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainObject : ITerrain
    {
        private readonly TileFactory _tileFactory;
        private readonly TileConnector _tileConnector;
        private readonly UnitFactory _unitFactory;
        private readonly RegionFactory _regionFactory;
        private readonly RegionGeometryRebuilder _regionGeometryRebuilder;
        private readonly RegionIncomeRebuilder _incomeRebuilder;
        private readonly RegionConnector _regionConnector;
        private TileArray _tileArray;

        public TerrainObject(TileFactory tileFactory, TileConnector tileConnector, UnitFactory unitFactory,
            RegionFactory regionFactory, RegionGeometryRebuilder regionGeometryRebuilder,
            RegionIncomeRebuilder incomeRebuilder, RegionConnector regionConnector)
        {
            _tileFactory = tileFactory;
            _regionFactory = regionFactory;
            _regionGeometryRebuilder = regionGeometryRebuilder;
            _incomeRebuilder = incomeRebuilder;
            _unitFactory = unitFactory;
            _regionConnector = regionConnector;
            _tileConnector = tileConnector;
        }

        public Vector2Int Size => _tileArray.Size;

        public void Initialize(TileArray tileArray) => _tileArray = tileArray;

        public bool IsInTerrain(HexPosition hex) => _tileArray.IsInArray(hex);

        public void CreateTile(HexPosition hex, RegionType regionType)
        {
            var tile = _tileFactory.Create(hex);

            _tileArray.Set(tile, hex);
            _tileConnector.Connect(tile, TileUtilities.FindNeighbors(hex, _tileArray));

            if (!TileUtilities.TryGetRegionFromNeighbors(tile, regionType, out var region))
                region = _regionFactory.Create(regionType);

            _regionConnector.Connect(tile, region);
            _incomeRebuilder.Rebuild(_regionGeometryRebuilder.Rebuild(region));
        }

        public void DestroyTile(TileData tile)
        {
            var region = tile.Region;

            _tileConnector.Disconnect(tile, new List<TileData>(tile.Neighbors));
            _regionConnector.Disconnect(tile, region);
            _tileArray.Remove(tile.Hex);
            _tileFactory.Destroy(tile);

            _incomeRebuilder.Rebuild(_regionGeometryRebuilder.Rebuild(region));
        }

        public void CreateUnit(TileData rootTile, UnitType unitType)
        {
            var entity = _unitFactory.Create(rootTile, unitType);
            rootTile.Unit = entity;
            _incomeRebuilder.Rebuild(rootTile.Region);
        }

        public void DestroyUnit(UnitData unit)
        {
            var rootTile = unit.RootTile;

            _unitFactory.Destroy(unit);
            rootTile.Unit = null;
            _incomeRebuilder.Rebuild(rootTile.Region);
        }

        public TileData GetTile(HexPosition hex) => _tileArray.Get(hex);

        public bool TryGetTile(HexPosition hex, out TileData tile) => _tileArray.TryGet(hex, out tile);

        public IEnumerator<TileData> GetEnumerator() => _tileArray.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}