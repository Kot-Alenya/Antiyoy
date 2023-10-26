using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Region.Modules;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region
{
    public class RegionManager : IRegionManager
    {
        private readonly RegionFactory _regionFactory;
        private readonly RegionsJoinTool _joinTool;
        private readonly RegionsSplitTool _splitTool;
        private readonly RegionsCommonTool _commonTool;
        private readonly List<RegionData> _changedRegions = new();

        public RegionManager(RegionFactory regionFactory)
        {
            _regionFactory = regionFactory;
            _commonTool = new RegionsCommonTool(regionFactory);
            _splitTool = new RegionsSplitTool(regionFactory, _commonTool);
            _joinTool = new RegionsJoinTool(_commonTool);
        }

        public void AddToRegion(TileData tile, RegionType regionType)
        {
            var region = GetOrCreateRegionFromNeighbors(tile, regionType);

            _commonTool.SetTileToRegion(tile, region);
            AddToRecalculateBuffer(region);
        }

        public void RemoveFromRegion(TileData tile)
        {
            _commonTool.RemoveTileFromRegion(tile, tile.Region);
            AddToRecalculateBuffer(tile.Region);
        }

        public void AddToRecalculateBuffer(RegionData region)
        {
            if (!_changedRegions.Contains(region))
                if (region.Tiles.Count > 0)
                    _changedRegions.Add(region);
        }

        public void RecalculateFromBufferAndClearBuffer()
        {
            var regionsToRecalculate = _changedRegions.OrderBy(r => r.Income);

            foreach (var region in regionsToRecalculate)
                Recalculate(region);

            _changedRegions.Clear();
        }

        private void Recalculate(RegionData region)
        {
            var regionToSplit = _joinTool.TryJoinWithNeighbors(region, out var joinRegion)
                ? joinRegion
                : region;

            var result = _splitTool.TrySplit(regionToSplit, out var splitResult)
                ? splitResult
                : new List<RegionData> { regionToSplit };

            foreach (var resultRegion in result)
                resultRegion.Income = GetIncome(resultRegion);
        }

        private int GetIncome(RegionData region)
        {
            var income = 0;

            foreach (var tile in region.Tiles)
            {
                if (tile.Entity != null)
                    income += tile.Entity.Income;
                else
                    income += 1;
            }

            return income;
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