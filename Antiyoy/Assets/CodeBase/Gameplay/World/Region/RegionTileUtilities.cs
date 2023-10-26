using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Region.Factory;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region
{
    public static class RegionTileUtilities
    {
        public static void SetTileToRegion(TileData tile, RegionData region)
        {
            region.Tiles.Add(tile);
            tile.Region = region;
            tile.Instance.SpriteRenderer.color = region.Color;
        }

        public static void RemoveTileFromRegion(TileData tile, RegionData region, IRegionFactory regionFactory)
        {
            region.Tiles.Remove(tile);

            if (region.Tiles.Count <= 0)
                regionFactory.Destroy(tile.Region);
        }

        public static void MoveTiles(RegionData fromRegion, RegionData toRegion, IRegionFactory regionFactory)
        {
            foreach (var tile in fromRegion.Tiles)
                SetTileToRegion(tile, toRegion);

            regionFactory.Destroy(fromRegion);
        }
    }
}