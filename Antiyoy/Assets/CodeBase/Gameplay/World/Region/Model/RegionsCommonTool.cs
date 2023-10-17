using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Region.Model
{
    public class RegionsCommonTool
    {
        private readonly RegionFactory _regionFactory;

        public RegionsCommonTool(RegionFactory regionFactory) => _regionFactory = regionFactory;

        public void SetRegion(TileData tile, RegionData region)
        {
            region.Tiles.Add(tile);
            tile.Region = region;
            tile.Instance.SpriteRenderer.color = region.Color;
        }

        public void RemoveRegion(TileData tile, RegionData region)
        {
            region.Tiles.Remove(tile);

            if (region.Tiles.Count <= 0)
                _regionFactory.Destroy(tile.Region);
        }
    }
}