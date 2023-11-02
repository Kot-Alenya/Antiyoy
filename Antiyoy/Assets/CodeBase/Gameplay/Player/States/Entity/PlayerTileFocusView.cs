using System.Collections.Generic;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public class PlayerTileFocusView
    {
        private readonly ITerrain _terrain;

        public PlayerTileFocusView(ITerrain terrain) => _terrain = terrain;

        public void FocusAllTiles()
        {
            //foreach (var tile in _terrain)
            //    tile.Instance.HideMaskSpriteRenderer.enabled = true;
        }

        public void UnFocusAllTiles()
        {
            //foreach (var tile in _terrain)
            //    tile.Instance.HideMaskSpriteRenderer.enabled = false;
        }

        public void UnFocusTiles(List<TileData> tiles)
        {
            //foreach (var tile in tiles)
            //    tile.Instance.HideMaskSpriteRenderer.enabled = false;
        }
    }
}