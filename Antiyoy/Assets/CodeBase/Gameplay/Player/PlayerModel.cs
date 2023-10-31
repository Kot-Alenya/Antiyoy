using CodeBase.Gameplay.Player.Controller;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Terrain.Entity;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player
{
    public class PlayerModel
    {
        private readonly PlayerData _playerData;
        private readonly IEntityFactory _entityFactory;

        private IPlayerUIMediator _uiMediator;

        private RegionData _selectedRegion;
        private TileData _selectedTile;

        public PlayerModel(PlayerData playerData, IEntityFactory entityFactory)
        {
            _playerData = playerData;
            _entityFactory = entityFactory;
        }

        public void Initialize(IPlayerUIMediator uiMediator) => _uiMediator = uiMediator;

        public void SelectTile(TileData tile)
        {
            if (tile.Region.Type != _playerData.RegionType)
            {
                UnSelectTile();
                return;
            }

            if (_selectedTile == null)
                _uiMediator.ShowUIWindow();

            _selectedTile = tile;
            UpdateUI();
        }

        public void UnSelectTile()
        {
            _selectedTile = null;
            _uiMediator.HideUIWindow();
        }

        public void CreateEntity(EntityType entityType) => _entityFactory.TryCreate(_selectedTile.Hex, entityType);

        private void UpdateUI()
        {
            _uiMediator.SetCoinsCount(_playerData.CoinsCount);
            _uiMediator.SetIncomeCount(_selectedTile.Region.Income);
        }
    }
}