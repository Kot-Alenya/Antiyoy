using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player
{
    public class PlayerController : IPlayerController
    {
        private PlayerUIWindow _uiWindow;

        private RegionData _selectedRegion;
        private TileData _selectedTile;
        
        private RegionType _playerRegionType = RegionType.Red;
        
        public void Initialize(PlayerUIWindow uiWindow) => _uiWindow = uiWindow;

        public void SelectTile(TileData tile)
        {
            if (tile.Region.Type != _playerRegionType)
            {
                UnSelectTile();
                return;
            }
            
            if(_selectedTile == null)
                _uiWindow.Show();
            
            _selectedTile = tile;
        }

        public void UnSelectTile()
        {
            _selectedTile = null;
            _uiWindow.Hide();
        }
    }
}