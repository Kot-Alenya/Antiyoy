namespace CodeBase.Dev.TEMPO.Region
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