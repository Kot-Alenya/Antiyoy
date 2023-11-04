using System.Collections.Generic;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player
{
    public class PlayerTerrainFocus
    {
        private readonly PlayerData _playerData;
        private readonly ITerrain _terrain;

        public PlayerTerrainFocus(PlayerData playerData, ITerrain terrain)
        {
            _playerData = playerData;
            _terrain = terrain;
        }

        public void ShowShadowField() => _playerData.Instance.ShadowField.SetActive(true);

        public void HideShadowField() => _playerData.Instance.ShadowField.SetActive(false);

        public void SetTilesAboveShadowFiled(List<TileData> tiles)
        {
            foreach (var tile in tiles)
                tile.Instance.SpriteMask.enabled = true;
        }

        public void SetAllTilesUnderShadowField()
        {
            foreach (var tile in _terrain)
                tile.Instance.SpriteMask.enabled = false;
        }

        public void ShowTilesOutline(List<TileData> tiles)
        {
            foreach (var tile in tiles)
            foreach (var direction in HexPositionDirectionUtilities.Directions)
            {
                var neighbourHex = tile.Hex + direction;
                var directionType = HexPositionDirectionUtilities.GetDirectionType(direction);

                if (_terrain.TryGetTile(neighbourHex, out var neighbor))
                {
                    if (!tiles.Contains(neighbor))
                        tile.Instance.Borders[directionType].enabled = true;
                }
                else
                    tile.Instance.Borders[directionType].enabled = true;
            }
        }

        public void HideAllTilesOutlines()
        {
            foreach (var tile in _terrain)
            foreach (var border in tile.Instance.Borders)
                border.Value.enabled = false;
        }
    }
}