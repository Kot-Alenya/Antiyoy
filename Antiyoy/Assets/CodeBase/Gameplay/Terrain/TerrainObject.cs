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

        private TerrainRegions Regions { get; }
    }
}