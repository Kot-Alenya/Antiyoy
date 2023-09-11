namespace CodeBase.Gameplay.Terrain
{
    public class TerrainObject
    {
        public TerrainObject(TerrainTiles tiles, TerrainRegions regions)
        {
            Tiles = tiles;
            Regions = regions;
        }

        public TerrainTiles Tiles { get; }

        public TerrainRegions Regions { get; }
    }
}