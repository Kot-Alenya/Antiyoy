using CodeBase.Gameplay.World.Terrain.Region.Factory;

namespace CodeBase.Gameplay.World.Terrain.Region
{
    public class RegionCoinsCounter
    {
        private readonly IRegionFactory _regionFactory;

        public RegionCoinsCounter(IRegionFactory regionFactory) => _regionFactory = regionFactory;

        public void RecountAllRegions()
        {
            foreach (var region in _regionFactory.Regions)
                region.CoinsCount += region.Income;
        }
    }
}