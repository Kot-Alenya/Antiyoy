using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuilder.Geometry
{
    public class RegionGeometryRebuilder
    {
        private readonly RegionJoiner _regionJoiner;
        private readonly RegionSplitter _regionSplitter;

        public RegionGeometryRebuilder(RegionJoiner regionJoiner, RegionSplitter regionSplitter)
        {
            _regionJoiner = regionJoiner;
            _regionSplitter = regionSplitter;
        }

        public List<RegionData> Rebuild(RegionData region)
        {
            var regionToSplit = _regionJoiner.TryJoinWithNeighbors(region, out var joinRegion)
                ? joinRegion
                : region;

            var result = _regionSplitter.TrySplit(regionToSplit, out var splitResult)
                ? splitResult
                : new List<RegionData> { regionToSplit };

            return result;
        }
    }
}