using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Hex
{
    public class HexObjectCollection<TObject> : IEnumerable<TObject>
    {
        private readonly TObject[] _tiles;
        private readonly Vector2Int _size;

        public HexObjectCollection(Vector2Int size)
        {
            _tiles = new TObject[size.x * size.y];
            _size = size;
        }

        public int Count => _tiles.Length;

        public void Set(TObject tile, HexCoordinates hex) =>
            _tiles[HexToIndex(hex)] = tile;

        public TObject Get(HexCoordinates hex) =>
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

        public IEnumerator<TObject> GetEnumerator() =>
            ((IEnumerable<TObject>)_tiles).GetEnumerator();

        private int HexToIndex(HexCoordinates hex) => hex.Y * _size.x + hex.X;
    }
}