using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Tile.Factory
{
    public interface ITileFactory
    {
        void Create(HexPosition hex, RegionType regionType);
        void Destroy(TileData tile);
        void Destroy(HexPosition hex);
        bool TryCreate(HexPosition hex, RegionType regionType);
        bool TryDestroy(HexPosition hex);
    }
}