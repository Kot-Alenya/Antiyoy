using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Region
{
    public class RegionConnector
    {
        private readonly RegionFactory _regionFactory;

        public RegionConnector(RegionFactory regionFactory) => _regionFactory = regionFactory;

        public void Connect(TileData tile, RegionData region)
        {
            region.Tiles.Add(tile);
            tile.Region = region;
            tile.Instance.SpriteRenderer.color = region.Color;
        }

        public void Disconnect(TileData tile, RegionData region)
        {
            region.Tiles.Remove(tile);

            if (region.Tiles.Count <= 0)
                _regionFactory.Destroy(tile.Region);
        }

        public void DisconnectAllTiles(RegionData region)
        {
            region.Tiles.Clear();
            _regionFactory.Destroy(region);
        }
    }
}