using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public interface IWorldTerrainController
    {
        public Vector2Int Size { get; }

        public bool IsHexInTerrain(HexPosition hex);

        public bool TryGetTile(HexPosition hex, out TileData tile);

        public bool TryCreateTile(HexPosition hex, RegionType regionType);

        public bool TryDestroyTile(HexPosition hex);

        public void RecalculateChangedRegions();

        public bool TryCreateEntity(HexPosition hex, EntityType entityType);

        public bool TryDestroyEntity(HexPosition hex);
    }
}