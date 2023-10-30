using CodeBase.Gameplay.Player.UI;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player.Controller
{
    public interface IPlayerController
    {
        public void Initialize(PlayerUIWindow playerUIWindow);

        public void SelectTile(TileData tile);

        public void UnSelectTile();

        public void UpdateUI();
    }
}