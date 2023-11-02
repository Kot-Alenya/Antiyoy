using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile
{
    public class TileArray : IEnumerable<TileObject>
    {
        private readonly TileObject[] _tiles;
        private readonly Vector2Int _size;

        public TileArray(Vector2Int size)
        {
            _tiles = new TileObject[size.x * size.y];
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

        public void Set(TileObject tile, HexPosition hex) => _tiles[GetIndex(hex)] = tile;

        public void Remove(HexPosition hex) => _tiles[GetIndex(hex)] = null;
        
        public TileObject Get(HexPosition hex) => _tiles[GetIndex(hex)];

        public bool TryGet(HexPosition hex, out TileObject tile)
        {
            if (IsInArray(hex) && Get(hex) != null)
            {
                tile = Get(hex);
                return true;
            }

            tile = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TileObject> GetEnumerator() => ((IEnumerable<TileObject>)_tiles).GetEnumerator();

        private int GetIndex(HexPosition hex)
        {
            var arrayIndex = HexMath.ToArrayIndex(hex);

            return arrayIndex.y * _size.x + arrayIndex.x;
        }
    }
}