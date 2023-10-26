using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Tile.Factory
{
    public interface ITileFactory
    {
        public void Create(HexPosition hex, RegionType regionType);

        public void Destroy(TileData tile);

        public bool TryCreate(HexPosition hex, RegionType regionType);

        public bool TryDestroy(HexPosition hex);
    }
}