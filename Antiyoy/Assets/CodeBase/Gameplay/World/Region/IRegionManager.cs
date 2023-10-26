using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region
{
    public interface IRegionManager
    {
        public void AddToRegion(TileData tile, RegionType regionType);
        
        public void RemoveFromRegion(TileData tile);

        public void AddToRecalculateBuffer(RegionData region);

        public void RecalculateFromBufferAndClearBuffer();
    }
}