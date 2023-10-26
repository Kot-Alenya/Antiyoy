﻿using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Region.Factory;
using CodeBase.Gameplay.World.Region.Rebuild;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region.Collection
{
    public class RegionCollection : IRegionCollection
    {
        private readonly IRegionFactory _regionFactory;
        private readonly IRegionRebuilder _regionRebuilder;

        public RegionCollection(IRegionFactory regionFactory, IRegionRebuilder regionRebuilder)
        {
            _regionFactory = regionFactory;
            _regionRebuilder = regionRebuilder;
        }

        public void AddToRegion(TileData tile, RegionType regionType)
        {
            var region = GetRegionFromNeighborsOrCreateNew(tile, regionType);

            RegionTileUtilities.SetTileToRegion(tile, region);
            _regionRebuilder.AddToRebuildBuffer(region);
        }

        public void RemoveFromRegion(TileData tile)
        {
            RegionTileUtilities.RemoveTileFromRegion(tile, tile.Region, _regionFactory);
            _regionRebuilder.AddToRebuildBuffer(tile.Region);
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