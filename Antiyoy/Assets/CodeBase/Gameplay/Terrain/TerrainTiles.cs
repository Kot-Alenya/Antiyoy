using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Terrain.Tile.Object;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainTiles : IEnumerable<TileObject>
    {
        private readonly TileObject[,] _tiles;
        private readonly Vector2Int _size;

        public TerrainTiles(Vector2Int size)
        {
            _tiles = new TileObject[size.x, size.y];
            _size = size;
        }

        public void Set(TileObject tile) => _tiles[tile.Index.x, tile.Index.y] = tile;

        public object Get(Vector2Int index) => _tiles[index.x, index.y];

        public bool IsTileInTerrain(Vector2Int index)
        {
            if (index.x < 0)
                return false;

            if (index.x > _size.x)
                return false;

            if (index.y < 0)
                return false;

            if (index.y > _size.y)
                return false;

            return true;
        }

        public bool IsTileCreated(Vector2Int index) => _tiles[index.x, index.y] != null;

        public IEnumerator<TileObject> GetEnumerator() => _tiles.Cast<TileObject>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}