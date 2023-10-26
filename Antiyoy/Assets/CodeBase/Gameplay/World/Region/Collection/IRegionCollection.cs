using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region.Collection
{
    public interface IRegionCollection
    {
        public void AddToRegion(TileData tile, RegionType regionType);

        public void RemoveFromRegion(TileData tile);
    }
}