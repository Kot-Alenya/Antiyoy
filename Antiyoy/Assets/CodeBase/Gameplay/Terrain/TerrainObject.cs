using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainObject
    {
        private readonly Vector2Int _size;

        public TerrainObject(TerrainTiles tiles, TerrainRegions regions, Vector2Int size)
        {
            _size = size;
            Tiles = tiles;
            Regions = regions;
        }

        public TerrainTiles Tiles { get; }

        public TerrainRegions Regions { get; }

        public bool IsHexInTerrain(HexPosition hex)
        {
            var index = HexMath.ToArrayIndex(hex);
            
            if (index.x < 0)
                return false;

            if (index.x >= _size.x)
                return false;

            if (index.y < 0)
                return false;

            if (index.y >= _size.y)
                return false;

            return true;
        }

        public void Connect(TileObject tileObject)
        {
        }
    }
}