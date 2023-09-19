using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Terrain.Region
{
    public class TerrainRegions
    {
        private readonly RegionFactory _regionFactory;
        private readonly TerrainRegionsJoinTool _joinTool = new();
        private readonly TerrainRegionsSplitTool _splitTool;

        public TerrainRegions(RegionFactory regionFactory)
        {
            _regionFactory = regionFactory;
            _splitTool = new(regionFactory);
        }

        public RegionData GetOrCreateRegionFromNeighbors(TileData tile, RegionType regionType)
        {
            foreach (var neighbour in tile.Neighbors)
                if (neighbour.Region.Type == regionType)
                    return neighbour.Region;

            return _regionFactory.Create(regionType);
        }

        public void Recalculate(List<RegionData> regions)
        {
            foreach (var region in regions)
                RecalculateRegion(region);
        }

        private void RecalculateRegion(RegionData region)
        {
            var regionToSplit = region;

            if (_joinTool.TryJoinWithNeighbors(region, out var joinResult))
                regionToSplit = joinResult;

            _splitTool.Split(regionToSplit);
        }
    }
}