using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainTiles : IEnumerable<TileObject>
    {
        private readonly TileObject[] _tiles;
        private readonly Vector2Int _size;

        public TerrainTiles(Vector2Int size)
        {
            _tiles = new TileObject[size.x * size.y];
            _size = size;
        }

        public void Set(TileObject tile, HexCoordinates hex) =>
            _tiles[HexToIndex(hex)] = tile;

        public TileObject Get(HexCoordinates hex) =>
            _tiles[HexToIndex(hex)];

        public bool IsIndexValid(HexCoordinates hex)
        {
            if (hex.X < 0)
                return false;

            if (hex.X >= _size.x)
                return false;

            if (hex.Y < 0)
                return false;

            if (hex.Y >= _size.y)
                return false;

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TileObject> GetEnumerator() =>
            ((IEnumerable<TileObject>)_tiles).GetEnumerator();

        private int HexToIndex(HexCoordinates hex) => hex.Y * _size.x + hex.X;
    }
}