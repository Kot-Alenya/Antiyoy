using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Object;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainFactory
    {
        private readonly List<TileObject> _activeTiles = new();
        private readonly TileFactory _tileFactory;
        private readonly Transform _root;

        public TerrainFactory(TileFactory tileFactory)
        {
            _tileFactory = tileFactory;
            _root = new GameObject("Terrain").transform;
        }

        public void Create()
        {
            var frontTiles = new List<TileObject>();
            var firstTile = _tileFactory.Create(Vector2.zero, _root);
            var maxBreakIndex = 10000;
            var breakIndex = 0;

            frontTiles.Add(firstTile);
            _activeTiles.Add(firstTile);

            while (frontTiles.Count > 0)
            {
                var tile = frontTiles[0];

                for (var sideIndex = 0; sideIndex < TileSide.Count; sideIndex++)
                {
                    var position = _tileFactory.GetTilePosition(sideIndex, tile.Position);

                    if (IsPositionOutsideBorders(position) || IsTileCreated(position))
                        continue;

                    var newTile = _tileFactory.Create(position, _root);

                    _activeTiles.Add(newTile);
                    frontTiles.Add(newTile);
                }

                frontTiles.Remove(tile);

                if (breakIndex > maxBreakIndex)
                {
                    UnityEngine.Debug.LogError("BREAK!");
                    break;
                }

                breakIndex++;
            }
        }

        private bool IsPositionOutsideBorders(Vector2 position)
        {
            var borders = new Vector2(5, 5);

            if (position.x < 0)
                return true;

            if (position.x > borders.x)
                return true;

            if (position.y < 0)
                return true;

            if (position.y > borders.y)
                return true;

            return false;
        }

        private bool IsTileCreated(Vector2 position)
        {
            foreach (var tile in _activeTiles)
                if (tile.Position == position)
                    return true;

            return false;
        }
    }
}