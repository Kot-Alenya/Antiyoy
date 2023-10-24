using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile
{
    public interface ITerrainTiles
    {
        public Transform TilesRoot { get; }

        public bool IsInTerrain(HexPosition hex);
        
        public void Set(TileData tile, HexPosition hex, RegionType regionType);

        public void Remove(TileData tile);

        public TileData Get(HexPosition hex);

        public bool TryGet(HexPosition hex, out TileData tile);
    }
}