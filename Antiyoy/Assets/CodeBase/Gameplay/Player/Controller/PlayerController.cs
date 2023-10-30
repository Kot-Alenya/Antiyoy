using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Player.UI;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player.Controller
{
    public class PlayerController : IPlayerController
    {
        private readonly PlayerData _playerData;

        private PlayerUIWindow _playerUIWindow;
        private RegionData _selectedRegion;
        private TileData _selectedTile;

        public PlayerController(PlayerData playerData) => _playerData = playerData;

        public void Initialize(PlayerUIWindow playerUIWindow) => _playerUIWindow = playerUIWindow;

        public void SelectTile(TileData tile)
        {
            if (tile.Region.Type != _playerData.RegionType)
            {
                UnSelectTile();
                return;
            }

            if (_selectedTile == null)
                _playerUIWindow.Show();

            _selectedTile = tile;
            UpdateUI();
        }

        public void UnSelectTile()
        {
            _selectedTile = null;
            _playerUIWindow.Hide();
        }

        public void UpdateUI()
        {
            _playerUIWindow.SetCoinsCount(_playerData.CoinsCount);
            _playerUIWindow.SetIncomeCount(_selectedTile.Region.Income);
        }
    }
}