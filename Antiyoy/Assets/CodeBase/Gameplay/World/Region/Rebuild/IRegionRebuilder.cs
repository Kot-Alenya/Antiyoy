using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuild
{
    public interface IRegionRebuilder
    {
        void AddToRebuildBuffer(RegionData region);
        void RebuildFromBufferAndClearBuffer();
        void RebuildIncome(RegionData region);
    }
}