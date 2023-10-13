using CodeBase.Gameplay.World.Data.Hex;
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

        public TerrainModel(TilesModel tilesModel, RegionsModel regionsModel)
        {
            _tilesModel = tilesModel;
            _regionsModel = regionsModel;
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

            return true;
        }

        public void RecalculateChangedRegions() => _regionsModel.RecalculateChangedRegions();
    }
}