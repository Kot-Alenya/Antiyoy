using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuild
{
    public interface IRegionRebuilder
    {
        public void AddToRebuildBuffer(RegionData region);

        public void RebuildFromBufferAndClearBuffer();
    }
}