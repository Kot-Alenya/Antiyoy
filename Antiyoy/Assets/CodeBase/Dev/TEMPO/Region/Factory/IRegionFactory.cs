using System.Collections.Generic;

namespace CodeBase.Dev.TEMPO.Region.Factory
{
    public interface IRegionFactory
    {
        public List<RegionData> Regions { get; }

        public RegionData Create(RegionType type);

        public void Destroy(RegionData region);
    }
}