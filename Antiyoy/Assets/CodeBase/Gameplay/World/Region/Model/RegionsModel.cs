using System.Collections.Generic;
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
            _splitTool = new(regionFactory);
        }

        public void Add(TileData tile, RegionType regionType)
        {
            var region = GetOrCreateRegionFromNeighbors(tile, regionType);

            region.Tiles.Add(tile);
            SetRegion(tile, region);

            _changedRegions.Add(region);
        }

        private void SetRegion(TileData tile, RegionData region)
        {
            tile.Region = region;
            tile.Instance.SpriteRenderer.color = region.Color;
        }

        public void Remove(TileData tile)
        {
            tile.Region.Tiles.Remove(tile);

            _changedRegions.Add(tile.Region);
        }

        public void RecalculateChangedRegions()
        {
            foreach (var region in _changedRegions)
                if (region.Tiles.Count > 0)
                    Recalculate(region);

            _changedRegions.Clear();
        }

        private void Recalculate(RegionData region)
        {
            var result = new List<RegionData>();

            if (_joinTool.TryJoinWithNeighbors(region, out var joinResult))
                result.Add(joinResult);
            else
                result.Add(region);

            if (_splitTool.TrySplit(result[0], out var splitResult))
                result = splitResult;

            foreach (var reg in result)
            foreach (var tile in reg.Tiles)
                tile.Instance.DebugText.text = reg.Tiles.Count.ToString();
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