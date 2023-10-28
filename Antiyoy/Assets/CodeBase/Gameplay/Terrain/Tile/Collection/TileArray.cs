using System.Collections;
using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile.Collection
{
    public class TileArray : ITileCollection
    {
        private readonly Vector2Int _size;
        private readonly TileData[] _tiles;

        public TileArray(IStaticDataProvider staticDataProvider)
        {
            var size = staticDataProvider.Get<TerrainStaticData>().Size;

            _tiles = new TileData[size.x * size.y];
            _size = size;
        }

        public Vector2Int Size => _size;

        public int Count => _tiles.Length;

        public bool IsInCollection(HexPosition hex)
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

        public void Set(TileData tile, HexPosition hex)
        {
            _tiles[GetIndex(hex)] = tile;
            TileCollectionUtilities.ConnectWithNeighbors(tile, this);
        }

        public void Remove(HexPosition hex)
        {
            TileCollectionUtilities.DisconnectFromNeighbors(_tiles[GetIndex(hex)]);
            _tiles[GetIndex(hex)] = null;
        }

        public TileData Get(HexPosition hex) => _tiles[GetIndex(hex)];
        public TileData Get(int index) => _tiles[index];

        public bool TryGet(HexPosition hex, out TileData tile)
        {
            if (IsInCollection(hex))
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