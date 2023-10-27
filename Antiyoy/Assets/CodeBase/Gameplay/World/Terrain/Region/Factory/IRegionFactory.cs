using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Factory
{
    public interface IRegionFactory
    {
        RegionData Create(RegionType type);

        void Destroy(RegionData region);
    }
}