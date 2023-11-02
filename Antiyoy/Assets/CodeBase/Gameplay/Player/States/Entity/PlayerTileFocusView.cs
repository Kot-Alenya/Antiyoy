using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public class PlayerTileFocusView
    {
        private readonly PlayerData _playerData;
        private readonly ITerrain _terrain;

        public PlayerTileFocusView(PlayerData playerData, ITerrain terrain)
        {
            _playerData = playerData;
            _terrain = terrain;
        }

        public void UnFocusAllTiles()
        {
            _playerData.Instance.ShadowField.SetActive(false);
            
            foreach (var tile in _terrain)
                tile.Instance.SpriteMask.enabled = false;
        }

        public void FocusTiles(List<TileData> tiles)
        {
            _playerData.Instance.ShadowField.SetActive(true);

            foreach (var tile in tiles)
                tile.Instance.SpriteMask.enabled = true;
        }
    }
}