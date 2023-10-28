using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile.Factory
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