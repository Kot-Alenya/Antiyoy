using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Tile.Data;

namespace CodeBase.Gameplay.Map.Data
{
    public class MapData
    {
        public TileArray Tiles;
        public RegionData[] Regions;
        public CountryData[] Countries;
    }
}