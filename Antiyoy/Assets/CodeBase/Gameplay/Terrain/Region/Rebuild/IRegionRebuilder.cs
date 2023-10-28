using CodeBase.Gameplay.Terrain.Region.Data;

namespace CodeBase.Gameplay.Terrain.Region.Rebuild
{
    public interface IRegionRebuilder
    {
        void AddToRebuildBuffer(RegionData region);
        void RebuildFromBufferAndClearBuffer();
        void RebuildIncome(RegionData region);
    }
}