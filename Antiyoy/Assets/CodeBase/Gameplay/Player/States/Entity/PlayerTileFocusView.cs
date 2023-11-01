﻿using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Player.States.Entity
{
    public class PlayerTileFocusView
    {
        private readonly ITileCollection _tileCollection;

        public PlayerTileFocusView(ITileCollection tileCollection) => _tileCollection = tileCollection;

        public void FocusAllTiles()
        {
            foreach (var tile in _tileCollection)
                tile.Instance.HideMaskSpriteRenderer.enabled = true;
        }

        public void UnFocusAllTiles()
        {
            foreach (var tile in _tileCollection)
                tile.Instance.HideMaskSpriteRenderer.enabled = false;
        }

        public void UnFocusTiles(List<TileData> tiles)
        {
            foreach (var tile in tiles)
                tile.Instance.HideMaskSpriteRenderer.enabled = false;
        }
    }
}