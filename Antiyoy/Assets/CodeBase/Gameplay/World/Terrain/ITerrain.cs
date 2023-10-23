using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public interface ITerrain
    {
        public Vector2Int Size { get; }

        public bool IsHexInTerrain(HexPosition hex);

        public TileData GetTile(HexPosition hex);

        public bool TryGetTile(HexPosition hex, out TileData tile);

        public void RecalculateChangedRegions();
    }
}