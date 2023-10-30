using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player
{
    public interface IPlayerController
    {
        public void Initialize(PlayerUIWindow uiWindow);
        
        public void SelectTile(TileData tile);

        public void UnSelectTile();
    }
}