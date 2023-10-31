using CodeBase.Gameplay.Terrain.Region.Data;

namespace CodeBase.Gameplay.Player
{
    public class PlayerTerrainSelectionView
    {
        public void SelectRegion(RegionData region)
        {
            foreach (var tile in region.Tiles)
            foreach (var neighbor in tile.Neighbors)
                if (neighbor.Region.Type != region.Type)
                    tile.Instance.DebugText.text = "BORDER";
        }

        public void UnSelectRegion(RegionData region)
        {
            foreach (var tile in region.Tiles)
                tile.Instance.DebugText.text = string.Empty;
        }
    }
}