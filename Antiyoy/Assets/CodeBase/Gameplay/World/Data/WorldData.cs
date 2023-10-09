using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Data
{
    public class WorldData : ICloneable<WorldData>
    {
        public TileArray Tiles;
        public RegionData[] Regions;
        public CountryData[] Countries;

        public WorldData Clone()
        {
            var tilesClone = Tiles.Clone();
            var regionsClone = (RegionData[])Regions.Clone();

            return new WorldData
            {
                Tiles = tilesClone,
                Regions = regionsClone
            };
        }
    }
}