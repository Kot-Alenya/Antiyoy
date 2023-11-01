using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Tile.Collection
{
    public class TileArray : IEnumerable<TileData>
    {
        private Vector2Int _size;
        private TileData[] _tiles;

        public void Initialize(Vector2Int size)
        {
            _tiles = new TileData[size.x * size.y];
            _size = size;
        }

        public Vector2Int Size => _size;

        public bool IsInArray(HexPosition hex)
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

        public TileData Get(HexPosition hex) => _tiles[GetIndex(hex)];

        public bool TryGet(HexPosition hex, out TileData tile)
        {
            if (IsInArray(hex))
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