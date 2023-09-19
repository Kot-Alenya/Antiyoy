using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Region.Data;

namespace CodeBase.Gameplay.Terrain.Region
{
    public class TerrainRegionsJoinTool
    {
        public bool TryJoinWithNeighbors(RegionData region, out RegionData result)
        {
            var neighbourRegions = GetNeighborRegions(region, region.Type);

            if (neighbourRegions.Count > 1)
            {
                result = JoinRegions(neighbourRegions);
                return true;
            }

            result = null;
            return false;
        }

        private List<RegionData> GetNeighborRegions(RegionData region, RegionType necessaryRegionType)
        {
            var regions = new List<RegionData>();

            foreach (var rootTile in region.Tiles)
            foreach (var tile in rootTile.Neighbors)
            {
                if (tile.Region.Type != necessaryRegionType)
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
                if (regions[i].Tiles.Count > previous.Tiles.Count)
                {
                    previous.MoveTiles(regions[i]);
                    previous = regions[i];
                }
                else
                    regions[i].MoveTiles(previous);
            }

            return previous;
        }
    }
}