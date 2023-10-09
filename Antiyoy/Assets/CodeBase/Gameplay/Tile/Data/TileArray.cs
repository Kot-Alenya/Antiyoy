using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using UnityEngine;

namespace CodeBase.Gameplay.Tile.Data
{
    public class TileArray : IEnumerable<TileData>
    {
        private readonly TileData[] _tiles;
        private readonly Vector2Int _size;

        public TileArray(Vector2Int size)
        {
            _tiles = new TileData[size.x * size.y];
            _size = size;
        }

        public bool IsHexInArraySize(HexPosition hex)
        {
            var index = HexMath.ToArrayIndex(hex);

            if (index.x < 0)
                return false;

            if (index.x >= _size.x)
                return false;

            if (index.y < 0)
                return false;

            return index.y < _size.y;
        }

        public void Set(TileData tile, HexPosition hex) => _tiles[GetIndex(hex)] = tile;

        public TileData Get(HexPosition hex) => _tiles[GetIndex(hex)];

        public bool TryGet(HexPosition hex, out TileData tile)
        {
            if (IsHexInArraySize(hex))
            {
                tile = Get(hex);
                return tile != null;
            }

            tile = null;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TileData> GetEnumerator() => ((IEnumerable<TileData>)_tiles).GetEnumerator();

        private int GetIndex(HexPosition hex)
        {
            var arrayIndex = HexMath.ToArrayIndex(hex);

            return arrayIndex.y * _size.x + arrayIndex.x;
        }
    }
}