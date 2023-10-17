using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region.Model
{
    public class RegionsModel
    {
        private readonly RegionFactory _regionFactory;
        private readonly RegionsJoinTool _joinTool = new();
        private readonly RegionsSplitTool _splitTool;
        private readonly List<RegionData> _changedRegions = new();

        public RegionsModel(RegionFactory regionFactory)
        {
            _regionFactory = regionFactory;
            _splitTool = new RegionsSplitTool(regionFactory);
        }

        public void Add(TileData tile, RegionType regionType)
        {
            var region = GetOrCreateRegionFromNeighbors(tile, regionType);

            region.Tiles.Add(tile);
            tile.Region = region;
            
            UpdateView(region);

            if (!_changedRegions.Contains(region))
                _changedRegions.Add(region);
        }

        public void Remove(TileData tile)
        {
            tile.Region.Tiles.Remove(tile);

            if (!_changedRegions.Contains(tile.Region))
                _changedRegions.Add(tile.Region);
        }

        public void RecalculateChangedRegions()
        {
            var regionsToRecalculate = _changedRegions.OrderBy(r => r.Tiles.Count);

            foreach (var region in regionsToRecalculate)
                Recalculate(region);

            _changedRegions.Clear();
        }

        private void Recalculate(RegionData region)
        {
            var regionToSplit = _joinTool.TryJoinWithNeighbors(region, out var joinedRegion)
                ? joinedRegion
                : region;

            var result = _splitTool.TrySplit(regionToSplit, out var splitResult)
                ? splitResult
                : new List<RegionData> { region };

            foreach (var resultRegion in result)
                UpdateView(resultRegion);
        }

        private void UpdateView(RegionData region)
        {
            foreach (var tile in region.Tiles)
            {
                tile.Instance.DebugText.text = region.Tiles.Count.ToString();
                tile.Instance.SpriteRenderer.color = region.Color;
            }
        }

        private RegionData GetOrCreateRegionFromNeighbors(TileData tile, RegionType regionType)
        {
            foreach (var neighbour in tile.Neighbors)
                if (neighbour.Region.Type == regionType)
                    return neighbour.Region;

            return _regionFactory.Create(regionType);
        }
    }
}