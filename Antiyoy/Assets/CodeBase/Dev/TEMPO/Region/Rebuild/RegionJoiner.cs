using System.Collections.Generic;

namespace CodeBase.Dev.TEMPO.Region.Rebuild
{
    public class RegionJoiner
    {
        private readonly IRegionFactory _regionFactory;
        private readonly List<RegionData> _neighborRegionsBuffer = new();

        public RegionJoiner(IRegionFactory regionFactory) => _regionFactory = regionFactory;

        public bool TryJoinWithNeighbors(RegionData region, out RegionData result)
        {
            var regions = GetNeighborRegions(region, region.Type);
            regions.Add(region);

            if (regions.Count > 1)
            {
                result = JoinRegions(regions);
                return true;
            }

            result = null;
            return false;
        }

        private List<RegionData> GetNeighborRegions(RegionData region, RegionType necessaryRegionType)
        {
            _neighborRegionsBuffer.Clear();
            var regions = _neighborRegionsBuffer;

            foreach (var rootTile in region.Tiles)
            foreach (var tile in rootTile.Neighbors)
            {
                if (tile.Region.Type != necessaryRegionType)
                    continue;

                if (tile.Region == region)
                    continue;

                if (!regions.Contains(tile.Region))
                    regions.Add(tile.Region);
            }

            return regions;
        }

        private RegionData JoinRegions(List<RegionData> regions)
        {
            var previous = regions[0];

            for (var i = 1; i < regions.Count; i++)
            {
                if (regions[i].Income > previous.Income)
                {
                    RegionTileUtilities.MoveTiles(previous, regions[i], _regionFactory);
                    previous = regions[i];
                }
                else
                    RegionTileUtilities.MoveTiles(regions[i], previous, _regionFactory);
            }

            return previous;
        }
    }
}