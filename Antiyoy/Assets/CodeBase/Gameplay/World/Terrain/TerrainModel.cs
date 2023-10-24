using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainModel : ITerrain
    {
        private readonly ITerrainRegions _terrainRegions;
        private readonly TileCollection _tileCollection;

        public TerrainModel(ITerrainRegions terrainRegions, TileCollection tileCollection)
        {
            _terrainRegions = terrainRegions;
            _tileCollection = tileCollection;
        }

        public Vector2Int Size => _tileCollection.Size;

        public bool IsHexInTerrain(HexPosition hex) => _tileCollection.IsInArraySize(hex);

        public TileData GetTile(HexPosition hex) => _tileCollection.Get(hex);

        public bool TryGetTile(HexPosition hex, out TileData tile) => _tileCollection.TryGet(hex, out tile);

        public void RecalculateChangedRegions() => _terrainRegions.RecalculateFromBufferAndClearBuffer();
    }
}