using System.Collections.Generic;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region.Model
{
    public class RegionsJoinTool
    {
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
            var regions = new List<RegionData>();

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
                if (regions[i].Tiles.Count > previous.Tiles.Count)
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
            {
                toRegion.Tiles.Add(tile);
                SetRegion(tile, toRegion);
            }

            fromRegion.Tiles.Clear();
        }

        private void SetRegion(TileData tile, RegionData region)
        {
            tile.Region = region;
            tile.Instance.SpriteRenderer.color = region.Color;
        }
    }
}