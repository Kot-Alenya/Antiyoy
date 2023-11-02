using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.World.Terrain.Region.Rebuilder
{
    public class RegionJoiner
    {
        private readonly RegionConnector _regionConnector;
        private readonly List<RegionData> _neighborRegionsBuffer = new();

        public RegionJoiner(RegionConnector regionConnector) => _regionConnector = regionConnector;

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
                    MoveTiles(previous, regions[i]);
                    previous = regions[i];
                }
                else
                    MoveTiles(regions[i], previous);
            }

            return previous;
        }

        private void MoveTiles(RegionData fromRegion, RegionData toRegion)
        {
            foreach (var tile in fromRegion.Tiles)
                _regionConnector.Connect(tile, toRegion);

            _regionConnector.DisconnectAllTiles(fromRegion);
        }
    }
}