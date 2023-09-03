namespace _dev
{
    public class TerrainRegions
    {
        public void SetRegion(TerrainRegion region)
        {
            foreach (var tile in region.Tiles) 
                tile.Data.SpriteRenderer.color = region.Color;
        }
    }
}