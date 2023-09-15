using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
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

        public void Set(TileObject tile, HexPosition hex) =>
            _tiles[GetIndex(hex)] = tile;

        public TileObject Get(HexPosition hex) =>
            _tiles[GetIndex(hex)];

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TileObject> GetEnumerator() =>
            ((IEnumerable<TileObject>)_tiles).GetEnumerator();

        private int GetIndex(HexPosition hex)
        {
            var arrayIndex = HexMath.ToArrayIndex(hex);

            return arrayIndex.y * _size.x + arrayIndex.x;
        }
    }
}