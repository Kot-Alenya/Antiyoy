using CodeBase.Gameplay.Terrain.Region.Data;

namespace CodeBase.Gameplay.Terrain.Region.Factory
{
    public interface IRegionFactory
    {
        RegionData Create(RegionType type);

        void Destroy(RegionData region);
    }
}