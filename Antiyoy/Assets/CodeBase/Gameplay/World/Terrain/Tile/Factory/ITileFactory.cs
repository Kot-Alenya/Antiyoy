using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Tile.Factory
{
    public interface ITileFactory
    {
        public void Create(HexPosition hex, RegionType regionType);

        public void Destroy(TileData tile);

        public void Destroy(HexPosition hex);
    }
}