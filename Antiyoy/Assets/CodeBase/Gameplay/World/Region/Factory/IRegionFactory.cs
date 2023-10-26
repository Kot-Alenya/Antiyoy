using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Region.Factory
{
    public interface IRegionFactory
    {
        RegionData Create(RegionType type);

        void Destroy(RegionData region);
    }
}