using System.Collections.Generic;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainRegions
    {
        private readonly Dictionary<RegionType, RegionObject> _regions = new();

        public TerrainRegions(params RegionObject[] regions)
        {
            foreach (var region in regions)
                _regions.Add(region.Type, region);
        }

        public RegionObject Get(RegionType type) => _regions[type];
    }
}