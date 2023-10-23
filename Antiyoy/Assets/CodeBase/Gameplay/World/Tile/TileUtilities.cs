using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Tile
{
    public static class TileUtilities
    {
        public static void ConnectWithNeighbors(TileData tile, TileCollection tiles)
        {
            var neighbours = GetNeighbours(tile.Hex, tiles);

            foreach (var neighbour in neighbours)
            {
                neighbour.Neighbors.Add(tile);
                tile.Neighbors.Add(neighbour);
            }
        }

        public static void DisconnectFromNeighbors(TileData tile)
        {
            foreach (var neighbour in tile.Neighbors)
                RemoveFromNeighbors(neighbour, tile);

            tile.Neighbors.Clear();
        }

        private static List<TileData> GetNeighbours(HexPosition rootHex, TileCollection tiles)
        {
            var neighbours = new List<TileData>();

            foreach (var direction in HexPositionDirections.Directions)
            {
                var neighbourHex = rootHex + direction;

                if (!tiles.IsInArraySize(neighbourHex))
                    continue;

                var neighbour = tiles.Get(neighbourHex);

                if (neighbour != null)
                    neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private static void RemoveFromNeighbors(TileData rootTile, TileData tileData)
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