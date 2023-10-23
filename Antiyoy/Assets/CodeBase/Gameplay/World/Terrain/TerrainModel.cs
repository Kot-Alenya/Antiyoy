using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Region.Model;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Gameplay.World.Tile.Model;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainModel : IWorldTerrainController
    {
        private readonly TilesModel _tilesModel;
        private readonly RegionsModel _regionsModel;
        private readonly EntityFactory _entityFactory;

        public TerrainModel(TilesModel tilesModel, RegionsModel regionsModel, EntityFactory entityFactory)
        {
            _tilesModel = tilesModel;
            _regionsModel = regionsModel;
            _entityFactory = entityFactory;
        }

        public Vector2Int Size => _tilesModel.Size;

        public bool IsHexInTerrain(HexPosition hex) => _tilesModel.IsHexInTiles(hex);

        public bool TryGetTile(HexPosition hex, out TileData tile) => _tilesModel.TryGetTile(hex, out tile);

        public bool TryCreateTile(HexPosition hex, RegionType regionType)
        {
            if (!_tilesModel.IsHexInTiles(hex) || _tilesModel.TryGetTile(hex, out _))
                return false;

            var tile = _tilesModel.CreateTile(hex);
            _regionsModel.Add(tile, regionType);

            return true;
        }

        public bool TryDestroyTile(HexPosition hex)
        {
            if (!_tilesModel.TryGetTile(hex, out var tile))
                return false;

            _regionsModel.Remove(tile);
            _tilesModel.DestroyTile(tile);

            if (tile.Entity != null)
                _entityFactory.Destroy(tile.Entity);

            return true;
        }

        public void RecalculateChangedRegions() => _regionsModel.RecalculateChangedRegions();

        public bool TryCreateEntity(HexPosition hex, EntityType entityType)
        {
            if (!_tilesModel.TryGetTile(hex, out var tile))
                return false;

            if (tile.Entity != null)
                return false;

            tile.Entity = _entityFactory.Create(entityType, tile.Instance.transform);
            _regionsModel.AddToChangedRegions(tile.Region);

            return true;
        }

        public bool TryDestroyEntity(HexPosition hex)
        {
            if (!_tilesModel.TryGetTile(hex, out var tile))
                return false;

            if (tile.Entity == null)
                return false;

            _entityFactory.Destroy(tile.Entity);
            tile.Entity = null;
            _regionsModel.AddToChangedRegions(tile.Region);

            return true;
        }
    }
}