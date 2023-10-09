using System.Collections.Generic;
using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Tile.Model
{
    public class TilesNeighborsTool
    {
        private readonly TileArray _tiles;

        public TilesNeighborsTool(TileArray tiles) => _tiles = tiles;

        public void ConnectNeighbors(TileData tile)
        {
            var neighbours = GetNeighbours(tile.Hex);

            foreach (var neighbour in neighbours)
            {
                neighbour.Neighbors.Add(tile);
                tile.Neighbors.Add(neighbour);
            }
        }

        public void DisconnectNeighbors(TileData tile)
        {
            foreach (var neighbour in tile.Neighbors)
                RemoveFromNeighbors(neighbour, tile);

            tile.Neighbors.Clear();
        }
        
        private List<TileData> GetNeighbours(HexPosition rootHex)
        {
            var neighbours = new List<TileData>();

            foreach (var direction in HexPositionDirections.Directions)
            {
                var neighbourHex = rootHex + direction;

                if (!_tiles.IsInArraySize(neighbourHex))
                    continue;

                var neighbour = _tiles.Get(neighbourHex);

                if (neighbour != null)
                    neighbours.Add(neighbour);
            }

            return neighbours;
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