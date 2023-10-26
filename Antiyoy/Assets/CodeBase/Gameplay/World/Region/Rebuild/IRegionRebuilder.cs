using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Region.Rebuild
{
    public interface IRegionRebuilder
    {
        public void AddToRebuildBuffer(RegionData region);

        public void RebuildFromBufferAndClearBuffer();
    }
}