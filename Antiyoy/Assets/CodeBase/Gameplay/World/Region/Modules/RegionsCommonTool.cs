using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Region.Factory;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region.Modules
{
    public class RegionsCommonTool
    {
        private readonly IRegionFactory _regionFactory;

        public RegionsCommonTool(IRegionFactory regionFactory) => _regionFactory = regionFactory;

        public void SetTileToRegion(TileData tile, RegionData region)
        {
            region.Tiles.Add(tile);
            tile.Region = region;
            tile.Instance.SpriteRenderer.color = region.Color;
        }

        public void RemoveTileFromRegion(TileData tile, RegionData region)
        {
            region.Tiles.Remove(tile);

            if (region.Tiles.Count <= 0)
                _regionFactory.Destroy(tile.Region);
        }

        public void MoveTiles(RegionData fromRegion, RegionData toRegion)
        {
            foreach (var tile in fromRegion.Tiles)
                SetTileToRegion(tile, toRegion);

            _regionFactory.Destroy(fromRegion);
        }
    }
}