using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Tile.Factory
{
    public interface ITileFactory
    {
        void Initialize(Transform tileRoot);
        void Create(HexPosition hex, RegionType regionType);
        void Destroy(TileData tile);
        void Destroy(HexPosition hex);
        bool TryCreate(HexPosition hex, RegionType regionType);
        bool TryDestroy(HexPosition hex);
    }
}