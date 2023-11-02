using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuilder
{
    public class RegionRebuilder
    {
        private readonly RegionJoiner _regionJoiner;
        private readonly RegionSplitter _regionSplitter;
        private readonly RegionIncomeRebuilder _incomeRebuilder;

        public RegionRebuilder(RegionJoiner regionJoiner, RegionSplitter regionSplitter,
            RegionIncomeRebuilder incomeRebuilder)
        {
            _regionJoiner = regionJoiner;
            _regionSplitter = regionSplitter;
            _incomeRebuilder = incomeRebuilder;
        }

        public void Rebuild(RegionData region)
        {
            var regionToSplit = _regionJoiner.TryJoinWithNeighbors(region, out var joinRegion)
                ? joinRegion
                : region;

            var result = _regionSplitter.TrySplit(regionToSplit, out var splitResult)
                ? splitResult
                : new List<RegionData> { regionToSplit };

            _incomeRebuilder.RebuildIncome(result);
        }
    }
}