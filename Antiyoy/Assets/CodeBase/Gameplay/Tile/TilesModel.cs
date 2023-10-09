using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Region.Model;
using CodeBase.Gameplay.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TilesModel
    {
        private readonly TileArray _tiles;
        private readonly RegionsModel _regions;
        private readonly GameObject _instance;
        private readonly TileFactory _tileFactory;

        public TilesModel(TileArray tiles, RegionsModel regions, GameObject instance, TileFactory tileFactory)
        {
            _tiles = tiles;
            _regions = regions;
            _instance = instance;
            _tileFactory = tileFactory;
        }

        public bool IsHexInTerrain(HexPosition hex) => _tiles.IsHexInArraySize(hex);

        public bool TryGetTile(HexPosition hex, out TileData tile) => _tiles.TryGet(hex, out tile);

        public void CreateTile(HexPosition hex, RegionType regionType)
        {
            var tile = _tileFactory.Create(_instance.transform, hex);

            _tiles.Set(tile, tile.Hex);
            ConnectNeighbors(tile);
            _regions.Add(tile, regionType);
        }

        public void DestroyTile(TileData tile)
        {
            DisconnectNeighbors(tile);
            _regions.Remove(tile);

            _tiles.Set(null, tile.Hex);
            _tileFactory.Destroy(tile);
        }

        private void ConnectNeighbors(TileData tile)
        {
            var neighbours = GetNeighbours(tile.Hex);

            foreach (var neighbour in neighbours)
            {
                neighbour.Neighbors.Add(tile);
                tile.Neighbors.Add(neighbour);
            }
        }

        private List<TileData> GetNeighbours(HexPosition rootHex)
        {
            var neighbours = new List<TileData>();

            foreach (var direction in HexPositionDirections.Directions)
            {
                var neighbourHex = rootHex + direction;

                if (!_tiles.IsHexInArraySize(neighbourHex))
                    continue;

                var neighbour = _tiles.Get(neighbourHex);

                if (neighbour != null)
                    neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private void DisconnectNeighbors(TileData tile)
        {
            foreach (var neighbour in tile.Neighbors)
                RemoveFromNeighbors(neighbour, tile);

            tile.Neighbors.Clear();
        }

        private void RemoveFromNeighbors(TileData rootTile, TileData tileData)
        {
            for (var i = 0; i < rootTile.Neighbors.Count; i++)
            {
                if (rootTile.Neighbors[i] == tileData)
                {
                    rootTile.Neighbors.RemoveAt(i);
                    return;
                }
            }
        }
    }
}