using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Region.Factory;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Region
{
    public class RegionCollection
    {
        private readonly IRegionFactory _regionFactory;
        private readonly IRegionRebuilder _regionRebuilder;

        public RegionCollection(IRegionFactory regionFactory, IRegionRebuilder regionRebuilder)
        {
            _regionFactory = regionFactory;
            _regionRebuilder = regionRebuilder;
        }

        public void AddTileToRegion(TileData tile, RegionType regionType)
        {
            var region = GetRegionFromNeighborsOrCreateNew(tile, regionType);

            RegionTileUtilities.SetTileToRegion(tile, region);
            _regionRebuilder.AddToRebuildBuffer(region);
            _regionRebuilder.RebuildIncome(region);
        }

        public void RemoveTileFromRegion(TileData tile)
        {
            var region = tile.Region;

            RegionTileUtilities.RemoveTileFromRegion(tile, region, _regionFactory);
            _regionRebuilder.AddToRebuildBuffer(region);
            _regionRebuilder.RebuildIncome(region);
        }

        private RegionData GetRegionFromNeighborsOrCreateNew(TileData tile, RegionType regionType)
        {
            foreach (var neighbour in tile.Neighbors)
                if (neighbour.Region.Type == regionType)
                    return neighbour.Region;

            return _regionFactory.Create(regionType);
        }
    }
}