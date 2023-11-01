using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Dev.TEMPO.Region.Rebuild
{
    public class RegionRebuilder : IRegionRebuilder
    {
        private readonly RegionSplitter _regionSplitter;
        private readonly RegionJoiner _regionJoiner;
        private readonly RegionIncomeRebuilder _regionIncomeRebuilder;
        private readonly List<RegionData> _rebuildBuffer = new();

        public RegionRebuilder(RegionSplitter regionSplitter, RegionJoiner regionJoiner,
            RegionIncomeRebuilder regionIncomeRebuilder)
        {
            _regionSplitter = regionSplitter;
            _regionJoiner = regionJoiner;
            _regionIncomeRebuilder = regionIncomeRebuilder;
        }

        public void AddToRebuildBuffer(RegionData region)
        {
            if (!_rebuildBuffer.Contains(region))
                if (region.Tiles.Count > 0)
                    _rebuildBuffer.Add(region);
        }

        public void RebuildFromBufferAndClearBuffer()
        {
            var regionsToRecalculate = _rebuildBuffer.OrderBy(r => r.Income);

            foreach (var region in regionsToRecalculate)
                Rebuild(region);

            _rebuildBuffer.Clear();
        }

        public void RebuildIncome(RegionData region) => _regionIncomeRebuilder.RebuildIncome(region);

        private void Rebuild(RegionData region)
        {
            var regionToSplit = _regionJoiner.TryJoinWithNeighbors(region, out var joinRegion)
                ? joinRegion
                : region;

            var result = _regionSplitter.TrySplit(regionToSplit, out var splitResult)
                ? splitResult
                : new List<RegionData> { regionToSplit };

            _regionIncomeRebuilder.RebuildIncome(result);
        }
    }
}