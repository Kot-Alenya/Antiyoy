using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Tile;

namespace CodeBase.Gameplay.World.NewTerrain
{
    public class RegionConnector
    {
        private readonly RegionFactory _regionFactory;

        public RegionConnector(RegionFactory regionFactory) => _regionFactory = regionFactory;

        public void Connect(TileObject tile, RegionObject region)
        {
            region.Tiles.Add(tile);
            tile.SetRegion(region);
        }

        public void Disconnect(TileObject tile, RegionObject region)
        {
            region.Tiles.Remove(tile);

            if (region.Tiles.Count <= 0)
                _regionFactory.Destroy(tile.Region);
        }
    }
}