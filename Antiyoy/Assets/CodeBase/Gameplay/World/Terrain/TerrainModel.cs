using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Model;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainModel : ITerrain
    {
        private readonly RegionsModel _regionsModel;
        private readonly TileCollection _tileCollection;

        public TerrainModel(RegionsModel regionsModel, TileCollection tileCollection)
        {
            _regionsModel = regionsModel;
            _tileCollection = tileCollection;
        }

        public Vector2Int Size => _tileCollection.Size;

        public bool IsHexInTerrain(HexPosition hex) => _tileCollection.IsInArraySize(hex);

        public TileData GetTile(HexPosition hex) => _tileCollection.Get(hex);

        public bool TryGetTile(HexPosition hex, out TileData tile) => _tileCollection.TryGet(hex, out tile);

        public void RecalculateChangedRegions() => _regionsModel.RecalculateChangedRegions();
    }
}